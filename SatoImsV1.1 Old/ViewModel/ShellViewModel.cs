using Impinj.OctaneSdk;
using Prism.Commands;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SatoImsV1._1.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private string _openVisibility;
        private string _closeVisibility;
        private int _optionListIndex;
        private int _listIndex;
        private ViewModelBase _currentViewModel;
        private ImpinjReader reader;

        public ImpinjReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }



        public string CloseVisibility
        {
            get { return _closeVisibility; }
            set
            {
                _closeVisibility = value;
                this.OnPropertyChanged("CloseVisibility");
            }
        }


        public string OpenVisibility
        {
            get { return _openVisibility; }
            set
            {
                _openVisibility = value;
                this.OnPropertyChanged("OpenVisibility");
            }
        }

        public int OptionListIndex
        {
            get => _optionListIndex; set
            {
                _optionListIndex = value;
                OnPropertyChanged(nameof(OptionListIndex));
            }
        }

        public int ListIndex
        {
            get => _listIndex; set
            {
                _listIndex = value;
                OnPropertyChanged(nameof(ListIndex));
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

        public DelegateCommand HamburgerCommand { get; set; }
        public DelegateCommand HamburgerCommandBack { get; set; }
        public DelegateCommand SelectedItemIndex { get; set; }
        public DelegateCommand SelectedOptionIndex { get; set; }

        public ShellViewModel()
        {
            Reader = new ImpinjReader();
        }

        protected override void RegisterCommands()
        {
            HamburgerTrigger();
            HamburgerCommand = new DelegateCommand(HamburgerTrigger);
            HamburgerCommandBack = new DelegateCommand(HamburgerBackTrigger);
            SelectedItemIndex = new DelegateCommand(SelectedIndex);
            SelectedOptionIndex = new DelegateCommand(SelectedOptionListIndex);

        }

        public void SelectedOptionListIndex()
        {
            switch (OptionListIndex)
            {
                case 0:
                    MessageBox.Show("0");
                    break;
                case 1:
                    MessageBox.Show("1");
                    break;
                case 2:
                    MessageBox.Show("2");
                    break;
                case 4:
                    Logout();
                    break;
            }
        }
        private async void SelectedIndex()
        {
            switch (ListIndex)
            {
                case 1:
                    LoadDashboard();
                    break;
                case 2:
                    await LoadItemList();
                    break;

                case 3:
                    LoadInvoiceCreation();
                    break;

                case 4:
                    MessageBox.Show("4");
                    break;
                case 5:
                    LoadReceiving();
                    break;
                case 6:
                    MessageBox.Show("6");
                    break;
                case 7:
                    MessageBox.Show("7");
                    break;
            }
        }

        private void DisconnectReader()
        {
            if (Reader.IsConnected)
            {
                Reader.Disconnect();
                Console.WriteLine("Disconnected");
            }
        }

        private async void LoadReceiving()
        {
            CurrentViewModel = await Task.Run(() => new ReceivingViewModel(new Data.IMSContext()));
        }

        private void LoadDashboard()
        {
            CurrentViewModel = new DashboardViewModel(Reader);
        }

        private async Task LoadItemList()
        {
            DisconnectReader();
            CurrentViewModel = await Task.Run(() => new ItemMasterlistViewModel());
        }

        private void LoadInvoiceCreation()
        {
            CurrentViewModel = new InvoiceCreationViewModel(new Data.IMSContext());

        }

        private void HamburgerBackTrigger()
        {
            CloseVisibility = "Collapsed";
            OpenVisibility = "Visible";
        }

        private void HamburgerTrigger()
        {
            CloseVisibility = "Visible";
            OpenVisibility = "Collapsed";
        }

        private void Logout()
        {
            Application.Current.Shutdown();
        }
    }
}
