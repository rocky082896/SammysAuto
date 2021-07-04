using Prism.Commands;
using System.Threading.Tasks;
using System.Windows;
using Teleperformance.ViewModel.Base;

namespace Teleperformance.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private string _openVisibility;
        private string _closeVisibility;
        private int _optionListIndex;
        private int _listIndex;
        private ViewModelBase _currentViewModel;


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

        public double _forex;
        public double CurrentForex
        {
            get { return _forex; }
            set { _forex = value; OnPropertyChanged(nameof(CurrentForex)); }
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
                    break;
                case 2:

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

                    break;
                case 2:
                    await LoadRegisterInbound();
                    break;

                case 3:
                    await LoadRegisterOutbound();
                    break;

                case 4:
                    await LoadInboundMonitoring();
                    break;

                case 5:
                    await LoadOutboundMonitoring();
                    break;

                case 6:
                    await LoadMovementList();
                    break;
            }
        }

        private async Task LoadMovementList()
        {
            if (InboundMonitoringViewModel.reader != null)
                InboundMonitoringViewModel.reader.Disconnect();

            if (OutboundMonitoringViewModel.reader != null)
                OutboundMonitoringViewModel.reader.Disconnect();

            CurrentViewModel = await Task.Run(() => new MovementListViewModel());
        }

        private async Task LoadRegisterInbound()
        {
            if (InboundMonitoringViewModel.reader != null)
                InboundMonitoringViewModel.reader.Disconnect();

            if (OutboundMonitoringViewModel.reader != null)
                OutboundMonitoringViewModel.reader.Disconnect();

            CurrentViewModel = await Task.Run(() => new InboundViewModel());

        }

        private async Task LoadInboundMonitoring()
        {
            if (OutboundMonitoringViewModel.reader != null)
                OutboundMonitoringViewModel.reader.Disconnect();

            CurrentViewModel = await Task.Run(() => new InboundMonitoringViewModel());
        }

        private async Task LoadRegisterOutbound()
        {
            if (InboundMonitoringViewModel.reader != null)
                InboundMonitoringViewModel.reader.Disconnect();

            if (OutboundMonitoringViewModel.reader != null)
                OutboundMonitoringViewModel.reader.Disconnect();

            if (InboundMonitoringViewModel.reader != null)
                InboundMonitoringViewModel.reader.Disconnect();
            CurrentViewModel = await Task.Run(() => new OutboundViewModel());
        }

        private async Task LoadOutboundMonitoring()
        {
            if (InboundMonitoringViewModel.reader != null)
                InboundMonitoringViewModel.reader.Disconnect();

            CurrentViewModel = await Task.Run(() => new OutboundMonitoringViewModel());
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
