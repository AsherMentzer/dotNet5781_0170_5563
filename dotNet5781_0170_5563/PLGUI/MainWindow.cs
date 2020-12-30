using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using BLAPI;

namespace ViewModel
{
    public class MainWindow : DependencyObject
    {
        IBL bl = BLFactory.GetBL("1");

        static readonly DependencyProperty BusProperty = DependencyProperty.Register("Bus", typeof(PO.Bus), typeof(MainWindow));
        public PO.Bus Bus { get => (PO.Bus)GetValue(BusProperty); set => SetValue(BusProperty, value); }

      //  static readonly DependencyProperty BusIDsProperty = DependencyProperty.Register("BusIDs", typeof(ObservableCollection<PO.ListedPerson>), typeof(MainWindow));
        //public ObservableCollection<PO.ListedPerson> StudentIDs { get => (ObservableCollection<PO.ListedPerson>)GetValue(StudentIDsProperty); set => SetValue(StudentIDsProperty, value); }

        public BO.Bus BusBO
        {
            set
            {
                if (value == null)
                    Bus = new PO.Bus();
                else
                {
                    value.DeepCopyTo(Bus);
                    //Student.ID = value.ID;
                    ////...
                    //Student.ListOfCourses.Clear();
                    //foreach (var fromCourse in value.ListOfCourses)
                    //{
                    //    PO.StudentCourse toCourse = new PO.StudentCourse();
                    //    toCourse.Grade = fromCourse.Grade;
                    //    toCourse. Number = fromCourse.Number;
                    //    // ...
                    //    Student.ListOfCourses.Add(toCourse);
                    //}
                }
                // update more properties in Student if needed... That is, properties that don't appear as is in studentBO...
            }
        }

       // public MainWindow() => Reset();

        BackgroundWorker getBustWorker;
        internal void blGetBus(string id)
        {
            if (getBustWorker != null)
                getBustWorker.CancelAsync();
            getBustWorker = new BackgroundWorker();
            getBustWorker.WorkerSupportsCancellation = true;
            getBustWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) =>
            {
                if (!((BackgroundWorker)sender).CancellationPending)
                    BusBO = (BO.Bus)args.Result;
            };
            getBustWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                object student = bl.GetBus((string)args.Argument);
                args.Result = worker.CancellationPending ? null : student;
            };
            getBustWorker.RunWorkerAsync(id);
        }

        //internal void Reset()
        //{
        //    if (getBustWorker != null)
        //    {
        //        getBustWorker.CancelAsync();
        //        getBustWorker = null;
        //    }
        //    if (getStudentIDsWorker != null)
        //    {
        //        getStudentIDsWorker.CancelAsync();
        //        getStudentIDsWorker = null;
        //    }
        //    Bus = new PO.Bus();
        //    blGetStudentIDs();
        //}

        //BackgroundWorker getStudentIDsWorker;
        //public void blGetStudentIDs()
        //{
        //    getStudentIDsWorker = new BackgroundWorker();
        //    getStudentIDsWorker.WorkerSupportsCancellation = true;
        //    getStudentIDsWorker.WorkerReportsProgress = true;
        //    getStudentIDsWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) => getStudentIDsWorker = null;
        //    getStudentIDsWorker.ProgressChanged += (object sender, ProgressChangedEventArgs args) =>
        //    {
        //        if (!((BackgroundWorker)sender).CancellationPending)
        //            StudentIDs.Add(new PO.ListedPerson() { Person = (BO.ListedPerson)args.UserState });
        //    };
        //    getStudentIDsWorker.DoWork += (object sender, DoWorkEventArgs args) =>
        //    {
        //        BackgroundWorker worker = (BackgroundWorker)sender;
        //        foreach (var item in bl.GetStudentIDNameList())
        //        {
        //            if (worker.CancellationPending) break;
        //            worker.ReportProgress(0, item);
        //        }
        //    };
        //    StudentIDs = new ObservableCollection<PO.ListedPerson>();
        //    getStudentIDsWorker.RunWorkerAsync();
        //}
    }
}