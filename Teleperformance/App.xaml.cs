using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Teleperformance.View;

namespace Teleperformance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Scheduler>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
