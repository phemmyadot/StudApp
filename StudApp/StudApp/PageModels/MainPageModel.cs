using FreshMvvm;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StudApp.PageModels
{
    internal class MainPageModel : FreshBasePageModel
    {
        #region Default Override functions
        public override void Init(object initData)
        {
            base.Init(initData);
            MainPageLabel = "Welcome to Student App!";

            Task task = ValidateAuth();
        }


        private async Task ValidateAuth()
        {
            await Task.Delay(3000);
            if (MainPageLabel.Length > 10)
            {
                await CoreMethods.PushPageModel<DashboardPageModel>();
            }
            else
            {
                await CoreMethods.PushPageModel<LoginPageModel>();
            }
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
        #endregion

        #region Properties
        string _mainPageLabel = string.Empty;
        public string MainPageLabel
        {
            get
            {
                return _mainPageLabel;
            }
            set
            {
                if (_mainPageLabel != value)
                {
                    _mainPageLabel = value;
                    RaisePropertyChanged(nameof(MainPageLabel));
                }
            }
        }
        #endregion

    }
}
