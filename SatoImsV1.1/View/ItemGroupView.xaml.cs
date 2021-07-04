using SatoImsV1._1.ViewModel;
using System.Windows;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for ItemGroupView.xaml
    /// </summary>
    public partial class ItemGroupView : Window
    {
        public ItemGroupView(NewItemViewModel dispatcher)
        {
            InitializeComponent();
            DataContext = new ItemGroupViewModel(dispatcher);

        }
    }
}
