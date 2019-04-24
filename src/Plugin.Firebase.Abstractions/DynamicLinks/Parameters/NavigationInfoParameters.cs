namespace Plugin.Firebase.Abstractions.DynamicLinks.Parameters
{
    public sealed class NavigationInfoParameters
    {
        public NavigationInfoParameters(bool forcedRedirectEnabled)
        {
            ForcedRedirectEnabled = forcedRedirectEnabled;
        }

        public bool ForcedRedirectEnabled { get;}
    }
}