using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using pssst.Api.Pcl;
using pssst.Api.Pcl.Interface;
using pssst.Client.BusinessLogic;
using pssst.Client.DataAccess;
using pssst.Client.Interface;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace pssst.Client
{
    public sealed partial class App : MvvmAppBase
    {
        private readonly IUnityContainer container = new UnityContainer();

        public App()
        {
            this.InitializeComponent();
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            this.container.RegisterInstance(this.NavigationService);

            this.container.RegisterType<IUserRepository, UserRepositorySQLite>();
            this.container.RegisterType<IPssstClientService, PssstClientService>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<IPssstClient, PssstClient>(new InjectionConstructor());
            this.container.RegisterType<IPssstSettingsService, PssstSettingsService>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());

            return Task.FromResult<object>(null);
        }

        protected override object Resolve(Type type)
        {
            return this.container.Resolve(type);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            this.NavigationService.Navigate(Experiences.CommunicationOverview.ToString(), null);
            return Task.FromResult<object>(null);
        }
    }
}