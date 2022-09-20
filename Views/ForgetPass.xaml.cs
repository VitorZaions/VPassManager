using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPassManager;
using VPassSample.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VPassSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPass : ContentPage
    {
        public CContexto contexto;

        public ForgetPass()
        {
            InitializeComponent();
            contexto = new CContexto();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Verificações de vazio

            if (string.IsNullOrWhiteSpace(txUser.Text))
            {
                DisplayAlert("Erro", "Insira o usuário", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(txToken.Text))
            {
                DisplayAlert("Erro", "Insira o Token", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(txPass.Text))
            {
                DisplayAlert("Erro", "Insira a nova senha", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(txPass2.Text))
            {
                DisplayAlert("Erro", "Insira a confirmação da nova senha", "OK");
                return;
            }

            if (txPass.Text != txPass2.Text)
            {
                DisplayAlert("Erro", "Senha nova e confirmação diferentes.", "OK");
                return;
            }

            try
            {            
                CUsuario MyUser = contexto.conexao.Query<CUsuario>($"select * from CUsuario where Usuario = '{txUser.Text}' and Token = '{txToken.Text}'").SingleOrDefault();
                if(MyUser == null)
                {
                    DisplayAlert("Erro", "Combinação de Token e E-mail inválidos.", "OK");
                    return;
                }

                MyUser.Senha = txPass.Text;
                contexto.conexao.Update(MyUser);
                DisplayAlert("Alterar Senha", "Senha atualizada com sucesso!", "OK");
            }
            catch
            {
                DisplayAlert("Erro", "Não foi possível alterar a senha.", "OK");
            }


        }
    }
}