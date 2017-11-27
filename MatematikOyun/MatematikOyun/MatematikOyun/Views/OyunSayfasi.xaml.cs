using MatematikOyun.App_Data;
using MatematikOyun.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatematikOyun.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OyunSayfasi : ContentPage
    {
        public OyunSayfasi()
        {
            InitializeComponent();
            lbl_rastgeleSayiUret();
            GeriSayimAraci(60);
        }
        Sayilar sayi = new Sayilar();
        SQLiteManager manager;
        private void onInsert()
        {
            manager = new SQLiteManager();
            OyunDetaylari o = new OyunDetaylari();
            o.timeStart = DateTime.Now;
            o.Skor = sayi.ToplamPuan;
            var lis = manager.GetAll().ToList().Count;

            if (lis <= 20)
            {
                manager = new SQLiteManager();
                int isDurum = manager.Insert(o);
            }
            else
            {
                manager = new SQLiteManager();
                var liste = manager.GetAll().ToList().OrderBy(x => x.Skor).FirstOrDefault();

                if (liste.Skor < o.Skor)
                {
                    manager = new SQLiteManager();
                    manager.Delete(liste.ID);

                    manager = new SQLiteManager();
                    manager.Insert(o);
                }
            }
            manager = null;
        }
        public async void GeriSayimAraci(int t)
        {
            await Task.Delay(150);
            while (t >= 0)
            {
                lblSure.Text = "Kalan Süre   :  " + t.ToString();
                slider.Value = t;
                t--;
                await Task.Delay(800);
            }
            onInsert();
            if (sayi.ToplamPuan > Sayilar.OyunSkor)
            {
                Sayilar.OyunSkor = sayi.ToplamPuan;
                lblSkor.Text = "En Yüksek Skor  : " + Sayilar.OyunSkor;
                sayi.IsDurum = await DisplayAlert("Süre Doldu", "Tebrikler. Yeni Skor  : "+sayi.ToplamPuan, "TEKRAR OYNA", "ÇIKIŞ");
                sayi.ToplamPuan = 0;
            }
            else
            {
                sayi.IsDurum = await DisplayAlert("Süre Doldu", "Skoru Geçemediniz.", "TEKRAR OYNA", "ÇIKIŞ");
                sayi.ToplamPuan = 0;
            }

            if (sayi.IsDurum == true)
            {
                lblSkor.Text = "En Yüksek Skor  : " + Sayilar.OyunSkor;
                lblPuan.Text = "Toplam Puan : 0";
                lbl_rastgeleSayiUret();
                GeriSayimAraci(60);
            }

            if (sayi.IsDurum == false)
                await Navigation.PushModalAsync(new ListPage());

            manager = null;
        }
        private double Toplama(double a, double b) => a + b;
        private double Cikarma(double a, double b) => a - b;
        private double Carpma(double a, double b) => a * b;
        private double Bolme(double a, double b) => a / b;

        private void lbl_rastgeleSayiUret()
        {
            btnDeafult();
            Random SayiUret = new Random();

            sayi.UretilenSayi1 = SayiUret.Next(0, 101);
            sayi.UretilenSayi2 = SayiUret.Next(1, 101);

            sayi.Isaret = (byte)SayiUret.Next(1, 5);   //Hangi Matematiksel işlem olacağını belirtiyor
            sayi.Yeri = (byte)SayiUret.Next(1, 5);  //Cevabın yeri

            switch (sayi.Isaret)
            {
                case 1: sayi.Sonuc = Toplama(sayi.UretilenSayi1, sayi.UretilenSayi2); sayi.Islem = "+"; break;
                case 2: sayi.Sonuc = Cikarma(sayi.UretilenSayi1, sayi.UretilenSayi2); sayi.Islem = "-"; break;
                case 3: sayi.Sonuc = Carpma(sayi.UretilenSayi1, sayi.UretilenSayi2); sayi.Islem = "*"; break;
                case 4: sayi.Sonuc = Bolme(sayi.UretilenSayi1, sayi.UretilenSayi2); sayi.Islem = "/"; break;
            }
            lblText.Text = sayi.UretilenSayi1 + "\t\t" + sayi.Islem + "\t\t" + sayi.UretilenSayi2;
            butonlaraSonucYaz(); //Sonucu Rastgele butona atadım.
        }

        private void butonlaraSonucYaz()  //Sonucu Rastgele butona atadım.
        {
            double gecici = sayi.Sonuc;
            switch (sayi.Yeri)
            {
                case 1: btn1.Text = string.Format("{0:0.##}", sayi.Sonuc); break;
                case 2: btn2.Text = string.Format("{0:0.##}", sayi.Sonuc); break;
                case 3: btn3.Text = string.Format("{0:0.##}", sayi.Sonuc); break;
                case 4: btn4.Text = string.Format("{0:0.##}", sayi.Sonuc); break;
            }

            for (int i = 1; i < 5; i++)
            {
                gecici+=5;
                if (i != sayi.Yeri)
                {
                    switch (i)
                    {
                        case 1: btn1.Text = string.Format("{0:0.##}", gecici); break;
                        case 2: btn2.Text = string.Format("{0:0.##}", gecici); break;
                        case 3: btn3.Text = string.Format("{0:0.##}", gecici); break;
                        case 4: btn4.Text = string.Format("{0:0.##}",gecici); break;
                    }
                }
            }

        }

        private async void btn1_Clicked(object sender, EventArgs e)
        {
            if (btn1.Text == string.Format("{0:0.##}", sayi.Sonuc))
            {
                if (sayi.Sayac == 0)
                    sayi.ToplamPuan += 10;

                btn1.BackgroundColor = Color.Green;
                lblPuan.Text = "Toplam Puan : " + sayi.ToplamPuan;
                sayi.Sayac = 0;
                btnlariDondur(1);
                await Task.Delay(150);
                lbl_rastgeleSayiUret();
            }
            else
            {
                btn1.BackgroundColor = Color.Red;
                sayi.Sayac++;
            }

        }

        private async void btn2_Clicked(object sender, EventArgs e)
        {
            if (btn2.Text == string.Format("{0:0.##}", sayi.Sonuc))
            {
                if (sayi.Sayac == 0)
                    sayi.ToplamPuan += 10;

                btn2.BackgroundColor = Color.Green;
                lblPuan.Text = "Toplam Puan : " + sayi.ToplamPuan;
                sayi.Sayac = 0;
                btnlariDondur(2);
                await Task.Delay(150);
                lbl_rastgeleSayiUret();
            }
            else
            {
                btn2.BackgroundColor = Color.Red;
                sayi.Sayac++;
            }
        }

        private async void btn3_Clicked(object sender, EventArgs e)
        {
            if (btn3.Text == string.Format("{0:0.##}", sayi.Sonuc))
            {
                if (sayi.Sayac == 0)
                    sayi.ToplamPuan += 10;

                btn3.BackgroundColor = Color.Green;
                lblPuan.Text = "Toplam Puan : " + sayi.ToplamPuan;
                sayi.Sayac = 0;
                btnlariDondur(3);

                await Task.Delay(150);
                lbl_rastgeleSayiUret();
            }
            else
            {
                btn3.BackgroundColor = Color.Red;
                sayi.Sayac++;
            }
        }

        private async void btn4_Clicked(object sender, EventArgs e)
        {
            if (btn4.Text == string.Format("{0:0.##}", sayi.Sonuc))
            {
                if (sayi.Sayac == 0)
                    sayi.ToplamPuan += 10;

                btn4.BackgroundColor = Color.Green;
                lblPuan.Text = "Toplam Puan : " + sayi.ToplamPuan;
                sayi.Sayac = 0;
                btnlariDondur(4);
                await Task.Delay(150);
                lbl_rastgeleSayiUret();
            }
            else
            {
                btn4.BackgroundColor = Color.Red;
                sayi.Sayac++;
            }
        }

        private void btnDeafult() // Butonun Arka Plan Rengini Eski Haline Döndürüyorum
        {
            manager = new SQLiteManager();
            if (manager.GetAll().ToList().OrderByDescending(x => x.Skor).FirstOrDefault()==null)
                Sayilar.OyunSkor = 0;

            else
            {
                manager = new SQLiteManager();
                Sayilar.OyunSkor = manager.GetAll().ToList().OrderByDescending(x => x.Skor).FirstOrDefault().Skor;
            }

            lblSkor.Text = "En Yüksek Skor  : " + Sayilar.OyunSkor;
            lblPuan.Text = "Toplam Puan : " + sayi.ToplamPuan;
            btn1.BackgroundColor = Color.LightSteelBlue;
            btn2.BackgroundColor = Color.LightSteelBlue;
            btn3.BackgroundColor = Color.LightSteelBlue;
            btn4.BackgroundColor = Color.LightSteelBlue;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            btn3.IsEnabled = true;
            btn4.IsEnabled = true;
            manager = null;
        }

        private void btnlariDondur(int item)
        {
            for (int i = 1; i < 5; i++)
            {
                if (i != item)
                {
                    switch (i)
                    {
                        case 1: btn1.IsEnabled = false; break;
                        case 2: btn2.IsEnabled = false; break;
                        case 3: btn3.IsEnabled = false; break;
                        case 4: btn4.IsEnabled = false; break;
                    }
                }

            }
        }
    }
}