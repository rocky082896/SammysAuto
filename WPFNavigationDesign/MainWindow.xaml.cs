using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFNavigationDesign.View;

namespace WPFNavigationDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnCloseMenu.Visibility = Visibility.Collapsed;
            //gControls.Children.Clear();
            //gControls.Children.Add(new IdleView());
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Collapsed;
            stackProgramVer.Visibility = Visibility.Collapsed;
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Collapsed;
            btnCloseMenu.Visibility = Visibility.Visible;
            stackProgramVer.Visibility = Visibility.Visible;
        }

        private void btnTry_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void btnHome_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Home");
        }

        private void btnTracking_Selected(object sender, RoutedEventArgs e)
        {
            
            //gControls.Children.Clear();
            gControls.Content=new ScanHistory();
            lblIndicator.Text = "Scan History";
        }

        private void btnInventoryMan_Selected(object sender, RoutedEventArgs e)
        {
            //gControls.Children.Clear();
            //gControls.Children.Add(new InitialRecordView());
            //lblIndicator.Text = "Scan History";
        }

        private void btnRegister_Selected(object sender, RoutedEventArgs e)
        {
            //gControls.Children.Clear();
            //gControls.Children.Add(new MasterDataView());
            //lblIndicator.Text = "Master Data";
        }

        private void btnAssetTransfer_Selected(object sender, RoutedEventArgs e)
        {
            //gControls.Children.Clear();
            //gControls.Children.Add(new UserManagementView());
            //lblIndicator.Text = "User Management";
        }

        private void btnAssetDisposal_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Asset Disposal");
        }

        private void btnAccountability_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Asset Accountability");
        }

        private void btnAccount_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Account");
        }

        private void btnSettings_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings");
        }

        private void btnHelp_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Help");
        }

        private void btnLogout_Selected(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
