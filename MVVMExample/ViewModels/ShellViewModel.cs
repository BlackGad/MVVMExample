using System.Windows;
using System.Windows.Controls;
using Autofac;
using MVVMExample.Views;

namespace MVVMExample.ViewModels
{
    internal class ShellViewModel : DependencyObject
    {
        #region Constructors

        public ShellViewModel(ILifetimeScope scope, DataTemplateSelector selector)
        {
            Area1ViewModel = scope.Resolve<Area1ViewModel>();
            Area2ViewModel = scope.Resolve<Area2ViewModel>();
            ViewResolver = selector;
        }

        #endregion

        #region Properties

        public Area1ViewModel Area1ViewModel { get; }
        public Area2ViewModel Area2ViewModel { get; }

        public DataTemplateSelector ViewResolver { get; }

        #endregion
    }
}