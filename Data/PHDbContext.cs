using Microsoft.EntityFrameworkCore;

namespace tp7518.Data;

public class PHDbContext : DbContext
{
    public PHDbContext(DbContextOptions<PHDbContext> options):base(options)
    {                           
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);        

        // Cambiar la convención de nombres de tablas.
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.DisplayName());
        }        
    }
}

