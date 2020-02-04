using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SellerCenterLazada.Models
{
    public class UserLogin
    {
        public int shopId { get; set; }
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

        public UserLogin(string username, string password, string name, string cookie, string status)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.cookie = cookie;
            this.status = status;
        }
    }
}
