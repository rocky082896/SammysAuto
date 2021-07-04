using System.Windows;
using Teleperformance.ViewModel;

namespace Teleperformance.View
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
            DataContext = new ShellViewModel();
        }
    }
}
