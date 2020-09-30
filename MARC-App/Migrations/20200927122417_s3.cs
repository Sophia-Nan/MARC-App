using Microsoft.EntityFrameworkCore.Migrations;

namespace MARC_App.Migrations
{
    public partial class s3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersBooking");

            migrationBuilder.DropColumn(
                name: "Additional",
                table: "BookInstruments");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "BookInstruments");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalReq",
                table: "BookInstruments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Approval",
                table: "BookInstruments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InstrumentConditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<bool>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentConditions_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentConditions_InstrumentId",
                table: "InstrumentConditions",
                column: "InstrumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentConditions");

            migrationBuilder.DropColumn(
                name: "AdditionalReq",
                table: "BookInstruments");

            migrationBuilder.DropColumn(
                name: "Approval",
                table: "BookInstruments");

            migrationBuilder.AddColumn<string>(
                name: "Additional",
                table: "BookInstruments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "BookInstruments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UsersBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookInstrumentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersBooking_BookInstruments_BookInstrumentId",
                        column: x => x.BookInstrumentId,
                        principalTable: "BookInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersBooking_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersBooking_BookInstrumentId",
                table: "UsersBooking",
                column: "BookInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersBooking_UserId",
                table: "UsersBooking",
                column: "UserId");
        }
    }
}
