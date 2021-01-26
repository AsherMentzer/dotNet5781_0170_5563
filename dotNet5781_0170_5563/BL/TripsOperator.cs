using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TripsOperator
    {
        #region singelton
        static readonly TripsOperator instance = new TripsOperator();
        static TripsOperator() { }// static ctor to ensure instance init is done just before first usage
        TripsOperator() { } // default => private
        public static TripsOperator Instance { get => instance; }// The public Instance property to use
        #endregion

        //public EventHandler 
    }
}
