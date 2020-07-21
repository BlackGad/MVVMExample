using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using MVVMExample.Components;
using MVVMExample.Models;
using MVVMExample.ViewModels;
using MVVMExample.Views;

namespace MVVMExample
{
    public partial class App
    {
        #region Constants

        private static readonly IReadOnlyDictionary<Type, Type> ViewModelAssociations;

        #endregion

        #region Static members

        private static IContainer CreateIoCContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ShellViewModel>().AsSelf();
            builder.RegisterType<ShellView>().AsSelf();

            builder.RegisterType<Area1ViewModel>().AsSelf();
            builder.RegisterType<Area1View>().AsSelf();

            builder.RegisterType<Area2ViewModel>().AsSelf();
            builder.RegisterType<Area2View>().AsSelf();

            builder.RegisterType<BusinessLogicService>().AsSelf().SingleInstance();

            IContainer ioCContainer = null;

            //We will use trick with AccessToModifiedClosure to catch container
            var viewResolver = new RelayTemplateSelector((item, container) =>
            {
                //Check our association
                if (item != null && ViewModelAssociations.TryGetValue(item.GetType(), out var viewType))
                {
                    //Return our custom data template which delegate control creation to IoC 
                    return new ViewDataTemplate
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        ViewFactory = type => ioCContainer?.Resolve(viewType) as FrameworkElement
                    };
                }

                //There are no known ViewModel -> View associations
                return null;
            });

            builder.RegisterInstance(viewResolver)
                   .As<DataTemplateSelector>()
                   .SingleInstance();

            ioCContainer = builder.Build();
            return ioCContainer;
        }

        #endregion

        #region Constructors

        static App()
        {
            //Map used for ViewModel -> View association
            ViewModelAssociations = new Dictionary<Type, Type>
            {
                { typeof(ShellViewModel), typeof(ShellView) },
                { typeof(Area1ViewModel), typeof(Area1View) },
                { typeof(Area2ViewModel), typeof(Area2View) },
            };
        }

        #endregion

        #region Override members

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Setup our IoC
            var iocContainer = CreateIoCContainer();

            //Spawn host window
            var shellWindow = new Window
            {
                Title = "Main window",
                //Resolve window content with IoC
                Content = iocContainer.Resolve<ShellViewModel>(),
                //Assign our custom view resolver
                ContentTemplateSelector = iocContainer.Resolve<DataTemplateSelector>()
            };

            shellWindow.Show();
        }

        #endregion
    }
}