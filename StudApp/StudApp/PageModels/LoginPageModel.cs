using FreshMvvm;
using Newtonsoft.Json;
using StudApp.AuthHelpers;
using System;
using System.Diagnostics;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudApp.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {

        public OAuth2Authenticator authenticator;
        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                this.RaisePropertyChanged();

            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);


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
                return new Command(() =>
                {

                    authenticator = new OAuth2Authenticator(
                    clientId: AuthHelpers.Constants.AndroidClientId,
                    clientSecret: null,
                    scope: AuthHelpers.Constants.Scope,
                    authorizeUrl: new Uri(AuthHelpers.Constants.AuthorizeUrl),
                    redirectUrl: new Uri(AuthHelpers.Constants.AndroidRedirectUrl),
                    accessTokenUrl: new Uri(AuthHelpers.Constants.AccessTokenUrl),
                    getUsernameAsync: null,
                    isUsingNativeUI: true);

                    authenticator.Completed += OnAuthCompleted;
                    authenticator.Error += OnAuthError;
                    authenticator.IsLoadableRedirectUri = true;
                    AuthenticationState.Authenticator = authenticator;

                    try
                    {
                        OAuthLoginPresenter presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                        presenter.Login(authenticator);
                    }
                    catch (Exception e)
                    {
                        Debug.Write(e.Message);
                    }
                });
            }
        }
        #endregion

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            IsLoading = true;
            OAuth2Authenticator authenticator = sender as OAuth2Authenticator;
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
                var request = new OAuth2Request("GET", new Uri(AuthHelpers.Constants.UserInfoUrl), null, e.Account);
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
                    GoogleOAuthToken token = new GoogleOAuthToken
                    {
                        TokenType = e.Account.Properties["token_type"],
                        AccessToken = e.Account.Properties["access_token"]
                    };

                    await SecureStorage.SetAsync("token", token.AccessToken);
                    await CoreMethods.PushPageModel<DashboardPageModel>();
                }
                else
                {
                    await SecureStorage.SetAsync("token", "");
                }
            }
            else
            {
                await SecureStorage.SetAsync("token", "");
            }
            IsLoading = false;
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            OAuth2Authenticator authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }


    }


}