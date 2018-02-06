using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.Domain;

namespace WellApp.UI.Services
{
    public interface IWellRepository
    {
        Task<List<Well>> GetWellsAsync();
        Task<List<BindableItem>> GetCountiesAsync();
        Task<Well> GetWellAsync(int id);
        Task<Well> AddWellAsync(Well well);
        Task<Well> UpdateWellAsync(Well well);
        Task DeleteWellAsync(int wellId);
    }
}
