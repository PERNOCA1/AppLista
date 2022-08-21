using AppLista.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLista.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

        public Listagem()
        {
            InitializeComponent();

            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoProduto());
            } catch(Exception ex)
            {
                DisplayAlert("OPS", ex.Message, "OK");
            }
            

        }

        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.Preco * i.Quantidade);

            string msg = "O total da compra é: " + soma;

            DisplayAlert("OPS", msg, "OK");

        }

        protected override void OnAppearing()
        {
            if(lista_produtos.Count == 0)
            {
                System.Threading.Tasks.Task.Run(async () =>
                {
                    List<Produto> temp = await App.Database.GetAll();

                    foreach (Produto item in temp)
                    {
                        lista_produtos.Add(item);
                    }

                    ref_carregando.IsRefreshing = false;
                });
            }
           
        }


    }
}