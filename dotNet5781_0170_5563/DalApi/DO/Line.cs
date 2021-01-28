using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// enum for the area of the line
    /// </summary>
    public enum Areas { General = 1, North, West, Center, Jerusalem };
   /// <summary>
   /// class with only properties
   /// for defenition of line
   /// </summary>
    public class Line 
    {
        public int LineId { get; set; }
        public int LineNumber { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Areas area { get; set; }
       
    }
}
