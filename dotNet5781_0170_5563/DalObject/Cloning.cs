using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using  DO;
namespace Dal
{
    static class Cloning
    {
        internal static IClonable Clone(this IClonable original)
        {
            IClonable target = (IClonable)Activator.CreateInstance(original.GetType());
            //...
            return target;
        }

      
    }
}
