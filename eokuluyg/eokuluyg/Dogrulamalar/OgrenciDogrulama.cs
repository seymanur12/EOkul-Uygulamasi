using eokuluyg.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// OgrenciDogrulama.cs   class'ı 
// AbstractValidator'dan  türemeli. 
// ve
// FluentValidation  Kütüphanesi eklenmeli.
namespace eokuluyg.Dogrulamalar
{
    internal class OgrenciDogrulama : AbstractValidator<Ogrenci>
    {
        public OgrenciDogrulama()
        {   
            // Ad Soyad  boş geçilemez
            RuleFor(x => x.Ad).NotEmpty().WithMessage("Öğrenci Adı  boş Geçilemez");
            RuleFor(x => x.Soyad).NotEmpty().WithMessage("Öğrenci Soyadı  boş Geçilemez");
            // Ad Soyad min bu kadar uzunlukta olmalı
            RuleFor(x => x.Ad).MinimumLength(3).WithMessage("Öğrenci Adı  enaz 3 karakterden oluşmalı");
            RuleFor(x => x.Soyad).MinimumLength(2).WithMessage("Öğrenci Soyadı  enaz 2 karakterden oluşmalı");
            // Okul No boş olmamalı
            RuleFor(x => x.OkulNo).NotNull().WithMessage("Okul No boş geçilemez ve rakamdan oluşmalı");


        }
    }
}
// AbstractValidator<>
// Hangi class'dan oluşmuş  class üzerinde doğrulama yapılcaksa
//                          o class dosyasının adı verilir. 

// Models içindeki Ogrenci.cs, üzerinde Doğrulama yapılcak.