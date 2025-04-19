using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Shared;

namespace IKEA.DAL.Data.Configurations
{
    public class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

            builder.Property(D => D.CreatedOn)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(D => D.LastModifiedOn)
                   .HasComputedColumnSql("GETDATE()");
        }
    }
}
