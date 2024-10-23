using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eokuluyg.Models;
using eokuluyg.ViewModel;
using MySql.Data.MySqlClient;
using Dapper;
namespace eokuluyg.Vtislem
{

    internal class VtislemOgrenciPuan : Base
    {
        public List<OgrenciDersPuan> TumPuanlariCek()
        {
            List<OgrenciDersPuan> puanlar = null;
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {

                string sorgu = "select ders.DersAdi, ders.DersSaati, ogrencipuan.*,ogrenci.Ad,ogrenci.Soyad,ogrenci.OkulNo  from ogrencipuan " +
                                "inner join ders on ders.Id = ogrencipuan.DersId " +
                                "inner join ogrenci on ogrenci.Id = ogrencipuan.OgrenciId";
                // Sütünları birleştirmesi için, JOİN'li  SORGUMUZ
                // Bu sorgu ile veritabanından gelen veriler.
                //--------------- MODELS class'ı ile birebir eşleşmeli,
                //--------------- buna uygun MODELS class'ımızı daha önce oluşturduk.
                puanlar = baglanti.Query<OgrenciDersPuan>(sorgu).ToList();


            }

            return puanlar;

        }

        public List<OgrenciDersPuan> OgrenciPuanAraOgrenciIdYeGore(int ogrenciId)
        {
            List<OgrenciDersPuan> puanlar = null;
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                string sorgu = "select ders.DersAdi, ders.DersSaati, ogrencipuan.*,ogrenci.Ad,ogrenci.Soyad,ogrenci.OkulNo  from ogrencipuan " +
                               "inner join ders on ders.Id = ogrencipuan.DersId " +
                               "inner join ogrenci on ogrenci.Id = ogrencipuan.OgrenciId " +
                               "where OgrenciId=@OgrenciId";
                // Join'li  SORGUMUZ, Buna uygun
                // Bu sorgu ile veritabanından gelen veriler.
                //--------------- MODELS class'ı ile birebir eşleşmeli,
                //--------------- buna uygun MODELS class'ımızı daha önce oluşturduk.
                //--------------------------------------------------------------------------------- where OgrenciId = @OgrenciId 
                //--------------------------------------------------------------------------------- ogrenciId'ye göre arama yapıyoruz
                puanlar = baglanti.Query<OgrenciDersPuan>(sorgu, new { OgrenciId = ogrenciId }).ToList();


            }
            return puanlar;

        }



        //------------------------------------------------ Bu fonksiyona Öğrenci Notlarını gönderiyoruz. 
        //------------------------------------------------               DersSaatini gönderiyoruz
        //------------------------------------------------               Öğrenci Notları ile DersSaatini çarpıyorz
        public double? agirlikliOrtalamaHesapla(List<OgrenciDersPuan> puanlar)
        {
            int dersSaatToplam = 0;
            double? agirlikliToplam = 0;
            foreach (var puan in puanlar)
            {
                dersSaatToplam += puan.DersSaati;
                agirlikliToplam += puan.DersSaati * puan.Ortalama;

            }

            return agirlikliToplam / dersSaatToplam;
        }



        //---------------------------------------- DersId'ye göre arama yap. Sorguda
        public List<OgrenciDersPuan> OgrenciPuanAraDersIdYeGore(int dersId)
        {
            List<OgrenciDersPuan> puanlar = null;
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                string sorgu = "select ders.DersAdi, ders.DersSaati, ogrencipuan.*,ogrenci.Ad,ogrenci.Soyad,ogrenci.OkulNo  from ogrencipuan " +
                               "inner join ders on ders.Id = ogrencipuan.DersId " +
                               "inner join ogrenci on ogrenci.Id = ogrencipuan.OgrenciId " +
                               "where DersId=@DersId";

                puanlar = baglanti.Query<OgrenciDersPuan>(sorgu, new { DersId = dersId }).ToList();


            }
            return puanlar;

        }




        //-----OgrenciPuan.cs  Tablosundan 
        //----------------------------------------------- ogrenciId ve  dersId    bilgileri ile
        public OgrenciPuan PuanBulOgrenciveDerseGore(int ogrenciId, int dersId) // Ogrencinin mat dersindeki NOTUNU öğrenebiliriz.
        {
            OgrenciPuan bulunanOgrenciPuan = null;
            string sorgu = "select *  from ogrencipuan  where OgrenciId=@OgrenciId and DersId=@DersId";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                bulunanOgrenciPuan = baglanti.QuerySingleOrDefault<OgrenciDersPuan>(sorgu, new { OgrenciId = ogrenciId, DersId = dersId });
            }
            return bulunanOgrenciPuan;
        }

    }


}
