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
    class WellRepository : IWellRepository
    {
        GroundwaterContext _context = new GroundwaterContext();

        public Task<List<Well>> GetWellsAsync()
        {
            return _context.Wells.ToListAsync();
        }

        public Task<List<County>> GetCountiesAsync()
        {
            var Counties = new List<County>();

            var countyStrings = _context.Wells.Select(w => w.County)
            .Distinct()
            .OrderBy(c => c)
            .Select(c => new County(c))
            .ToListAsync();

            return countyStrings;
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
    }
}
