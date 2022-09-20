using VPassSample.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace VPassSample
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("registration", typeof(RegistrationModal));
            Routing.RegisterRoute("forgetpass", typeof(ForgetPass));
        }
    }

    public class HiddenItem : ShellItem
    {

    }
}
