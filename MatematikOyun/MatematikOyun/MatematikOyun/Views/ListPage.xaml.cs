using MatematikOyun.App_Data;
using MatematikOyun.Model;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatematikOyun.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
            List<OyunDetaylari> oyunDetaylari = new List<OyunDetaylari>();
            SQLiteManager manager = new SQLiteManager();
            oyunDetaylari = manager.GetAll().OrderByDescending(x => x.Skor).ToList();
            lstOyunDetay.BindingContext = oyunDetaylari;
        }
        private void onOyunaDon(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new OyunSayfasi());
        }
        private void onOyunAnasayfa(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());

        }
    }
}