using Microsoft.EntityFrameworkCore;
using TowBoatSalvageWebApp.Models;

namespace TowBoatSalvageWebApp.Data
{
    public class SalvageDbContext : DbContext
    {
        // These DbSets map to tables in SQLite
        public DbSet<SalvageColumn> Columns { get; set; }
        public DbSet<SalvageRow> Rows {  get; set; }
        public DbSet<SalvageCell> Cells {  get; set; }
        public DbSet<SalvageFile> Files {  get; set; }

        public SalvageDbContext(DbContextOptions<SalvageDbContext> options) : base(options) { }

        // Model configuration for table/column relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure that for each (Row, Column) pair, there is only one cell
            modelBuilder.Entity<SalvageCell>()
                .HasIndex(c => new { c.RowId, c.ColumnId })
                .IsUnique();

            // Configure cascade deltes or restrics as needed
            modelBuilder.Entity<SalvageCell>()
                .HasMany(c => c.Files)
                .WithOne(f => f.Cell)
                .HasForeignKey(f => f.CellId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
