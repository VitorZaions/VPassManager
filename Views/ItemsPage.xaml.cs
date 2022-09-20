using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VPassSample.Models;
using VPassSample.Views;
using System.Windows.Input;
using System.Data.Common;
using VPassManager.Views;
using VPassManager;

namespace VPassSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        public CContexto contexto;

        public ItemsPage()
        {
            InitializeComponent();
            contexto = new CContexto();
            AtualizaLista();
        }

        public void MyCommandHandler(object parameter)
        {
            // Do stuff with parameter
        }

        private void AtualizaLista()
        {
            List<CCategoria> Cats = contexto.conexao.Query<CCategoria>($"SELECT * FROM CCategoria where IDUsuario = {utils.User.IDUsuario}").ToList();
            ListaCategorias.ItemsSource = Cats;
        }                

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            var modalPage = new NewItemPage(new CCategoria());
            modalPage.Disappearing += (sender2, e2) =>
            {
                AtualizaLista();
            };
            await Navigation.PushModalAsync(new NavigationPage(modalPage));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
 

        async void OnEditarTap(object sender, EventArgs args)
        {
            try
            {   
                int id = Convert.ToInt32(((Image)sender).AutomationId);
                CCategoria MyCat = contexto.conexao.Query<CCategoria>($"select * from CCategoria where IDCategoria = '{id}'").SingleOrDefault();
                var modalPage = new NewItemPage(MyCat);
                modalPage.Disappearing += (sender2, e2) =>
                {
                    AtualizaLista();
                };
                await Navigation.PushModalAsync(new NavigationPage(modalPage));
            }
            catch
            {
                await DisplayAlert("Erro", "Não foi possível atualizar.", "OK");
            }
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
                        List<CSenha> Passwords = contexto.conexao.Query<CSenha>($"SELECT * FROM CSenha where IDCategoria = {id}").ToList();

                        if(Passwords.Count > 0)
                        {                            
                            DisplayAlert("Erro", "Existem senhas nesta categoria, não foi possível excluir.", "OK");
                            return;
                        }
                        
                        // Tudo Certo Exclui
                        contexto.conexao.Execute($"delete from CCategoria where IDCategoria = {id}");
                        AtualizaLista();
                        DisplayAlert("Categorias", "Categoria removida com sucesso!", "OK");
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
                var Cat = (CCategoria)e.Item;
                var modalPage = new PasswordsList(Cat);
                modalPage.Disappearing += (sender2, e2) =>
                {
                    AtualizaLista();
                };
                await Navigation.PushModalAsync(new NavigationPage(modalPage));
            }
            catch
            {
                await DisplayAlert("Erro", "Não foi possível obter as senhas.", "OK");
            }

            // Manually deselect item.
            ListaCategorias.SelectedItem = null;
        }
    }
}