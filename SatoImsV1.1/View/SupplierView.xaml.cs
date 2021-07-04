using SatoImsV1._1.ViewModel;
using System.Windows;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for SupplierView.xaml
    /// </summary>
    public partial class SupplierView : Window
    {
        public SupplierView(InvoiceCreationViewModel dispatcher)
        {
            InitializeComponent();
            DataContext = new SupplierViewModel(dispatcher);
        }
    }
}
