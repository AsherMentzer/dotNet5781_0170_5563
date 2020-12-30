using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BadBusLicenceIdException : Exception
    {
        public string ID;
       // private string v;

        public BadBusLicenceIdException(string id) : base() => ID = id;
        public BadBusLicenceIdException(string id, string message) :
            base(message) => ID = id;
      

        public BadBusLicenceIdException(string id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad bus Licence id: {ID}";
      
    }

    [Serializable]
    public class BadStationIdException : Exception
    {
        public int ID;
       
        public BadStationIdException(int id) : base() => ID = id;
        public BadStationIdException(int id, string message) :
            base(message) => ID = id;


        public BadStationIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad station id: {ID}";

    }

    [Serializable]
    public class BadBusLineIdException : Exception
    {
        public int ID;
        // private string v;

        public BadBusLineIdException(int id) : base() => ID = id;
        public BadBusLineIdException(int id, string message) :
            base(message) => ID = id;


        public BadBusLineIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad bus Line id: {ID}";

    }

    [Serializable]
    public class BadLineExistException : Exception
    {
        public int ID;

        public BadLineExistException(int id) : base() => ID = id;
        public BadLineExistException(int id, string message) :
            base(message) => ID = id;


        public BadLineExistException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad bus Line id: {ID}";

    }

}
