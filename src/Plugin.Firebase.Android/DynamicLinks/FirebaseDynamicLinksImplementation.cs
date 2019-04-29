using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Extensions;
using Firebase.DynamicLinks;
using Plugin.Firebase.Abstractions.DynamicLinks;
using Plugin.Firebase.Abstractions.DynamicLinks.EventArgs;
using Exception = Java.Lang.Exception;

namespace Plugin.Firebase.DynamicLinks
{
    public class FirebaseDynamicLinksImplementation : IFirebaseDynamicLinks
    {
        private static event EventHandler<DynamicLinkReceivedEventArgs> _dynamicLinkReceived;
        private static event EventHandler<DynamicLinkFailedEventArgs> _dynamicLinkFailed;
        
        private static string _dynamicLink;
        
        public static async Task HandleDynamicLinkAsync(Intent intent)
        {
            try {
                var dynamicLink = (PendingDynamicLinkData) await FirebaseDynamicLinks.Instance.GetDynamicLink(intent);
                _dynamicLink = dynamicLink.Link.ToString();
                _dynamicLinkReceived?.Invoke(null, new DynamicLinkReceivedEventArgs(_dynamicLink));
            } catch(Exception e) {
                _dynamicLinkFailed?.Invoke(null, new DynamicLinkFailedEventArgs(e.Message));
            }
        }
        
        public string GetDynamicLink()
        {
            return _dynamicLink;
        }

        public IDynamicLinkBuilder CreateDynamicLink()
        {
            return new DynamicLinkBuilder();
        }

        public void Dispose()
        {
        }
        
        public event EventHandler<DynamicLinkReceivedEventArgs> DynamicLinkReceived {
            add => _dynamicLinkReceived += value;
            remove => _dynamicLinkReceived -= value;
        }
        
        public event EventHandler<DynamicLinkFailedEventArgs> DynamicLinkFailed {
            add => _dynamicLinkFailed += value;
            remove => _dynamicLinkFailed -= value;
        }
    }
}