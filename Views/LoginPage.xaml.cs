using VPassSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using VPassManager;

namespace VPassSample.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void AbrirMain()
        {           
            await Shell.Current.GoToAsync("//main");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           
            try
            {
                if (string.IsNullOrEmpty(txUser.Text))
                {
                    DisplayAlert("Erro", "Insira o usuário", "OK");
                    return;
                }


                if (string.IsNullOrEmpty(txPass.Text))
                {
                    DisplayAlert("Erro", "Insira a senha.", "OK");
                    return;
                }

                CUsuario MyUser = CContexto.conexao.Query<CUsuario>($"select * from CUsuario where Usuario = '{txUser.Text}'").SingleOrDefault();

                if(MyUser == null)
                {
                    DisplayAlert("Erro", "Este usuário não existe.", "OK");
                    return;
                }

                if (MyUser.Senha != txPass.Text)
                {
                    DisplayAlert("Erro", "Senha inválida.", "OK");
                    return;
                }

                // Passou em tudo, então pode abrir o main
                utils.User = MyUser;
                AbrirMain();

            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login/registration");
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login/forgetpass");
        }
    }
}
