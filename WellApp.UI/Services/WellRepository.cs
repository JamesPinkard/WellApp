using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WellApp.Domain;
using WellApp.Data;
using System.Linq.Expressions;

namespace WellApp.UI.Services
{
    public class WellRepository : IWellRepository, IAttributeTable<Well>
    {
        GroundwaterContext _context = new GroundwaterContext();

        public WellRepository(GroundwaterContext context)
        {
            _context = context;
        }

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

        public Task<List<BindableItem>> GetAttributeValuesAsync<TAttribute>(Expression<Func<Well, TAttribute>>selector) where TAttribute : IComparable<TAttribute>
        {
            var attributes = _context.Wells.Select(selector)
            .Distinct()
            .OrderBy(c => c)
            .Select(c => new BindableItem(c))
            .ToListAsync();

            return attributes;
        }
    }
}
