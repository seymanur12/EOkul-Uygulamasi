using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eokuluyg.Models;

namespace eokuluyg.Dogrulamalar
{
    internal class OgrenciPuanDogrulama : AbstractValidator<OgrenciPuan>
    {
        public OgrenciPuanDogrulama()
        {
            RuleFor(x => x.Yazili1).NotNull().WithMessage("1. Yazılı boş geçilemez ve rakamdan oluşmalı");
            RuleFor(x => x.Yazili1).ExclusiveBetween(0, 100).WithMessage("1. Yazılı 0-100 Arası olmalı");
            RuleFor(x => x.Yazili2).NotNull().WithMessage("2. Yazılı boş geçilemez ve rakamdan oluşmalı");
            RuleFor(x => x.Yazili2).ExclusiveBetween(0, 100).WithMessage("2. Yazılı 0-100 Arası olmalı");
            RuleFor(x => x.Performans1).NotNull().WithMessage("1. Performans boş geçilemez ve rakamdan oluşmalı");
            RuleFor(x => x.Performans1).ExclusiveBetween(0, 100).WithMessage("1. Performans 0-100 Arası olmalı");
            RuleFor(x => x.Performans2).NotNull().WithMessage("2. Performans boş geçilemez ve rakamdan oluşmalı");
            RuleFor(x => x.Performans2).ExclusiveBetween(0, 100).WithMessage("2. Performans 0-100 Arası olmalı");
        }

    }
}

