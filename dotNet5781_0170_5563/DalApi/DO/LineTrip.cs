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
    public class LineTrip 
    {
        public int Id { get; set; }
        public int LineId{ get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Frequncy { get; set; }
        public TimeSpan EndTime { get; set; }
        
        //private TimeSpan startTime;
        //private TimeSpan freqTime;
        //private TimeSpan endTime;

        //[XmlIgnore]
        //public TimeSpan StartTime { get { return startTime; } set { startTime = value; } }
        //[XmlElement("StartTime", DataType = "duration")]
        //[DefaultValue("PT10M")]
        //public string STime { get { return XmlConvert.ToString(startTime); } set { startTime = XmlConvert.ToTimeSpan(value); } }

        //[XmlIgnore]
        //public TimeSpan Frequency { get { return freqTime; } set { freqTime = value; } }
        //[XmlElement("Frequency", DataType = "duration")]
        //[DefaultValue("PT10M")]
        //public string FreqTime { get { return XmlConvert.ToString(freqTime); } set { freqTime = XmlConvert.ToTimeSpan(value); } }

        //[XmlIgnore]
        //public TimeSpan EndTime { get { return endTime; } set { endTime = value; } }
        //[XmlElement("EndTime", DataType = "duration")]
        //[DefaultValue("PT10M")]

        //public string ETime { get { return XmlConvert.ToString(endTime); } set { endTime = XmlConvert.ToTimeSpan(value); } }
        
    }
}
