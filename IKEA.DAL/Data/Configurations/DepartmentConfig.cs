using IKEA.DAL.Models.DepartmentModel;

namespace IKEA.DAL.Data.Configurations
{
    public class DepartmentConfig : BaseEntityConfig<Department> ,  IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id)
                   .UseIdentityColumn(10, 10);

            builder.Property(D => D.Name)
                   .HasColumnType("varchar(20)");

            builder.Property(D => D.Code)
                   .HasColumnType("varchar(20)");

            base.Configure(builder);
        }
    }
}
