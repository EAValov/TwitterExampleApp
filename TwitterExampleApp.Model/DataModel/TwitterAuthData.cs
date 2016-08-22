namespace TwitterExampleApp.Model.DataModel
{
    /// <summary>
    /// данные для авторизации в твиттере.
    /// </summary>
    public class TwitterAuthData
    {
        /// <summary>
        /// Consumer Key (API Key).
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Consumer Secret (API Secret).
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Access Token.
        /// </summary>
        public string OAuthToken { get; set; }

        /// <summary>
        /// Access Token Secret.
        /// </summary>
        public string OAuthTokenSecret { get; set; }

    }
}
