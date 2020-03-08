using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoronavirusSOS.Views;
using CoronavirusSOS.ViewModels;
using CoronavirusSOS.RestService;
using Xamarin.Essentials;

namespace CoronavirusSOS.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        public static int count { get; set; }
        ItemsViewModel viewModel;
        RestApi RestApi;
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            RestApi = new RestApi();
            count = 0;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Update;
            if (item == null)
                return;
            if (!string.IsNullOrEmpty(item.Url))
            {
                await Launcher.OpenAsync(new Uri(item.Url));
            }
            

            // Manually deselect item.

        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (viewModel.Items.Count == 0)
                {
                    viewModel.LoadItemsCommand.Execute(null);
                    var values = RestApi.GetVsAsync().ToArray();
                    test.Text = values[0];
                    test1.Text = values[1];
                    test2.Text = values[2];
                }
                   
            });
           
           
           

        }
    }
}