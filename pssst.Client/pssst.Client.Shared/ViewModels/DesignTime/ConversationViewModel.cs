using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using pssst.Client.Interface;
using pssst.Client.Model;

namespace pssst.Client.ViewModels.DesignTime
{
    public sealed class ConversationViewModel : IConversationViewModel
    {
        public ConversationViewModel()
        {
            this.User = "Chuck Norris";
            this.Messages = new ObservableCollection<Message>()
            {
                new Message()
                {
                    Sender = "Chuck Norris",
                    Sent = new DateTime(2015, 1, 2, 3, 13, 2),
                    Text = "When Chuck Norris throws exceptions, it’s across the room."
                },
                new Message()
                {
                    Sender = "Chuck Norris",
                    Sent = new DateTime(2015, 2, 2, 3, 13, 2),
                    Text = "All arrays Chuck Norris declares are of infinite size, because Chuck Norris knows no bounds."
                }
            };
        }

        public string User { get; private set; }

        public ObservableCollection<Message> Messages { get; private set; }

        public string Message { get; set; }

        public ICommand SendMessageCommand { get; private set; }
    }
}
