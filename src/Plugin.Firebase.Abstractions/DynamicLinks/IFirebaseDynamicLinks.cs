using System;
using Plugin.Firebase.Abstractions.DynamicLinks.EventArgs;

namespace Plugin.Firebase.Abstractions.DynamicLinks
{
    public interface IFirebaseDynamicLinks : IDisposable
    {
        string GetDynamicLink();
        IDynamicLinkBuilder CreateDynamicLink();
        
        event EventHandler<DynamicLinkReceivedEventArgs> DynamicLinkReceived;
        event EventHandler<DynamicLinkFailedEventArgs> DynamicLinkFailed;
    }
}