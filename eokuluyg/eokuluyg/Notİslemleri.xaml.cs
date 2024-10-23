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
//using eokuluyg.ViewModel;
using eokuluyg.Vtislem;
using eokuluyg.Dogrulamalar;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;
using eokuluyg.FaydalıCLasslar;

//---------------------------- Eğer bir metot değer döndürmüyorsa, void anahtar kelimesi kullanılmalıdır
namespace eokuluyg
{


    public partial class Notİslemleri : UserControl
    {
        VtislemDers vtIslemDers;

        VtislemOgrenciPuan vtIslemOgrenciPuan;

        VtislemOgrenci vtIslemOgrenci;


        OgrenciSecmePenceresi ogrenciSecmePenceresi;

        onİzlemePenceresi ozIzlemePenceresi;

        Ogrenci? secilenOgrenci;

        OgrenciPuan seciliOgrenciPuan;


        public Notİslemleri()
        {
            InitializeComponent();

            vtIslemOgrenciPuan = new VtislemOgrenciPuan();
            vtIslemOgrenci = new VtislemOgrenci();

            ////////////////////////////////////////////////////////////////////nesne türettik 
            vtIslemDers = new VtislemDers();
            ////////////////////////////////////////////////////////////////////Not işlemlerinde:Ders Seçiniz ComboBox ALANININ İSMİ : cbDers 
            ////////////////////////////////////////////////////////////////////cbDers ComboBox ALANIN ItemsSourcesine YANİ : içerisine hangi verilerin yükleneceğini belirtir
            cbDers.ItemsSource = vtIslemDers.TumdersleriCek();
            //////////////////////////////////////////////////////////////////// Kullanıcı ComboBox içindeki bir öğeyi seçtiğinde, 
            //////////////////////////////////////////////////////////////////// o öğenin Id özelliği, SelectedValue olarak dönecek.
            cbDers.SelectedValuePath = "Id";
            //////////////////////////////////////////////////////////////////// her bir öğenin hangi özelliğinin görüntüleneceğini belirlemek için kullanılır.
            //////////////////////////////////////////////////////////////////// aşağıda,ComboBox'ta görüntülenecek her öğe, veri kaynağındaki DersAdi adlı özelliğe göre gösterilecek
            cbDers.DisplayMemberPath = "DersAdi";

            ogrenciSecmePenceresi = new OgrenciSecmePenceresi();
            //------------------------------------------------------------------- ogrenciSecmePenceresi.cs 'deki
            //------------------------------------------------------------------- ogrenciSecmePenceresi.XAML' deki  lstOgrenciler  ALANIN DA
            //------------------------------------------------------------------- Bir satır seçilirse:
            //------------------------------------------------------------------- 
            //------------------------------------------------------------------- bu fonksiyon tetiklensin dedik. 
            //------------------------------------------------------------------- bu fonksiyon, seçilen satırdaki bilgileri yakalıcak. 
            //-------------------------------------------------------------------
            ogrenciSecmePenceresi.lstOgrenciler.SelectionChanged += LstOgrenciler_SelectionChanged;

            ozIzlemePenceresi = new onİzlemePenceresi();

            //******************************************************************** aşağıdaki DataGrid ALANINA Tüm verileri getirdik.
            //********************************************************************
            //********************************************************************
            dtGridPuan.ItemsSource = vtIslemOgrenciPuan.TumPuanlariCek();
        }

        private void LstOgrenciler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ogrenciSecmePenceresi.lstOgrenciler.SelectedItem != null)//------------------------------------------------------ boş satır olmamalı
            {
                secilenOgrenci = ogrenciSecmePenceresi.lstOgrenciler.SelectedItem as Ogrenci;
                //               ogrenciSecmePenceresi.lstOgrenciler.SelectedItem  bu seçilen satır, zaten Ogrenci class'ı türünde biliyoruz.
                //                                                                as Ogrenci  class'ına çevrilmesini istiyoruz. 
                //               çevirme başarılı olursa.  
                //                       başarılı olmazsa. sevilenOgrenci içi NULL olur.

                txtSecilenOgrenci.Text = secilenOgrenci.ToString();
                //txtSecilenOgrenci    İNPUT alanına  bilgiler yazılır. 
            }
        }

        private void btnOgrenciSec_Click(object sender, RoutedEventArgs e)
        {
            ogrenciSecmePenceresi.ShowDialog();
        }

        private void btnYazdir_Click(object sender, RoutedEventArgs e)
        {
            ozIzlemePenceresi.ShowDialog();
        }


        //******************************************************** Aşağıdaki Tablo olan DataGrid alanında Seçilen SATIR.
        //********************************************************
        private void dtGridPuan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

    }
}
