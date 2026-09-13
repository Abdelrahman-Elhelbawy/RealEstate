using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Agent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_properties_propertyId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_properties_Agent_agentId",
                table: "properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyImage_properties_propertyId",
                table: "PropertyImage");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReport_properties_propertyId",
                table: "PropertyReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyReport",
                table: "PropertyReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyImage",
                table: "PropertyImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "PropertyReport",
                newName: "propertyReports");

            migrationBuilder.RenameTable(
                name: "PropertyImage",
                newName: "propertyImages");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "messages");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "agents");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyReport_propertyId",
                table: "propertyReports",
                newName: "IX_propertyReports_propertyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyImage_propertyId",
                table: "propertyImages",
                newName: "IX_propertyImages_propertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_propertyId",
                table: "messages",
                newName: "IX_messages_propertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_propertyReports",
                table: "propertyReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_propertyImages",
                table: "propertyImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_agents",
                table: "agents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_properties_propertyId",
                table: "messages",
                column: "propertyId",
                principalTable: "properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_properties_agents_agentId",
                table: "properties",
                column: "agentId",
                principalTable: "agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_propertyImages_properties_propertyId",
                table: "propertyImages",
                column: "propertyId",
                principalTable: "properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_propertyReports_properties_propertyId",
                table: "propertyReports",
                column: "propertyId",
                principalTable: "properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_properties_propertyId",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_properties_agents_agentId",
                table: "properties");

            migrationBuilder.DropForeignKey(
                name: "FK_propertyImages_properties_propertyId",
                table: "propertyImages");

            migrationBuilder.DropForeignKey(
                name: "FK_propertyReports_properties_propertyId",
                table: "propertyReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_propertyReports",
                table: "propertyReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_propertyImages",
                table: "propertyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_agents",
                table: "agents");

            migrationBuilder.RenameTable(
                name: "propertyReports",
                newName: "PropertyReport");

            migrationBuilder.RenameTable(
                name: "propertyImages",
                newName: "PropertyImage");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "agents",
                newName: "Agent");

            migrationBuilder.RenameIndex(
                name: "IX_propertyReports_propertyId",
                table: "PropertyReport",
                newName: "IX_PropertyReport_propertyId");

            migrationBuilder.RenameIndex(
                name: "IX_propertyImages_propertyId",
                table: "PropertyImage",
                newName: "IX_PropertyImage_propertyId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_propertyId",
                table: "Message",
                newName: "IX_Message_propertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyReport",
                table: "PropertyReport",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyImage",
                table: "PropertyImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_properties_propertyId",
                table: "Message",
                column: "propertyId",
                principalTable: "properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_properties_Agent_agentId",
                table: "properties",
                column: "agentId",
                principalTable: "Agent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyImage_properties_propertyId",
                table: "PropertyImage",
                column: "propertyId",
                principalTable: "properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReport_properties_propertyId",
                table: "PropertyReport",
                column: "propertyId",
                principalTable: "properties",
                principalColumn: "Id");
        }
    }
}
