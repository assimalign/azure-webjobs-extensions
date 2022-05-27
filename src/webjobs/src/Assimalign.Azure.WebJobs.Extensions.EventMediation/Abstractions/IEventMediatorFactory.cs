namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
public interface IEventMediatorFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediatorId"></param>
    /// <returns></returns>
    IEventMediator CreateMediator(string mediatorId);
}
