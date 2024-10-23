using eokuluyg.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eokuluyg.Dogrulamalar
{

    internal class DersDogrulama : AbstractValidator<Ders>
    {

        public DersDogrulama()
        {

            RuleFor(x => x.DersKodu).NotEmpty().WithMessage("Ders Kodu boş geçilemez");
            RuleFor(x => x.DersAdi).NotEmpty().WithMessage("Ders Adı boş geçilemez");

            RuleFor(x => x.DersKodu).Length(5).WithMessage("Ders Kodu 5 KArakterden Oluşmalı");
            RuleFor(x => x.DersAdi).MinimumLength(4).WithMessage("Ders Adı En az 4 harften Oluşmalı");
        }
    }
}
