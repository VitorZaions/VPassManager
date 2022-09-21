using VPassSample.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPassManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VPassSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationModal : ContentPage
    {
        public RegistrationModal()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txUser.Text))
                {
                    DisplayAlert("Erro", "Insira o Usuário", "OK");
                    return;
                }

                CUsuario MyUser = CContexto.conexao.Query<CUsuario>($"select * from CUsuario where Usuario = '{txUser.Text}'").SingleOrDefault();

                if(MyUser != null)
                {
                    DisplayAlert("Erro", "Este usuário já existe.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(txEmail.Text))
                {
                    DisplayAlert("Erro", "Insira o E-mail.", "OK");
                    return;
                }

                if (!utils.IsValidEmail(txEmail.Text))
                {
                    DisplayAlert("Erro", "Email inválido", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(txPass.Text))
                {
                    DisplayAlert("Erro", "Insira a senha", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(txPass.Text))
                {
                    DisplayAlert("Erro", "Insira a Confirmação da senha.", "OK");
                    return;
                }

                if (txPass.Text != txPass2.Text)
                {
                    DisplayAlert("Erro", "Senha e confirmação de senha diferentes.", "OK");
                    return;
                }

                // Tudo certo, insere no banco
                CUsuario User = new CUsuario();
                User.Usuario = txUser.Text;
                User.Senha = txPass.Text;
                User.Email = txEmail.Text;
                Random TheRandom = new Random();
                User.Token = TheRandom.Next(10000, 99999);

                CContexto.Inserir(User);

                DisplayAlert("Inserir", "Usuário registrado com sucesso!", "OK");
                DisplayAlert("Inserir", $"Guarde seu token de recuperação: {User.Token}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }

        }
    }
}