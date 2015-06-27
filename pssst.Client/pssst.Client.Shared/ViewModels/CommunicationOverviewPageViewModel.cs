using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using pssst.Client.Interface;
using pssst.Client.Model;
using Windows.UI.Xaml.Navigation;

namespace pssst.Client.ViewModels
{
    public sealed class CommunicationOverviewPageViewModel : ViewModel, ICommunicationOverviewViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IPssstClientService pssstService;

        public CommunicationOverviewPageViewModel(
            INavigationService navigationService,
            IPssstClientService pssstService)
        {
            this.navigationService = navigationService;
            this.pssstService = pssstService;
        }

        private ObservableCollection<Message> messages;

        public ObservableCollection<Message> Messages
        {
            get
            {
                if (this.messages == null)
                {
                    this.messages = new ObservableCollection<Message>();
                }

                return this.messages;
            }
        }

        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            await System.Threading.Tasks.Task.Run(() => this.pssstService.Configure());

            this.RefreshCommand.Execute(null);
        }

        private ICommand openSettingsCommand;

        public ICommand OpenSettingsCommand
        {
            get
            {
                if (this.openSettingsCommand == null)
                {
                    this.openSettingsCommand = new DelegateCommand(() => this.navigationService.Navigate(Experiences.Settings, null), () => true);
                }

                return this.openSettingsCommand;
            }
        }

        private ICommand openNewMessageCommand;

        public ICommand OpenNewMessageCommand
        {
            get
            {
                if(this.openNewMessageCommand == null)
                {
                    this.openNewMessageCommand = new DelegateCommand(() => this.navigationService.Navigate(Experiences.NewMessage, null), () => true);
                }

                return this.openNewMessageCommand;
            }
        }

        private ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get
            {
                if(this.refreshCommand == null)
                {
                    this.refreshCommand = new DelegateCommand(this.OnExecuteRefreshCommand, () => true);
                }

                return this.refreshCommand;
            }
        }

        private void OnExecuteRefreshCommand()
        {
            foreach (Message message in this.pssstService.GetReceivedMessages())
            {
                this.Messages.Add(message);
            }
        }
    }
}
