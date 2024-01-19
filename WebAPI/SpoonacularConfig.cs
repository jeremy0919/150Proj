namespace WebAPI
{

    // This structure simply defines elements that match our configuration file so that we can load
    // them up using a single call of the getvalue for the .net configuration manager utility.
    public class SpoonacularConfig
    {
        public string BaseUri { get; init; }
        public string ApiKey { get; init; }
        public string ApiSecret { get; init; }
    }
}
