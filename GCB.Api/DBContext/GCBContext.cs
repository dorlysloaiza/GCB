
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


public class GCBContext : DbContext
{
    

    private readonly AuditInterceptor _auditInterceptor;

   
    public GCBContext(DbContextOptions<GCBContext> options, AuditInterceptor auditInterceptor)
        : base(options)
    {
        _auditInterceptor = auditInterceptor;
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraciones adicionales del modelo
    }
}
