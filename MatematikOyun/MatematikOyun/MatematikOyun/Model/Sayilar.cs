namespace MatematikOyun.Model
{
    public class Sayilar
        {
            public double UretilenSayi1 { get; set; }
            public double UretilenSayi2 { get; set; }
            public byte Isaret { get; set; } //Hangi Matematiksel işlem olacağını belirtiyor
            public byte Yeri { get; set; } //Cevabı rastgele bir butona bastım
            public double Sonuc { get; set; }
            public string Islem { get; set; } //Toplama - Çıkarma - Çarpma -- Bölme
            public uint ToplamPuan { get; set; }

            public byte Sayac { get; set; } = 0;
            public static uint OyunSkor { get; set; } = 0;

            public bool IsDurum { get; set; }

        }
}
