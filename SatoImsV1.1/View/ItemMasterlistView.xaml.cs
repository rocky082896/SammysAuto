using SatoImsV1._1.ViewModel;
using System.Windows.Controls;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for ItemMasterlistView.xaml
    /// </summary>
    public partial class ItemMasterlistView : UserControl
    {
        public ItemMasterlistView()
        {
            InitializeComponent();
            DataContext = new ItemMasterlistViewModel();
        }
    }
}
