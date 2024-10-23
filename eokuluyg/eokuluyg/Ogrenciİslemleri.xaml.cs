using eokuluyg.Dogrulamalar;
using eokuluyg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;
using eokuluyg.Vtislem;
using eokuluyg.FaydalıCLasslar;

namespace eokuluyg
{

    public partial class Ogrenciİslemleri : UserControl
    {
        // Veritabanı bağlantı
        // Veritabanı sorgu    yazdığımız fonksiyonların olduğu dosyamızı,buraya yeni nesne üreterek, ekleyelim.
        // FONSİYONLara ulaşabilcez.
        VtIslemOgrenci vtIslemOgrenci;
        public Ogrenciİslemleri()
        {
            InitializeComponent();
            vtIslemOgrenci = new VtIslemOgrenci();

            //-------------------------- dosyadaki fonksiyonu çağırdık.
            //--------------------------           fonksiyondan gelen sonuc'u   değişkene atadık. 
            dtGridOgrenci.ItemsSource = vtIslemOgrenci.SoneklenenOnOgrenciyiSec();
        }

        //_______________________________________________________________________________________________________________________________________________________
        void temizle()
        {
            HataList.Items.Clear();    // Hata listesi alanını temizle

            txtAd.Clear();             // inputları temizle
            txtOgrenciId.Clear();
            txtOkulNo.Clear();
            txtSoyad.Clear();
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e) // TEMİZLE butonuna verdiğimiz fonksiyon. 
        { 
            temizle();                                                  // temizle()  fonksiyonunu çalıştırıyor.
        }
        //_______________________________________________________________________________________________________________________________________________________


        // Aşağıdaki  Tablodan  seçilen  satır.
        // ------------------------------------ Yukardaki  inputlara gelsin. dicez : 
        private void dtGridOgrenci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ogrenci secilenOgrenci = dtGridOgrenci.SelectedItem as Ogrenci;

            if (secilenOgrenci != null)
            {
                txtOgrenciId.Text = secilenOgrenci.Id.ToString();
                txtAd.Text = secilenOgrenci.Ad;
                txtSoyad.Text = secilenOgrenci.Soyad;
                txtOkulNo.Text = secilenOgrenci.OkulNo.ToString();
            }


        }




        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            HataList.Items.Clear(); //---------------------------------------------------------------------------------- Hata listesini temizledik.

            Ogrenci eklenecekOgrenci = new Ogrenci()  //---------------------------------------------------------------- Nesne oluşturduk.
            {                                         //-------------------------- input alanlarındaki verilerden oluşan
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                OkulNo = (Int32.TryParse(txtOkulNo.Text, out int okulNo) ? okulNo : (int?)null)// yanlışsa soru işaretinden sonrası çalışcak
            };           // String(Text) olarak verilen veriyi, İnt32'ye çevirmeye çalışcak



            OgrenciDogrulama dogrulama = new OgrenciDogrulama();//  OgrenciDogrulama class'ından  değişken oluşturduk. 

            ValidationResult dogrulamaSonucu = dogrulama.Validate(eklenecekOgrenci);//          bu değişkeni, fonksiyona gönderdik. 
                                                                                   //           gelen sonucu değişkene koyduk. 
                                                                                   //           DOĞRULAMALAR'a gidip gelcek, gelen değer 0 veya 1 olcak

