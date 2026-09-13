using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SlavinIgorKT_31_23.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "disciplines",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название дисциплины"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Флаг удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disciplines__discipline_id", x => x.DisciplineId);
                });

            migrationBuilder.CreateTable(
                name: "specialtys",
                columns: table => new
                {
                    SpecialtyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название специальности"),
                    code = table.Column<string>(type: "varchar", maxLength: 20, nullable: false, comment: "Код специальности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specialtys__specialty_id", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false, comment: "Название группы"),
                    course = table.Column<int>(type: "integer", nullable: false, comment: "Курс обучения"),
                    specialty_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор специальности"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Флаг удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_groups__group_id", x => x.GroupId);
                    table.ForeignKey(
                        name: "fk_groups_specialty_id",
                        column: x => x.specialty_id,
                        principalTable: "specialtys",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя студента"),
                    last_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    group_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор группы"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Флаг удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students__student_id", x => x.StudentId);
                    table.ForeignKey(
                        name: "fk_students_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<int>(type: "integer", nullable: false, comment: "Оценка"),
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор студента"),
                    discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grades__grade_id", x => x.GradeId);
                    table.ForeignKey(
                        name: "fk_grades_discipline_id",
                        column: x => x.discipline_id,
                        principalTable: "disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_grades_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_grades_fk_discipline_id",
                table: "grades",
                column: "discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_grades_fk_student_id",
                table: "grades",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "idx_groups_fk_specialty_id",
                table: "groups",
                column: "specialty_id");

            migrationBuilder.CreateIndex(
                name: "idx_students_fk_group_id",
                table: "students",
                column: "group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "disciplines");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "specialtys");
        }
    }
}
