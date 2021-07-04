using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFNavigationDesign.View
{
    /// <summary>
    /// Interaction logic for ScanHistory.xaml
    /// </summary>
    public partial class ScanHistory : UserControl
    {
        public ScanHistory()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            motherGrid.ShowGridLines = true;
        }
    }
}
