﻿using Dapper;
using eokuluyg.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eokuluyg.Vtislem
{
    // Base.cs dosyasında BAGLANTİ STRİNGİ var.
    class VtislemDers : Base
    {
        //public void TumdersleriCek()
        public List<Ders> TumdersleriCek()   // FONKSİYON 
        {

            List<Ders> dersler = null;
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                // baglanti sağlandiysa içeri girer : 
                // içerde veritabanı için sorgularımızı yazıcaz : 

                string sorgu = "select * from ders order by Id";
                dersler = baglanti.Query<Ders>(sorgu).ToList();
            }

            return dersler;
        }

        public List<Ders> SonEklenenOnDersiCek()
        {
            List<Ders> dersler = null;
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                string sorgu = "select * from  ders ORDER BY ID DESC LIMIT 10";
                dersler = baglanti.Query<Ders>(sorgu).ToList();
            }
            return dersler;

        }

        public int YeniDersEkle(Ders eklenecekDers)
        {
            int eklenenDersSayisi;
            string sorgu = "Insert into ders(DersKodu,DersAdi,DersSaati) values (@DersKodu,@DersAdi,@DersSaati)";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                eklenenDersSayisi = baglanti.Execute(sorgu, eklenecekDers);
            }
            return eklenenDersSayisi;
        }

        public Ders DersSecDersKodunaGore(string dersKodu)
        {
            Ders bulunanDers = null;
            string sorgu = "select * from ders where DersKodu=@DersKodu";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {

                bulunanDers = baglanti.QuerySingleOrDefault<Ders>(sorgu, new { DersKodu = dersKodu });
            }
            return bulunanDers;

        }

        public List<Ders> DersAdiVeKodunaGoreArama(string dersAdi, string dersKodu)
        {
            List<Ders> dersler = null;
            string sorgu = "select * from ders where DersKodu like @DersKodu and  DersAdi like @DersAdi";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {

                dersler = baglanti.Query<Ders>(sorgu, new { DersKodu = dersKodu + "%", DersAdi = dersAdi + "%" }).ToList();
            }
            return dersler;

        }

        public Ders DersAraIdYeGore(int id)
        {
            Ders bulunanDers = null;
            string sorgu = "select * from ders where Id =@Id ";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {

                bulunanDers = baglanti.QuerySingleOrDefault<Ders>(sorgu, new { Id = id });
            }
            return bulunanDers;
        }

        public Ders GuncellemeIcinDersAra(string derskodu, int id)
        {
            Ders bulunanDers = null;
            string sorgu = "select * from ders where DersKodu=@DersKodu and Id !=@Id ";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {

                bulunanDers = baglanti.QuerySingleOrDefault<Ders>(sorgu, new { DersKodu = derskodu, Id = id });
            }
            return bulunanDers;
        }

        public int DersGuncelle(Ders guncellenecekDers)
        {
            int guncellenenDersSayisi;
            string sorgu = "update ders set DersKodu=@DersKodu,DersAdi=@DersAdi,DersSaati=@DersSaati where Id=@Id";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                guncellenenDersSayisi = baglanti.Execute(sorgu, guncellenecekDers);
            }
            return guncellenenDersSayisi;
        }

        public int DersSil(int dersId)
        {
            int silinenDersSayisi;
            string sorgu = "delete from ders  where Id=@Id";
            using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
            {
                silinenDersSayisi = baglanti.Execute(sorgu, new { Id = dersId });
            }
            return silinenDersSayisi;
        }


    }
}