using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chaveamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Versao = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TravadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chaveamentos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NomeLocal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EhRecorrente = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SerieEventoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    MaximoParticipantes = table.Column<int>(type: "int", nullable: true),
                    QuantidadeVagasFixas = table.Column<int>(type: "int", nullable: true),
                    QuantidadeVagasSorteio = table.Column<int>(type: "int", nullable: true),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inscricoes_evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioMcId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InscritoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscricoes_evento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mcs_fixos_evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioMcId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mcs_fixos_evento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mcs_pre_selecionados_evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioMcId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mcs_pre_selecionados_evento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "noticias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Conteudo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PublicadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_noticias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "papeis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_papeis", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "permissoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Codigo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissoes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "registros_timeline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Metadados = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registros_timeline", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EhMc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EhOrganizador = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vitorias_mc_evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioMcId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataVitoria = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vitorias_mc_evento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rodadas_chaveamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChaveamentoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NumeroRodada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rodadas_chaveamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rodadas_chaveamento_chaveamentos_ChaveamentoId",
                        column: x => x.ChaveamentoId,
                        principalTable: "chaveamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "papeis_permissoes",
                columns: table => new
                {
                    PapelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PermissaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_papeis_permissoes", x => new { x.PapelId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_papeis_permissoes_papeis_PapelId",
                        column: x => x.PapelId,
                        principalTable: "papeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_papeis_permissoes_permissoes_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "permissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios_papeis",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PapelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_papeis", x => new { x.UsuarioId, x.PapelId });
                    table.ForeignKey(
                        name: "FK_usuarios_papeis_papeis_PapelId",
                        column: x => x.PapelId,
                        principalTable: "papeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarios_papeis_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "confrontos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RodadaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChaveamentoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParticipanteAId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ParticipanteBId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    VencedorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    EhBye = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    VencedorPorWalkover = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdStatusConfronto = table.Column<int>(type: "int", nullable: false),
                    RodadaChaveamentoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_confrontos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_confrontos_rodadas_chaveamento_RodadaChaveamentoId",
                        column: x => x.RodadaChaveamentoId,
                        principalTable: "rodadas_chaveamento",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "papeis",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "ADMINISTRADOR" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "ORGANIZADOR" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "MC" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "VISUALIZADOR" }
                });

            migrationBuilder.InsertData(
                table: "permissoes",
                columns: new[] { "Id", "Codigo" },
                values: new object[,]
                {
                    { new Guid("000003e8-0000-0000-0000-000000000000"), "eventos.criar" },
                    { new Guid("000003e9-0000-0000-0000-000000000000"), "eventos.abrirInscricoes" },
                    { new Guid("000003ea-0000-0000-0000-000000000000"), "eventos.fecharInscricoes" },
                    { new Guid("000003eb-0000-0000-0000-000000000000"), "eventos.sortearChaveamento" },
                    { new Guid("000003ec-0000-0000-0000-000000000000"), "eventos.ressortearChaveamento" },
                    { new Guid("000003ed-0000-0000-0000-000000000000"), "eventos.registrarVencedor" },
                    { new Guid("000003ee-0000-0000-0000-000000000000"), "eventos.finalizar" },
                    { new Guid("000003ef-0000-0000-0000-000000000000"), "noticias.publicar" },
                    { new Guid("000003f0-0000-0000-0000-000000000000"), "noticias.lerRascunhos" },
                    { new Guid("000003f1-0000-0000-0000-000000000000"), "doacoes.registrar" },
                    { new Guid("000003f2-0000-0000-0000-000000000000"), "eventos.inscricao.criar" },
                    { new Guid("000003f3-0000-0000-0000-000000000000"), "eventos.ler.abertos" },
                    { new Guid("000003f4-0000-0000-0000-000000000000"), "eventos.inscricoes.listar" },
                    { new Guid("000003f5-0000-0000-0000-000000000000"), "eventos.fixos.definirVagas" },
                    { new Guid("000003f6-0000-0000-0000-000000000000"), "eventos.fixos.definirLista" },
                    { new Guid("000003f7-0000-0000-0000-000000000000"), "eventos.preselecao.criar" },
                    { new Guid("000003f8-0000-0000-0000-000000000000"), "eventos.mc.removerPorWo" },
                    { new Guid("000003f9-0000-0000-0000-000000000000"), "confrontos.wo.registrar" },
                    { new Guid("000003fa-0000-0000-0000-000000000000"), "mc.wo.lerProprio" },
                    { new Guid("000003fb-0000-0000-0000-000000000000"), "ranking.ler" },
                    { new Guid("000003fc-0000-0000-0000-000000000000"), "eventos.ler.publico" }
                });

            migrationBuilder.InsertData(
                table: "papeis_permissoes",
                columns: new[] { "PapelId", "PermissaoId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003e8-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003e9-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003ea-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003eb-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003ec-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003ed-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003ee-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003ef-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f0-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f1-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f2-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f3-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f4-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f5-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f6-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f7-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f8-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003f9-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003fa-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003fb-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("000003fc-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003e8-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003e9-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003ea-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003eb-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003ec-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003ed-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003ee-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003f0-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003f4-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003f5-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003f6-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003f7-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003f8-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("000003f9-0000-0000-0000-000000000000") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("000003f2-0000-0000-0000-000000000000") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("000003f3-0000-0000-0000-000000000000") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("000003fa-0000-0000-0000-000000000000") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("000003fb-0000-0000-0000-000000000000") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("000003fc-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_confrontos_RodadaChaveamentoId",
                table: "confrontos",
                column: "RodadaChaveamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_papeis_permissoes_PermissaoId",
                table: "papeis_permissoes",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_rodadas_chaveamento_ChaveamentoId",
                table: "rodadas_chaveamento",
                column: "ChaveamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_papeis_PapelId",
                table: "usuarios_papeis",
                column: "PapelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "confrontos");

            migrationBuilder.DropTable(
                name: "eventos");

            migrationBuilder.DropTable(
                name: "inscricoes_evento");

            migrationBuilder.DropTable(
                name: "mcs_fixos_evento");

            migrationBuilder.DropTable(
                name: "mcs_pre_selecionados_evento");

            migrationBuilder.DropTable(
                name: "noticias");

            migrationBuilder.DropTable(
                name: "papeis_permissoes");

            migrationBuilder.DropTable(
                name: "registros_timeline");

            migrationBuilder.DropTable(
                name: "usuarios_papeis");

            migrationBuilder.DropTable(
                name: "vitorias_mc_evento");

            migrationBuilder.DropTable(
                name: "rodadas_chaveamento");

            migrationBuilder.DropTable(
                name: "permissoes");

            migrationBuilder.DropTable(
                name: "papeis");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "chaveamentos");
        }
    }
}
