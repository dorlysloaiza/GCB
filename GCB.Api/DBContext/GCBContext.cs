
using GCB.Entities.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


public class GCBContext : DbContext
{




    public GCBContext(DbContextOptions<GCBContext> options)
        : base(options)
    {

    }

    public DbSet<Transaccion> Transacciones { get; set; }
    public DbSet<RegistroAuditoria> RegistrosAuditoria { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<AdjuntoGCB> AdjuntosGCB { get; set; }
    public DbSet<CuentaBanco> CuentaBanco { get; set; }
    public DbSet<Banco> Bancos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
    }
}

    
