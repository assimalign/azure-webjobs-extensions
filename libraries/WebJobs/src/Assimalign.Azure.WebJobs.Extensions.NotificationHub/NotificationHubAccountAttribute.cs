﻿using System;
using Microsoft.Azure.WebJobs;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter)]
public sealed class NotificationHubAccountAttribute : Attribute, IConnectionProvider
{
    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    /// <param name="account">
    ///  A string value indicating the Azure Storage connection string to use. This string
    ///  should be in one of the following formats. These checks will be applied in order
    ///  and the first match wins. - The name of an "AzureWebJobs" prefixed app setting
    ///  or connection string name. E.g., if your setting name is "AzureWebJobsMyStorage",
    ///  you can specify "MyStorage" here. - Can be a string containing %% values (e.g.
    ///  %StagingStorage%). The value provided will be passed to any INameResolver registered
    ///  on the JobHostConfiguration to resolve the actual setting name to use. - Can
    ///  be an app setting or connection string name of your choosing.
    /// </param>
    public NotificationHubAccountAttribute(string account)
    {
        Account = account;
    }


    /// <summary>
    /// Gets or sets the app setting name that contains the Azure notification hub connection
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string IConnectionProvider.Connection
    {
        get
        {
            return Account;
        }
        set
        {
            Account = value;
        }
    }
}
