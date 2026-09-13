using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Message : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_properties_propertyId",
                table: "messages");

            migrationBuilder.RenameColumn(
                name: "propertyId",
                table: "messages",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_propertyId",
                table: "messages",
                newName: "IX_messages_PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_properties_PropertyId",
                table: "messages",
                column: "PropertyId",
                principalTable: "properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_properties_PropertyId",
                table: "messages");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "messages",
                newName: "propertyId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_PropertyId",
                table: "messages",
                newName: "IX_messages_propertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_properties_propertyId",
                table: "messages",
                column: "propertyId",
                principalTable: "properties",
                principalColumn: "Id");
        }
    }
}
