using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.OData
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class ODataResourceQueryAttribute : Attribute
    {
        



        internal void Validate()
        {
            if (context == null)
            {
                throw Error.ArgumentNull(nameof(context));
            }

            base.OnActionExecuting(context);

            RequestQueryData requestQueryData = new RequestQueryData()
            {
                QueryValidationRunBeforeActionExecution = false,
            };

            context.HttpContext.Items.Add(nameof(RequestQueryData), requestQueryData);

            HttpRequest request = context.HttpContext.Request;
            ODataPath path = request.ODataFeature().Path;

            ODataQueryContext queryContext = null;

            // For OData based controllers.
            if (path != null)
            {
                IEdmType edmType = path.EdmType;

                // When $count is at the end, the return type is always int. Trying to instead fetch the return type of the actual type being counted on.
                if (request.IsCountRequest())
                {
                    edmType = path.Segments[path.Segments.Count - 2].EdmType;
                }

                IEdmType elementType = edmType.AsElementType();

                IEdmModel edmModel = request.GetModel();

                // For Swagger metadata request. elementType is null.
                if (elementType == null || edmModel == null)
                {
                    return;
                }

                Type clrType = edmModel.GetTypeMappingCache().GetClrType(
                    elementType.ToEdmTypeReference(isNullable: false),
                    edmModel);

                // CLRType can be missing if untyped registrations were made.
                if (clrType != null)
                {
                    queryContext = new ODataQueryContext(edmModel, clrType, path);
                }
                else
                {
                    // In case where CLRType is missing, $count, $expand verifications cannot be done.
                    // More importantly $expand required ODataQueryContext with clrType which cannot be done
                    // If the model is untyped. Hence for such cases, letting the validation run post action.
                    return;
                }
            }
            else
            {
                // For non-OData Json based controllers.
                // For these cases few options are supported like IEnumerable<T>, Task<IEnumerable<T>>, T, Task<T>
                // Other cases where we cannot determine the return type upfront, are not supported
                // Like IActionResult, SingleResult. For such cases, the validation is run in OnActionExecuted
                // When we have the result.
                ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor == null)
                {
                    return;
                }

                Type returnType = controllerActionDescriptor.MethodInfo.ReturnType;
                Type elementType;

                // For Task<> get the base object.
                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    returnType = returnType.GetGenericArguments().First();
                }

                // For NetCore2.2+ new type ActionResult<> was created which encapculates IActionResult and T result.
                // However we don't exactly have a version specific to NetCore2.2 (also at the time of writing this code
                // 2.2 and 3.0 are both out of support), hence the code is made to work on NetCore3.1+ only.
#if NETCOREAPP3_1 || NET5_0
                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ActionResult<>))
                {
                    returnType = returnType.GetGenericArguments().First();
                }
#endif
                if (TypeHelper.IsCollection(returnType))
                {
                    elementType = TypeHelper.GetImplementedIEnumerableType(returnType);
                }
                else if (TypeHelper.IsGenericType(returnType) && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    elementType = returnType.GetGenericArguments().First();
                }
                else
                {
                    return;
                }

                IEdmModel edmModel = this.GetModel(
                    elementType,
                    request,
                    controllerActionDescriptor);

                queryContext = new ODataQueryContext(
                    edmModel,
                    elementType);
            }

            // Create and validate the query options.
            requestQueryData.QueryValidationRunBeforeActionExecution = true;
            requestQueryData.ProcessedQueryOptions = new ODataQueryOptions(queryContext, request);

            try
            {
                ValidateQuery(request, requestQueryData.ProcessedQueryOptions);
            }
            catch (ArgumentOutOfRangeException e)
            {
                context.Result = CreateBadRequestResult(
                    Error.Format(SRResources.QueryParameterNotSupported, e.Message),
                    e);
            }
            catch (NotImplementedException e)
            {
                context.Result = CreateBadRequestResult(
                    Error.Format(SRResources.UriQueryStringInvalid, e.Message),
                    e);
            }
            catch (NotSupportedException e)
            {
                context.Result = CreateBadRequestResult(
                    Error.Format(SRResources.UriQueryStringInvalid, e.Message),
                    e);
            }
            catch (InvalidOperationException e)
            {
                // Will also catch ODataException here because ODataException derives from InvalidOperationException.
                context.Result = CreateBadRequestResult(
                    Error.Format(SRResources.UriQueryStringInvalid, e.Message),
                    e);
            }
        }
    }
}
