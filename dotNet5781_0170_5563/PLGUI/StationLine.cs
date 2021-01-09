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
        public class StationLine : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            //static readonly DependencyProperty LineIdProperty = DependencyProperty.Register("LineId", typeof(int), typeof(StationLine));
            //static readonly DependencyProperty StationIdProperty = DependencyProperty.Register("StationId", typeof(int), typeof(StationLine));
            //static readonly DependencyProperty NumInLineProperty = DependencyProperty.Register("NumInLine", typeof(int), typeof(StationLine));
            //static readonly DependencyProperty StationNameProperty = DependencyProperty.Register("StationName", typeof(string), typeof(StationLine));
            //static readonly DependencyProperty DistanceProperty = DependencyProperty.Register("Distance", typeof(double), typeof(StationLine));
            //static readonly DependencyProperty AverageTravleTimeProperty = DependencyProperty.Register("AverageTravleTime", typeof(TimeSpan), typeof(StationLine));
            int lineId;
            int stationId;
            int numInLine;
            string stationName;
            double distance;
            TimeSpan averageTravleTime;
            public int LineId { get { return lineId; } set { if (lineId != value) { lineId = value; OnPropertyChanged(); } } }

            public int StationId { get { return stationId; } set { if (stationId != value) { stationId = value; OnPropertyChanged(); } } }
            public int NumInLine { get { return numInLine; } set { if (numInLine != value) { numInLine = value; OnPropertyChanged(); } } }
            public string StationName { get { return stationName; } set { if (stationName != value) { stationName = value; OnPropertyChanged(); } } }
            public double Distance { get { return distance; } set { if (distance != value) { distance = value; OnPropertyChanged(); } } }
            public TimeSpan AverageTravleTime { get { return averageTravleTime; } set { if (averageTravleTime != value) { averageTravleTime = value; OnPropertyChanged(); } } }


        }
    }
}


