using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCodeForML.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserNameandId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserPredictionDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserPredictionDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserPredictionDetail");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserPredictionDetail");
        }
    }
}
