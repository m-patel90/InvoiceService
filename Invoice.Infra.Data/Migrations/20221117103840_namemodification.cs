using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Infra.Data.Migrations
{
    public partial class namemodification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoieInfo_InvoiceInfoId",
                table: "InvoiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoieInfo",
                table: "InvoieInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails");

            migrationBuilder.RenameTable(
                name: "InvoieInfo",
                newName: "InvoiceInfo");

            migrationBuilder.RenameTable(
                name: "InvoiceDetails",
                newName: "InvoieDetails");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InvoiceInfoId",
                table: "InvoieDetails",
                newName: "IX_InvoieDetails_InvoiceInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceInfo",
                table: "InvoiceInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoieDetails",
                table: "InvoieDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoieDetails_InvoiceInfo_InvoiceInfoId",
                table: "InvoieDetails",
                column: "InvoiceInfoId",
                principalTable: "InvoiceInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoieDetails_InvoiceInfo_InvoiceInfoId",
                table: "InvoieDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoieDetails",
                table: "InvoieDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceInfo",
                table: "InvoiceInfo");

            migrationBuilder.RenameTable(
                name: "InvoieDetails",
                newName: "InvoiceDetails");

            migrationBuilder.RenameTable(
                name: "InvoiceInfo",
                newName: "InvoieInfo");

            migrationBuilder.RenameIndex(
                name: "IX_InvoieDetails_InvoiceInfoId",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InvoiceInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoieInfo",
                table: "InvoieInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoieInfo_InvoiceInfoId",
                table: "InvoiceDetails",
                column: "InvoiceInfoId",
                principalTable: "InvoieInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
