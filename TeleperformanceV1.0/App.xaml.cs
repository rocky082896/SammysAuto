using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace TeleperformanceV1._0
{
    /// <summary>
    /// |Interaction logic for App.xaml
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
