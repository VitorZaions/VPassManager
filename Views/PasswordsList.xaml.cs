using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPassSample;
using VPassSample.Models;
using VPassSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VPassManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordsList : ContentPage
    {
        private CCategoria _Categoria;
        public CContexto contexto;
        public PasswordsList(CCategoria Categoria)
        {
            InitializeComponent();
            contexto = new CContexto();
            _Categoria = Categoria;

            LBL_SubDesc.Text = _Categoria.Descricao;
            AtualizaLista();
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void AtualizaLista()
        {
            List<CSenha> Cats = contexto.conexao.Query<CSenha>($"SELECT * FROM CSenha where IDCategoria = {_Categoria.IDCategoria}").ToList();
            ListaSenhas.ItemsSource = Cats;
        }               

        void OnExcluirTap(object sender, EventArgs args)
        {            
            try
            {
                DisplayAlert("Question", "Deseja realmente remover o registro?", "Sim", "Não").ContinueWith(t =>
                {
                    if (t.Result)
                    {
                        int id = Convert.ToInt32(((Image)sender).AutomationId);
                        contexto.conexao.Execute($"delete from CSenha where IDSenha = {id}");
                        AtualizaLista();
                        DisplayAlert("Categorias", "Senha removida com sucesso!", "OK");
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch
            {
                DisplayAlert("Erro", "Não foi possível remover.", "OK");
            }
        }

        async private void ListaCategorias_ItemTapped(object sender, ItemTappedEventArgs e)
        {            
            try
            {
                var CSen = (CSenha)e.Item;
                var modalPage = new NewItemPassword(CSen,CSen.IDCategoria);
                modalPage.Disappearing += (sender2, e2) =>
                {
                    AtualizaLista();
                };
                await Navigation.PushModalAsync(new NavigationPage(modalPage));
            }
            catch
            {
                await DisplayAlert("Erro", "Não foi possível obter a senha.", "OK");
            }

            // Manually deselect item.
            ListaSenhas.SelectedItem = null;
        }

        async private void AddItem_Clicked(object sender, EventArgs e)
        {
            var modalPage = new NewItemPassword(new CSenha(),_Categoria.IDCategoria);
            modalPage.Disappearing += (sender2, e2) =>
            {
                AtualizaLista();
            };
            await Navigation.PushModalAsync(new NavigationPage(modalPage));
        }
    }
}