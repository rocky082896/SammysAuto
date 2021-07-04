using System.Windows.Controls;
using TrinityAttenTrace.ViewModel;

namespace TrinityAttenTrace.View
{
    /// <summary>
    /// Interaction logic for StudentListView.xaml
    /// </summary>
    public partial class StudentListView : UserControl
    {
        public StudentListView()
        {
            InitializeComponent();
            DataContext = new StudentListViewModel();
        }
    }
}
