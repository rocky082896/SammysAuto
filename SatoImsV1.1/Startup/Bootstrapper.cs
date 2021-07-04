﻿using Prism.Unity;
using System.Windows;
using Unity;

namespace SatoImsV1._1.Startup
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<View.ShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
