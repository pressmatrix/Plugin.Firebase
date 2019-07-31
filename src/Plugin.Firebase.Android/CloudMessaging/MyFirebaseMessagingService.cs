using System;
using Android.App;
using Firebase.Messaging;
using Plugin.Firebase.Android.Extensions;
using Plugin.Firebase.CloudMessaging;

namespace Plugin.Firebase.Android.CloudMessaging
{
    [Service]
    [IntentFilter(new [] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            CrossFirebaseCloudMessaging.Current.OnNotificationReceived(message.ToFCMNotification());
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            CrossFirebaseCloudMessaging.Current.OnNewToken(token);
        }
    }
}
