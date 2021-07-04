using System;
using System.Windows.Forms;
using Impinj.OctaneSdk;
using System.Threading.Dispatcher;

namespace SampleRFIDWinform
{
    public partial class Form1 : Form
    {
        public static ImpinjReader reader;

        public Form1()
        {
            InitializeComponent();
            reader = new ImpinjReader();
        }

        public void RunReader()
        {
            try
            {
                reader.Connect("speedwayr-11-de-89.local");
                Settings settings = reader.QueryDefaultSettings();

                settings.Antennas.GetAntenna(1).MaxRxSensitivity = true;
                settings.Antennas.GetAntenna(1).MaxTxPower = true;
                settings.Antennas.GetAntenna(2).MaxRxSensitivity = true;
                settings.Antennas.GetAntenna(2).MaxTxPower = true;

                settings.SearchMode = SearchMode.SingleTarget;
                settings.Session = 0;
                settings.Report.IncludeAntennaPortNumber = true;

                settings.Report.Mode = ReportMode.Individual;
                reader.ApplySettings(settings);
                reader.TagsReported += OnTagsReported;
                reader.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Octane SDK exception has occurred :" + ex.Message);
                System.Diagnostics.Trace.
                    WriteLine("An Octane SDK exception has occurred : {0}", ex.Message);
            }
        }
        private void OnTagsReported(Object sender, TagReport args)
        {
            Action action = delegate ()
            {
                foreach (Tag tag in args)
                {
                    textBox1.Text = tag.Epc.ToString().Trim().Replace(" ", "");
                }
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, action);
        }
    }
}
