using System.Windows;
using TrinityAttenTrace.ViewModel;

namespace TrinityAttenTrace.View
{
    /// <summary>
    /// Interaction logic for AddStudentView.xaml
    /// </summary>
    public partial class AddStudentView : Window
    {
        public AddStudentView(AddStudentViewModel addStudent)
        {
            InitializeComponent();
            DataContext = addStudent;
        }
    }
}
