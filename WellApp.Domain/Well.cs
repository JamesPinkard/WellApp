using System;
using System.ComponentModel.Design;

namespace WellApp.Domain
{
    public class Well
    {        
        public int WellId { get; set; }
        public string StateWellNumber { get; set; }        
        public string County { get; set; }
        public string RiverBasin { get; set; }
        public int GMA { get; set; }
        public string GCD { get; set; }
        public string RWPA { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int WellDepth { get; set; }
        public int GroundSurfaceElevation { get; set; }
        public DateTime DrillingEndDate { get; set; }
        public int WellReportTrackingNumber { get; set; }
        public Aquifer Aquifer { get; set; }
        public int AquiferId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
