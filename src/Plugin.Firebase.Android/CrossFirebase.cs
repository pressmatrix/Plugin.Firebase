using System;
using Android.Content;
using Firebase;

namespace Plugin.Firebase.Android
{
    public static class CrossFirebase
    {
        public static void Initialize(Context context)
        {
            if(Current == null) {
                Current = FirebaseApp.InitializeApp(context);
            }
        }

        public static FirebaseApp Current { get; private set; }
        public static string ProjectId => Current.Options.ProjectId;
    }
}