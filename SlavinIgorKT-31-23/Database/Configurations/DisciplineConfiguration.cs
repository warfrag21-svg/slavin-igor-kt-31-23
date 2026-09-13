using SlavinIgorkt_31_23.Database.Helpers;
using SlavinIgorkt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SlavinIgorkt_31_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "disciplines";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.DisciplineId)
                   .HasName($"pk_{TableName}__discipline_id");

            builder.Property(p => p.DisciplineId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .HasColumnName("name")
                   .HasComment("Название дисциплины")
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(100);

            builder.Property(p => p.IsDeleted)
                   .HasColumnName("is_deleted")
                   .HasComment("Флаг удаления")
                   .HasColumnType(ColumnType.Bool);
        }
    }
}