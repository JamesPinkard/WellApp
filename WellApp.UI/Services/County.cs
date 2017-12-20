using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellApp.UI.Services
{
    public class County : BindableBase
    {
        public string Name { get; set; }

        public County(string name)
        {
            Name = name;
        }
        
    }
}
