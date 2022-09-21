using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VPassSample.Models;
using VPassManager;
using Xamarin.Essentials;

namespace VPassSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPassword : ContentPage
    {
        private CSenha _CSen;
        int _IDCategoria;

        public NewItemPassword(CSenha CSen, int IDCategoria)
        {
            InitializeComponent();
            _CSen = CSen;
            _IDCategoria = IDCategoria;

            if (CSen.IDSenha != 0)
            {
                Title = "Editar Senha";
                PreencherTela();
            }
        }

        private void PreencherTela()
        {
            txIdentificacao.Text = _CSen.Identificacao;
            txPass.Text = _CSen.Senha;
        }

        async void FecharForm()
        {
            await Navigation.PopModalAsync();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txIdentificacao.Text))
            {
                DisplayAlert("Erro", "Insira a Identificação.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(txPass.Text))
            {
                DisplayAlert("Erro", "Insira a senha.", "OK");
                return;
            }

            _CSen.Identificacao = txIdentificacao.Text;
            _CSen.Senha = txPass.Text;
            _CSen.IDCategoria = _IDCategoria;

            if (_CSen.IDSenha != 0)
            {
                CContexto.conexao.Update(_CSen);
            }
            else
            {
                CContexto.conexao.Insert(_CSen);
            }

            FecharForm();
        }

        void OnViewTap(object sender, EventArgs args)
        {
            txPass.IsPassword = !txPass.IsPassword;           
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            FecharForm();
        }

        private void Copiar_Tapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txPass.Text))
            {
                Clipboard.SetTextAsync(txPass.Text);
                DisplayAlert("Copiar Senha", $"Senha copiada com sucesso!", "OK");
            }
            else
            {
                DisplayAlert("Erro", "Nenhuma senha a ser copiada.", "OK");
                return;
            }
        }
    }
}