using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellApp.UI.Services
{
    public class BindableItem : BindableBase
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public BindableItem(string name)
        {
            Name = name;
        }

        public BindableItem(object obj)
        {
            Name = obj.ToString();
        }
        
    }
}
