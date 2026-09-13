using SlavinIgorkt_31_23.Database.Helpers;
using SlavinIgorkt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SlavinIgorkt_31_23.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const string TableName = "students";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.StudentId)
                   .HasName($"pk_{TableName}__student_id");

            builder.Property(p => p.StudentId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                   .HasColumnName("first_name")
                   .HasComment("Имя студента")
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(100);

            builder.Property(p => p.LastName)
                   .HasColumnName("last_name")
                   .HasComment("Фамилия студента")
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(100);

            builder.Property(p => p.GroupId)
                   .HasColumnName("group_id")
                   .HasComment("Идентификатор группы")
                   .HasColumnType(ColumnType.Int);

            builder.Property(p => p.IsDeleted)
                   .HasColumnName("is_deleted")
                   .HasComment("Флаг удаления")
                   .HasColumnType(ColumnType.Bool);

            // Описание связи: Group (1) -> Student (∞)
            builder.HasOne(p => p.Group)
                   .WithMany()
                   .HasForeignKey(p => p.GroupId)
                   .HasConstraintName($"fk_{TableName}_group_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.GroupId, $"idx_{TableName}_fk_group_id");

            builder.Navigation(p => p.Group).AutoInclude();
        }
    }
}