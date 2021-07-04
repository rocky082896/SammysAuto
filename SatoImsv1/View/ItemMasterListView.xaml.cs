using SatoImsv1.ViewModel;
using System.Windows.Controls;

namespace SatoImsv1.View
{
    /// <summary>
    /// Interaction logic for ItemMasterListView.xaml
    /// </summary>
    public partial class ItemMasterListView : UserControl
    {
        public ItemMasterListView()
        {
            InitializeComponent();
            DataContext = new ItemMasterListViewModel();
        }
    }
}
