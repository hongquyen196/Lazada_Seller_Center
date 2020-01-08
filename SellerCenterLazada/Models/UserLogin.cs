using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SellerCenterLazada.Models
{
    public class UserLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string cookie { get; set; }
        public string status { get; set; }
        public UserLogin(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
