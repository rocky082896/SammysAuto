using SatoImsV1._1.ViewModel;
using System.Windows;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for AddCategoryView.xaml
    /// </summary>
    public partial class AddCategoryView : Window
    {
        public AddCategoryView(NewItemViewModel dispatcher)
        {
            InitializeComponent();
            DataContext = new AddCategoryViewModel(dispatcher);
        }
    }
}
