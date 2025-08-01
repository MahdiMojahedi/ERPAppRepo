using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProgramDayMovement");

            migrationBuilder.DropTable(
                name: "tblMovement");

            migrationBuilder.DropTable(
                name: "tblProgramDays");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblProgram");

            migrationBuilder.DropColumn(
                name: "EducationDegree",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "AspNetUsers",
                newName: "NationalID");

            migrationBuilder.CreateTable(
                name: "tblMaster",
                columns: table => new
                {
                    MasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByPersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMaster", x => x.MasterId);
                    table.ForeignKey(
                        name: "FK_tblMaster_AspNetUsers_CreatedByPersonId",
                        column: x => x.CreatedByPersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSubsidiary",
                columns: table => new
                {
                    SubsidiaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsLastLevel = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    CreatedByPersonId = table.Column<int>(type: "int", nullable: false),
                    ParentSubsidiaryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubsidiary", x => x.SubsidiaryId);
                    table.ForeignKey(
                        name: "FK_tblSubsidiary_AspNetUsers_CreatedByPersonId",
                        column: x => x.CreatedByPersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSubsidiary_tblMaster_MasterId",
                        column: x => x.MasterId,
                        principalTable: "tblMaster",
                        principalColumn: "MasterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSubsidiary_tblSubsidiary_ParentSubsidiaryId",
                        column: x => x.ParentSubsidiaryId,
                        principalTable: "tblSubsidiary",
                        principalColumn: "SubsidiaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblMaster_Code",
                table: "tblMaster",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblMaster_CreatedByPersonId",
                table: "tblMaster",
                column: "CreatedByPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubsidiary_Code",
                table: "tblSubsidiary",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSubsidiary_CreatedByPersonId",
                table: "tblSubsidiary",
                column: "CreatedByPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubsidiary_MasterId",
                table: "tblSubsidiary",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubsidiary_ParentSubsidiaryId",
                table: "tblSubsidiary",
                column: "ParentSubsidiaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSubsidiary");

            migrationBuilder.DropTable(
                name: "tblMaster");

            migrationBuilder.RenameColumn(
                name: "NationalID",
                table: "AspNetUsers",
                newName: "UserType");

            migrationBuilder.AddColumn<int>(
                name: "EducationDegree",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tblProgram",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_CoachID = table.Column<int>(type: "int", nullable: false),
                    FK_StudentID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProgram", x => x.ProgramID);
                    table.ForeignKey(
                        name: "FK_tblProgram_AspNetUsers_FK_CoachID",
                        column: x => x.FK_CoachID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblProgram_AspNetUsers_FK_StudentID",
                        column: x => x.FK_StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblMovement",
                columns: table => new
                {
                    MovementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_categoryID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GifFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMovement", x => x.MovementID);
                    table.ForeignKey(
                        name: "FK_tblMovement_tblCategory_FK_categoryID",
                        column: x => x.FK_categoryID,
                        principalTable: "tblCategory",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "tblProgramDays",
                columns: table => new
                {
                    ProgramDaysID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_ProgramID = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRestDay = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProgramDays", x => x.ProgramDaysID);
                    table.ForeignKey(
                        name: "FK_tblProgramDays_tblProgram_FK_ProgramID",
                        column: x => x.FK_ProgramID,
                        principalTable: "tblProgram",
                        principalColumn: "ProgramID");
                });

            migrationBuilder.CreateTable(
                name: "tblProgramDayMovement",
                columns: table => new
                {
                    ProgramDayMovementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_MovementID = table.Column<int>(type: "int", nullable: false),
                    FK_ProgramDaysID = table.Column<int>(type: "int", nullable: false),
                    DurationSeconds = table.Column<int>(type: "int", nullable: false),
                    OrderInDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reps = table.Column<short>(type: "smallint", nullable: false),
                    Sets = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProgramDayMovement", x => x.ProgramDayMovementID);
                    table.ForeignKey(
                        name: "FK_tblProgramDayMovement_tblMovement_FK_MovementID",
                        column: x => x.FK_MovementID,
                        principalTable: "tblMovement",
                        principalColumn: "MovementID");
                    table.ForeignKey(
                        name: "FK_tblProgramDayMovement_tblProgramDays_FK_ProgramDaysID",
                        column: x => x.FK_ProgramDaysID,
                        principalTable: "tblProgramDays",
                        principalColumn: "ProgramDaysID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblMovement_FK_categoryID",
                table: "tblMovement",
                column: "FK_categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProgram_FK_CoachID",
                table: "tblProgram",
                column: "FK_CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProgram_FK_StudentID",
                table: "tblProgram",
                column: "FK_StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProgramDayMovement_FK_MovementID",
                table: "tblProgramDayMovement",
                column: "FK_MovementID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProgramDayMovement_FK_ProgramDaysID",
                table: "tblProgramDayMovement",
                column: "FK_ProgramDaysID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProgramDays_FK_ProgramID",
                table: "tblProgramDays",
                column: "FK_ProgramID");
        }
    }
}
