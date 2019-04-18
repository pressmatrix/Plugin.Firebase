using System;
using Plugin.Firebase.Abstractions.DynamicLinks;

namespace Plugin.Firebase.DynamicLinks
{
    public class CrossFirebaseDynamicLinks
    {
        static Lazy<IFirebaseDynamicLinks> implementation = new Lazy<IFirebaseDynamicLinks>(CreateFirebaseDynamicLinks, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        private static IFirebaseDynamicLinks CreateFirebaseDynamicLinks()
        {
            #if NETSTANDARD2_0
            return null;
            #else
            #pragma warning disable IDE0022 // Use expression body for methods
            return new FirebaseDynamicLinksImplementation();
            #pragma warning restore IDE0022 // Use expression body for methods
            #endif
        }
        
        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => implementation.Value != null;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static IFirebaseDynamicLinks Current {
            get {
                var ret = implementation.Value;
                if (ret == null) {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        private static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

        /// <summary>
        /// Dispose of everything 
        /// </summary>
        public static void Dispose()
        {
            if(implementation != null && implementation.IsValueCreated) {
                implementation.Value.Dispose();
                implementation = new Lazy<IFirebaseDynamicLinks>(CreateFirebaseDynamicLinks, System.Threading.LazyThreadSafetyMode.PublicationOnly);
            }
        }
    }
}