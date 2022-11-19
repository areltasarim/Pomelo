using EticaretWebCoreEntity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EticaretWebCoreEntity
{
    public class Markalar : BaseEntity
    {

        [Required(ErrorMessage = "Lutfen Marka Adini Giriniz."), MaxLength(250, ErrorMessage = "Marka Adi 250 Karakterden Fazla Olmamaz.")]
        public string MarkaAdi { get; set; }
        public string Url { get; set; }
        public string Resim { get; set; }
        public int Sira { get; set; }


        public override void Build(ModelBuilder builder)
        {
            builder.Entity<Markalar>(entity =>
            {
                entity
               .Property(p => p.MarkaAdi)
               .HasMaxLength(250);
            });
        }
    }

}