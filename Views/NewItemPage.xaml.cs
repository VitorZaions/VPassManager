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
    public partial class NewItemPage : ContentPage
    {
        public CContexto contexto;
        private CCategoria Cat;

        public Item Item { get; set; }

        public NewItemPage(CCategoria Categoria)
        {
            contexto = new CContexto();
            InitializeComponent();
            Cat = Categoria;

            if (Cat.IDCategoria != 0)
            {
                Title = "Editar Categoria";
                PreencherTela();
            }
        }

        private void PreencherTela()
        {
            txDesc.Text = Cat.Descricao;
        }

        async void FecharForm()
        {
            await Navigation.PopModalAsync();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txDesc.Text))
            {
                DisplayAlert("Erro", "Insira a Descrição.", "OK");
                return;
            }

            Cat.Descricao = txDesc.Text;
            Cat.IDUsuario = utils.User.IDUsuario;

            if (Cat.IDCategoria != 0)
            {
                
                contexto.conexao.Update(Cat);
            }
            else
            {
                contexto.conexao.Insert(Cat);
            }

            FecharForm();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            FecharForm();
        }
    }
}