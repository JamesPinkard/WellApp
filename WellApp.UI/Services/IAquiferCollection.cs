using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellApp.UI.Services
{
    public interface IAquiferCollection
    {
        Task<List<BindableItem>> GetAquifersAsync();
    }
}
