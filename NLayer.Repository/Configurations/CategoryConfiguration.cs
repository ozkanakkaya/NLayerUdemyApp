using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);//Id key olacak.
            builder.Property(x => x.Id).UseIdentityColumn();//kaçar kaçar artacak(birer birer).
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);//zorunlu,max 50 karakter

            builder.ToTable("Categories");//tablo adı

        }
    }
}
