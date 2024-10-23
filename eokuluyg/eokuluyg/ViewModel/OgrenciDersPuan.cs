using eokuluyg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eokuluyg.ViewModel
{
    internal class OgrenciDersPuan : OgrenciPuan
    {

        public String DersAdi { get; set; }
        public int DersSaati { get; set; }
        public int OkulNo { get; set; }
        public String Ad { get; set; }
        public String Soyad { get; set; }
        public String AdSoyad
        {
            get
            {
                return $"{Ad} {Soyad}";  // iki özelliği birleştirdik
            }

        }


    }
}
