using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using pssst.Client.BusinessLogic;
using pssst.Client.Interface;
using pssst.Client.Model;

namespace pssst.Client.ViewModels.DesignTime
{
    public sealed class SettingsPageViewModel : ISettingsPageViewModel
    {
        public SettingsPageViewModel()
        {
            this.ServerUri = "http://designtimeserver.com:80";
        }

        public string ServerUri { get; set; }

        public string UserName { get; set; }

        public ICommand CreateUserCommand { get; set; }


        public ObservableCollection<string> Users { get; set; }


        public string SelectedUser { get; set; }
    }
}
