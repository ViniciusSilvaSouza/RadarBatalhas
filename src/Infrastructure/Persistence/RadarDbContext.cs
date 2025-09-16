using Domain.Entities.Eventos;
using Domain.Entities.Noticia;
using Domain.Entities.Timeline;
using Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class RadarDbContext(DbContextOptions<RadarDbContext> options) : DbContext(options)
{
    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<InscricaoEvento> Inscricoes => Set<InscricaoEvento>();
    public DbSet<Chaveamento> Chaveamentos => Set<Chaveamento>();
    public DbSet<RodadaChaveamento> Rodadas => Set<RodadaChaveamento>();
    public DbSet<Confronto> Confrontos => Set<Confronto>();
    public DbSet<VitoriaMcEvento> Vitorias => Set<VitoriaMcEvento>();
    public DbSet<McFixoEvento> McsFixos => Set<McFixoEvento>();
    public DbSet<McPreSelecionadoEvento> McsPreSelecionados => Set<McPreSelecionadoEvento>();

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Papel> Papeis => Set<Papel>();
    public DbSet<Permissao> Permissoes => Set<Permissao>();
    public DbSet<UsuarioPapel> UsuariosPapeis => Set<UsuarioPapel>();
    public DbSet<PapelPermissao> PapeisPermissoes => Set<PapelPermissao>();

    public DbSet<Noticia> Noticias => Set<Noticia>();
    public DbSet<RegistroTimeline> RegistrosTimeline => Set<RegistroTimeline>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // snake_case plural convention (simplificada para o template)
        modelBuilder.Entity<Evento>().ToTable("eventos");
        modelBuilder.Entity<InscricaoEvento>().ToTable("inscricoes_evento");
        modelBuilder.Entity<Chaveamento>().ToTable("chaveamentos");
        modelBuilder.Entity<RodadaChaveamento>().ToTable("rodadas_chaveamento");
        modelBuilder.Entity<Confronto>().ToTable("confrontos");
        modelBuilder.Entity<VitoriaMcEvento>().ToTable("vitorias_mc_evento");
        modelBuilder.Entity<McFixoEvento>().ToTable("mcs_fixos_evento");
        modelBuilder.Entity<McPreSelecionadoEvento>().ToTable("mcs_pre_selecionados_evento");
        modelBuilder.Entity<Usuario>().ToTable("usuarios");
        modelBuilder.Entity<Papel>().ToTable("papeis");
        modelBuilder.Entity<Permissao>().ToTable("permissoes");
        modelBuilder.Entity<UsuarioPapel>().ToTable("usuarios_papeis");
        modelBuilder.Entity<PapelPermissao>().ToTable("papeis_permissoes");
        modelBuilder.Entity<Noticia>().ToTable("noticias");
        modelBuilder.Entity<RegistroTimeline>().ToTable("registros_timeline");

        modelBuilder.Entity<UsuarioPapel>().HasKey(up => new { up.UsuarioId, up.PapelId });
        modelBuilder.Entity<PapelPermissao>().HasKey(pp => new { pp.PapelId, pp.PermissaoId });

        SeedRbac(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedRbac(ModelBuilder modelBuilder)
    {
        // Roles
        var roleAdmin = new Guid("11111111-1111-1111-1111-111111111111");
        var roleOrg   = new Guid("22222222-2222-2222-2222-222222222222");
        var roleMc    = new Guid("33333333-3333-3333-3333-333333333333");
        var roleVis   = new Guid("44444444-4444-4444-4444-444444444444");

        modelBuilder.Entity<Papel>().HasData(
            new Papel { Id = roleAdmin, Nome = "ADMINISTRADOR" },
            new Papel { Id = roleOrg,   Nome = "ORGANIZADOR" },
            new Papel { Id = roleMc,    Nome = "MC" },
            new Papel { Id = roleVis,   Nome = "VISUALIZADOR" }
        );

        // Permissions
        string[] perms =
        [
            "eventos.criar","eventos.abrirInscricoes","eventos.fecharInscricoes","eventos.sortearChaveamento","eventos.ressortearChaveamento","eventos.registrarVencedor","eventos.finalizar",
            "noticias.publicar","noticias.lerRascunhos","doacoes.registrar",
            "eventos.inscricao.criar","eventos.ler.abertos","eventos.inscricoes.listar",
            "eventos.fixos.definirVagas","eventos.fixos.definirLista","eventos.preselecao.criar","eventos.mc.removerPorWo","confrontos.wo.registrar",
            "mc.wo.lerProprio","ranking.ler","eventos.ler.publico"
        ];

        var permIds = perms.Select((p, i) => new { p, id = GuidUtility(i) }).ToList();
        modelBuilder.Entity<Permissao>().HasData(permIds.Select(x => new Permissao { Id = x.id, Codigo = x.p }));

        // Admin -> all permissions
        modelBuilder.Entity<PapelPermissao>().HasData(permIds.Select(x => new PapelPermissao { PapelId = roleAdmin, PermissaoId = x.id }));

        // Organizador subset
        string[] orgPerms = 
        [
            "eventos.criar","eventos.abrirInscricoes","eventos.fecharInscricoes","eventos.sortearChaveamento","eventos.ressortearChaveamento","eventos.registrarVencedor","eventos.finalizar",
            "noticias.lerRascunhos","eventos.inscricoes.listar",
            "eventos.fixos.definirVagas","eventos.fixos.definirLista","eventos.preselecao.criar","eventos.mc.removerPorWo","confrontos.wo.registrar"
        ];
        var orgIds = permIds.Where(x => orgPerms.Contains(x.p)).Select(x => x.id).ToList();
        modelBuilder.Entity<PapelPermissao>().HasData(orgIds.Select(id => new PapelPermissao { PapelId = roleOrg, PermissaoId = id }));

        // MC subset
        string[] mcPerms =  ["eventos.inscricao.criar", "eventos.ler.abertos", "mc.wo.lerProprio"];
        var mcIds = permIds.Where(x => mcPerms.Contains(x.p)).Select(x => x.id).ToList();
        modelBuilder.Entity<PapelPermissao>().HasData(mcIds.Select(id => new PapelPermissao { PapelId = roleMc, PermissaoId = id }));

        // Visualizador
        string[] visPerms =  ["ranking.ler", "eventos.ler.publico"];
        var visIds = permIds.Where(x => visPerms.Contains(x.p)).Select(x => x.id).ToList();
        modelBuilder.Entity<PapelPermissao>().HasData(visIds.Select(id => new PapelPermissao { PapelId = roleVis, PermissaoId = id }));
    }

    private static Guid GuidUtility(int index)
    {
        // Deterministic GUIDs for seeding (based on index)
        var bytes = new byte[16];
        BitConverter.GetBytes(index + 1000).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
}
