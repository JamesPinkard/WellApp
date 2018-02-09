using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WellApp.UI.Services
{
    public interface IAttributeTable<T> 
    {
        Task<List<BindableItem>> GetAttributeValuesAsync(
            Expression<Func<T, string>> selector);
    }
}
