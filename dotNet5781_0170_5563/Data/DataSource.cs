using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DO;

namespace Data
{
    public static class DataSource
    {
        public static List<Bus> buses;
        public static List<User> users;
        public static List<Station> stations = new List<Station>();
        public static List<Line> lines;
        public static int linesId = 1;
        public static List<AdjacentStations> pairs;
        public static List<StationLine> stationsLine;
        public static List<BusOnTrip> travelBuses;
        public static List<LineTrip> linesTrip;
        static List<int> serialNumbers = new List<int>();

        static DataSource()
        {
            string BusPath = @"BusXml.xml"; //XElement
            string UserPath = @"UserXml.xml"; //XElement
            string LinePath = @"LineXml.xml"; //XElement
            string StationPath = @"StationXml.xml"; //XElement
            //string UserPath = @"UserXml.xml"; //XElement
            //string BusOnTripPath = @"BusOnTripXml.xml"; //XElement
            string AdjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
            //string LineTripPath = @"LineTripXml.xml"; //XElement
           // string TripPath = @"TripXml.xml"; //XElement
            string StationLinePath = @"LineStationXml.xml"; //XElement 
            string SerialNumbers = @"SerialNumbersXml.xml";
            string LineTripPath = @"LineTripXml.xml";
            InitialAllList();
            XMLTools.SaveListToXMLSerializer<Bus>(buses, BusPath);
            XMLTools.SaveListToXMLSerializer<User>(users, UserPath);
            XMLTools.SaveListToXMLSerializer<Station>(stations, StationPath);
            XMLTools.SaveListToXMLSerializer<Line>(lines, LinePath);
            XMLTools.SaveListToXMLSerializer<StationLine>(stationsLine, StationLinePath);
            XMLTools.SaveListToXMLSerializer<AdjacentStations>(pairs, AdjacentStationsPath);
            XMLTools.SaveListToXMLSerializer<int>(serialNumbers, SerialNumbers);
            XMLTools.SaveListToXMLSerializer<LineTrip>(linesTrip, LineTripPath);
        }
        static void InitialAllList()
        {
            #region buses
            buses = new List<Bus>();
            for (int i = 90; i > 60; --i)
            {
                Bus bus = new Bus { LicenseId = "123456" + i, status = Status.ready, Fuel = 1200, Killometrage = 0, ActiveDate = new DateTime(2020, 1, i % 30 + 1) };
                buses.Add(bus);
            }



            buses.Add(new Bus
            {
                LicenseId = "87654321",
                ActiveDate = new DateTime(2018, 5, 22),
                Fuel = 20,
                Killometrage = 99990,
                status = Status.ready
            });


            buses.Add(new Bus
            {
                LicenseId = "98765432",
                ActiveDate = new DateTime(2018, 2, 10),
                Fuel = 1200,
                Killometrage = 100000,
                status = Status.needFix
            });
            #endregion

            #region ststions
            int[] idArr = new int[] { 84,103,173,176,277,317,334,525,645,664,666,1002,1171,1352,1601,1625,1727,1927,2524,2539,
                2583,2946,3287,3315,3374,3633,3821,3855,4075,4116,4156,4216,4218,5060,5062,5602,5666,6109,
                6248,9937,20090,20163,21109,21370,21558,21616,22273,22942,22980,23064,25114,26416,26544,26946,
                31582,32247,32267,32269,32270,32274,32276,32376,32411,32779,36709,36780,38315,38317 };
            double[] lonArr = new double[] {35.209791,35.214106,35.195787,35.192721,35.188178,35.189665,35.201537,35.223334,35.197526,
                35.2156,35.219221,35.183006,35.220651,35.223655,35.215658,35.22077,35.207975,35.231163,35.181048,35.1969,35.234681,35.21895,35.186019,
                35.216946,35.200768,35.217353,35.212823,35.216372,35.208029,35.208542,35.202593,35.199532,35.202644,35.216855,
                35.21149,35.145723,35.213308,35.202487,35.219594,35.205389,34.834567,34.827959,34.833799,34.831208,34.833839,
                34.836024,34.822094,34.843553,34.825758,34.836363,34.845151,34.829253,34.834218,34.82545,34.882211,34.881445,34.857857,
                34.867399,34.854627,34.850598,34.847907,34.869311,34.886288,34.876335,34.87814,34.88653,34.868576,34.876071};
            double[] lanArr = new double[] { 31.790758, 31.8, 31.791204, 31.792105, 31.78751, 31.818019, 31.816595, 31.7591, 31.790348,
                31.799833, 31.796452, 31.772776, 31.792065, 31.798562, 31.784994, 31.795967, 31.792259, 31.799996, 31.787175,
                31.787352, 31.838017, 31.773999, 31.774226, 31.746158, 31.784519, 31.788651, 31.800142, 31.781537, 31.79454,
                31.793164, 31.788603, 31.787171, 31.787704, 31.794101, 31.799615, 31.798621, 31.799489, 31.789082, 31.796305,
                31.792123, 32.096519, 32.102516, 32.054798, 32.091484, 32.075695, 32.092587, 32.093343, 32.074823, 32.091435,
                32.085267, 32.091267, 32.091396, 32.092321, 32.098763, 32.085236, 32.08561, 32.092094, 32.091208, 32.092397,
                32.092462, 32.092361, 32.085554, 32.075781, 32.098998, 32.08663, 32.094938, 32.088538, 32.097637};
            string[] addArr = new string[] { "מלכי ישראל/הטורים", "גולדה/הרטום", "גבעת שאול/כתב סופר", "גבעת שאול/נג'ארה",
                "מרכז שטנר/כנפי נשרים", "רקאנטי/אידלזון", "שיבת ציון/שירת הים", "דרך חברון/אסתר המלכה", "ויצמן/גבעת שאול",
                "ישיבת חורב/שדרות גולדה מאיר", "שמואל הנביא/בר אילן", "בית וגן/שדרות הרצל", "יחזקאל/רבנו גרשום",
                "שד' אשכול/משמר הגבול", "ככר הדוידקה/הנביאים", "חטיבת הראל/שמואל הנביא", "ירמיהו/שמגר", "שד' אשכול/בר לב",
                "מסוף אגד/כנפי נשרים", "מרכז הרב/הרב צבי יהודה", "שדרות נווה יעקב/משה דיין", "קרן היסוד/אחד העם",
                "בי''ח שערי צדק/בייט", "דרך חברון/הסדנא", "ביטוח לאומי", "ככר השבת/מלכי ישראל", "מסוף אגד/הר חוצבים",
                "המלך ג'ורג'/בן יהודה", "רב שפע/שמגר", "שמגר/ירמיהו", "ת. מרכזית ירושלים/יפו", "גשר המיתרים/שד' הרצל",
                "בנייני האומה/שז''ר", "בר אילן/רבנו גרשום", "קרית מדע/גולדה", "מחלף הראל", "שפע חיים/שדרות גולדה מאיר",
                "ת. מרכזית ירושלים/הורדה", "שמואל הנביא/הרב בלוי", "ירמיהו/אהליאב", "אברבנאל/המכבים", "אזור התעשייה",
                "כביש 4/מחלף אלוף שדה", "דרך ז'בוטינסקי/אבן עזרא", "האדמור מנדבורנא/חזון אי''ש", "דרך ז'בוטינסקי/הרב שך",
                "מגדל קונקורד/דרך בן גוריון", "מחלף גבעת שמואל/כביש 4", "דרך ז'בוטינסקי/רבי עקיבא", "חזון אי''ש/רבי עקיבא",
                "מחלף גהה/כביש 4", "דרך ז'בוטינסקי/דב גרונר", "דרך ז'בוטינסקי/סוקולוב", "קניון איילון","רוטשילד / פרנקפורטר",
                "דוד פרנקפורטר/רוטשילד", "דרך זאב ז'בוטינסקי/הסיבים", "בי''ח בלינסון/ז'בוטינסקי", "ז'בוטינסקי/אלברט איינשטיין",
                "דרך זאב ז'בוטינסקי/היצירה", "מחלף גהה/ז'בוטינסקי", "ארלוזורוב/צה''ל", "מיכל פינס / רוטשילד", "קניון אם מושבות",
                "טרומפלדור/פיק''א", "ת. מרכזית פ''ת/רציפים עירוני", "בית חולים שניידר", "חנן לויתן/ברוריה בת תרדיון" };

            for (int i = 0; i < idArr.Length; ++i)
            {
                stations.Add(new Station { StationId = idArr[i], Longitude = lonArr[i], Latitude = lanArr[i], StationName = addArr[i] });
            }

            #endregion
            #region lines
            lines = new List<Line>
            {
                new Line//1
                {
                    LineId=linesId++,
                    LineNumber=66 ,
                    FirstStation= 22980,
                    LastStation=36780,
                    area=Areas.Center,
                },
                new Line///2
                {
                    LineId=linesId++,
                    LineNumber=82 ,
                    FirstStation= 22980,
                    LastStation=32411,
                    area=Areas.Center,
                },
                new Line//3
                {
                    LineId=linesId++,
                    LineNumber=51 ,
                    FirstStation= 22980,
                    LastStation=32799,
                    area=Areas.Center,
                },
                new Line//4
                {
                    LineId=linesId++,
                    LineNumber=400 ,
                    FirstStation= 20163,
                    LastStation=6109,
                    area=Areas.General,
                },
                new Line//5
                {
                    LineId=linesId++,
                    LineNumber=402 ,
                    FirstStation= 26946,
                    LastStation=5666,
                    area=Areas.General,
                },
                new Line//6
                {
                    LineId=linesId++,
                    LineNumber=59,
                    FirstStation= 3374,
                    LastStation=2583,
                    area=Areas.Jerusalem,
                },
                 new Line//7
                {
                    LineId=linesId++,
                    LineNumber=64,
                    FirstStation= 2524,
                    LastStation=317,
                    area=Areas.Jerusalem,
                },
                  new Line//8
                {
                    LineId=linesId++,
                    LineNumber=69,
                    FirstStation= 2524,
                    LastStation=2583,
                    area=Areas.Jerusalem,
                },
                   new Line///9
                {
                    LineId=linesId++,
                    LineNumber=74,
                    FirstStation= 2524,
                    LastStation=3315,
                    area=Areas.Jerusalem,
                },
                    new Line///10
                {
                    LineId=linesId++,
                    LineNumber=39,
                    FirstStation= 1002,
                    LastStation=3821,
                    area=Areas.Jerusalem,
                },
            };
            #endregion
            #region station line
            stationsLine = new List<StationLine>
            {
                #region center
                new StationLine{LineId=1,StationId=22980,NumInLine=1},
                new StationLine{LineId=1,StationId=26416,NumInLine=2},
                new StationLine{LineId=1,StationId=26544,NumInLine=3},
                new StationLine{LineId=1,StationId=32276,NumInLine=4},
                new StationLine{LineId=1,StationId=32270,NumInLine=5},
                new StationLine{LineId=1,StationId=32267,NumInLine=6},
                new StationLine{LineId=1,StationId=32269,NumInLine=7},
                new StationLine{LineId=1,StationId=36709,NumInLine=8},
                new StationLine{LineId=1,StationId=32247,NumInLine=9},
                new StationLine{LineId=1,StationId=36780,NumInLine=10},

                new StationLine{LineId=2,StationId=22980,NumInLine=1},
                new StationLine{LineId=2,StationId=26416,NumInLine=2},
                new StationLine{LineId=2,StationId=26544,NumInLine=3},
                new StationLine{LineId=2,StationId=32276,NumInLine=4},
                new StationLine{LineId=2,StationId=32274,NumInLine=5},
                new StationLine{LineId=2,StationId=32267,NumInLine=6},
                new StationLine{LineId=2,StationId=38315,NumInLine=7},
                new StationLine{LineId=2,StationId=32376,NumInLine=8},
                new StationLine{LineId=2,StationId=31582,NumInLine=9},
                new StationLine{LineId=2,StationId=32411,NumInLine=10},

                new StationLine{LineId=3,StationId=22980,NumInLine=1},
                new StationLine{LineId=3,StationId=26416,NumInLine=2},
                new StationLine{LineId=3,StationId=21370,NumInLine=3},
                new StationLine{LineId=3,StationId=26544,NumInLine=4},
                new StationLine{LineId=3,StationId=32276,NumInLine=5},
                new StationLine{LineId=3,StationId=32270,NumInLine=6},
                new StationLine{LineId=3,StationId=32267,NumInLine=7},
                new StationLine{LineId=3,StationId=32269,NumInLine=8},
                new StationLine{LineId=3,StationId=38317,NumInLine=9},
                new StationLine{LineId=3,StationId=32779,NumInLine=10},
                #endregion

                #region general
                new StationLine{LineId=4,StationId=20163,NumInLine=1},
                new StationLine{LineId=4,StationId=22273,NumInLine=2},
                new StationLine{LineId=4,StationId=22980,NumInLine=3},
                new StationLine{LineId=4,StationId=21370,NumInLine=4},
                new StationLine{LineId=4,StationId=21616,NumInLine=5},
                new StationLine{LineId=4,StationId=25114,NumInLine=6},
                new StationLine{LineId=4,StationId=22942,NumInLine=7},
                new StationLine{LineId=4,StationId=21109,NumInLine=8},
                new StationLine{LineId=4,StationId=5602,NumInLine=9},
                new StationLine{LineId=4,StationId=6109,NumInLine=10},

                new StationLine{LineId=5,StationId=26946,NumInLine=1},
                new StationLine{LineId=5,StationId=20090,NumInLine=2},
                new StationLine{LineId=5,StationId=23064,NumInLine=3},
                new StationLine{LineId=5,StationId=21558,NumInLine=4},
                new StationLine{LineId=5,StationId=22942,NumInLine=5},
                new StationLine{LineId=5,StationId=645,NumInLine=6},
                new StationLine{LineId=5,StationId=4218,NumInLine=7},
                new StationLine{LineId=5,StationId=4116,NumInLine=8},
                new StationLine{LineId=5,StationId=6248,NumInLine=9},
                new StationLine{LineId=5,StationId=5666,NumInLine=10},
                #endregion
                #region jerusalem
                new StationLine{LineId=6,StationId=3374,NumInLine=1},
                new StationLine{LineId=6,StationId=4216,NumInLine=2},
                new StationLine{LineId=6,StationId=4075,NumInLine=3},
                new StationLine{LineId=6,StationId=84,NumInLine=4},
                new StationLine{LineId=6,StationId=3633,NumInLine=5},
                new StationLine{LineId=6,StationId=5060,NumInLine=6},
                new StationLine{LineId=6,StationId=1625,NumInLine=7},
                new StationLine{LineId=6,StationId=1352,NumInLine=8},
                new StationLine{LineId=6,StationId=1927,NumInLine=9},
                new StationLine{LineId=6,StationId=2583,NumInLine=10},

                new StationLine{LineId=7,StationId=2524,NumInLine=1},
                new StationLine{LineId=7,StationId=277,NumInLine=2},
                new StationLine{LineId=7,StationId=173,NumInLine=3},
                new StationLine{LineId=7,StationId=9937,NumInLine=4},
                new StationLine{LineId=7,StationId=5060,NumInLine=5},
                new StationLine{LineId=7,StationId=666,NumInLine=6},
                new StationLine{LineId=7,StationId=103,NumInLine=7},
                new StationLine{LineId=7,StationId=5062,NumInLine=8},
                new StationLine{LineId=7,StationId=334,NumInLine=9},
                new StationLine{LineId=7,StationId=317,NumInLine=10},

                new StationLine{LineId=8,StationId=2524,NumInLine=1},
                new StationLine{LineId=8,StationId=277,NumInLine=2},
                new StationLine{LineId=8,StationId=176,NumInLine=3},
                new StationLine{LineId=8,StationId=173,NumInLine=4},
                new StationLine{LineId=8,StationId=9937,NumInLine=5},
                new StationLine{LineId=8,StationId=1727,NumInLine=6},
                new StationLine{LineId=8,StationId=5060,NumInLine=7},
                new StationLine{LineId=8,StationId=1625,NumInLine=8},
                new StationLine{LineId=8,StationId=1927,NumInLine=9},
                new StationLine{LineId=8,StationId=2583,NumInLine=10},

                new StationLine{LineId=9,StationId=2524,NumInLine=1},
                new StationLine{LineId=9,StationId=277,NumInLine=2},
                new StationLine{LineId=9,StationId=2539,NumInLine=3},
                new StationLine{LineId=9,StationId=4216,NumInLine=4},
                new StationLine{LineId=9,StationId=4156,NumInLine=5},
                new StationLine{LineId=9,StationId=1601,NumInLine=6},
                new StationLine{LineId=9,StationId=3855,NumInLine=7},
                new StationLine{LineId=9,StationId=2946,NumInLine=8},
                new StationLine{LineId=9,StationId=525,NumInLine=9},
                new StationLine{LineId=9,StationId=3315,NumInLine=10},

                new StationLine{LineId=10,StationId=1002,NumInLine=1},
                new StationLine{LineId=10,StationId=3287,NumInLine=2},
                new StationLine{LineId=10,StationId=3374,NumInLine=3},
                new StationLine{LineId=10,StationId=4216,NumInLine=4},
                new StationLine{LineId=10,StationId=4218,NumInLine=5},
                new StationLine{LineId=10,StationId=84,NumInLine=6},
                new StationLine{LineId=10,StationId=3633,NumInLine=7},
                new StationLine{LineId=10,StationId=1171,NumInLine=8},
                new StationLine{LineId=10,StationId=664,NumInLine=9},
                new StationLine{LineId=10,StationId=3821,NumInLine=10}
              

                #endregion
            };
            #endregion

            #region pairs
            pairs = new List<AdjacentStations>
            {
                //39
            new AdjacentStations{ StationId1 = 1002, StationId2 = 3287, Distance = 1.1, AverageTravleTime = new TimeSpan(0,5,0) },
            new AdjacentStations{ StationId1 = 3287, StationId2 = 3374, Distance = 2.7, AverageTravleTime = new TimeSpan(0,16,0) },
            new AdjacentStations{ StationId1 = 3374, StationId2 = 4216, Distance = 0.3, AverageTravleTime = new TimeSpan(0,2,0) },
            new AdjacentStations{ StationId1 = 4216, StationId2 = 4218, Distance = 0.4, AverageTravleTime = new TimeSpan(0,2,0) },
            new AdjacentStations{ StationId1 = 4218, StationId2 = 84, Distance = 0.8, AverageTravleTime = new TimeSpan(0,3,0) },
            new AdjacentStations{ StationId1 = 83, StationId2 = 3633, Distance = 0.8, AverageTravleTime = new TimeSpan(0,4,0) },
            new AdjacentStations{ StationId1 = 3633, StationId2 = 1171, Distance = 0.6, AverageTravleTime = new TimeSpan(0,2,0) },
            new AdjacentStations{ StationId1 = 1171, StationId2 = 664, Distance = 2.8, AverageTravleTime = new TimeSpan(0,13,0) },
            new AdjacentStations{ StationId1 = 664, StationId2 = 3821, Distance = 0.8, AverageTravleTime = new TimeSpan(0,3,0) },
            
            // 59
            new AdjacentStations{ StationId1 = 3374, StationId2 = 4216, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 4216, StationId2 = 4075, Distance = 3.7, AverageTravleTime = new TimeSpan(0,14,0) },
            new AdjacentStations{ StationId1 = 4075, StationId2 = 84, Distance = 0.5, AverageTravleTime = new TimeSpan(0,2,0) },
            new AdjacentStations{ StationId1 = 84, StationId2 = 3633, Distance = 0.8, AverageTravleTime = new TimeSpan(0,3,0) },
            new AdjacentStations{ StationId1 = 3633, StationId2 = 5060, Distance = 1.1, AverageTravleTime = new TimeSpan(0,4,0) },
            new AdjacentStations{ StationId1 = 5060, StationId2 = 1625, Distance = 0.4, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 1625, StationId2 = 1352, Distance = 2.4, AverageTravleTime = new TimeSpan(0,2,0) },
            new AdjacentStations{ StationId1 = 1352, StationId2 = 1927, Distance = 0.6, AverageTravleTime = new TimeSpan(0,3,0) },
            new AdjacentStations{ StationId1 = 1927, StationId2 = 2583, Distance = 5.3, AverageTravleTime = new TimeSpan(0,20,0) },
            
            // 64
                new AdjacentStations{ StationId1 = 2524, StationId2 = 277, Distance = 0.6, AverageTravleTime = new TimeSpan(0,2,0) },
                new AdjacentStations{ StationId1 = 277, StationId2 = 173, Distance = 1.2, AverageTravleTime = new TimeSpan(0,5,0) },
                new AdjacentStations{ StationId1 = 173, StationId2 = 9937, Distance = 2.2, AverageTravleTime = new TimeSpan(0,5,0) },
                new AdjacentStations{ StationId1 = 9937, StationId2 = 5060, Distance = 1.1, AverageTravleTime = new TimeSpan(0,5,0) },
                new AdjacentStations{ StationId1 = 5060, StationId2 = 666, Distance = 0.5, AverageTravleTime = new TimeSpan(0,1,0) },
                new AdjacentStations{ StationId1 = 666, StationId2 = 103, Distance = 0.6, AverageTravleTime = new TimeSpan(0,3,0) },
                new AdjacentStations{ StationId1 = 103, StationId2 = 5062, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
                new AdjacentStations{ StationId1 = 5062, StationId2 = 334, Distance = 6.4, AverageTravleTime = new TimeSpan(0,13,0) },
                new AdjacentStations{ StationId1 = 334, StationId2 = 317, Distance = 3.7, AverageTravleTime = new TimeSpan(0,9,0) },
                //69
                new AdjacentStations{ StationId1 = 2524, StationId2 = 277, Distance = 0.6, AverageTravleTime = new TimeSpan(0,2,0) },
                new AdjacentStations{ StationId1 = 277, StationId2 = 176, Distance = 0.9, AverageTravleTime = new TimeSpan(0,4,0) },
                new AdjacentStations{ StationId1 = 176, StationId2 = 173, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
                new AdjacentStations{ StationId1 = 173, StationId2 = 9937, Distance = 1.2, AverageTravleTime = new TimeSpan(0,5,0) },
                new AdjacentStations{ StationId1 = 9937, StationId2 = 1727, Distance = 0.2, AverageTravleTime = new TimeSpan(0,1,0) },
                new AdjacentStations{ StationId1 = 1727, StationId2 = 5060, Distance = 0.9, AverageTravleTime = new TimeSpan(0,3,0) },
                new AdjacentStations{ StationId1 = 5060, StationId2 = 1625, Distance = 0.4, AverageTravleTime = new TimeSpan(0,2,0) },
                new AdjacentStations{ StationId1 = 1625, StationId2 = 1927, Distance = 1.2, AverageTravleTime = new TimeSpan(0,5,0) },
                new AdjacentStations{ StationId1 = 1927, StationId2 = 2583, Distance = 5.4, AverageTravleTime = new TimeSpan(0,19,0) },

                //74
                //new PairOfConsecutiveStation{ StationId1 = 2524, StationId2 = 277, Distance = 0.6, AverageTravleTime = new TimeSpan(0,2,0) },
                new AdjacentStations{ StationId1 = 277, StationId2 = 2539, Distance = 0.8, AverageTravleTime = new TimeSpan(0,4,0) },
                new AdjacentStations{ StationId1 = 2539, StationId2 = 4216, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },///////4215/4216
                new AdjacentStations{ StationId1 = 4216, StationId2 = 4156, Distance = 0.5, AverageTravleTime = new TimeSpan(0,2,0) },
                new AdjacentStations{ StationId1 = 4156, StationId2 = 1601, Distance = 1.8, AverageTravleTime = new TimeSpan(0,7,0) },
                new AdjacentStations{ StationId1 = 1601, StationId2 = 3855, Distance = 0.7, AverageTravleTime = new TimeSpan(0,3,0) },
                new AdjacentStations{ StationId1 = 3855, StationId2 = 2946, Distance = 0.9, AverageTravleTime = new TimeSpan(0,4,0) },
                new AdjacentStations{ StationId1 = 2946, StationId2 = 525, Distance = 2.1, AverageTravleTime = new TimeSpan(0,8,0) },
                new AdjacentStations{ StationId1 = 525, StationId2 = 3315, Distance = 1.6, AverageTravleTime = new TimeSpan(0,7,0) },

                // 51
            new AdjacentStations{ StationId1 = 22980, StationId2 = 26416, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 26416, StationId2 = 21370, Distance = 0.2, AverageTravleTime = new TimeSpan(0,0,32) },
            new AdjacentStations{ StationId1 = 21370, StationId2 = 26544, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 26544, StationId2 = 32276, Distance = 1.3, AverageTravleTime = new TimeSpan(0,5,0) },
            new AdjacentStations{ StationId1 = 32276, StationId2 = 32270, Distance = 0.6, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 32270, StationId2 = 32267, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 32267, StationId2 = 32269, Distance = 1, AverageTravleTime = new TimeSpan(0,3,0) },
            new AdjacentStations{ StationId1 = 32269, StationId2 = 38317, Distance = 1.4, AverageTravleTime = new TimeSpan(0,4,0) },
            new AdjacentStations{ StationId1 = 38317, StationId2 = 32779, Distance = 0.2, AverageTravleTime = new TimeSpan(0,0,45) },////////////////check 32779 ot 32799
                // 66
            //new PairOfConsecutiveStation{ StationId1 = 22980, StationId2 = 26416, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 26416, StationId2 = 26544, Distance = 0.5, AverageTravleTime = new TimeSpan(0,2,0) },
            new AdjacentStations{ StationId1 = 26544, StationId2 = 32276, Distance = 1.3, AverageTravleTime = new TimeSpan(0,3,0) },
            //new PairOfConsecutiveStation{ StationId1 = 26544, StationId2 = 32276, Distance = 1.3, AverageTravleTime = new TimeSpan(0,5,0) },
            //new PairOfConsecutiveStation{ StationId1 = 32276, StationId2 = 32270, Distance = 0.6, AverageTravleTime = new TimeSpan(0,1,0) },
            //new PairOfConsecutiveStation{ StationId1 = 32270, StationId2 = 32267, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            //new PairOfConsecutiveStation{ StationId1 = 32267, StationId2 = 32269, Distance = 1, AverageTravleTime = new TimeSpan(0,3,0) },
            new AdjacentStations{ StationId1 = 32269, StationId2 = 36709, Distance = 1.3, AverageTravleTime = new TimeSpan(0,4,0) },
            new AdjacentStations{ StationId1 = 36709, StationId2 = 32247, Distance = 0.5, AverageTravleTime = new TimeSpan(0,1,45) },
            new AdjacentStations{ StationId1 = 32247, StationId2 = 36780, Distance = 0.5, AverageTravleTime = new TimeSpan(0,2,45) },/////////////////////update and check

            // 82
            //new PairOfConsecutiveStation{ StationId1 = 22980, StationId2 = 26416, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            //new PairOfConsecutiveStation{ StationId1 = 26416, StationId2 = 21370, Distance = 0.2, AverageTravleTime = new TimeSpan(0,0,32) },
            //new PairOfConsecutiveStation{ StationId1 = 21370, StationId2 = 26544, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 32276, StationId2 = 32274, Distance = 0.3, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 32274, StationId2 = 32267, Distance = 0.6, AverageTravleTime = new TimeSpan(0,2,0) },
            new AdjacentStations{ StationId1 = 32267, StationId2 = 38315, Distance = 1.4, AverageTravleTime = new TimeSpan(0,3,0) },
            new AdjacentStations{ StationId1 = 38315, StationId2 = 32376, Distance = 0.4, AverageTravleTime = new TimeSpan(0,1,0) },
            new AdjacentStations{ StationId1 = 32376, StationId2 = 31582, Distance = 1.3, AverageTravleTime = new TimeSpan(0,4,0) },
            new AdjacentStations{ StationId1 = 31582, StationId2 = 32411, Distance = 1.2, AverageTravleTime = new TimeSpan(0,3,37) },
            
            // 400
            new AdjacentStations { StationId1 = 20163, StationId2 = 22273, Distance = 1.6, AverageTravleTime = new TimeSpan(0, 6, 0) },////////check 2273 or 22273
            new AdjacentStations { StationId1 = 22273, StationId2 = 22980, Distance = 0.6, AverageTravleTime = new TimeSpan(0, 2, 32) },////
            new AdjacentStations { StationId1 = 22980, StationId2 = 21370, Distance = 0.5, AverageTravleTime = new TimeSpan(0, 2, 0) },
            new AdjacentStations { StationId1 = 21370, StationId2 = 21616, Distance = 0.5, AverageTravleTime = new TimeSpan(0, 1, 0) },
            new AdjacentStations { StationId1 = 21616, StationId2 = 25114, Distance = 0.9, AverageTravleTime = new TimeSpan(0, 2, 0) },
            new AdjacentStations { StationId1 = 25114, StationId2 = 22942, Distance = 1.9, AverageTravleTime = new TimeSpan(0, 1, 30) },
            new AdjacentStations { StationId1 = 22942, StationId2 = 21109, Distance = 2.6, AverageTravleTime = new TimeSpan(0, 5, 0) },
            new AdjacentStations { StationId1 = 21109, StationId2 = 5602, Distance = 51.5, AverageTravleTime = new TimeSpan(0, 36, 0) },
            new AdjacentStations { StationId1 = 5602, StationId2 = 6109, Distance = 6.7, AverageTravleTime = new TimeSpan(0, 12, 45) },
               
            // 402
            new AdjacentStations { StationId1 = 26946, StationId2 = 20090, Distance = 1.3, AverageTravleTime = new TimeSpan(0, 7, 0) },///20090/20092
            new AdjacentStations { StationId1 = 20090, StationId2 = 23064, Distance = 0.9, AverageTravleTime = new TimeSpan(0, 6, 32) },///20090/20092
            new AdjacentStations { StationId1 = 23064, StationId2 = 21558, Distance = 1.1, AverageTravleTime = new TimeSpan(0, 7, 0) },
            new AdjacentStations { StationId1 = 21558, StationId2 = 22942, Distance = 1.8, AverageTravleTime = new TimeSpan(0, 2, 0) },
            new AdjacentStations { StationId1 = 22942, StationId2 = 645, Distance = 59.6, AverageTravleTime = new TimeSpan(0, 19, 0) },
            new AdjacentStations { StationId1 = 645, StationId2 = 4218, Distance = 0.6, AverageTravleTime = new TimeSpan(0, 1, 30) },
            new AdjacentStations { StationId1 = 4218, StationId2 = 4116, Distance = 1.3, AverageTravleTime = new TimeSpan(0, 1, 0) },
            new AdjacentStations { StationId1 = 4116, StationId2 = 6248, Distance = 2, AverageTravleTime = new TimeSpan(0, 3, 0) },
            new AdjacentStations { StationId1 = 6248, StationId2 = 5666, Distance = 1.1, AverageTravleTime = new TimeSpan(0, 2, 45) },
              };
            #endregion


            #region Users
            users = new List<User>
            {
               new User
               {
                   UserName="Asher",
                   Password="1111",
                   isAdmin=true
               },
               new User
               {
                   UserName="Israel",
                   Password="0000",
                   isAdmin=true
               },
               new User
               {
                   UserName="1",
                   Password="1",
                   isAdmin=true,
               }
            };
            #endregion
            #region LineTrip
            linesTrip = new List<LineTrip>()
            {
                new LineTrip
                {
                    LineId=1,
                    StartTime=new TimeSpan(13,0,0)
                },
                new LineTrip
                {
                    LineId=1,
                    StartTime=new TimeSpan(13,5,0)
                },
                new LineTrip
                {
                    LineId=1,
                    StartTime=new TimeSpan(13,10,0)
                },
                new LineTrip
                {
                    LineId=2,
                    StartTime=new TimeSpan(13,2,0)
                },
                    new LineTrip
                {
                    LineId=3,
                    StartTime=new TimeSpan(13,3,0)
                },
            };
            #endregion
        }
    }
}
