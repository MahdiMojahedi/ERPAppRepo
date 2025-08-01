using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FK_StudentID = table.Column<int>(type: "int", nullable: false),
                    FK_CoachID = table.Column<int>(type: "int", nullable: false)
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GifFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FK_categoryID = table.Column<int>(type: "int", nullable: false)
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
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRestDay = table.Column<bool>(type: "bit", nullable: false),
                    FK_ProgramID = table.Column<int>(type: "int", nullable: false)
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
                    OrderInDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sets = table.Column<short>(type: "smallint", nullable: false),
                    Reps = table.Column<short>(type: "smallint", nullable: false),
                    DurationSeconds = table.Column<int>(type: "int", nullable: false),
                    FK_ProgramDaysID = table.Column<int>(type: "int", nullable: false),
                    FK_MovementID = table.Column<int>(type: "int", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
