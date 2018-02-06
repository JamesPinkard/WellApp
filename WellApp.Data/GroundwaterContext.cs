using Microsoft.EntityFrameworkCore;
using WellApp.Domain;

namespace WellApp.Data
{
    public class GroundwaterContext : DbContext
    {
        public DbSet<Well> Wells { get; set; }
        public DbSet<Aquifer> Aquifers { get; set; }

        public GroundwaterContext()
        {

        }

        public GroundwaterContext(DbContextOptions<GroundwaterContext> options): base (options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder.UseSqlite(@"Data Source = C:\Users\jpinkard\Documents\Visual Studio 2017\Projects\WellApp\Database\TexasWells.sqlite3; "); 
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Well>()
        //        .HasOne(w => w.Aquifer)
        //        .WithMany(a => a.Wells);
        //}
    }
}
