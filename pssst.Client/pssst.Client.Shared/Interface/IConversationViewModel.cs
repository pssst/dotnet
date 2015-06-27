using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using pssst.Client.Model;

namespace pssst.Client.Interface
{
    public interface IConversationViewModel
    {
        string User { get; }

        ObservableCollection<Message> Messages { get; }

        string Message { get; set; }

        ICommand SendMessageCommand { get; }
    }
}