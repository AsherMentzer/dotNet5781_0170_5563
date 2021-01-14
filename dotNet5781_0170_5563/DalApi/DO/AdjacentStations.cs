using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    [Serializable]
    public class AdjacentStations 
    {
        private TimeSpan time;
        [XmlIgnore]
        public TimeSpan AverageTravleTime { get { return time; } set { time = value; } }
        [XmlElement("AverageTravleTime", DataType = "duration")]
        [DefaultValue("PT10M")]
        public string Time { get { return XmlConvert.ToString(time); } set { time = XmlConvert.ToTimeSpan(value); } }
        public int StationId1 { get; set; }
        public int StationId2 { get; set; }
        public double Distance { get; set; }
        //public TimeSpan AverageTravleTime { get; set; } 
     }
}
