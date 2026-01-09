using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OneCNPJ.Data.Migrations
{
    /// <inheritdoc />
    public partial class Teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cnae",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
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
                name: "cnpj_importacao",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data_referencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    versao_origem = table.Column<string>(type: "text", nullable: false),
                    url_origem = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    inicio_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fim_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    mensagem = table.Column<string>(type: "text", nullable: true),
                    CnpjEmpresaId = table.Column<long>(type: "bigint", nullable: false),
                    status_empresas = table.Column<int>(type: "integer", nullable: false),
                    linhas_empresas_total = table.Column<long>(type: "bigint", nullable: false),
                    linhas_empresas_importadas = table.Column<long>(type: "bigint", nullable: false),
                    linhas_empresas_falhas = table.Column<long>(type: "bigint", nullable: false),
                    status_estabelecimentos = table.Column<int>(type: "integer", nullable: false),
                    linhas_estabelecimentos_total = table.Column<long>(type: "bigint", nullable: false),
                    linhas_estabelecimentos_importadas = table.Column<long>(type: "bigint", nullable: false),
                    linhas_estabelecimentos_falhas = table.Column<long>(type: "bigint", nullable: false),
                    status_socios = table.Column<int>(type: "integer", nullable: false),
                    linhas_socios_total = table.Column<long>(type: "bigint", nullable: false),
                    linhas_socios_importadas = table.Column<long>(type: "bigint", nullable: false),
                    linhas_socios_falhas = table.Column<long>(type: "bigint", nullable: false),
                    status_simples = table.Column<int>(type: "integer", nullable: false),
                    linhas_simples_total = table.Column<long>(type: "bigint", nullable: false),
                    linhas_simples_importadas = table.Column<long>(type: "bigint", nullable: false),
                    linhas_simples_falhas = table.Column<long>(type: "bigint", nullable: false),
                    status_satelites = table.Column<int>(type: "integer", nullable: false),
                    linhas_satelites_importadas = table.Column<long>(type: "bigint", nullable: false),
                    linhas_satelites_falhas = table.Column<long>(type: "bigint", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_importacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "layout",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    linha_cabecalho = table.Column<int>(type: "integer", nullable: false),
                    linha_dados = table.Column<int>(type: "integer", nullable: false),
                    formato_cnpj = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layout", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "motivo_situacao_cadastral",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
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
                    id = table.Column<long>(type: "bigint", nullable: false),
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
                    id = table.Column<long>(type: "bigint", nullable: false),
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
                    id = table.Column<long>(type: "bigint", nullable: false),
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
                    id = table.Column<long>(type: "bigint", nullable: false),
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
                name: "cnpj_conteudo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    importacao_id = table.Column<long>(type: "bigint", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    payload_json = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_conteudo", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_conteudo_cnpj_importacao_importacao_id",
                        column: x => x.importacao_id,
                        principalTable: "cnpj_importacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "layout_campo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cabecalho = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cabecalho_ordem = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    obrigatorio = table.Column<bool>(type: "boolean", nullable: false),
                    atributo_classe = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    atributo_classe_amigavel = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    model_obrigatorio = table.Column<bool>(type: "boolean", nullable: false),
                    ordem = table.Column<int>(type: "integer", nullable: false),
                    LayoutId = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layout_campo", x => x.id);
                    table.ForeignKey(
                        name: "FK_layout_campo_layout_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "layout",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_empresa",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    importacao_id = table.Column<long>(type: "bigint", nullable: false),
                    cnpj_basico = table.Column<string>(type: "text", nullable: false),
                    razao_social = table.Column<string>(type: "text", nullable: false),
                    natureza_juridica_id = table.Column<long>(type: "bigint", nullable: false),
                    qualificacao_socio_id = table.Column<long>(type: "bigint", nullable: false),
                    capital_social = table.Column<string>(type: "text", nullable: false),
                    porte = table.Column<int>(type: "integer", nullable: false),
                    ente_federativo_responsavel = table.Column<string>(type: "text", nullable: true),
                    CnpjEmpresaId = table.Column<long>(type: "bigint", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_empresa", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_empresa_cnpj_importacao_CnpjEmpresaId",
                        column: x => x.CnpjEmpresaId,
                        principalTable: "cnpj_importacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_empresa_cnpj_importacao_importacao_id",
                        column: x => x.importacao_id,
                        principalTable: "cnpj_importacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnpj_empresa_natureza_juridica_natureza_juridica_id",
                        column: x => x.natureza_juridica_id,
                        principalTable: "natureza_juridica",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_empresa_qualificacao_socio_qualificacao_socio_id",
                        column: x => x.qualificacao_socio_id,
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
                    importacao_id = table.Column<long>(type: "bigint", nullable: false),
                    cnpj_empresa = table.Column<long>(type: "bigint", nullable: false),
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
                    cnae_id = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_cnpj_estabelecimento_cnae_cnae_id",
                        column: x => x.cnae_id,
                        principalTable: "cnae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_estabelecimento_cnpj_empresa_cnpj_empresa",
                        column: x => x.cnpj_empresa,
                        principalTable: "cnpj_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_estabelecimento_cnpj_importacao_importacao_id",
                        column: x => x.importacao_id,
                        principalTable: "cnpj_importacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    importacao_id = table.Column<long>(type: "bigint", nullable: false),
                    cnpj_empresa = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_cnpj_simples_cnpj_empresa_cnpj_empresa",
                        column: x => x.cnpj_empresa,
                        principalTable: "cnpj_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cnpj_simples_cnpj_importacao_importacao_id",
                        column: x => x.importacao_id,
                        principalTable: "cnpj_importacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_socio",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnpj_basico = table.Column<long>(type: "bigint", nullable: false),
                    importacao_id = table.Column<long>(type: "bigint", nullable: false),
                    tipo_socio = table.Column<string>(type: "text", nullable: false),
                    nome_socio = table.Column<string>(type: "text", nullable: false),
                    documento_socio = table.Column<string>(type: "text", nullable: false),
                    qualificacao_socio_id = table.Column<long>(type: "bigint", nullable: false),
                    data_entrada_sociedade = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CnpjEmpresaId = table.Column<long>(type: "bigint", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_socio", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_socio_cnpj_empresa_CnpjEmpresaId",
                        column: x => x.CnpjEmpresaId,
                        principalTable: "cnpj_empresa",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_cnpj_socio_cnpj_importacao_importacao_id",
                        column: x => x.importacao_id,
                        principalTable: "cnpj_importacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnpj_socio_qualificacao_socio_qualificacao_socio_id",
                        column: x => x.qualificacao_socio_id,
                        principalTable: "qualificacao_socio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "falha",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CnpjEmpresaId = table.Column<long>(type: "bigint", nullable: false),
                    linha = table.Column<int>(type: "integer", nullable: false),
                    linha_conteudo = table.Column<List<string>>(type: "text[]", nullable: false),
                    coluna_origem = table.Column<List<string>>(type: "text[]", nullable: false),
                    campo_destino = table.Column<List<string>>(type: "text[]", nullable: false),
                    valor_recebido = table.Column<List<string>>(type: "text[]", nullable: false),
                    motivo = table.Column<List<string>>(type: "text[]", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_falha", x => x.id);
                    table.ForeignKey(
                        name: "FK_falha_cnpj_empresa_CnpjEmpresaId",
                        column: x => x.CnpjEmpresaId,
                        principalTable: "cnpj_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ignorado",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CnpjEmpresaId = table.Column<long>(type: "bigint", nullable: false),
                    linha = table.Column<long>(type: "bigint", nullable: false),
                    cabecalho = table.Column<List<string>>(type: "text[]", nullable: false),
                    conteudo = table.Column<List<string>>(type: "text[]", nullable: false),
                    motivo = table.Column<List<string>>(type: "text[]", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ignorado", x => x.id);
                    table.ForeignKey(
                        name: "FK_ignorado_cnpj_empresa_CnpjEmpresaId",
                        column: x => x.CnpjEmpresaId,
                        principalTable: "cnpj_empresa",
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
                    importacao_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_cnpj_estab_cnae_sec_cnpj_importacao_importacao_id",
                        column: x => x.importacao_id,
                        principalTable: "cnpj_importacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_conteudo_importacao_id",
                table: "cnpj_conteudo",
                column: "importacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_empresa_CnpjEmpresaId",
                table: "cnpj_empresa",
                column: "CnpjEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_empresa_importacao_id",
                table: "cnpj_empresa",
                column: "importacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_empresa_natureza_juridica_id",
                table: "cnpj_empresa",
                column: "natureza_juridica_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_empresa_qualificacao_socio_id",
                table: "cnpj_empresa",
                column: "qualificacao_socio_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estab_cnae_sec_cnae_id",
                table: "cnpj_estab_cnae_sec",
                column: "cnae_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estab_cnae_sec_estabelecimento_id",
                table: "cnpj_estab_cnae_sec",
                column: "estabelecimento_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estab_cnae_sec_importacao_id",
                table: "cnpj_estab_cnae_sec",
                column: "importacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_cnae_id",
                table: "cnpj_estabelecimento",
                column: "cnae_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_cnpj_empresa",
                table: "cnpj_estabelecimento",
                column: "cnpj_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_estabelecimento_importacao_id",
                table: "cnpj_estabelecimento",
                column: "importacao_id");

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
                name: "IX_cnpj_simples_cnpj_empresa",
                table: "cnpj_simples",
                column: "cnpj_empresa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_simples_importacao_id",
                table: "cnpj_simples",
                column: "importacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_socio_CnpjEmpresaId",
                table: "cnpj_socio",
                column: "CnpjEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_socio_importacao_id",
                table: "cnpj_socio",
                column: "importacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_socio_qualificacao_socio_id",
                table: "cnpj_socio",
                column: "qualificacao_socio_id");

            migrationBuilder.CreateIndex(
                name: "IX_falha_CnpjEmpresaId",
                table: "falha",
                column: "CnpjEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ignorado_CnpjEmpresaId",
                table: "ignorado",
                column: "CnpjEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_layout_campo_LayoutId",
                table: "layout_campo",
                column: "LayoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cnpj_conteudo");

            migrationBuilder.DropTable(
                name: "cnpj_estab_cnae_sec");

            migrationBuilder.DropTable(
                name: "cnpj_simples");

            migrationBuilder.DropTable(
                name: "cnpj_socio");

            migrationBuilder.DropTable(
                name: "falha");

            migrationBuilder.DropTable(
                name: "ignorado");

            migrationBuilder.DropTable(
                name: "layout_campo");

            migrationBuilder.DropTable(
                name: "cnpj_estabelecimento");

            migrationBuilder.DropTable(
                name: "layout");

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
                name: "cnpj_importacao");

            migrationBuilder.DropTable(
                name: "natureza_juridica");

            migrationBuilder.DropTable(
                name: "qualificacao_socio");
        }
    }
}
