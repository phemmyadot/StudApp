using FreshMvvm;
using Newtonsoft.Json;
using StudApp.AuthHelpers;
using StudApp.Pages;
using System;
using System.Diagnostics;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudApp.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {

        public string clientId = null;
        public string redirectUri = null;
        public OAuth2Authenticator authenticator;
        public override void Init(object initData)
        {
            base.Init(initData);


            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = AppConstant.Constants.iOSClientId;
                    redirectUri = AppConstant.Constants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = AppConstant.Constants.AndroidClientId;
                    redirectUri = AppConstant.Constants.AndroidRedirectUrl;
                    break;
            }
            //await SecureStorage.SetAsync("appName", AppConstant.Constants.AppName);

             authenticator = new OAuth2Authenticator(
             clientId:   clientId,
             clientSecret:   null,
             scope:   AppConstant.Constants.Scope,
             authorizeUrl:   new Uri(AppConstant.Constants.AuthorizeUrl),
              redirectUrl:  new Uri(redirectUri),
             accessTokenUrl:   new Uri(AppConstant.Constants.AccessTokenUrl),
              getUsernameAsync:  null,
            isUsingNativeUI:    true);
          
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }

        #region Commands
       
        public Command Login_User
        {
            get
            {
                return new Command( () =>
                {
                    
                    authenticator.Completed += OnAuthCompleted;
                    authenticator.Error += OnAuthError;
                    authenticator.IsLoadableRedirectUri = true;
                    AuthenticationState.Authenticator = authenticator;

                    var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                    presenter.Login(authenticator);
                });
            }
        }
        #endregion

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(AppConstant.Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                }

                if (user != null)
                {
                    //App.Current.MainPage = new NavigationPage(new DashboardPage());

                }

                //await store.SaveAsync(account = e.Account, AppConstant.Constants.AppName);
                //await DisplayAlert("Email address", user.Email, "OK");
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }


    }


}