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
using System.Windows.Shapes;
using System.ComponentModel;
using eokuluyg.Models;
using eokuluyg.Vtislem;

namespace eokuluyg
{
    /// OgrenciSecmePenceresi.xaml etkileşim mantığı
    /// OgrenciSecmePENCERESİ ,  Window class'ından Türemiştir.
    public partial class OgrenciSecmePenceresi : Window
    {
        VtislemOgrenci VtislemOgrenci; //------------------------------------------------------- Başka dosyadaki class'ı kullanabilmek için nesne türettik.
        public OgrenciSecmePenceresi()
        {
            InitializeComponent();
        }


        // e o anda yapılan işlemle ilgili bilgiyi tutar
        protected override void OnClosing(CancelEventArgs e)
        { /* Öğrenci Secme PENCERESİ'inde çarpıya basınca, sonra tekrar açmak istersek açılmaz.
       bunun için PENCEREDE, çarpıya basınca silinmesin,arkada kalsın demek için : 
            */
            base.OnClosing(e);

            e.Cancel = true; // e.Cancel  ise ture yap dedik

            this.Visibility = Visibility.Hidden;

        }


        // Hem Çarpı Butonu
        // Hem Geri dön butonu aynı işlem :
        // ------------------------------- geri dön tuşuna basınca pencereyi gizle dedik
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        // Ogrenci SECME PENCERESİNDE
        // ad   soyad   okulnı   İNPUT alanları değişirse, 
        // AŞAĞIDAKİ   ogrenciListesi    güncellensin dedik. 
        //_________________________________________________________________________________________________________________ ogrenciListesiniGuncelle()
        private void txtAd_TextChanged(object sender, TextChangedEventArgs e)
        {
            ogrenciListesiniGuncelle();
        }

        private void txtSoyad_TextChanged(object sender, TextChangedEventArgs e)
        {
            ogrenciListesiniGuncelle();
        }

        private void txtOkulNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            ogrenciListesiniGuncelle();
        }
        //_________________________________________________________________________________________________________________ ogrenciListesiniGuncelle()

        private void ogrenciListesiniGuncelle()
        {

            int? okulNo = (Int32.TryParse(txtOkulNo.Text, out int okulno) ? okulno : (int?)null);
            //--------------------------------------------------------------------------------------txtOkulNo   Int32'ye dönüştürülcek.

            List<Ogrenci> ogrenciler = null;


            if (okulNo != null) //------------------------------------------------------------------   OkulNo  NULL değilse, 
            {
                ogrenciler = VtislemOgrenci.OgrencileriSecOkulNoyaGore(okulno); //-----------------------------OkulNo ya göre seç Fonksiyonu ile getircez
            }
            else              //--------------------------------------------------------------------   OkulNo  NULL ise, Ad Soyad'a bakıcaz: 
            {

                ogrenciler = VtislemOgrenci.OgrencileriSecAdSoyadaGore(txtAd.Text.Trim(), txtSoyad.Text.Trim());

            }

            lstOgrenciler.ItemsSource = ogrenciler;


        }
    }
}
