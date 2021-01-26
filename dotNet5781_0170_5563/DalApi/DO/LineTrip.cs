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
    //public class LineTrip
    //{
    //    //public int Id { get; set; }
    //    public int LineId { get; set; }
    //    public TimeSpan StartTime { get; set; }
    //    //public TimeSpan Frequncy { get; set; }
    //    //public TimeSpan EndTime { get; set; }



    //}
    public class LineTrip
    {
        public int Id { get; set; }
        public int LineId { get; set; }

        private TimeSpan startTime;
        //private TimeSpan freqTime;
        //private TimeSpan endTime;

        [XmlIgnore]
        public TimeSpan StartTime { get { return startTime; } set { startTime = value; } }
        [XmlElement("starttime", DataType  = "duration")]
        [DefaultValue("pt10m")]
        public string Stime { get { return XmlConvert.ToString(startTime); } set { startTime = XmlConvert.ToTimeSpan(value); } }

        //[xmlignore]
        //public timespan frequency { get { return freqtime; } set { freqtime = value; } }
        //[xmlelement("frequency", datatype = "duration")]
        //[defaultvalue("pt10m")]
        //public string freqtime { get { return xmlconvert.tostring(freqtime); } set { freqtime = xmlconvert.totimespan(value); } }

        //[xmlignore]
        //public timespan endtime { get { return endtime; } set { endtime = value; } }
        //[xmlelement("endtime", datatype = "duration")]
        //[defaultvalue("pt10m")]

        //public string etime { get { return xmlconvert.tostring(endtime); } set { endtime = xmlconvert.totimespan(value); } }

    }
}