using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OneCNPJ.Data.Migrations
{
    /// <inheritdoc />
    public partial class startPack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cadfi",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    status_nao_adaptados_175 = table.Column<int>(type: "integer", nullable: false),
                    linhas_nao_adaptados_175 = table.Column<int>(type: "integer", nullable: false),
                    linhas_importadas_nao_adaptados_175 = table.Column<int>(type: "integer", nullable: false),
                    linhas_falhas_nao_adaptados_175 = table.Column<int>(type: "integer", nullable: false),
                    linhas_ignoradas_nao_adaptados_175 = table.Column<int>(type: "integer", nullable: false),
                    linhas_com_erros_nao_adaptados_175 = table.Column<List<int>>(type: "integer[]", nullable: false),
                    status_registro_fundo = table.Column<int>(type: "integer", nullable: false),
                    linhas_registro_fundo = table.Column<int>(type: "integer", nullable: false),
                    linhas_importadas_registro_fundo = table.Column<int>(type: "integer", nullable: false),
                    linhas_falhas_registro_fundo = table.Column<int>(type: "integer", nullable: false),
                    linhas_ignoradas_registro_fundo = table.Column<int>(type: "integer", nullable: false),
                    linhas_com_erros_registro_fundo = table.Column<List<int>>(type: "integer[]", nullable: false),
                    status_registro_classe = table.Column<int>(type: "integer", nullable: false),
                    linhas_registro_classe = table.Column<int>(type: "integer", nullable: false),
                    linhas_importadas_registro_classe = table.Column<int>(type: "integer", nullable: false),
                    linhas_falhas_registro_classe = table.Column<int>(type: "integer", nullable: false),
                    linhas_ignoradas_registro_classe = table.Column<int>(type: "integer", nullable: false),
                    linhas_com_erros_registro_classe = table.Column<List<int>>(type: "integer[]", nullable: false),
                    status_registro_subclasse = table.Column<int>(type: "integer", nullable: false),
                    linhas_registro_subclasse = table.Column<int>(type: "integer", nullable: false),
                    linhas_importadas_registro_subclasse = table.Column<int>(type: "integer", nullable: false),
                    linhas_falhas_registro_subclasse = table.Column<int>(type: "integer", nullable: false),
                    linhas_ignoradas_registro_subclasse = table.Column<int>(type: "integer", nullable: false),
                    linhas_com_erros_registro_subclasse = table.Column<List<int>>(type: "integer[]", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadfi", x => x.id);
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
                    formato_cadfi = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layout", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "conteudo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadfiId = table.Column<long>(type: "bigint", nullable: false),
                    cabecalho = table.Column<List<string>>(type: "text[]", nullable: false),
                    tp_fundo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cnpj_fundo = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    denom_social = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dt_reg = table.Column<DateOnly>(type: "date", nullable: true),
                    @const = table.Column<DateOnly>(name: "const", type: "date", nullable: true),
                    cd_cvm = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    dt_cancel = table.Column<DateOnly>(type: "date", nullable: true),
                    sit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    dt_ini_sit = table.Column<DateOnly>(type: "date", nullable: true),
                    dt_ini_ativ = table.Column<DateOnly>(type: "date", nullable: true),
                    dt_ini_exerc = table.Column<DateOnly>(type: "date", nullable: true),
                    dt_fim_exerc = table.Column<DateOnly>(type: "date", nullable: true),
                    classe = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    dt_ini_classe = table.Column<DateOnly>(type: "date", nullable: true),
                    rentab_fundo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    condom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    fundo_cotas = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    fundo_exclusivo = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    trib_lprazo = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    publico_alvo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    entid_invest = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    taxa_perfm = table.Column<decimal>(type: "numeric(26,2)", precision: 26, scale: 2, nullable: false),
                    inf_taxa_perfm = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    taxa_adm = table.Column<decimal>(type: "numeric(26,8)", precision: 26, scale: 8, nullable: false),
                    inf_taxa_adm = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    vl_patrim_liq = table.Column<decimal>(type: "numeric(26,2)", precision: 26, scale: 2, nullable: false),
                    dt_patrim_liq = table.Column<DateOnly>(type: "date", nullable: true),
                    diretor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cnpj_admin = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    admin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    pf_pj_gestor = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    cpf_cnpj_gestor = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    gestor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cnpj_auditor = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    auditor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cnpj_custodiante = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    custodiante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cnpj_controlador = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    controlador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    invest_cempr_exter = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    classe_anbima = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conteudo", x => x.id);
                    table.ForeignKey(
                        name: "FK_conteudo_cadfi_CadfiId",
                        column: x => x.CadfiId,
                        principalTable: "cadfi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "falha",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadfiId = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_falha_cadfi_CadfiId",
                        column: x => x.CadfiId,
                        principalTable: "cadfi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ignorado",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadfiId = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_ignorado_cadfi_CadfiId",
                        column: x => x.CadfiId,
                        principalTable: "cadfi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "registro_fundo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadfiId = table.Column<long>(type: "bigint", nullable: false),
                    cabecalho = table.Column<List<string>>(type: "text[]", nullable: false),
                    id_registro_fundo = table.Column<long>(type: "bigint", nullable: false),
                    cnpj_fundo = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    codigo_cvm = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    data_registro = table.Column<DateOnly>(type: "date", nullable: true),
                    data_constituição = table.Column<DateOnly>(type: "date", nullable: true),
                    tipo_fundo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    denominacao_social = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    data_cancelamento = table.Column<DateOnly>(type: "date", nullable: true),
                    situacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    data_inicio_situacao = table.Column<DateOnly>(type: "date", nullable: true),
                    data_adaptacao_rcvm_175 = table.Column<DateOnly>(type: "date", nullable: true),
                    data_inicio_exercicio_social = table.Column<DateOnly>(type: "date", nullable: true),
                    data_fim_exercicio_social = table.Column<DateOnly>(type: "date", nullable: true),
                    patrimonio_liquido = table.Column<decimal>(type: "numeric(26,2)", precision: 26, scale: 2, nullable: false),
                    data_patrimonio_liquido = table.Column<DateOnly>(type: "date", nullable: true),
                    diretor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cnpj_administrador = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    administrador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tipo_pessoa_gestor = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    documento_gestor = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    gestor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registro_fundo", x => x.id);
                    table.ForeignKey(
                        name: "FK_registro_fundo_cadfi_CadfiId",
                        column: x => x.CadfiId,
                        principalTable: "cadfi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "registro_classe",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadfiId = table.Column<long>(type: "bigint", nullable: false),
                    RegistroFundoId = table.Column<long>(type: "bigint", nullable: true),
                    cabecalho = table.Column<List<string>>(type: "text[]", nullable: false),
                    id_registro_fundo = table.Column<long>(type: "bigint", nullable: false),
                    id_registro_classe = table.Column<long>(type: "bigint", nullable: false),
                    cnpj_classe = table.Column<string>(type: "text", nullable: false),
                    codigo_cvm = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    data_registro = table.Column<DateOnly>(type: "date", nullable: true),
                    data_constituicao = table.Column<DateOnly>(type: "date", nullable: true),
                    data_inicio = table.Column<DateOnly>(type: "date", nullable: true),
                    tipo_classe = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    denominacao_social = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    situacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    classificacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    data_inicio_situacao = table.Column<DateOnly>(type: "date", nullable: true),
                    indicador_desempenho = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    classe_cotas = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    classe_anbima = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tributacao_longo_prazo = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    entidade_investimento = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    permitido_aplicacao_cem_por_cento_exterior = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    classe_esg = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    forma_condominio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    exclusivo = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    publico_alvo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    patrimonio_liquido = table.Column<decimal>(type: "numeric(28,2)", nullable: true),
                    data_patrimonio_liquido = table.Column<DateOnly>(type: "date", nullable: true),
                    cnpj_auditor = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    auditor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cnpj_custodiante = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    custodiante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cnpj_controlador = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    controlador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registro_classe", x => x.id);
                    table.ForeignKey(
                        name: "FK_registro_classe_cadfi_CadfiId",
                        column: x => x.CadfiId,
                        principalTable: "cadfi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_registro_classe_registro_fundo_RegistroFundoId",
                        column: x => x.RegistroFundoId,
                        principalTable: "registro_fundo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "registro_subclasse",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadfiId = table.Column<long>(type: "bigint", nullable: false),
                    RegistroClasseId = table.Column<long>(type: "bigint", nullable: true),
                    cabecalho = table.Column<List<string>>(type: "text[]", nullable: false),
                    id_registro_classe = table.Column<long>(type: "bigint", nullable: false),
                    id_registro_subclasse = table.Column<long>(type: "bigint", nullable: false),
                    codigo_cvm = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    data_constituicao = table.Column<DateOnly>(type: "date", nullable: true),
                    data_inicio = table.Column<DateOnly>(type: "date", nullable: true),
                    denominacao_social = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    situacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    data_inicio_situacao = table.Column<DateOnly>(type: "date", nullable: true),
                    forma_condominio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    exclusivo = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    publico_alvo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registro_subclasse", x => x.id);
                    table.ForeignKey(
                        name: "FK_registro_subclasse_cadfi_CadfiId",
                        column: x => x.CadfiId,
                        principalTable: "cadfi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_registro_subclasse_registro_classe_RegistroClasseId",
                        column: x => x.RegistroClasseId,
                        principalTable: "registro_classe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_conteudo_CadfiId",
                table: "conteudo",
                column: "CadfiId");

            migrationBuilder.CreateIndex(
                name: "IX_falha_CadfiId",
                table: "falha",
                column: "CadfiId");

            migrationBuilder.CreateIndex(
                name: "IX_ignorado_CadfiId",
                table: "ignorado",
                column: "CadfiId");

            migrationBuilder.CreateIndex(
                name: "IX_layout_campo_LayoutId",
                table: "layout_campo",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_classe_CadfiId",
                table: "registro_classe",
                column: "CadfiId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_classe_RegistroFundoId",
                table: "registro_classe",
                column: "RegistroFundoId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_fundo_CadfiId",
                table: "registro_fundo",
                column: "CadfiId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_subclasse_CadfiId",
                table: "registro_subclasse",
                column: "CadfiId");

            migrationBuilder.CreateIndex(
                name: "IX_registro_subclasse_RegistroClasseId",
                table: "registro_subclasse",
                column: "RegistroClasseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conteudo");

            migrationBuilder.DropTable(
                name: "falha");

            migrationBuilder.DropTable(
                name: "ignorado");

            migrationBuilder.DropTable(
                name: "layout_campo");

            migrationBuilder.DropTable(
                name: "registro_subclasse");

            migrationBuilder.DropTable(
                name: "layout");

            migrationBuilder.DropTable(
                name: "registro_classe");

            migrationBuilder.DropTable(
                name: "registro_fundo");

            migrationBuilder.DropTable(
                name: "cadfi");
        }
    }
}
