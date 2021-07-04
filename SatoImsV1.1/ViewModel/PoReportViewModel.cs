using Prism.Commands;
using SatoImsV1._1.Reports;
using SatoImsV1._1.ViewModel.Base;
using System.Collections.ObjectModel;
using System.Windows;

namespace SatoImsV1._1.ViewModel
{
    public class PoReportViewModel: ViewModelBase
    {
        private OutgoingViewModel receiver;
        public CrystalDecisions.CrystalReports.Engine.ReportDocument Report { get; set; }
        public DelegateCommand loadReport { get; set; }



        public PoReportViewModel(OutgoingViewModel dispatcher)
        {
            receiver = dispatcher;
        }

        protected override void RegisterCollections()
        {
            Report = new PoReport();
            
        }

        protected override void RegisterCommands()
        {
            loadReport = new DelegateCommand(showReport);
        }



        public void showReport()
        {
            //Report = new PoReport();
            MessageBox.Show("Report Loaded");
        }
    }
}
