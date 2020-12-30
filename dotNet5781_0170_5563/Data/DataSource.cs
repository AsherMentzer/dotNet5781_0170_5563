using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Data
{
    public static class DataSource
    {
        public static List<Bus> buses;
        public static List<Station> stations;
        public static List<BusLine> lines;
        public static int linesId = 1;
        public static List<PairOfConsecutiveStation> pairs;
        public static List<StationLine> stationsLine;
        public static List<TravelBus> travelBuses;
        public static List<LineExist> linesExists;
        static DataSource()
        {
            InialAllList();
        }
        static void InialAllList()
        {
            #region buses
            buses = new List<Bus>();
            for (int i = 90; i > 60; ++i)
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
                "מחלף גהה/כביש 4", "דרך ז'בוטינסקי/דב גרונר", "דרך ז'בוטינסקי/סוקולוב", "קניון איילון, רוטשילד / פרנקפורטר",
                "דוד פרנקפורטר/רוטשילד", "דרך זאב ז'בוטינסקי/הסיבים", "בי''ח בלינסון/ז'בוטינסקי", "ז'בוטינסקי/אלברט איינשטיין",
                "דרך זאב ז'בוטינסקי/היצירה", "מחלף גהה/ז'בוטינסקי", "ארלוזורוב/צה''ל", "מיכל פינס / רוטשילד", "קניון אם מושבות",
                "טרומפלדור/פיק''א", "ת. מרכזית פ''ת/רציפים עירוני", "בית חולים שניידר", "חנן לויתן/ברוריה בת תרדיון" };
            for (int i = idArr.Length; i > 0; --i)
            {
                stations.Add(new Station { StationId = idArr[i], Longitude = lonArr[i], Latitude = lanArr[i], StationName = addArr[i] });
            }

            #endregion
            #region lines
            lines = new List<BusLine>
            {
                new BusLine
                {
                    LineId=linesId++,
                    LineNumber=66 ,
                    FirstStation= 22980,
                    LastStation=36780,
                    area=Areas.Center,
                },
                new BusLine
                {
                    LineId=linesId++,
                    LineNumber=82 ,
                    FirstStation= 22980,
                    LastStation=32411,
                    area=Areas.Center,
                },
                new BusLine
                {
                    LineId=linesId++,
                    LineNumber=51 ,
                    FirstStation= 22980,
                    LastStation=32799,
                    area=Areas.Center,
                },
                new BusLine
                {
                    LineId=linesId++,
                    LineNumber=400 ,
                    FirstStation= 20163,
                    LastStation=6109,
                    area=Areas.General,
                },
                new BusLine
                {
                    LineId=linesId++,
                    LineNumber=402 ,
                    FirstStation= 26946,
                    LastStation=5666,
                    area=Areas.General,
                },
                new BusLine
                {
                    LineId=linesId++,
                    LineNumber=59,
                    FirstStation= 3374,
                    LastStation=2583,
                    area=Areas.Jerusalem,
                },
                 new BusLine
                {
                    LineId=linesId++,
                    LineNumber=64,
                    FirstStation= 2524,
                    LastStation=317,
                    area=Areas.Jerusalem,
                },
                  new BusLine
                {
                    LineId=linesId++,
                    LineNumber=69,
                    FirstStation= 2524,
                    LastStation=2583,
                    area=Areas.Jerusalem,
                },
                   new BusLine
                {
                    LineId=linesId++,
                    LineNumber=74,
                    FirstStation= 2524,
                    LastStation=3315,
                    area=Areas.Jerusalem,
                },
                    new BusLine
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

            #endregion
        }
    }
}
