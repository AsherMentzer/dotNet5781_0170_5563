using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PLGUI
{
    namespace PO
    {
        public class BusLine : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            ObservableCollection<StationLine> stations = new ObservableCollection<StationLine>();

            int lineId;
            int lineNumber;
            string first;
            string last;
            // int firstStation;
            //int lastStation;
            BO.Areas area;
            //   public IEnumerable<StationLine> Stations { get; set; }

            //    string numLine;
            //Agency codeAgency;
            //Areas area;
            //string moreInfo;
            //string first;
            //string last;
            /// <summary>
            /// Represents the inner unique number of the "Line"
            /// </summary>
            public int LineId { get { return lineId; } set { if (lineId != value) { lineId = value;OnPropertyChanged(); } } }
            /// <summary>
            /// Represents the number of the Line
            /// </summary>
            public int LineNumber { get { return lineNumber; } set { if (lineNumber != value) { lineNumber = value; OnPropertyChanged(); } } }
            /// <summary>
            /// Represents the agency of the Line
            /// </summary>
            //public Agency CodeAgency { get { return codeAgency; } set { if (codeAgency != value) { codeAgency = value; OnPropertyChanged(); } } }
            /// <summary>
            /// Represents the area of the Line
            /// </summary>
            public BO.Areas Area { get { return area; } set { if (area != value) { area = value; OnPropertyChanged(); } } }
            /// <summary>
            /// Represents all stops in the Line
            /// </summary>
            public ObservableCollection<StationLine> Stations { get { return stations; } set { stations = new ObservableCollection<StationLine>(value); } }
            /// <summary>
            /// Represents the more info of about the Line
            /// </summary>
            // public string MoreInfo { get { return moreInfo; } set { if (moreInfo != value) { moreInfo = value; OnPropertyChanged(); } } }

            public string NameFirstLineStop
            {
                get { return Stations[0].StationName; }
                set
                {
                    if (value != first)
                    {
                        first = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string NameLastLineStop
            {
                get { return Stations[Stations.Count - 1].StationName; }
                set
                {
                    if (value != last)
                    {
                        last = value;
                        OnPropertyChanged();
                    }
                }
            }
        }
    }
}
//        public class BusLine : DependencyObject
//        {
//            static readonly DependencyProperty IDProperty = DependencyProperty.Register("LineId", typeof(int), typeof(BusLine));
//            static readonly DependencyProperty LineNumberProperty = DependencyProperty.Register("LineNumber", typeof(int), typeof(BusLine));
//            static readonly DependencyProperty FirstStationProperty = DependencyProperty.Register("FirstStation", typeof(int), typeof(BusLine));
//            static readonly DependencyProperty LirstStationProperty = DependencyProperty.Register("Last Station", typeof(int), typeof(BusLine));
//            static readonly DependencyProperty AreaProperty = DependencyProperty.Register("Area", typeof(BO.Areas), typeof(BusLine));

//            public int LineId { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }
//            public int LineNumber { get => (int)GetValue(LineNumberProperty); set => SetValue(LineNumberProperty, value); }
//            public int FirstStation { get => (int)GetValue(FirstStationProperty); set => SetValue(FirstStationProperty, value); }
//            public int LirstStation { get => (int)GetValue(LirstStationProperty); set => SetValue(LirstStationProperty, value); }
//            public BO.Areas Area { get => (BO.Areas)GetValue(AreaProperty); set => SetValue(AreaProperty, value); }

//            public ObservableCollection<PO.StationLine> Stations { get; } = new ObservableCollection<PO.StationLine>();
//        }
//    }

//}
