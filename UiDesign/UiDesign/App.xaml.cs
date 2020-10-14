using System;
using UiDesign.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiDesign
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            XF.Material.Forms.Material.Init(this);

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#6441a5");
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#86BAA1");

            MainPage = new NavigationPage(new StartPage());

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#6441a5");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#86BAA1");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
