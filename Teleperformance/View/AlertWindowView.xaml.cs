using System;
using System.Windows;
using System.Windows.Threading;
using Teleperformance.ViewModel;

namespace Teleperformance.View
{
    /// <summary>
    /// Interaction logic for AlertWindowView.xaml
    /// </summary>
    public partial class AlertWindowView : Window
    {
        public AlertWindowView(AlertWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 6);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
