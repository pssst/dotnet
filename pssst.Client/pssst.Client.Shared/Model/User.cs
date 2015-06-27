using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace pssst.Client.Model
{
    public sealed class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }
}
