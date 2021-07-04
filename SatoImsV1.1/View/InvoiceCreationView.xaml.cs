using SatoImsV1._1.Data;
using SatoImsV1._1.ViewModel;
using System.Windows.Controls;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for InvoiceCreationView.xaml
    /// </summary>
    public partial class InvoiceCreationView : UserControl
    {
        public InvoiceCreationView()
        {
            InitializeComponent();
            DataContext = new InvoiceCreationViewModel(new IMSContext());
        }
    }
}
