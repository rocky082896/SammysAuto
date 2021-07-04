using Impinj.OctaneSdk;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Windows.Threading;

namespace SatoImsV1._1.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private ImpinjReader reader;

        public Dispatcher Dispatcher
        {
            get;
            set;
        }

        public ImpinjReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        public DashboardViewModel(ImpinjReader reader)
        {
            try
            {
                Reader = reader;
                if (!Reader.IsConnected)
                {
                    Reader.Connect("speedwayr-11-de-89.local");
                    Settings settings = Reader.QueryDefaultSettings();
                    settings.Antennas.GetAntenna(1).TxPowerInDbm = 19;
                    settings.Antennas.GetAntenna(1).RxSensitivityInDbm = -45;
                    settings.SearchMode = SearchMode.SingleTarget;
                    settings.Session = 1;
                    settings.Report.IncludeAntennaPortNumber = true;
                    settings.Report.Mode = ReportMode.Individual;
                    Reader.ApplySettings(settings);

                    Reader.TagsReported += OnTagReported;
                    Reader.Start();
                    Console.WriteLine(Reader.IsConnected.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void OnTagReported(ImpinjReader reader, TagReport report)
        {
            Action action = delegate ()
            {
                foreach (Tag tag in report)
                {
                    Console.Write("OnTagReported: " + tag.Epc.ToString());
                }
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, action);
        }
    }
}
