// using Microsoft.EntityFrameworkCore.Migrations;
//
// #nullable disable
//
// namespace TrackingCodeApi.Migrations
// {
//     /// <inheritdoc />
//     public partial class CodigoTag : Migration
//     {
//         /// <inheritdoc />
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropForeignKey(
//                 name: "FK_MOTO_AUDITORIA_MOVIMENTACAO_AuditoriaIdAudit",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropForeignKey(
//                 name: "FK_MOTO_setor_SetorIdSetor",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropIndex(
//                 name: "IX_MOTO_AuditoriaIdAudit",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropIndex(
//                 name: "IX_MOTO_SetorIdSetor",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropColumn(
//                 name: "AuditoriaIdAudit",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropColumn(
//                 name: "SetorIdSetor",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.RenameColumn(
//                 name: "placa",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "PLACA");
//
//             migrationBuilder.RenameColumn(
//                 name: "modelo",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "MODELO");
//
//             migrationBuilder.RenameColumn(
//                 name: "id_setor",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "ID_SETOR");
//
//             migrationBuilder.RenameColumn(
//                 name: "id_audit",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "ID_AUDIT");
//
//             migrationBuilder.RenameColumn(
//                 name: "data_cadastro",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "DATA_CADASTRO");
//
//             migrationBuilder.RenameColumn(
//                 name: "chassi",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "CHASSI");
//
//             migrationBuilder.AlterColumn<string>(
//                 name: "PLACA",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "CHAR(7)",
//                 nullable: true,
//                 oldClrType: typeof(string),
//                 oldType: "NVARCHAR2(2000)",
//                 oldNullable: true);
//
//             migrationBuilder.AlterColumn<string>(
//                 name: "MODELO",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "VARCHAR2(30)",
//                 nullable: false,
//                 oldClrType: typeof(string),
//                 oldType: "NVARCHAR2(2000)");
//
//             migrationBuilder.AlterColumn<string>(
//                 name: "CHASSI",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "CHAR(17)",
//                 nullable: false,
//                 oldClrType: typeof(string),
//                 oldType: "NVARCHAR2(450)");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_MOTO_ID_AUDIT",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "ID_AUDIT");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_MOTO_ID_SETOR",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "ID_SETOR");
//
//             migrationBuilder.AddForeignKey(
//                 name: "FK_MOTO_AUDITORIA_MOVIMENTACAO_ID_AUDIT",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "ID_AUDIT",
//                 principalSchema: "RM558785",
//                 principalTable: "AUDITORIA_MOVIMENTACAO",
//                 principalColumn: "id_audit");
//
//             migrationBuilder.AddForeignKey(
//                 name: "FK_MOTO_setor_ID_SETOR",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "ID_SETOR",
//                 principalSchema: "RM558785",
//                 principalTable: "setor",
//                 principalColumn: "id_setor",
//                 onDelete: ReferentialAction.Cascade);
//         }

        /// <inheritdoc />
//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropForeignKey(
//                 name: "FK_MOTO_AUDITORIA_MOVIMENTACAO_ID_AUDIT",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropForeignKey(
//                 name: "FK_MOTO_setor_ID_SETOR",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropIndex(
//                 name: "IX_MOTO_ID_AUDIT",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.DropIndex(
//                 name: "IX_MOTO_ID_SETOR",
//                 schema: "RM558785",
//                 table: "MOTO");
//
//             migrationBuilder.RenameColumn(
//                 name: "PLACA",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "placa");
//
//             migrationBuilder.RenameColumn(
//                 name: "MODELO",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "modelo");
//
//             migrationBuilder.RenameColumn(
//                 name: "ID_SETOR",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "id_setor");
//
//             migrationBuilder.RenameColumn(
//                 name: "ID_AUDIT",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "id_audit");
//
//             migrationBuilder.RenameColumn(
//                 name: "DATA_CADASTRO",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "data_cadastro");
//
//             migrationBuilder.RenameColumn(
//                 name: "CHASSI",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 newName: "chassi");
//
//             migrationBuilder.AlterColumn<string>(
//                 name: "placa",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "NVARCHAR2(2000)",
//                 nullable: true,
//                 oldClrType: typeof(string),
//                 oldType: "CHAR(7)",
//                 oldNullable: true);
//
//             migrationBuilder.AlterColumn<string>(
//                 name: "modelo",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "NVARCHAR2(2000)",
//                 nullable: false,
//                 oldClrType: typeof(string),
//                 oldType: "VARCHAR2(30)");
//
//             migrationBuilder.AlterColumn<string>(
//                 name: "chassi",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "NVARCHAR2(450)",
//                 nullable: false,
//                 oldClrType: typeof(string),
//                 oldType: "CHAR(17)");
//
//             migrationBuilder.AddColumn<int>(
//                 name: "AuditoriaIdAudit",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "NUMBER(10)",
//                 nullable: true);
//
//             migrationBuilder.AddColumn<int>(
//                 name: "SetorIdSetor",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 type: "NUMBER(10)",
//                 nullable: false,
//                 defaultValue: 0);
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_MOTO_AuditoriaIdAudit",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "AuditoriaIdAudit");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_MOTO_SetorIdSetor",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "SetorIdSetor");
//
//             migrationBuilder.AddForeignKey(
//                 name: "FK_MOTO_AUDITORIA_MOVIMENTACAO_AuditoriaIdAudit",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "AuditoriaIdAudit",
//                 principalSchema: "RM558785",
//                 principalTable: "AUDITORIA_MOVIMENTACAO",
//                 principalColumn: "id_audit");
//
//             migrationBuilder.AddForeignKey(
//                 name: "FK_MOTO_setor_SetorIdSetor",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "SetorIdSetor",
//                 principalSchema: "RM558785",
//                 principalTable: "setor",
//                 principalColumn: "id_setor",
//                 onDelete: ReferentialAction.Cascade);
//         }
//     }
// }
