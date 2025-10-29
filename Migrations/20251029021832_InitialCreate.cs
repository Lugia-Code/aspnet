using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingCodeApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RM558785");

            /* migrationBuilder.CreateTable(
                 name: "setor",
                 schema: "RM558785",
                 columns: table => new
                 {
                     id_setor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                     nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                     descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                     coordenadas_limite = table.Column<double>(type: "BINARY_DOUBLE", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_setor", x => x.id_setor);
                 });

             migrationBuilder.CreateTable(
                 name: "tag",
                 schema: "RM558785",
                 columns: table => new
                 {
                     codigo_tag = table.Column<int>(type: "NUMBER(10)", nullable: false)
                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                     status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                     data_vinculo = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                     chassi = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_tag", x => x.codigo_tag);
                 });

             migrationBuilder.CreateTable(
                 name: "USUARIO",
                 schema: "RM558785",
                 columns: table => new
                 {
                     IdFuncionario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                     Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                     Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                     Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                     Funcao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_USUARIO", x => x.IdFuncionario);
                 });

             migrationBuilder.CreateTable(
                 name: "localizacao",
                 schema: "RM558785",
                 columns: table => new
                 {
                     id_localizacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                     x = table.Column<decimal>(type: "DECIMAL(18,6)", precision: 18, scale: 6, nullable: false),
                     y = table.Column<decimal>(type: "DECIMAL(18,6)", precision: 18, scale: 6, nullable: false),
                     codigo_tag = table.Column<int>(type: "NUMBER(10)", nullable: false),
                     id_setor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_localizacao", x => x.id_localizacao);
                     table.ForeignKey(
                         name: "FK_localizacao_setor_id_setor",
                         column: x => x.id_setor,
                         principalSchema: "RM558785",
                         principalTable: "setor",
                         principalColumn: "id_setor",
                         onDelete: ReferentialAction.Cascade);
                     table.ForeignKey(
                         name: "FK_localizacao_tag_codigo_tag",
                         column: x => x.codigo_tag,
                         principalSchema: "RM558785",
                         principalTable: "tag",
                         principalColumn: "codigo_tag",
                         onDelete: ReferentialAction.Cascade);
                 });

             migrationBuilder.CreateTable(
                 name: "AUDITORIA_MOVIMENTACAO",
                 schema: "RM558785",
                 columns: table => new
                 {
                     id_audit = table.Column<int>(type: "NUMBER(10)", nullable: false)
                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                     id_funcionario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                     tipo_operacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                     data_operacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                     valores_novos = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                     valores_anteriores = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_AUDITORIA_MOVIMENTACAO", x => x.id_audit);
                     table.ForeignKey(
                         name: "FK_AUDITORIA_MOVIMENTACAO_USUARIO_id_funcionario",
                         column: x => x.id_funcionario,
                         principalSchema: "RM558785",
                         principalTable: "USUARIO",
                         principalColumn: "IdFuncionario",
                         onDelete: ReferentialAction.Cascade);
                 });

             migrationBuilder.CreateTable(
                 name: "MOTO",
                 schema: "RM558785",
                 columns: table => new
                 {
                     chassi = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                     placa = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                     modelo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                     data_cadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                     codigo_tag = table.Column<int>(type: "NUMBER(10)", nullable: false),
                     id_setor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                     id_audit = table.Column<int>(type: "NUMBER(10)", nullable: true),
                     SetorIdSetor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                     AuditoriaIdAudit = table.Column<int>(type: "NUMBER(10)", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_MOTO", x => x.chassi);
                     table.ForeignKey(
                         name: "FK_MOTO_AUDITORIA_MOVIMENTACAO_AuditoriaIdAudit",
                         column: x => x.AuditoriaIdAudit,
                         principalSchema: "RM558785",
                         principalTable: "AUDITORIA_MOVIMENTACAO",
                         principalColumn: "id_audit");
                     table.ForeignKey(
                         name: "FK_MOTO_setor_SetorIdSetor",
                         column: x => x.SetorIdSetor,
                         principalSchema: "RM558785",
                         principalTable: "setor",
                         principalColumn: "id_setor",
                         onDelete: ReferentialAction.Cascade);
                     table.ForeignKey(
                         name: "FK_MOTO_tag_codigo_tag",
                         column: x => x.codigo_tag,
                         principalSchema: "RM558785",
                         principalTable: "tag",
                         principalColumn: "codigo_tag",
                         onDelete: ReferentialAction.Cascade);
                 });

             migrationBuilder.CreateIndex(
                 name: "IX_AUDITORIA_MOVIMENTACAO_id_funcionario",
                 schema: "RM558785",
                 table: "AUDITORIA_MOVIMENTACAO",
                 column: "id_funcionario");

             migrationBuilder.CreateIndex(
                 name: "IX_localizacao_codigo_tag",
                 schema: "RM558785",
                 table: "localizacao",
                 column: "codigo_tag");

             migrationBuilder.CreateIndex(
                 name: "IX_localizacao_id_setor",
                 schema: "RM558785",
                 table: "localizacao",
                 column: "id_setor");

             migrationBuilder.CreateIndex(
                 name: "IX_MOTO_AuditoriaIdAudit",
                 schema: "RM558785",
                 table: "MOTO",
                 column: "AuditoriaIdAudit");

             migrationBuilder.CreateIndex(
                 name: "IX_MOTO_codigo_tag",
                 schema: "RM558785",
                 table: "MOTO",
                 column: "codigo_tag",
                 unique: true);

             migrationBuilder.CreateIndex(
                 name: "IX_MOTO_SetorIdSetor",
                 schema: "RM558785",
                 table: "MOTO",
                 column: "SetorIdSetor");
         }

         /// <inheritdoc />
         protected override void Down(MigrationBuilder migrationBuilder)
         {
             migrationBuilder.DropTable(
                 name: "localizacao",
                 schema: "RM558785");

             migrationBuilder.DropTable(
                 name: "MOTO",
                 schema: "RM558785");

             migrationBuilder.DropTable(
                 name: "AUDITORIA_MOVIMENTACAO",
                 schema: "RM558785");

             migrationBuilder.DropTable(
                 name: "setor",
                 schema: "RM558785");

             migrationBuilder.DropTable(
                 name: "tag",
                 schema: "RM558785");

             migrationBuilder.DropTable(
                 name: "USUARIO",
                 schema: "RM558785");
         
      */
        }
    }
}
