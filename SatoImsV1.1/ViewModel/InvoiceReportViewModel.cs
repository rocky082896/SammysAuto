using CrystalDecisions.CrystalReports.Engine;
using SatoImsV1._1.Reports;
using SatoImsV1._1.ViewModel.Base;


namespace SatoImsV1._1.ViewModel
{
    public class InvoiceReportViewModel: ViewModelBase
    {
        private OutgoingViewModel receiver;
        public CrystalDecisions.CrystalReports.Engine.ReportDocument Report { get; set; }
        public InvoiceReportViewModel(OutgoingViewModel dispatcher)
        {
            receiver = dispatcher;
        }

        protected override void RegisterCollections()
        {
            Report = new InvoiceReport();
        }
    }
}
