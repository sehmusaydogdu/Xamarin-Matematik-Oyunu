using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatematikOyun.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            lblTitle.Text = "Matematik Dünyasına Hoşgeldiniz";
        }
        private void onOyunaBasla(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new OyunSayfasi());
        }
        private void onOyunKullanimBilgileri(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ListPage());
        }
    }
}