using Impinj.OctaneSdk;
using System;
using System.Windows;
using System.Windows.Threading;

namespace SampleImpinj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ImpinjReader reader;
        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            reader = new ImpinjReader();
        }

        private void RunReader()
        {
            try
            {
                //Connect to reader
                reader.Connect("speedwayr-11-de-89.local");


                //Antenna Configuration
                //Configure Rx and Tx power
                //Tx level is the power in decibels per milliwatt (dBm) at which a modem/reader transmits its signal.
                //The Rx level is the power in dBm of the received signal
                Settings settings = reader.QueryDefaultSettings();
                settings.Antennas.GetAntenna(1).MaxRxSensitivity = true;
                settings.Antennas.GetAntenna(1).MaxTxPower = true;
                settings.Antennas.GetAntenna(2).MaxRxSensitivity = true;
                settings.Antennas.GetAntenna(2).MaxTxPower = true;

                //Search mode depends in Session
                //Inventory A to B vice versa
                settings.SearchMode = SearchMode.SingleTarget;
                settings.Session = 2;
                settings.Report.IncludeAntennaPortNumber = true;

                settings.Report.Mode = ReportMode.Individual;
                reader.ApplySettings(settings);
                reader.TagsReported += OnTagsReported;
                reader.Start();

                Console.WriteLine("Connected");
            }
            catch (Exception ex)
            {
                Console.
                    WriteLine("An Octane SDK exception has occurred : {0}", ex.Message);
            }
        }


        /***
         * 
         * BACKGROUND PROCESS FOR READING TAGS
         * **/

        private void OnTagsReported(Object sender, TagReport args)
        {
            Action action = delegate ()
            {
                foreach (Tag tag in args)
                {
                    Console.WriteLine(tag.Epc);
                    Console.WriteLine(tag.Tid);
                }
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, action);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            RunReader();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (reader.IsConnected)
            {
                reader.Stop();
            }
        }
    }
}
