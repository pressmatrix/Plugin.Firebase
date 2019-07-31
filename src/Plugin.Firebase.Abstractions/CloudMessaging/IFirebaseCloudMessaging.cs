using System;
using System.Threading.Tasks;
using Plugin.Firebase.Abstractions.CloudMessaging.EventArgs;

namespace Plugin.Firebase.Abstractions.CloudMessaging
{
    public interface IFirebaseCloudMessaging : IDisposable
    {
        void CheckIfValid();
        void OnNewToken(string token);
        void OnTokenRefresh();
        void OnNotificationReceived(FCMNotification fcmNotification);
        Task<string> GetTokenAsync();
        
        event EventHandler<FCMTokenChangedEventArgs> TokenChanged;
        event EventHandler<FCMNotificationReceivedEventArgs> NotificationReceived;
        event EventHandler<FCMNotificationTappedEventArgs> NotificationTapped;
        event EventHandler<FCMErrorEventArgs> Error;
    }
}