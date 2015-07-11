using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using pssst.Client.BusinessLogic;
using pssst.Client.Interface;
using pssst.Client.Model;

namespace pssst.Client.ViewModels
{
    /// <summary>
    /// Class implementing the ViewModel of the settings page.
    /// </summary>
    public sealed class SettingsPageViewModel : ViewModel, ISettingsPageViewModel
    {
        private readonly IPssstSettingsService settings;
        private readonly IPssstClientService pssstService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPageViewModel"/> class.
        /// </summary>
        /// <param name="settings">The settings service.</param>
        /// <param name="pssstService">The pssst client service.</param>
        public SettingsPageViewModel(
            IPssstSettingsService settings,
            IPssstClientService pssstService)
        {
            this.settings = settings;
            this.pssstService = pssstService;
        }

        /// <summary>
        /// Gets or sets the pssst server uri.
        /// </summary>
        /// <value>
        /// The server pssst server uri.
        /// </value>
        public string ServerUri
        {
            get
            {
                return this.settings.ServerUriSetting.ToString();
            }
            set
            {
                this.settings.ServerUriSetting = new Uri(value);
                this.pssstService.Configure();
            }
        }

        private string userName;

        /// <summary>
        /// Gets or sets the name of the user that should be created.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.SetProperty(ref this.userName, value);
                ((DelegateCommand)this.CreateUserCommand).RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<string> users;

        /// <summary>
        /// Gets the available users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollection<string> Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new ObservableCollection<string>();
                }

                return this.users;
            }
        }
        
        /// <summary>
        /// Gets or sets the selected user.
        /// </summary>
        /// <value>
        /// The selected user.
        /// </value>
        public string SelectedUser
        {
            get
            {
                return this.settings.SelectedUser;
            }
            set
            {
                this.settings.SelectedUser = value;
                this.SetUser();
            }
        }

        private ICommand createUserCommand;

        /// <summary>
        /// Creates a new user.
        /// </summary>
        public ICommand CreateUserCommand
        {
            get 
            {
                if (this.createUserCommand == null)
                {
                    this.createUserCommand = DelegateCommand.FromAsyncHandler(this.ExecuteCreateUserCommand, this.CanExecuteCreateUserCommand);
                }

                return this.createUserCommand;
            }
        }

        private async Task ExecuteCreateUserCommand()
        {
            await this.pssstService.CreateUser(this.UserName);

            await this.LoadUsers();
        }

        private bool CanExecuteCreateUserCommand()
        {
            return !string.IsNullOrEmpty(this.UserName);
        }

        private void SetUser()
        {
            this.pssstService.SetUser(this.SelectedUser);
        }

        private bool CanExecuteSetUserCommand()
        {
            return !string.IsNullOrEmpty(this.SelectedUser);
        }

        public async override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            await this.LoadUsers();
        }

        private async Task LoadUsers()
        {
            this.Users.Clear();

            IEnumerable<User> users = await this.pssstService.GetUsers();

            foreach (var user in users)
            {
                this.Users.Add(user.Name);
            }

            this.OnPropertyChanged("SelectedUser");
        }
    }
}
