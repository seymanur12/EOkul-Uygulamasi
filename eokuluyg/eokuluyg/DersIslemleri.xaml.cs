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
using eokuluyg.Models;
using eokuluyg.Vtislem;
using eokuluyg.Dogrulamalar;
// Dogrulama kütüphanesi 
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;
using eokuluyg.FaydalıCLasslar;
using eokuluyg.Models;
using eokuluyg.Vtislem;

namespace eokuluyg
{
    /// <summary>
    /// DersIslemleri.xaml etkileşim mantığı
    /// </summary>
    public partial class DersIslemleri : UserControl
    {
        VtislemDers VtislemDers;
        public DersIslemleri()
        {
            InitializeComponent();
            VtislemDers = new VtislemDers();
            dtGridDers.ItemsSource = VtislemDers.TumdersleriCek();
            dersSaatleriComboBoxiniDoldur();
        }

        void dersSaatleriComboBoxiniDoldur() // DersSaati input ALANI için. 
        {
            cbDersSaati.Items.Clear();
            for (int i = 1; i < 15; i++)
            {
                cbDersSaati.Items.Add(i.ToString()); //1 den 14 e olan sayılarla doldurcaz inputu 
            }                                       // Bu seçme alanının açılır kapanır pencerenin itemleri, bunlardan oluşsun dedik.   
            cbDersSaati.SelectedIndex = 1;

        }
        private void Temizle() // input alanlarını temizler bu fonksiyon
        {
            txtDersId.Clear();
            txtDersAdi.Clear();
            txtDersKodu.Clear();

            // ListBox'da sağdaki hata listesini temsil eden ListBox'un ismi  : HataList  imiş 
            //                         Bunun içindeki İtemleri temizle dedik. 
            HataList.Items.Clear();
        }

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            // ListBox'da sağdaki hata listesini temsil eden ListBox'un ismi  : HataList  imiş 
            //                         Bunun içindeki İtemleri temizle dedik. 
            HataList.Items.Clear();


            Ders eklenecekDers = new Ders()
            {
                DersAdi = txtDersAdi.Text,  // input alanının adı : txtDersAdi 
                DersKodu = txtDersKodu.Text,                     // txtDerskodu 
                DersSaati = Int32.Parse(cbDersSaati.Text)  // inputtaki değeri alıp bu değişkenlere koyduk. 

            };

            //-------------------------------------------------------------------------------------
            DersDogrulama dogrulama = new DersDogrulama();
      
            // Yukarda inputlara yazılan ders değerlerini aldık.
            //                             bu değerleri doğrulayalım :
            //                             bu yüzden    doğrulama içine: doğrulancak olan
            //                                                           eklenecekDers 'i yazdık.
            // Bu doğrulama sonucu
            //               validationResult'da olucak. 
            ValidationResult validationResult = dogrulama.Validate(eklenecekDers);

            if (validationResult.IsValid == false) //----------------------------------------------------- HATA VARSA : 
            { // Değeri   false ise
            // hata vardır. 
                foreach (var herbirHata in validationResult.Errors) // içindeki hatayı al 
                {                                                  // herbirhata  değişkeninde
                    HataList.Items.Add(herbirHata.ErrorMessage); //  herbirhata'yi  Listeye ekledik. 
                }
            }
            else
            {
                
                var bulunanders = VtislemDers.DersSecDersKodunaGore(txtDersKodu.Text);
                if (bulunanders == null) //----------------------------------------------------------------- bulunanDers = null ise 
                {                       //------------------------------------------------------------------ böyle bir ders veritabanında yok.
                                       //------------------------------------------------------------------- bu dersi ekleyebiliriz 
                    //---------------------------------.YeniDersEkle fonkisyonuna gönderdik.
                    //---------------------------------------------- fonksiyondan geriye == 1 dönerse 
                    int eklenenDersSayisi = VtislemDers.YeniDersEkle(eklenecekDers);

                    if (eklenenDersSayisi == 1)
                    {
                        //------------------------------------- Bilgi Mesajı : geriye == 1 dönerse , başarılı ile eklenir. 
                        MesajClass.BilgiMesajiGoster("Yeni Ders Başarı ile Eklenmiştir", "Ders ekleme");


                        dtGridDers.ItemsSource = VtislemDers.TumdersleriCek();//aşağıdaki tabloyu güncellemek için,yeni veri eklediğimiz için,tekrar tablo çağırdık.

                        // başarılı, ders ekleme işlemi sonrasında, ekranı temizleyelim:
                        Temizle();
                    }
                    else
                    {   
                        MesajClass.hataMesajiGoster("Ders ekleme sırasında hata oluştu", "Ders ekleme Hatası");

                    }


                }
                else
                {   //--------------------------HATA MESAJI :  yeni ders eklenmez. çünkü 
                    MesajClass.hataMesajiGoster($"{txtDersKodu.Text} kodlu ders daha önce eklenmiştir.", "Ders ekleme Hatası");

                }



            }
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            Temizle();
        }

