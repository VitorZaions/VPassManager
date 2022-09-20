using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using VPassManager;
using VPassSample.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VPassSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MyAccount : ContentPage
    {
        public CContexto contexto;

        public MyAccount()
        {
            InitializeComponent();
            contexto = new CContexto();
            lbUser.Text = utils.User.Usuario;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txPassOld.Text))
            {
                DisplayAlert("Erro", "Insira a senha Antiga.", "OK");
                return;
            }


            if (string.IsNullOrEmpty(txPassNew.Text))
            {
                DisplayAlert("Erro", "Insira a nova senha.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(txPassNew2.Text))
            {
                DisplayAlert("Erro", "Insira a confirmação da nova senha.", "OK");
                return;
            }

            if(txPassNew.Text != txPassNew2.Text)
            {
                DisplayAlert("Erro", "Senha nova e confirmação diferentes.", "OK");
                return;
            }

            if (txPassOld.Text != utils.User.Senha)
            {
                DisplayAlert("Erro", "Senha antiga inválida.", "OK");
                return;
            }

            string oldsenha = utils.User.Senha;
            int oldtoken = utils.User.Token;


            while(utils.User.Token == oldtoken)
            {
                Random TheRandom = new Random();
                utils.User.Token = TheRandom.Next(10000, 99999);
            }

            try
            {
                utils.User.Senha = txPassNew.Text;
                contexto.conexao.Update(utils.User);
                DisplayAlert("Inserir", $"Guarde seu token de recuperação: {utils.User.Token}", "OK");
                DisplayAlert("Alterar Senha", "Senha atualizada com sucesso!", "OK");
            }
            catch
            {
                utils.User.Senha = oldsenha;
                utils.User.Token = oldtoken;
                DisplayAlert("Erro", "Não foi possível alterar a senha.", "OK");
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txEmail.Text))
            {
                DisplayAlert("Erro", "Insira o novo E-mail.", "OK");
                return;
            }

            if (!utils.IsValidEmail(txEmail.Text))
            {
                DisplayAlert("Erro", "Insira um E-mail válido.", "OK");
                return;
            }

            string oldemail = utils.User.Email;

            try
            {
                utils.User.Email = txEmail.Text;
                contexto.conexao.Update(utils.User);
                DisplayAlert("Alterar Senha", "E-mail atualizado com sucesso!", "OK");
            }
            catch
            {
                utils.User.Email = oldemail;
                DisplayAlert("Erro", "Não foi possível alterar o E-mail.", "OK");
            }

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Reset();
        }


        void Reset()//require in the main Thread, also the app will crash
        {
            (App.Current as App).MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                (App.Current as App).MainPage = new AppShell();
            });
        }

        private async void OnExportTap(object sender, EventArgs args)
        {            
                await DisplayAlert("Question", "Deseja realmente realizar a exportação das suas senhas?", "Sim", "Não").ContinueWith(t =>
                {
                    if (t.Result)
                    {
                        try
                        {
                            string SenhasFinal = "";
                            List<CCategoria> Cats = contexto.conexao.Query<CCategoria>($"SELECT * FROM CCategoria where IDUsuario = {utils.User.IDUsuario}").ToList();
                            List<CSenha> Senhas = new List<CSenha>();

                            foreach (CCategoria Cat in Cats)
                            {
                                List<CSenha> SenhasCat = contexto.conexao.Query<CSenha>($"SELECT * FROM CSenha where IDCategoria = {Cat.IDCategoria}").ToList();

                                foreach (CSenha Senha in SenhasCat)
                                {
                                    Senhas.Add(Senha);
                                }
                            }

                            foreach (CSenha CSen in Senhas)
                            {
                                SenhasFinal += $"{CSen.Identificacao} : {CSen.Senha}{Environment.NewLine}";
                            }

                            if (Senhas.Count == 0)
                            {
                                DisplayAlert("Erro", "Não existem senhas salvas.", "OK");
                                return;
                            }

                           Clipboard.SetTextAsync(SenhasFinal);
                           DisplayAlert("Exportar Senhas", $"Senhas salvas em seu clipboard.", "OK");
                        }
                        catch
                        {
                           DisplayAlert("Erro", $"Não foi possível exportar suas senhas.", "OK");
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());           
        }               
    }
}