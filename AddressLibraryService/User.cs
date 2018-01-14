using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressLibraryService
{
    public class User
    {
        private string login;
        private string password;

        public string Password { get => password; set => password = value; }
        public string Login { get => login; set => login = value; }

        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
    }
}