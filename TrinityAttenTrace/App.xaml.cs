using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using TrinityAttenTrace.View;

namespace TrinityAttenTrace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<Services.ICustomerStore, Services.DbCustomerStore>();
            // register other needed services here
        }
    }
}
