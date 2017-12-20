using WellApp.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellApp.Repo.Text
{
    public class WellTextReport : ITextReport
    {
        List<Well> _wells = new List<Well>();
        public void Map(string[] line)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "yyyy'-'MM'-'dd";

            var well = new Well()
            {
                //LocalId = int.Parse(line[0]),
                StateWellNumber = line[0],
                County = line[1],
                RiverBasin = line[2],
                GMA = int.Parse(line[3]),
                RWPA = line[4],
                GCD = line[5],
                Latitude = double.Parse(line[12]),
                Longitude = double.Parse(line[16]),
                AquiferId = int.Parse(line[8]),                
            };

            if (DateTime.TryParseExact(line[31], format, provider, DateTimeStyles.None, out DateTime drillEndDate))
            {
                well.DrillingEndDate = drillEndDate;
            }
            else
            {
                well.DrillingEndDate = DateTime.Parse("01/01/1901");
            }
            if (DateTime.TryParseExact(line[53], format, provider, DateTimeStyles.None, out DateTime createdDate))
            {
                well.CreatedDate = createdDate;
            }
            else
            {
                well.CreatedDate = DateTime.Parse("01/01/1901");
            }
            if (DateTime.TryParseExact(line[54], format, provider, DateTimeStyles.None, out DateTime lastUpdatedDate))
            {
                well.LastUpdatedDate = lastUpdatedDate;
            }
            else
            {
                well.LastUpdatedDate = DateTime.Parse("01/01/1901");
            }
            if (int.TryParse(line[45],out int trackingNum))
            {
                well.WellReportTrackingNumber = trackingNum;
            }
            if (int.TryParse(line[23], out int wellDepth))
            {
                well.WellDepth = wellDepth;
            }            
            if (int.TryParse(line[25], out int groundSurfaceElevation))
            {
                well.GroundSurfaceElevation = groundSurfaceElevation;
            }

            _wells.Add(well);
        }

        public bool Validate(string[] line)
        {
            return line.Length == 55;
        }

        public List<Well> GetAll()
        {
            return _wells;
        }
    }
}
