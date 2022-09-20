using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VPassSample.Views;

namespace VPassSample
{
    public partial class App : Application
    {
        //public CContexto contexto;
        public App()
        {

            InitializeComponent();
            MainPage = new AppShell();
            //CContexto = new CContexto();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
