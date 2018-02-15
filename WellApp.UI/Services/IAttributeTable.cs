using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WellApp.UI.Services
{
    public interface IAttributeTable<TEntity> 
        where TEntity : class
    {
        Task<List<BindableItem>> GetAttributeValuesAsync<TAttribute>(
            Expression<Func<TEntity, TAttribute>> selector) where TAttribute : IComparable<TAttribute>;
    }
}
