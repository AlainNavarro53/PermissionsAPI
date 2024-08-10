using Microsoft.EntityFrameworkCore;
using PermissionsAPI.Models;

namespace PermissionsAPI.Data
{
    public class PermissionsDbContext : DbContext
    {
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options)
            : base(options) { }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<TipoPermiso> TipoPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.TipoPermiso)
                .WithMany(t => t.Permissions)
                .HasForeignKey(p => p.TipoPermisoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
