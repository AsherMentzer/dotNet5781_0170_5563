using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//using DO;
namespace DL
{

    static class Cloning
    {
        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject = new T();
            //T copyToObject = (T)Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);

            return copyToObject;
        }

    }
}
