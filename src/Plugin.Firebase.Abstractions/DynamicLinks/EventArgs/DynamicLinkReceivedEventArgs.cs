namespace Plugin.Firebase.Abstractions.DynamicLinks.EventArgs
{
    public sealed class DynamicLinkReceivedEventArgs : System.EventArgs
    {
        public DynamicLinkReceivedEventArgs(string link)
        {
            Link = link;
        }

        public string Link { get; }
    }
}