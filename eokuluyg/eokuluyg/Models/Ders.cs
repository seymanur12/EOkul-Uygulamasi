using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eokuluyg.Models
{
    public class Ders
    {
        public int Id { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public int DersSaati { get; set; }
        public override string ToString()
        {
            return $"{DersKodu} : {DersAdi}";
        }
    }
}
