using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using pssst.Client.Interface;
using pssst.Client.Model;

namespace pssst.Client.ViewModels.DesignTime
{
    public sealed class CommunicationOverviewViewModel : ICommunicationOverviewViewModel
    {
        public CommunicationOverviewViewModel()
        {
            //this.Conversations = new ObservableCollection<Conversation>()
            //{
            //    new Conversation()
            //    {
            //        User = "Gordon Freeman",
            //        LastMessage = "This is my last message."
            //    },
            //    new Conversation()
            //    {
            //        User = "Chuck Norris",
            //        LastMessage = "When Chuck Norris throws exceptions, it’s across the room."
            //    }
            //};

            this.Messages = new ObservableCollection<Message>()
            {
                new Message()
                {
                    Sender = "Gordon Freeman",
                    Text = "This is my last message.",
                    Sent = DateTime.Now
                },
                new Message()
                {
                    Sender = "Chuck Norris",
                    Text = "When Chuck Norris throws exceptions, it’s across the room.",
                    Sent = DateTime.Now
                }
            };
        }

        //public ObservableCollection<Conversation> Conversations { get; private set; }


        public ICommand OpenSettingsCommand { get { return null; } }

        public ObservableCollection<Message> Messages { get; private set; }


        public ICommand OpenNewMessageCommand { get { return null; } }


        public ICommand RefreshCommand { get { return null; } }
    }
}