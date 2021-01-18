using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
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
    [Serializable]
    public class BadPairIdException : Exception
    {
        public int station1ID, station2ID,station3ID;

        public BadPairIdException(int lid, int sid) : base() { station1ID = lid; station2ID = sid; }
        public BadPairIdException(int id1, int id2,int id3) : base() { station1ID = id1; station2ID = id2;station3ID = id3; }
        public BadPairIdException(int lid, int sid, string message) :
            base(message)
        { station1ID = lid; station2ID = sid; }


        public BadPairIdException(int lid, int sid, string message, Exception innerException) :
            base(message, innerException)
        { station1ID = lid; station2ID = sid; }

        public override string ToString() => base.ToString() + $", bad first station id: {station1ID}, or bad last station id: {station2ID}";

    }
    [Serializable]
    public class BadUSerNameException : Exception
    {
        public string ID;
        // private string v;

        public BadUSerNameException(string id) : base() => ID = id;
        public BadUSerNameException(string id, string message) :
            base(message) => ID = id;


        public BadUSerNameException(string id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad user name: {ID}";

    }
}


