using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SatoImsV1._1.ViewModel.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            RegisterCommands();
            RegisterCollections();
            RunReader();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RegisterCommands() { }
        protected virtual void RegisterCollections() { }
        protected virtual void RunReader() { }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
