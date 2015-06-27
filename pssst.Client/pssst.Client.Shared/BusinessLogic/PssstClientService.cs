using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using pssst.Api.Pcl.Interface;
using pssst.Client.Interface;
using pssst.Client.Model;

namespace pssst.Client.BusinessLogic
{
    public sealed class PssstClientService : IPssstClientService
    {
        private readonly IPssstSettingsService settings;
        private readonly IUserRepository userRepository;
        private readonly IPssstClient pssstClient;

        private Api.Pcl.Interface.User currentUser;

        public PssstClientService(
            IPssstSettingsService settings,
            IUserRepository userRepository,
            IPssstClient pssstClient)
        {
            this.settings = settings;
            this.userRepository = userRepository;
            this.pssstClient = pssstClient;
        }

        public async void Configure()
        {
            this.pssstClient.Configure(this.settings.ServerUriSetting);

            string selectedUser = this.settings.SelectedUser;

            if(!string.IsNullOrWhiteSpace(selectedUser))
            {
                await this.SetUser(selectedUser);
            }
        }

        public async Task SetUser(string username)
        {
            var user = await this.userRepository.LoadUser(username);

            if (user == null)
                return;

            this.currentUser = new Api.Pcl.Interface.User(
                user.Name,
                string.Empty,
                string.Empty,
                user.PrivateKey,
                user.PublicKey);
        }

        public void SendMessage(string receivername, string message)
        {
            if (string.IsNullOrEmpty(receivername))
                return;

            if (string.IsNullOrEmpty(message))
                return;

            Api.Pcl.Interface.User receiver = this.pssstClient.GetUser(receivername);

            if (receiver == null)
                return;

            this.pssstClient.SendMessage(this.currentUser, receiver, message);
        }

        public async Task CreateUser(string username)
        {
            if (string.IsNullOrEmpty(username))
                return;

            if (await this.userRepository.LoadUser(username) != null)
                return;

            Api.Pcl.Interface.User pssstUser = this.pssstClient.CreateUser(username);

            var user = new pssst.Client.Model.User();
            user.Name = pssstUser.Name;
            user.PrivateKey = pssstUser.PrivateKey;
            user.PublicKey = pssstUser.PublicKey;

            await this.userRepository.SaveUser(user);
        }

        public IEnumerable<Message> GetReceivedMessages()
        {
            if (this.currentUser == null)
                return new List<Message>();

            IList<Message> messages = new List<Message>();

            ReceivedMessageBody? receivedMessage;

            while ((receivedMessage = this.pssstClient.ReceiveMessage(this.currentUser)).HasValue)
            {
                messages.Add(new Message()
                    {
                        Sender = receivedMessage.Value.head.user,
                        Text = receivedMessage.Value.body
                    });
            }

            return messages;
        }


        public Task<IEnumerable<Model.User>> GetUsers()
        {
            return this.userRepository.LoadUsers();
        }
    }
}
