using Prism.Commands;
using SatoImsV1._1.Model;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Linq;
using System.Windows;

namespace SatoImsV1._1.ViewModel
{
    public class ForexViewModel: ViewModelBase
    {
        private string _inputForex;

        public DelegateCommand saveForex { get; set; }


        private ShellViewModel reciever;

        public ForexViewModel(ShellViewModel dispatcher)
        {
            reciever = dispatcher;
        }

        protected override void RegisterCommands()
        {
            saveForex = new DelegateCommand(saveInputForex);
        }

        public string InputForex
        {
            get { return _inputForex; }
            set { _inputForex = value; OnPropertyChanged(); }
        }

        private void saveInputForex()
        {

            if (Convert.ToDouble(InputForex) > 0 && !string.IsNullOrWhiteSpace(InputForex.ToString()))
            {
                Properties.Settings.Default["Forex"] = InputForex.ToString();
                Properties.Settings.Default.Save();
                MessageBox.Show("Forex Value Saved", "Forex Saving", MessageBoxButton.OK, MessageBoxImage.Information);
                InputForex = "0.00";
            }
            else
            {
                MessageBox.Show("Please Enter Forex Value!", "Forex Saving", MessageBoxButton.OK, MessageBoxImage.Information);
                InputForex = "0.00";
                //MessageBox.Show("Please Enter Forex Value!", "Forex Saving", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
