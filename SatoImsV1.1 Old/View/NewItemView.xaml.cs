using SatoImsV1._1.ViewModel;
using System.Windows;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for NewItemView.xaml
    /// </summary>
    public partial class NewItemView : Window
    {
        public NewItemView(InvoiceCreationViewModel dispatcher)
        {
            InitializeComponent();
            DataContext = new NewItemViewModel(dispatcher);
        }
    }
}