            if (dogrulamaSonucu.IsValid == false)
            { //-------------------------------------------------- DOĞRULAMADAN GEÇEMEDİ
                foreach (ValidationFailure herBirHata in dogrulamaSonucu.Errors)
                {
                    HataList.Items.Add(herBirHata.ErrorMessage);
                }
            }//--------------------------------------------------- DOĞRULAMADAN GEÇTİ 
            else
            {
                
                var ogrenci = vtIslemOgrenci.OgrenciSecOkulNoyaGore(Int32.Parse(txtOkulNo.Text));
                //------------------------------------------------ DOĞRULAMADAN GEÇTİ.TEKRAR KONTROLE SOKTUK : 
                if (ogrenci == null)
                {
                    // Bu eklencek olan öğrenciden aynı verilere sahip başka öğrenci varmı bunu öğrenelim.
                    //---------------------------------------.YeniOgrenciEkle  fonksiyonuna gönderilir.
                    //--------------------------------------- bu fonksiyon sonucunda geriye 1 dönerse öğrenci eklenir.
                    int eklenenOgrenciSayisi = vtIslemOgrenci.YeniOgrenciEkle(eklenecekOgrenci);

                    if (eklenenOgrenciSayisi == 1)
                    {
                        MesajClass.BilgiMesajiGoster("Yeni Öğrenci Başarı ile Eklenmiştir", "Öğrenci ekleme");// Kullanıcı bilgilendirme
                        dtGridOgrenci.ItemsSource = vtIslemOgrenci.SoneklenenOnOgrenciyiSec();
                        temizle();
                    }
                    else
                    {
                        MesajClass.hataMesajiGoster("Öğrenci ekleme sırasında hata oluştu", "Öğrenci ekleme Hatası");
                    }

                }
                else
                {
                    MesajClass.hataMesajiGoster($"{txtOkulNo.Text} okul nolu öğrenci daha önce eklenmiştir.", "Öğrenci ekleme Hatası");
                }

            }



        }










        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {// GÜNCELLEMEYİ  öğrencci İD'sine göre yapıcaz.
       // Tablodan seçilen öğrenci bilgileri, yukardaki inputa gelir. ya da biz inputa id yazarız. BU İNPUTTAKİ İD'ye göre : 
       // Bu yüzden önce
       // İNPUT id  alanı  boş mu   dolu mu onun kontrolünü yapalim önce : 

            if (txtOgrenciId.Text != "")                 // boş  değilse :
            {
                HataList.Items.Clear();
                Ogrenci guncellenecekOgrenci = new Ogrenci() // inputtdaki değerleri
                {                                         // şimdi oluşturduğumuz Ogrenci tipindeki değişkenimizde
                                                          // şimdi oluşturduğumuz değişkenlere eşitliyoruz.
                                                          // bu değişkenler OgrenciDoğrulamadaki  değişkenlere karşılık gelmeli isimleri.
                    Id = Int32.Parse(txtOgrenciId.Text),
                    Ad = txtAd.Text,
                    Soyad = txtSoyad.Text,
                    OkulNo = (Int32.TryParse(txtOkulNo.Text, out int okulNo) ? okulNo : (int?)null)
                };

                OgrenciDogrulama dogrulama = new OgrenciDogrulama();
                ValidationResult dogrulamaSonucu = dogrulama.Validate(guncellenecekOgrenci); // DOĞRULAMA sonucu gelen değer : 
                if (dogrulamaSonucu.IsValid == false)                                        // false ise hata vardır.
                {
                    foreach (ValidationFailure herBirHata in dogrulamaSonucu.Errors)
                    {
                        HataList.Items.Add(herBirHata.ErrorMessage);
                    }
                }
                else                                                                        // Hata yoktur : 
                {
                    //------------------------------------------------------- şimdi Aynı Okul Noya ait 2 öğrenci olamayacağı için kontrolü yapılır.
                    var ogrenci = vtIslemOgrenci.GuncellemeIcinOgrenciAra(Int32.Parse(txtOkulNo.Text), Int32.Parse(txtOgrenciId.Text));
                    //------------------------------------------------------------------------------------------ GüncellemeIcınOgrenci ara Fonksiyonu bunun için oluşturuldu.
                    //------------------------------------------------------------------------------------------ GüncellemeIcınOgrenci ara Fonksiyonu aynı no'da başka öğrenci varmı ona bakar.
                    if (ogrenci == null)
                    {
                        string guncellemeOnayıMesaji = $"{txtOkulNo.Text} : {txtAd.Text} {txtSoyad.Text} ad soyadlı öğrenciyi Güncellemek İstediğinize Emin misiniz?";
                        MessageBoxResult cevap = MesajClass.SoruMesajiGoster(guncellemeOnayıMesaji, "Öğrenci Güncelleme Onayı");
                        if (cevap == MessageBoxResult.Yes)
                        {

                            int guncellenenOgrenciSayisi = vtIslemOgrenci.OgrenciGuncelle(guncellenecekOgrenci);//------------------------------- Burda güncellenir.
                            if (guncellenenOgrenciSayisi == 1)
                            {
                                MesajClass.BilgiMesajiGoster("Öğrenci Güncelleme Başarılı", "Öğrenci Güncelleme");
                                dtGridOgrenci.ItemsSource = vtIslemOgrenci.SoneklenenOnOgrenciyiSec();
                                temizle();
                            }
                            else
                            {
                                MesajClass.hataMesajiGoster("Öğrenci Güncelleme sırasında hata oluştu", "Öğrenci ekleme Hatası");
                            }
                        }


                    }
                    else
                    {
                        MesajClass.hataMesajiGoster($"{txtOkulNo.Text} okul nolu öğrenci daha önce eklenmiştir.", "Öğrenci ekleme Hatası");

                    }
                }

            }
            else
            {
                MesajClass.hataMesajiGoster("Lütfen Güncelleştirilecek Öğrenciyi Seçiniz", "Öğrenci Güncelleme Hatası");
            }

        }





        // ARAMA BUTONU 
        // ad(input) ve soyadda(input) yazan bilgiye göre  ARAMA yapılsın
        // ya da
        // OkulNo(input) da ki bilgiye  göre arama yapılsın : 
        //--------------------------------------- ama ilk OkulNo'ya göre arama yapılsın, OkulNo boş ise ad ve soyada göre arama yapcak : 
        private void btnAra_Click(object sender, RoutedEventArgs e)
        {
            int? OkulNo = (Int32.TryParse(txtOkulNo.Text, out int okulNo) ? okulNo : (int?)null);

            string Ad = txtAd.Text;
            string Soyad = txtSoyad.Text;
            if (OkulNo != null)//-------------------------------------------------------------- okulno boş değilse
            {
                var ogrenciler = vtIslemOgrenci.OgrencileriSecOkulNoyaGore(okulNo);

                if (ogrenciler.Count == 0) //------------------------------------ gelen değer 0 ise öyle bir öğrenci yoktur.
                {
                    MesajClass.BilgiMesajiGoster("Aranılan Öğrenci Bulunamadı", "Öğrenci Armama");
                }
                else                     //-------------------------------------- ogrenci bulunmuştur. aşağıdaki Tabloda bu öğrencinin bilgileri gözükür.
                {
                    dtGridOgrenci.ItemsSource = ogrenciler;
                }
            }
            else //---------------------------------------------------------------------------- okulno boş ise 
            {   //------------------------------------------------------------------------------ Ad ve Soyad inputu boş mu ona bakılır : 
                if (Ad.Trim() != "" || Soyad.Trim() != "")
                {
                    var ogrenciler = vtIslemOgrenci.OgrencileriSecAdSoyadaGore(Ad, Soyad);
                    if (ogrenciler.Count == 0)
                    {
                        MesajClass.BilgiMesajiGoster("Aranılan Kriterlere uygun Öğrenci Bulunamadı", "Öğrenci Armama");
                    }
                    else
                    {
                        dtGridOgrenci.ItemsSource = ogrenciler;
                    }
                }


            }
        }



        // SİL   BUTONUNA,    aşağıdaki      btnSil_Click    fonksiyonunu verdik
        // SİL   BUTONUNA     basılınca      btnSil_Click    fonksiyonu   çalışcak.
        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            //------------------------------------------------------------------- Aşağıdaki tablodan öğrenci seçilince,Yukarıdaki input alanlarına bilgileri doluyordu.
            //------------------------------------------------------------------- ya da yukardaki OgrenciId inputu   dolu mu ona bakılır.
            if (txtOgrenciId.Text != "")//--------------------------------------- boş değilse : 
            {
                int ogrenciId = Int32.Parse(txtOgrenciId.Text);
                string silmeOnayıMesaji = $"{txtOkulNo.Text} : {txtAd.Text} {txtSoyad.Text} ad soyadlı öğrenciyi Silmek İstediğinize Emin misiniz?";
                MessageBoxResult cevap = MesajClass.SoruMesajiGoster(silmeOnayıMesaji, "Öğrenci Silme Onayı");
                if (cevap == MessageBoxResult.Yes)
                {
                    int silinenOgrenciSayisi = vtIslemOgrenci.OgrenciSil(ogrenciId);
                    if (silinenOgrenciSayisi == 1)
                    {
                        MesajClass.BilgiMesajiGoster("Öğrenci Silme Başarılı", "Öğrenci Silme");
                        dtGridOgrenci.ItemsSource = vtIslemOgrenci.SoneklenenOnOgrenciyiSec();
                        temizle();
                    }
                    else
                    {
                        MesajClass.hataMesajiGoster("Öğrenci Silme sırasında hata oluştu", "Öğrenci Silme Hatası");
                    }
                }

            }
            else
            {
                MesajClass.hataMesajiGoster("Lütfen Silinecek Öğrenciyi Seçiniz", "Öğrenci Silme Hatası");
            }
        }
    }


}
