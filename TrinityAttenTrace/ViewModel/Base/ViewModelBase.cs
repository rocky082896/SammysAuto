using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrinityAttenTrace.ViewModel.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            RegisterCommands();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RegisterCommands() { }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
