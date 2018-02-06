using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.Data;
using WellApp.Domain;

namespace WellApp.XUnit
{
    public class GroundwaterContextTestBase : IDisposable
    {
        protected readonly GroundwaterContext _context;

        public GroundwaterContextTestBase()
        {
            var options = new DbContextOptionsBuilder<GroundwaterContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new GroundwaterContext(options);
            _context.Database.EnsureCreated();

            var edwards = new Aquifer { AquiferID = 11, AquiferCode = "218EDRDA", AquiferCodeDescriprion = "Edwards and Associated Limestones", AquiferName = "Edwards (Balcones Fault Zone)" };
            var antlers = new Aquifer { AquiferID = 13, AquiferCode = "218ALRS", AquiferCodeDescriprion = "Antlers Sand", AquiferName = "Edwards-Trinity Plateau" };
            var gulfCoast = new Aquifer { AquiferID = 15, AquiferCode = "", AquiferCodeDescriprion = "", AquiferName = "Gulf Coast" };
            var wells = new[]
            {
                new Well {StateWellNumber = "2217381", GMA = 10, County = "Travis", AquiferId = 11, Aquifer = edwards},
                new Well { StateWellNumber = "2096010", GMA = 7, County = "Glasscock", AquiferId = 13, Aquifer = antlers },
                new Well { StateWellNumber = "2173076", GMA = 16, County = "Nueces", AquiferId = 15, Aquifer = gulfCoast },
                new Well { StateWellNumber = "2159993", GMA = 13, County = "Medina", AquiferId = 11, Aquifer = edwards },
                new Well { StateWellNumber = "2160018", GMA = 13, County = "Atascosa", AquiferId = 11, Aquifer = edwards },
                new Well { StateWellNumber = "2220063", GMA = 9, County = "Kendall", AquiferId = 13, Aquifer = antlers },
                new Well { StateWellNumber = "2181848", GMA = 10, County = "Uvalde", AquiferId = 13, Aquifer = antlers },
                new Well { StateWellNumber = "2184349", GMA = 10, County = "Uvalde", AquiferId = 13, Aquifer = antlers },
            };

            _context.Wells.AddRange(wells);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
