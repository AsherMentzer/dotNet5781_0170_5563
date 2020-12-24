using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineExist : IClonable
    {
        public int LineId{ get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Frequency{ get; set; }
        public TimeSpan EndTime { get; set; }
        
    }
}
