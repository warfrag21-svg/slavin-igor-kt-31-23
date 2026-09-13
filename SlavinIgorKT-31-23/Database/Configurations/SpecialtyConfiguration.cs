using SlavinIgorkt_31_23.Database.Helpers;
using SlavinIgorkt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SlavinIgorkt_31_23.Database.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        private const string TableName = "specialtys";

        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.SpecialtyId)
                   .HasName($"pk_{TableName}__specialty_id");

            builder.Property(p => p.SpecialtyId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                   .HasColumnName("title")
                   .HasComment("Название специальности")
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(100);

            builder.Property(p => p.Code)
                   .HasColumnName("code")
                   .HasComment("Код специальности")
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(20);
        }
    }
}