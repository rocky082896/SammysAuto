using SatoImsV1._1.ViewModel;
using System.Windows;


namespace SatoImsV1._1
{
    /// <summary>
    /// Interaction logic for ForexView.xaml
    /// </summary>
    public partial class ForexView : Window
    {
        public ForexView(ShellViewModel dispatcher)
        {
            InitializeComponent();
            DataContext = new ForexViewModel(dispatcher);
        }
    }
}
