using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WellApp.Domain;
using WellApp.Data;

namespace WellApp.UI.Services
{
    class WellRepository : IWellRepository, IGmaCollection, IAquiferCollection
    {
        GroundwaterContext _context = new GroundwaterContext();

        public Task<List<Well>> GetWellsAsync()
        {
            return _context.Wells
                .Include(w => w.Aquifer)
                .ToListAsync();
        }

        public Task<Well> GetWellAsync(int id)
        {
            return _context.Wells.FirstOrDefaultAsync(w => w.WellId == id);
        }

        public async Task<Well> AddWellAsync(Well well)
        {
            _context.Wells.Add(well);
            await _context.SaveChangesAsync();
            return well;            
        }

        public async Task<Well> UpdateWellAsync(Well well)
        {
            if(!_context.Wells.Local.Any(w=>w.WellId == well.WellId))
            {
                _context.Wells.Attach(well);
            }
            _context.Entry(well).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return well;
        }

        public async Task DeleteWellAsync(int wellId)
        {
            var well = _context.Wells.FirstOrDefault(w => w.WellId == wellId);
            if (well != null)
            {
                _context.Wells.Remove(well);
            }
            await _context.SaveChangesAsync();
        }

        public Task<List<BindableItem>> GetCountiesAsync()
        {
            var countyStrings = _context.Wells.Select(w => w.County)
            .Distinct()
            .OrderBy(c => c)
            .Select(c => new BindableItem(c))
            .ToListAsync();

            return countyStrings;
        }

        public Task<List<BindableItem>> GetGmasAsync()
        {
            var gmaItems = _context.Wells.Select(w => w.GMA)
            .Distinct()
            .OrderBy(g => g)
            .Select(g => new BindableItem(g.ToString()))
            .ToListAsync();

            return gmaItems;
        }

        public Task<List<BindableItem>> GetAquifersAsync()
        {           
            var aquiferItems = _context.Wells.Select(w => w.Aquifer.AquiferName)
                .Distinct()
                .OrderBy(a => a)
                .Select(a => new BindableItem(a))
                .ToListAsync();

            return aquiferItems;
        }
    }
}
