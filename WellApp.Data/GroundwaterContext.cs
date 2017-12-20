using Microsoft.EntityFrameworkCore;
using WellApp.Domain;

namespace WellApp.Data
{
    public class GroundwaterContext : DbContext
    {
        public DbSet<Well> Wells { get; set; }
        public DbSet<Aquifer> Aquifers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = WellData; Trusted_Connection = True; ");
            optionsBuilder.UseSqlite(@"Data Source = C:\Users\jpinkard\Documents\Visual Studio 2017\Projects\WellApp\Database\TexasWells.sqlite3; ");
        }
    }
}
