using System.Collections.Generic;

namespace WellApp.Domain
{
    public sealed class Aquifer
    {        
        public int AquiferID { get; set; }
        public string AquiferName { get; set; }
        public string AquiferCode { get; set; }
        public string AquiferCodeDescriprion { get; set; }

        public override string ToString()
        {
            return AquiferName;
        }
    }
}
