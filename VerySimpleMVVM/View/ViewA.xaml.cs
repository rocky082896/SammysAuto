using System.Windows.Controls;
using VerySimpleMVVM.ViewModel;

namespace VerySimpleMVVM.View
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA : UserControl
    {
        public ViewA()
        {
            InitializeComponent();
            this.DataContext = new ViewAViewModel();
        }
    }
}
