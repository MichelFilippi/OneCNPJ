using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OneCNPJ.Data.Migrations
{
    /// <inheritdoc />
    public partial class AAhh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cnae",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnae", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "motivo_situacao_cadastral",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motivo_situacao_cadastral", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "municipio",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "natureza_juridica",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_natureza_juridica", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pais",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pais", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "qualificacao_socio",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qualificacao_socio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_empresa",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnpj_basico = table.Column<string>(type: "text", nullable: false),
                    razao_social = table.Column<string>(type: "text", nullable: false),
                    natureza_juridica_id = table.Column<long>(type: "bigint", nullable: false),
                    qualificacao_responsavel_id = table.Column<long>(type: "bigint", nullable: false),
                    capital_social = table.Column<string>(type: "text", nullable: false),
                    porte = table.Column<int>(type: "integer", nullable: false),
                    ente_federativo_responsavel = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_empresa", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_empresa_natureza_juridica_natureza_juridica_id",
                        column: x => x.natureza_juridica_id,
                        principalTable: "natureza_juridica",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_empresa_qualificacao_socio_qualificacao_responsavel_id",
                        column: x => x.qualificacao_responsavel_id,
                        principalTable: "qualificacao_socio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_estabelecimento",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnpj_basico = table.Column<long>(type: "bigint", nullable: false),
                    cnpj_ordem = table.Column<string>(type: "text", nullable: false),
                    cnpj_dv = table.Column<string>(type: "text", nullable: false),
                    identificador_matriz_filial = table.Column<string>(type: "text", nullable: false),
                    nome_fantasia = table.Column<string>(type: "text", nullable: true),
                    situacao_cadastral = table.Column<string>(type: "text", nullable: false),
                    data_situacao_cadastral = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    motivo_situacao_cadastral_id = table.Column<long>(type: "bigint", nullable: false),
                    nome_cidade_exterior = table.Column<string>(type: "text", nullable: true),
                    pais_id = table.Column<long>(type: "bigint", nullable: true),
                    data_inicio_atividade = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cnae_principal_id = table.Column<long>(type: "bigint", nullable: false),
                    tipo_logradouro = table.Column<string>(type: "text", nullable: true),
                    logradouro = table.Column<string>(type: "text", nullable: true),
                    numero = table.Column<string>(type: "text", nullable: true),
                    complemento = table.Column<string>(type: "text", nullable: true),
                    bairro = table.Column<string>(type: "text", nullable: true),
                    cep = table.Column<string>(type: "text", nullable: true),
                    uf = table.Column<string>(type: "text", nullable: true),
                    municipio_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_estabelecimento", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_estabelecimento_cnae_cnae_principal_id",
                        column: x => x.cnae_principal_id,
                        principalTable: "cnae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_estabelecimento_cnpj_empresa_cnpj_basico",
                        column: x => x.cnpj_basico,
                        principalTable: "cnpj_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_estabelecimento_motivo_situacao_cadastral_motivo_situa~",
                        column: x => x.motivo_situacao_cadastral_id,
                        principalTable: "motivo_situacao_cadastral",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_estabelecimento_municipio_municipio_id",
                        column: x => x.municipio_id,
                        principalTable: "municipio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_estabelecimento_pais_pais_id",
                        column: x => x.pais_id,
                        principalTable: "pais",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_simples",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnpj_basico = table.Column<long>(type: "bigint", nullable: false),
                    optante_simples = table.Column<bool>(type: "boolean", nullable: false),
                    data_opcao_simples = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data_exclusao_simples = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    optante_mei = table.Column<bool>(type: "boolean", nullable: false),
                    data_opcao_mei = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data_exclusao_mei = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_simples", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_simples_cnpj_empresa_cnpj_basico",
                        column: x => x.cnpj_basico,
                        principalTable: "cnpj_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_socio",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnpj_basico = table.Column<long>(type: "bigint", nullable: false),
                    tipo_socio = table.Column<string>(type: "text", nullable: false),
                    nome_socio = table.Column<string>(type: "text", nullable: false),
                    documento_socio = table.Column<string>(type: "text", nullable: false),
                    qualificacao_socio_id = table.Column<long>(type: "bigint", nullable: false),
                    data_entrada_sociedade = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_socio", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_socio_cnpj_empresa_cnpj_basico",
                        column: x => x.cnpj_basico,
                        principalTable: "cnpj_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_socio_qualificacao_socio_qualificacao_socio_id",
                        column: x => x.qualificacao_socio_id,
                        principalTable: "qualificacao_socio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_estab_cnae_sec",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estabelecimento_id = table.Column<long>(type: "bigint", nullable: false),
                    cnae_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_estab_cnae_sec", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_estab_cnae_sec_cnae_cnae_id",
                        column: x => x.cnae_id,
                        principalTable: "cnae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_estab_cnae_sec_cnpj_estabelecimento_estabelecimento_id",
                        column: x => x.estabelecimento_id,
                        principalTable: "cnpj_estabelecimento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_empresa_natureza_juridica_id",
                table: "cnpj_empresa",
                column: "natureza_juridica_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_empresa_qualificacao_responsavel_id",
                table: "cnpj_empresa",
                column: "qualificacao_responsavel_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estab_cnae_sec_cnae_id",
                table: "cnpj_estab_cnae_sec",
                column: "cnae_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estab_cnae_sec_estabelecimento_id",
                table: "cnpj_estab_cnae_sec",
                column: "estabelecimento_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_cnae_principal_id",
                table: "cnpj_estabelecimento",
                column: "cnae_principal_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_cnpj_basico",
                table: "cnpj_estabelecimento",
                column: "cnpj_basico");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_motivo_situacao_cadastral_id",
                table: "cnpj_estabelecimento",
                column: "motivo_situacao_cadastral_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_municipio_id",
                table: "cnpj_estabelecimento",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_pais_id",
                table: "cnpj_estabelecimento",
                column: "pais_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_simples_cnpj_basico",
                table: "cnpj_simples",
                column: "cnpj_basico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_socio_cnpj_basico",
                table: "cnpj_socio",
                column: "cnpj_basico");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_socio_qualificacao_socio_id",
                table: "cnpj_socio",
                column: "qualificacao_socio_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cnpj_estab_cnae_sec");

            migrationBuilder.DropTable(
                name: "cnpj_simples");

            migrationBuilder.DropTable(
                name: "cnpj_socio");

            migrationBuilder.DropTable(
                name: "cnpj_estabelecimento");

            migrationBuilder.DropTable(
                name: "cnae");

            migrationBuilder.DropTable(
                name: "cnpj_empresa");

            migrationBuilder.DropTable(
                name: "motivo_situacao_cadastral");

            migrationBuilder.DropTable(
                name: "municipio");

            migrationBuilder.DropTable(
                name: "pais");

            migrationBuilder.DropTable(
                name: "natureza_juridica");

            migrationBuilder.DropTable(
                name: "qualificacao_socio");
        }
    }
}
