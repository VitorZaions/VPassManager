using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VPassSample.Models;
using VPassManager;

namespace VPassSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPassword : ContentPage
    {
        public CContexto contexto;
        private CSenha _CSen;
        int _IDCategoria;

        public NewItemPassword(CSenha CSen, int IDCategoria)
        {
            contexto = new CContexto();
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
                contexto.conexao.Update(_CSen);
            }
            else
            {
                contexto.conexao.Insert(_CSen);
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
    }
}