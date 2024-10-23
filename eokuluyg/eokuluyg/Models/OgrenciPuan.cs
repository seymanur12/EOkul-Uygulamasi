using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eokuluyg.Models
{
    internal class OgrenciPuan
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public int DersId { get; set; }
        public int? Yazili1 { get; set; }
        public int? Yazili2 { get; set; }
        public int? Performans1 { get; set; }
        public int? Performans2 { get; set; }
        public double? Ortalama { get; set; }
        public string Durum { get; set; }

        public void OrtalamaVeDurumHesapla()
        {
            Ortalama = (Yazili1 + Yazili2 + Performans1 + Performans2) / 4.0;
            Durum = Ortalama < 50 ? "Kaldı" : "Geçti";
        }
    }
}
