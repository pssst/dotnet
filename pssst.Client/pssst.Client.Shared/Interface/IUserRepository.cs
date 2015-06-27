using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using pssst.Client.Model;

namespace pssst.Client.Interface
{
    public interface IUserRepository
    {
        Task SaveUser(User user);
        Task<User> LoadUser(string username);
        Task<IEnumerable<User>> LoadUsers();
    }
}
