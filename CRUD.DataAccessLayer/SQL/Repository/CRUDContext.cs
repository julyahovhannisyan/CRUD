using CRUD.DataAccessLayer.SQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class CRUDContext : DbContext
{

    public CRUDContext() :base(){ }
    public CRUDContext(DbContextOptions<CRUDContext> options)
         : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql("server=localhost; port=3306; database=ArticlesDB; user=root; password=123456; Persist Security Info=False; Connect Timeout=300",
                ServerVersion.AutoDetect("server=localhost; port=3306; database=ArticlesDB; user=root; password=123456; Persist Security Info=False; Connect Timeout=300"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    public DbSet<Article> Article { get; set; }
}
