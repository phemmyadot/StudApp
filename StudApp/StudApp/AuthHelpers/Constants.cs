using System.Globalization;

namespace StudApp.AuthHelpers
{
    public class Constants
    {
        public static string AppName = "OAuthNativeFlow";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string AndroidClientId = "423567803684-n4n5852itf2pgoe3iidr60so7gq7n8nh.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        public static string AndroidRedirectUrl = "com.googleusercontent.apps.423567803684-n4n5852itf2pgoe3iidr60so7gq7n8nh:/oauth2redirect";

        public static string baseUrl = "http://etestapi.test.eminenttechnology.com/api/";

        public static string SyncfusionLicense = "NzI2ODM4QDMyMzAyZTMzMmUzMERnWmFTdlhWRkRVUzZ0djlrVUJsREpWa3BiNGZhUUNUS090RDdIR3ArZXc9";

    }
}
