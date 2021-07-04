using VerySimpleMVVM.ViewModel.Base;

namespace VerySimpleMVVM.ViewModel
{
    public class ViewAViewModel : ViewModelBase
    {
        private string _fname;
        private string _lname;

        public string LastName
        {
            get { return _lname; }
            set
            {
                if (_lname != value)
                {
                    _lname = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged("FullName");
                }


            }
        }

        public string FirstName
        {
            get { return _fname; }
            set
            {
                if (_fname != value)
                {
                    _fname = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged("FullName");
                }

            }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        public ViewAViewModel()
        {


        }
    }
}
