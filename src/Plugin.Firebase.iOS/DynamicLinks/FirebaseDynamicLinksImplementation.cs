using System;
using Foundation;
using Plugin.Firebase.Abstractions.DynamicLinks;
using Plugin.Firebase.Abstractions.DynamicLinks.EventArgs;
using UIKit;
using FirebaseDynamicLinks = Firebase.DynamicLinks.DynamicLinks;
using DynamicLink = Firebase.DynamicLinks.DynamicLink;

namespace Plugin.Firebase.DynamicLinks
{
    public class FirebaseDynamicLinksImplementation : IFirebaseDynamicLinks
    {
        private static event EventHandler<DynamicLinkReceivedEventArgs> _dynamicLinkReceived;
        private static event EventHandler<DynamicLinkFailedEventArgs> _dynamicLinkFailed;
        
        private static string _dynamicLink;
        
        public static bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
			if(userActivity.WebPageUrl != null) {
				FirebaseDynamicLinks.SharedInstance.HandleUniversalLink(userActivity.WebPageUrl, HandleDynamicLink);
			}
			return false;
        }

        private static void HandleDynamicLink(DynamicLink link, NSError error)
        {
	        if(error == null) {
		        HandleDynamicLink(link);
	        } else {
		        _dynamicLinkFailed?.Invoke(null, new DynamicLinkFailedEventArgs(error.LocalizedDescription));
	        }
        }

        private static void HandleDynamicLink(DynamicLink link)
        {
	        _dynamicLink = link.Url.AbsoluteString;
	        _dynamicLinkReceived?.Invoke(null, new DynamicLinkReceivedEventArgs(_dynamicLink));
        }

        public static bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
			var link = FirebaseDynamicLinks.SharedInstance.FromCustomSchemeUrl(url);
			if(link == null) {
				return false;
			} else {
				HandleDynamicLink(link);
				return true;
			}
        }

        public void Dispose()
        {
        }

        public string GetDynamicLink()
        {
	        return _dynamicLink;
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