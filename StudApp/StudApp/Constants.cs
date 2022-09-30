using System.Globalization;

namespace StudApp.AppConstant
{
    public class Constants
    {
		public static string AppName = "OAuthNativeFlow";

		// OAuth
		// For Google login, configure at https://console.developers.google.com/
		public static string iOSClientId = "423567803684-n4n5852itf2pgoe3iidr60so7gq7n8nh.apps.googleusercontent.com";
		public static string AndroidClientId = "844060696235-ltgivjolb8v7ioidint435qa2o8ls38d.apps.googleusercontent.com";//todo

		// These values do not need changing
		public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string iOSRedirectUrl = "com.googleusercontent.apps.423567803684-n4n5852itf2pgoe3iidr60so7gq7n8nh:/oauth2redirect";
		public static string AndroidRedirectUrl = "com.googleusercontent.apps.844060696235-ltgivjolb8v7ioidint435qa2o8ls38d:/oauth2redirect";

		public static string iOSCallbackUrl = "com.eminent.studentapp://dev-z-ylsbp3.us.auth0.com/android/com.eminent.studentapp/callback";
		public static string AndroidCallbackUrl = "com.eminent.studentapp://dev-z-ylsbp3.us.auth0.com/ios/com.eminent.studentapp/callback";

		public static string baseUrl = "http://etestapi.test.eminenttechnology.com/api/";

		public static string SyncfusionLicense = "NzI2ODM4QDMyMzAyZTMzMmUzMERnWmFTdlhWRkRVUzZ0djlrVUJsREpWa3BiNGZhUUNUS090RDdIR3ArZXc9";

    }
}
