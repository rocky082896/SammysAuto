using SatoImsV1._1.ViewModel;
using System.Windows.Controls;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for ReceivingView.xaml
    /// </summary>
    public partial class ReceivingView : UserControl
    {
        public ReceivingView()
        {
            InitializeComponent();
            DataContext = new ReceivingViewModel(new Data.IMSContext());
        }
    }
}
