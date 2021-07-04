using System.Windows;
using VerySimpleMVVM.ViewModel;

namespace VerySimpleMVVM.View
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell(ShellViewModel shellViewModel)
        {
            InitializeComponent();
            DataContext = shellViewModel;
        }
    }
}
