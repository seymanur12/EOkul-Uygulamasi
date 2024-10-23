using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eokuluyg.FaydalıCLasslar
{
    internal class MesajClass
    {
        // BU MESAJLAR SIK SIK  KULLANILCAGI   İÇİN , DOSYALARDA 
        //
        // TEK Bİ SEFER BURDA OLUŞTURDUK. 
        // Ve HER gerektiğinde   burdan   çağırıp   kullanacağız.
        public static MessageBoxResult hataMesajiGoster(string aciklama, string baslik)
        {
            return MessageBox.Show(aciklama, baslik, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult SoruMesajiGoster(string aciklama, string baslik)
        {
            return MessageBox.Show(aciklama, baslik, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }
        public static MessageBoxResult BilgiMesajiGoster(string aciklama, string baslik)
        {
            return MessageBox.Show(aciklama, baslik, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
