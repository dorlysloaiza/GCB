using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GCB.Api.Migrations
{
    /// <inheritdoc />
    public partial class attach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GCBAttachments_Transactions_TransactionId1",
                table: "AdjuntoGCB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GCBAttachments",
                table: "AdjuntoGCB");

            migrationBuilder.RenameTable(
                name: "AdjuntoGCB",
                newName: "AdjuntoGCB");

            migrationBuilder.RenameIndex(
                name: "IX_GCBAttachments_TransactionId1",
                table: "AdjuntoGCB",
                newName: "IX_GCBAttachment_TransactionId1");

            migrationBuilder.AddColumn<string>(
                name: "TipoEntidad",
                table: "AdjuntoGCB",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GCBAttachment",
                table: "AdjuntoGCB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GCBAttachment_Transactions_TransactionId1",
                table: "AdjuntoGCB",
                column: "TransactionId1",
                principalTable: "Transacciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GCBAttachment_Transactions_TransactionId1",
                table: "AdjuntoGCB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GCBAttachment",
                table: "AdjuntoGCB");

            migrationBuilder.DropColumn(
                name: "TipoEntidad",
                table: "AdjuntoGCB");

            migrationBuilder.RenameTable(
                name: "AdjuntoGCB",
                newName: "AdjuntoGCB");

            migrationBuilder.RenameIndex(
                name: "IX_GCBAttachment_TransactionId1",
                table: "AdjuntoGCB",
                newName: "IX_GCBAttachments_TransactionId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GCBAttachments",
                table: "AdjuntoGCB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GCBAttachments_Transactions_TransactionId1",
                table: "AdjuntoGCB",
                column: "TransactionId1",
                principalTable: "Transacciones",
                principalColumn: "Id");
        }
    }
}
