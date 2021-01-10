using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace PLGUI
{
    namespace PO
    {
        class LineStation : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            int lineId;
            int lineNumber;
            int lastStation;

            public int LineId { get { return lineId; } set { if (lineId != value) { lineId = value; OnPropertyChanged(); } } }
            public int LineNumber { get { return lineNumber; } set { if (lineNumber != value) { lineNumber = value; OnPropertyChanged(); } } }
            public int LastStationId { get { return lastStation; } set { if (lastStation != value) { lastStation = value; OnPropertyChanged(); } } }
        }
    }
}
