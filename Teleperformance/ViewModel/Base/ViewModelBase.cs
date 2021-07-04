using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Teleperformance.ViewModel.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            RegisterCommands();
            RegisterCollections();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RegisterCommands() { }
        protected virtual void RegisterCollections() { }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
