using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using pssst.Client.Model;

namespace pssst.Client.Interface
{
    public interface ICommunicationOverviewViewModel
    {
        ObservableCollection<Message> Messages { get; }

        ICommand OpenSettingsCommand { get; }

        ICommand OpenNewMessageCommand { get; }

        ICommand RefreshCommand { get; }
    }
}