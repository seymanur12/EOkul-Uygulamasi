using Dapper;
using eokuluyg.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// VERİTABANI İŞLEMLERİ   : Ogrenciİslemleri sayfası için.
namespace eokuluyg.Vtislem
{        internal class VtislemOgrenci : Base
        {
            public List<Ogrenci> OgrencileriSecAdSoyadaGore(string Ad, String Soyad)//---------------------------------------------- Fonksiyona gelen parametreler bunlar
            {
            //------------------ OgrencileriSecAdSoyadaGore   ARAMA Yapcak. adsoyada göre seçicek

                List<Ogrenci> ogrencis = null; // LİSTEMİZİ TANIMLADIK. 
                                               // LİSTEMİZİN değerini ilk NULL yaptık.

                using (IDbConnection baglanti = new MySqlConnection(ConnectionString)) // db bağlantısı
                {
                    // Sorgu : 
                    string sorgu = "select * from ogrenci where Ad Like @Ad and Soyad Like @Soyad";
                    // Sorguyu çalıştır : 
                    ogrencis = baglanti.Query<Ogrenci>(sorgu, new { Ad = Ad + "%", Soyad = Soyad + "%" }).ToList();
                                                                                   //---------------------------------------------- Sorguya gönderilen Ad ve Soyad'ı parametre olarak gönderdik.
                                                                                                        // Gelen değeri
                                                                                                        // ogrencis    değişkenine koyduk.
                                                                                  //--------------------------------- @Ad @Soyad



                }


                return ogrencis;

            }


        // Okul numarasına göre Öğrenci seçmek.
        // Fonksiyonun,dönüş tipi Öğrenci listesi : 
        //-----List<Ogrenci>
        public List<Ogrenci> OgrencileriSecOkulNoyaGore(int? okulNo) 
            {
                List<Ogrenci> ogrencis = null;// Liste oluşturduk
                                              // Listenin ilk değeri NULL olsun dedik.
                                              

                using (IDbConnection baglanti = new MySqlConnection(ConnectionString)) //  db bağlantısı kurduk.
                {
                    string sorgu = "select * from ogrenci where OkulNo=@OkulNo";//------------------------------ sorgu yazdık           : OkulNo'ya göre arama yap sorgusu
                    ogrencis = baglanti.Query<Ogrenci>(sorgu, new { OkulNo = okulNo }).ToList();//-------------- sorgu çalıştırdık. çalıştırırken 
                }
                return ogrencis;

            }



        // Tek bir Öğrenci seçmek. No'ya göre 
        // Fonsiyonun geriye dönceği verinin tipi : Ogrenci
        public Ogrenci OgrenciSecOkulNoyaGore(int? okulNo)
            {
                Ogrenci bulunanOgrenci = null;  // Değişken oluşturduk. Başlangiç değeri NULL olsun dedik.
            
                string sorgu = "select * from ogrenci where OkulNo=@OkulNo"; //---------------------------------------------------------------- sorgu : 

                using (IDbConnection baglanti = new MySqlConnection(ConnectionString)) //------------------------------------------------------ db bağlantısı : 
                {

                    bulunanOgrenci = baglanti.QuerySingleOrDefault<Ogrenci>(sorgu, new { OkulNo = okulNo });
                                        //    QuerySingleOrDefault  Fonskiyonu : Tek bir kayıt varsa onu getirir.
                                        //                                       birden fazla kayıt varsa ya da Kayıt yoksa : default yani yukarda verilen NULL değerini döndürür.
                
                    //------------------------------------------------------------------ OkulNo fonksiyona parametre olarak gelen,
                    //------------------------------------------------------------------ sorguya gönderdik.
            }
                return bulunanOgrenci;

            }


        
        // İd'ye göre  Tek bir öğrenci getirir.
        // Fonksiyonun geriye döndüreceği verinin, veri tipi : Ogrenci.
        public Ogrenci OgrenciSecIdyeGore(int? id)
            {
                Ogrenci bulunanOgrenci = null;        // Değişken oluşturduk. Bu Değişken içi ilk başta Null değerinde olsun.

                string sorgu = "select * from ogrenci where Id=@Id"; //--------------------------------------------------------------------------- Sorgu :

                using (IDbConnection baglanti = new MySqlConnection(ConnectionString)) //--------------------------------------------------------- db Baglantısı :
                {

                    bulunanOgrenci = baglanti.QuerySingleOrDefault<Ogrenci>(sorgu, new { Id = id });
                }
                return bulunanOgrenci;

            }


            
        // Ogrenci SEÇİLDİ. 
        // Daha sonra
        // ogrencinin OkulNo'su değiştirilmek isteniyorsa
        //
        //--------------------------------------------------------- 
            public Ogrenci GuncellemeIcinOgrenciAra(int? okulNo, int id)
            {
                Ogrenci bulunanOgrenci = null;
                string sorgu = "select * from ogrenci where OkulNo=@OkulNo and Id !=@Id "; //----------------------------------------------------------------- sorgu : 

                using (IDbConnection baglanti = new MySqlConnection(ConnectionString))//---------------------------------------------------------------------- baglanti : 
                {

                    bulunanOgrenci = baglanti.QuerySingleOrDefault<Ogrenci>(sorgu, new { OkulNo = okulNo, Id = id });
                }
                return bulunanOgrenci;

            }


        // Geriye Ogrenci türünden LİSTE dönücek. 
            public List<Ogrenci> SoneklenenOnOgrenciyiSec()
            {
                List<Ogrenci> ogrencis = null; //------------------- Boş Ogrenci Listesi türünden, değişken oluşturduk. 

                string sorgu = "select * from ogrenci ORDER BY Id DESC LIMIT 10";//------------------------------------------------------------ sorgu 
                                                        // ıd'ye göre, DESC artan sırada sıralancak. böylece en son eklenen en aşağıda olucak. Son eklenen 10 öğrenciyi getircek.
                using (IDbConnection baglanti = new MySqlConnection(ConnectionString))//------------------------------------------------------- baglantı
                {
                    ogrencis = baglanti.Query<Ogrenci>(sorgu).ToList();
                }
                return ogrencis;

            }



        // BU FONKSİYON 
        // Parametre olarak verilen    Ogrenci Nesnesi türündeki 
        //                                     eklencekOgrenci 'yi   TABLO'ya  EKLİCEK.
            public int YeniOgrenciEkle(Ogrenci eklenecekOgrenci)
            {
                int eklenenOgrenciSayisi;

                string sorgu = "Insert into ogrenci(Ad,Soyad,OkulNo) values (@Ad,@Soyad,@OkulNo)"; //----------------------------------------------------------- sorgu 
                           //           Fonksiyona gelen      
                          //            eklecekOgrenci 'deki  özellikler (Ad,Soyad,OkulNo ....) 
                          //                                  Sorgudaki  özellikle eşleşicek... 

                using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
                {
                    eklenenOgrenciSayisi = baglanti.Execute(sorgu, eklenecekOgrenci); // ... Burda
                }
                return eklenenOgrenciSayisi;
            }




        // GÜNCELLENECEK OGRENCİ ,   PARAMETRE    olarak    FONKSİYONA  GELİR:
        // GÜNCELLENECEK OGRENCİ :   guncellenecekOgrenci 
        //                           guncellenecekOgrenci   parametresi içinde (Ad,Soyad,OkulNo... gibi özellikler vardır.)  
            public int OgrenciGuncelle(Ogrenci guncellenecekOgrenci)
            {
                int guncellenenOgrenciSayisi;
                string sorgu = "update ogrenci set Ad=@Ad,Soyad=@Soyad,OkulNo=@OkulNo where Id=@Id";
                using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
                {
                    guncellenenOgrenciSayisi = baglanti.Execute(sorgu, guncellenecekOgrenci);
                }
                return guncellenenOgrenciSayisi;
            }




        // Sadece OgrenciId'ye   göre   silme işlemi yapılcak.
            public int OgrenciSil(int ogrenciId)
            {
                int silinenOgrenciSayisi;
                string sorgu = "delete from ogrenci  where Id=@Id";
                using (IDbConnection baglanti = new MySqlConnection(ConnectionString))
                {
                    silinenOgrenciSayisi = baglanti.Execute(sorgu, new { Id = ogrenciId });
                }
                return silinenOgrenciSayisi;
            }
        }
    }
