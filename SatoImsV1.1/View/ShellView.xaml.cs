using SatoImsV1._1.ViewModel;
using System.Windows;

namespace SatoImsV1._1.View
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView(ShellViewModel shellViewModel)
        {
            InitializeComponent();
            DataContext = shellViewModel;
        }
    }
}
