using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using pssst.Client.Interface;
using pssst.Client.Model;
using SQLite;
using System.Linq;

namespace pssst.Client.DataAccess
{
    public sealed class UserRepositorySQLite : IUserRepository
    {
        private SQLiteAsyncConnection connection = new SQLiteAsyncConnection("pssstTest.db");

        public UserRepositorySQLite()
        {
             this.connection.CreateTableAsync<User>();
        }

        public async Task SaveUser(User user)
        {
            await this.connection.InsertAsync(user);
        }

        public async Task<User> LoadUser(string username)
        {
            IEnumerable<User> users = await this.LoadUsers();

            return users.FirstOrDefault(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        
        public async Task<IEnumerable<User>> LoadUsers()
        {
            IEnumerable<User> users = await this.connection.Table<User>().ToListAsync();

            return users;
        }
    }
}
