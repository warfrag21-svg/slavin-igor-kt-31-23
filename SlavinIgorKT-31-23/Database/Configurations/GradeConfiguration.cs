using SlavinIgorkt_31_23.Database.Helpers;
using SlavinIgorkt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SlavinIgorkt_31_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "grades";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.GradeId)
                   .HasName($"pk_{TableName}__grade_id");

            builder.Property(p => p.GradeId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Value)
                   .HasColumnName("value")
                   .HasComment("Оценка")
                   .HasColumnType(ColumnType.Int);

            builder.Property(p => p.StudentId)
                   .HasColumnName("student_id")
                   .HasComment("Идентификатор студента")
                   .HasColumnType(ColumnType.Int);

            builder.Property(p => p.DisciplineId)
                   .HasColumnName("discipline_id")
                   .HasComment("Идентификатор дисциплины")
                   .HasColumnType(ColumnType.Int);

            // Описание связи: Student (1) -> Grade (∞)
            builder.HasOne(p => p.Student)
                   .WithMany()
                   .HasForeignKey(p => p.StudentId)
                   .HasConstraintName($"fk_{TableName}_student_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.StudentId, $"idx_{TableName}_fk_student_id");

            builder.Navigation(p => p.Student).AutoInclude();

            // Описание связи: Discipline (1) -> Grade (∞)
            builder.HasOne(p => p.Discipline)
                   .WithMany()
                   .HasForeignKey(p => p.DisciplineId)
                   .HasConstraintName($"fk_{TableName}_discipline_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_discipline_id");

            builder.Navigation(p => p.Discipline).AutoInclude();
        }
    }
}