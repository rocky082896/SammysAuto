using Prism.Commands;
using VerySimpleMVVM.ViewModel.Base;

namespace VerySimpleMVVM.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private ViewModelBase _viewModelBase;

        public DelegateCommand ViewACommand { get; set; }
        public DelegateCommand ViewBCommand { get; set; }

        public ViewModelBase CurrentViewModel
        {
            get { return _viewModelBase; }
            set
            {
                _viewModelBase = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

        public ShellViewModel()
        {
            ViewACommand = new DelegateCommand(ViewAShowControl);
            ViewBCommand = new DelegateCommand(ViewBShowControl);
        }

        private void ViewBShowControl()
        {
            CurrentViewModel = new ViewBViewModel();
        }

        private void ViewAShowControl()
        {
            CurrentViewModel = new ViewAViewModel();
        }
    }
}
