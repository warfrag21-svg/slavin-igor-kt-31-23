using SlavinIgorkt_31_23.Database.Helpers;
using SlavinIgorkt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SlavinIgorkt_31_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "groups";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.GroupId)
                   .HasName($"pk_{TableName}__group_id");

            builder.Property(p => p.GroupId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .HasColumnName("name")
                   .HasComment("Название группы")
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(50);

            builder.Property(p => p.Course)
                   .HasColumnName("course")
                   .HasComment("Курс обучения")
                   .HasColumnType(ColumnType.Int);

            builder.Property(p => p.SpecialtyId)
                   .HasColumnName("specialty_id")
                   .HasComment("Идентификатор специальности")
                   .HasColumnType(ColumnType.Int);

            builder.Property(p => p.IsDeleted)
                   .HasColumnName("is_deleted")
                   .HasComment("Флаг удаления")
                   .HasColumnType(ColumnType.Bool);

            // Описание связи: Specialty (1) -> Group (∞)
            builder.HasOne(p => p.Specialty)
                   .WithMany()
                   .HasForeignKey(p => p.SpecialtyId)
                   .HasConstraintName($"fk_{TableName}_specialty_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.SpecialtyId, $"idx_{TableName}_fk_specialty_id");

            builder.Navigation(p => p.Specialty).AutoInclude();
        }
    }
}