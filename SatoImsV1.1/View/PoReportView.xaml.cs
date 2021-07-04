using SatoImsV1._1.ViewModel;
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
using System.Windows.Shapes;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for PoReportView.xaml
    /// </summary>
    public partial class PoReportView : Window
    {
        public PoReportView(OutgoingViewModel dispatcher)
        {
            InitializeComponent();
            DataContext = new PoReportViewModel(dispatcher);
        }

      
    }
}
