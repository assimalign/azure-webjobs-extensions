using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus.Primitives;
using Azure.Core;
using Azure.Identity;

namespace Assimalign.Azure.WebJobs.Bindings.ServiceBus
{
    //internal class ServiceBusAccessorTokenProvider : ITokenProvider
    //{

    //    private readonly DefaultAzureCredential credentials;
    //    AzureActiveDirectoryTokenProvider.AuthenticationCallback callback;

    //    public ServiceBusAccessorTokenProvider(
    //        DefaultAzureCredential credentials)
    //    {
    //        this.credentials = credentials;
    //        this.callback = GetIdentityTokenAsync;
    //    }


    //    public TokenProvider Provider { get; internal set; }
        

    //    public Task<SecurityToken> GetTokenAsync(string appliesTo, TimeSpan timeout)
    //    {
    //        Task.Run<SecurityToken>(() =>
    //        {
    //            Provider = ManagedIdentityTokenProvider.CreateManagedIdentityTokenProvider();
    //            if (TryGetSecurityToken(appliesTo, timeout, out var msiToken))
    //            {
    //                return msiToken;
    //            }

    //            Provider = TokenProvider.CreateAzureActiveDirectoryTokenProvider(callback, "");
    //            if (TryGetSecurityToken(appliesTo, timeout, out var token))
    //            {
    //                return token;
    //            }


    //            throw new Exception();
    //        });
    //    }


    //    private bool TryGetSecurityToken(string appliesTo, TimeSpan timeout, out SecurityToken token)
    //    {
    //        token = null;
    //        try
    //        {
    //            token = Provider.GetTokenAsync(appliesTo, timeout).GetAwaiter().GetResult();
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        return true;
    //    }


    //    private async Task<string> GetTokenViaVisualStudioCredentials(string audience, string authority, object state)
    //    {
    //        var provider = new VisualStudioCredential(new VisualStudioCredentialOptions()
    //        {


    //        });

    //        var token = await provider.GetTokenAsync(new TokenRequestContext(new[] { "https://servicebus.azure.net/" }), CancellationToken.None);

    //        return token.Token;
    //    }
    //}
}
