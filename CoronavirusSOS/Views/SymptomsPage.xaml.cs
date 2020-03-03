using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoronavirusSOS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SymptomsPage : ContentPage
    {
        public SymptomsPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://www.google.com/search?q=doctors+near+me"));
        }
    }
}