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
                // FirebaseApp.Initialize(context) does not initialize the projectId, which leads to a crash in FirebaseFirestore.Instance 
                // This workaround will be needed until it's fixed via https://github.com/xamarin/GooglePlayServicesComponents/commit/723ebdc00867a4c70c51ad2d0dcbd36474ce8ff1
                var options = GetFirebaseOptions(context);
                Current = FirebaseApp.InitializeApp(context, options, options.ProjectId);
                ProjectId = Current.Options.ProjectId;
            }
        }

        private static FirebaseOptions GetFirebaseOptions(Context context)
        {
            var baseOptions = FirebaseOptions.FromResource(context);
            return new FirebaseOptions.Builder(baseOptions)
                .SetProjectId(GetProjectId(baseOptions))
                .Build();
        }

        private static string GetProjectId(FirebaseOptions options)
        {
            if(options.ProjectId != null) {
                return options.ProjectId;
            } else if(options.StorageBucket == null) {
                return GetProjectIdFromDatabaseUrl(options.DatabaseUrl);
            } else {
                return GetProjectIdFromStorageBucket(options.StorageBucket);
            }
        }

        // Some google-service.json files from older projects don't contain the storage_bucket field, so this is an attempt
        // to extract the projectId from the databaseUrl (which works for at least what I've encountered so far ¯\_(ツ)_/¯)
        private static string GetProjectIdFromDatabaseUrl(string databaseUrl)
        {
            var startIndex = databaseUrl.IndexOf("api-project", StringComparison.InvariantCulture);			
            if(startIndex > -1) {
                var tempPrefix = databaseUrl.Substring(0, startIndex).Replace("https://", "").Replace("-", ".");
                var prefix = tempPrefix.ReplaceChar(':', tempPrefix.Length - 1);
                var firstDotIndex = databaseUrl.IndexOf(".", StringComparison.InvariantCulture);
                var suffix = databaseUrl.Substring(startIndex, firstDotIndex - startIndex);
                return prefix + suffix;
            } else {
                throw new FirebaseException("Couldn't extract projectId from google-services.json");
            }
        }

        private static string GetProjectIdFromStorageBucket(string storageBucket)
        {
            var split = storageBucket.Split('.');
            if(split.Length > 0) {
                return split[0];
            } else {
                throw new FirebaseException("Couldn't extract projectId from google-services.json");
            }
        }

        public static FirebaseApp Current { get; private set; }
        public static string ProjectId { get; private set; }
    }
}