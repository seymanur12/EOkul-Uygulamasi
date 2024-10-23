using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eokuluyg.Models
{
    class Ogrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int? OkulNo { get; set; }
        public string Soyad { get; set; }
        public override string ToString()
        {
            return $"{OkulNo} : {Ad} {Soyad} ";
        }
    }
}
// int?   soru işareti null değerini alamaz. demek
