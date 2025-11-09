// ﻿using System;
// using Microsoft.EntityFrameworkCore.Migrations;

// #nullable disable

// namespace TrackingCodeApi.Migrations
// {
//     /// <inheritdoc />
//     public partial class InitialCreate : Migration
//     {
//         /// <inheritdoc />
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.EnsureSchema(
//                 name: "RM558785");

//             migrationBuilder.CreateTable(
//                 name: "AUDITORIA_MOVIMENTACAO",
//                 schema: "RM558785",
//                 columns: table => new
//                 {
//                     ID_AUDIT = table.Column<int>(type: "NUMBER(10)", nullable: false)
//                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                     id_funcionario = table.Column<int>(type: "NUMBER(10)", nullable: false),
//                     tipo_operacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
//                     data_operacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
//                     valores_novos = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
//                     valores_anteriores = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AUDITORIA_MOVIMENTACAO", x => x.ID_AUDIT);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "SETOR",
//                 schema: "RM558785",
//                 columns: table => new
//                 {
//                     ID_SETOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
//                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                     NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
//                     DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
//                     COORDENADAS_LIMITE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_SETOR", x => x.ID_SETOR);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "MOTO",
//                 schema: "RM558785",
//                 columns: table => new
//                 {
//                     CHASSI = table.Column<string>(type: "CHAR(17)", nullable: false),
//                     PLACA = table.Column<string>(type: "CHAR(7)", nullable: true),
//                     MODELO = table.Column<string>(type: "VARCHAR2(30)", nullable: false),
//                     DATA_CADASTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
//                     ID_SETOR = table.Column<int>(type: "NUMBER(10)", nullable: false),
//                     ID_AUDIT = table.Column<int>(type: "NUMBER(10)", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_MOTO", x => x.CHASSI);
//                     table.ForeignKey(
//                         name: "FK_MOTO_AUDITORIA_MOVIMENTACAO_ID_AUDIT",
//                         column: x => x.ID_AUDIT,
//                         principalSchema: "RM558785",
//                         principalTable: "AUDITORIA_MOVIMENTACAO",
//                         principalColumn: "ID_AUDIT");
//                     table.ForeignKey(
//                         name: "FK_MOTO_SETOR_ID_SETOR",
//                         column: x => x.ID_SETOR,
//                         principalSchema: "RM558785",
//                         principalTable: "SETOR",
//                         principalColumn: "ID_SETOR",
//                         onDelete: ReferentialAction.Cascade);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "TAG",
//                 schema: "RM558785",
//                 columns: table => new
//                 {
//                     CODIGO_TAG = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
//                     BATERIA = table.Column<int>(type: "NUMBER(10)", nullable: false),
//                     STATUS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
//                     DATA_VINCULO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
//                     CHASSI = table.Column<string>(type: "CHAR(17)", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_TAG", x => x.CODIGO_TAG);
//                     table.ForeignKey(
//                         name: "FK_TAG_MOTO_CHASSI",
//                         column: x => x.CHASSI,
//                         principalSchema: "RM558785",
//                         principalTable: "MOTO",
//                         principalColumn: "CHASSI",
//                         onDelete: ReferentialAction.SetNull);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "localizacao",
//                 schema: "RM558785",
//                 columns: table => new
//                 {
//                     id_localizacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
//                         .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
//                     x = table.Column<decimal>(type: "DECIMAL(18,6)", precision: 18, scale: 6, nullable: false),
//                     y = table.Column<decimal>(type: "DECIMAL(18,6)", precision: 18, scale: 6, nullable: false),
//                     codigo_tag = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
//                     id_setor = table.Column<int>(type: "NUMBER(10)", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_localizacao", x => x.id_localizacao);
//                     table.ForeignKey(
//                         name: "FK_localizacao_SETOR_id_setor",
//                         column: x => x.id_setor,
//                         principalSchema: "RM558785",
//                         principalTable: "SETOR",
//                         principalColumn: "ID_SETOR",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_localizacao_TAG_codigo_tag",
//                         column: x => x.codigo_tag,
//                         principalSchema: "RM558785",
//                         principalTable: "TAG",
//                         principalColumn: "CODIGO_TAG",
//                         onDelete: ReferentialAction.Cascade);
//                 });

//             migrationBuilder.CreateIndex(
//                 name: "IX_localizacao_codigo_tag",
//                 schema: "RM558785",
//                 table: "localizacao",
//                 column: "codigo_tag");

//             migrationBuilder.CreateIndex(
//                 name: "IX_localizacao_id_setor",
//                 schema: "RM558785",
//                 table: "localizacao",
//                 column: "id_setor");

//             migrationBuilder.CreateIndex(
//                 name: "IX_MOTO_ID_AUDIT",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "ID_AUDIT");

//             migrationBuilder.CreateIndex(
//                 name: "IX_MOTO_ID_SETOR",
//                 schema: "RM558785",
//                 table: "MOTO",
//                 column: "ID_SETOR");

//             migrationBuilder.CreateIndex(
//                 name: "IX_TAG_CHASSI",
//                 schema: "RM558785",
//                 table: "TAG",
//                 column: "CHASSI",
//                 unique: true,
//                 filter: "\"CHASSI\" IS NOT NULL");
//         }

//         /// <inheritdoc />
//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropTable(
//                 name: "localizacao",
//                 schema: "RM558785");

//             migrationBuilder.DropTable(
//                 name: "TAG",
//                 schema: "RM558785");

//             migrationBuilder.DropTable(
//                 name: "MOTO",
//                 schema: "RM558785");

//             migrationBuilder.DropTable(
//                 name: "AUDITORIA_MOVIMENTACAO",
//                 schema: "RM558785");

//             migrationBuilder.DropTable(
//                 name: "SETOR",
//                 schema: "RM558785");
//         }
//     }
// }
