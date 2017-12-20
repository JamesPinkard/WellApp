using System;
using System.Collections.Generic;
using WellApp.Domain;
using MoreLinq;
using System.Linq;
using System.IO;

namespace WellApp.Repo.Text
{
    public class AquiferTextReport : ITextReport
    {
        List<Aquifer> _aquifers = new List<Aquifer>();
        public void Map(string[] line)
        {
            var aqu = new Aquifer()
            {
                AquiferCode = line[6],
                AquiferCodeDescriprion = line[7],
                AquiferID = Int32.Parse(line[8]),
                AquiferName = line[9]
            };

            _aquifers.Add(aqu);
        }

        public List<Aquifer> GetAll()
        {
            return _aquifers;
        }

        public List<Aquifer> GetUnique()
        {
            return _aquifers.DistinctBy(aq => aq.AquiferID).ToList();
        }

        public bool Validate(string[] line)
        {
            return line.Length == 55;
        }
    }
}