        //!________ AŞAĞIDAKİ TABLODA kullanıcı bir satır seçerse
        // <DataGrid>     içindeki  SelectionChanged="dtGridDers_SelectionChanged"    özelliğindeki bu fonksiyonun adını verdik : 
        private void dtGridDers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ders secilenDers = dtGridDers.SelectedItem as Ders; 
            //   seçilen item   ,  Ders class'ından oluşturulmuş bir nesne olduğunu biliyoruz.
            //   bu yüzden
            //   seçilen item'i  " as Ders  "   diyerek, Ders  nesnesine çeviriyoruz.
            //   seçilen satır Ders nesnesine çevir
            //   secilenDers item'i,"  as Ders " ile Ders nesnesine çevrilirse, null olmucak.
            //   secilenDers, çevrilmesse değeri NULL olur.
            if (secilenDers != null)
            {
            //   seçilen satırdaki bilgileri seçerek alcaz: adını, idsini, kodunu, saatini 
                txtDersAdi.Text = secilenDers.DersAdi.ToString();
                txtDersId.Text = secilenDers.Id.ToString();
                txtDersKodu.Text = secilenDers.DersKodu.ToString();
                cbDersSaati.SelectedIndex = secilenDers.DersSaati - 1;
            //  seçilen satırdaki bilgileri, değişkene koyduk 
            }
        }

        //!________ AŞAĞIDAKİ TABLODA
        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            if (txtDersId.Text != "")
            {
                int dersId = Int32.Parse(txtDersId.Text);
                string silmeOnayıMesaji = $"{txtDersKodu.Text} : {txtDersAdi.Text}  dersini Silmek İstediğinize Emin misiniz?";
                MessageBoxResult cevap = MesajClass.SoruMesajiGoster(silmeOnayıMesaji, "Ders Silme Onayı");

                if (cevap == MessageBoxResult.Yes)
                {
                    int silinenDersSayisi = VtislemDers.DersSil(dersId); // DersSil  fonksiyonuna gönderiliyor.
                                                                         // VtislemDers.cs'deki   DersSil fonksiyonu
                                                                         // başarılı silerse 1 döner geriye.
                    if (silinenDersSayisi == 1)
                    {
                        MesajClass.BilgiMesajiGoster("Ders Silme Başarılı", "Ders Silme");
                        dtGridDers.ItemsSource = VtislemDers.SonEklenenOnDersiCek();
                        Temizle();
                    }
                    else
                    {
                        MesajClass.hataMesajiGoster("Ders Silme sırasında hata oluştu", "Ders Silme Hatası");
                    }
                }

            }
            else
            {
                MesajClass.hataMesajiGoster("Lütfen Silinecek Dersi Seçiniz", "Ders Silme Hatası");
            }
        }

        private void btnAra_Click(object sender, RoutedEventArgs e)
        {//------------------------------------------------------------------- DersAdi   ve  DersKodu na göre arama yapcaz
            if (txtDersAdi.Text.Trim() != string.Empty || txtDersKodu.Text.Trim() != string.Empty)
            {
                //Arama İşlemini Burada Başlat
                var dersler = VtislemDers.DersAdiVeKodunaGoreArama(txtDersAdi.Text, txtDersKodu.Text);
                if (dersler.Count == 0)
                {
                    MesajClass.BilgiMesajiGoster("Kriterlere uygun Ders Bulunamadı", "Ders Arama");
                }
                else
                {
                    dtGridDers.ItemsSource = dersler;
                }

            }
            else
            {
                MesajClass.BilgiMesajiGoster("Lütfen Aramak istediğiniz Ders adını veya kodunu giriniz", "Ders Arama");

            }
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if (txtDersId.Text != "")
            {
                HataList.Items.Clear();
                Ders guncellenecekDers = new Ders()
                {
                    Id = Int32.Parse(txtDersId.Text),
                    DersAdi = txtDersAdi.Text,
                    DersKodu = txtDersKodu.Text,
                    DersSaati = Int32.Parse(cbDersSaati.Text)
                };
                DersDogrulama dogrulama = new DersDogrulama();
                ValidationResult dogrulamaSonucu = dogrulama.Validate(guncellenecekDers);
                if (dogrulamaSonucu.IsValid == false)
                {
                    foreach (ValidationFailure herBirHata in dogrulamaSonucu.Errors)
                    {
                        HataList.Items.Add(herBirHata.ErrorMessage);
                    }
                }
                else
                {
                    //Aynı Okul Noya ait 2 Ders olamayacağı için kontrol gerekli
                    var ders = VtislemDers.GuncellemeIcinDersAra(txtDersKodu.Text, Int32.Parse(txtDersId.Text));
                    if (ders == null)
                    {
                        //Güncelleme Yapılabilir
                        string GuncellemeOnayıMesaji = $"{txtDersKodu.Text} : {txtDersAdi.Text}  dersini Güncellemek İstediğinize Emin misiniz?";
                        MessageBoxResult cevap = MesajClass.SoruMesajiGoster(GuncellemeOnayıMesaji, "Ders Güncelleme Onayı");
                        if (cevap == MessageBoxResult.Yes)
                        {
                            int guncellenenDersSayisi = VtislemDers.DersGuncelle(guncellenecekDers);
                            if (guncellenenDersSayisi == 1)
                            {
                                MesajClass.BilgiMesajiGoster("Ders Güncelleme Başarılı", "Öğrenci Güncelleme");
                                dtGridDers.ItemsSource = VtislemDers.SonEklenenOnDersiCek();
                                Temizle();
                            }
                            else
                            {
                                MesajClass.hataMesajiGoster("Ders Güncelleme sırasında hata oluştu", "Ders Güncelleme Hatası");
                            }


                        }

                    }
                    else
                    {
                        MesajClass.hataMesajiGoster($"{txtDersKodu.Text} kodlu Ders daha önce eklenmiştir.", "Ders güncellemeHatası");
                    }


                }
            }
            else
            {

                MesajClass.hataMesajiGoster("Lütfen Güncelleştirilecek Dersi Seçiniz", "Ders Güncelleme Hatası");
            }

        }
    }
}
