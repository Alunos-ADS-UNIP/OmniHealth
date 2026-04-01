using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Models;

namespace OmniHealth.API.Data;

public class OmniHealthDbContext : DbContext
{
    public OmniHealthDbContext(DbContextOptions<OmniHealthDbContext> options)
        : base(options) { }

    // ─── DbSets ───────────────────────────────────────────────────────────────
    public DbSet<Usuario>             Usuarios        { get; set; }
    public DbSet<Paciente>            Pacientes       { get; set; }
    public DbSet<Medico>              Medicos         { get; set; }
    public DbSet<Farmaceutico>        Farmaceuticos   { get; set; }
    public DbSet<Funcionario>         Funcionarios    { get; set; }
    public DbSet<Prontuario>          Prontuarios     { get; set; }
    public DbSet<Consulta>            Consultas       { get; set; }
    public DbSet<Prescricao>          Prescricoes     { get; set; }
    public DbSet<ItemPrescricao>      ItensPrescricao { get; set; }
    public DbSet<Medicamento>         Medicamentos    { get; set; }
    public DbSet<Lote>                Lotes           { get; set; }
    public DbSet<MovimentacaoEstoque> Movimentacoes   { get; set; }
    public DbSet<Exame>               Exames          { get; set; }
    public DbSet<Internacao>          Internacoes     { get; set; }
    public DbSet<Log>                 Logs            { get; set; }

    // ─── View (keyless) ───────────────────────────────────────────────────────
    public DbSet<VwEstoque> VwEstoque { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        // ── Índices únicos ────────────────────────────────────────────────────
        mb.Entity<Usuario>().HasIndex(u => u.Cpf).IsUnique();
        mb.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
        mb.Entity<Medico>().HasIndex(m => m.Crm).IsUnique();
        mb.Entity<Farmaceutico>().HasIndex(f => f.Crf).IsUnique();

        // ── View VW_ESTOQUE (entidade sem chave) ──────────────────────────────
        mb.Entity<VwEstoque>().HasNoKey().ToView("VW_ESTOQUE");

        // ── TipoUsuario como string ───────────────────────────────────────────
        mb.Entity<Usuario>()
            .Property(u => u.TipoUsuario)
            .HasConversion<string>();

        // ── Relacionamento Paciente → Prontuario (1:1) ────────────────────────
        mb.Entity<Prontuario>()
            .HasOne(p => p.Paciente)
            .WithOne(p => p.Prontuario)
            .HasForeignKey<Prontuario>(p => p.IdPaciente);

        // ── Consulta: evita cascata múltipla (Paciente e Prontuario) ──────────
        mb.Entity<Consulta>()
            .HasOne(c => c.Paciente)
            .WithMany()
            .HasForeignKey(c => c.IdPaciente)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Exame: id_consulta e id_internacao opcionais ──────────────────────
        mb.Entity<Exame>()
            .HasOne(e => e.Consulta)
            .WithMany(c => c.Exames)
            .HasForeignKey(e => e.IdConsulta)
            .OnDelete(DeleteBehavior.SetNull);

        mb.Entity<Exame>()
            .HasOne(e => e.Internacao)
            .WithMany(i => i.Exames)
            .HasForeignKey(e => e.IdInternacao)
            .OnDelete(DeleteBehavior.SetNull);

        // ── MovimentacaoEstoque: id_item opcional ─────────────────────────────
        mb.Entity<MovimentacaoEstoque>()
            .HasOne(m => m.ItemPrescricao)
            .WithMany(i => i.Movimentacoes)
            .HasForeignKey(m => m.IdItem)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
