using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using pssst.Client.Model;

namespace pssst.Client.Interface
{
    public interface IPssstClientService
    {
        Task SetUser(string username);
        Task<IEnumerable<User>> GetUsers();
        void SendMessage(string receivername, string message);
        Task CreateUser(string username);
        IEnumerable<Message> GetReceivedMessages();
        void Configure();
    }
}
