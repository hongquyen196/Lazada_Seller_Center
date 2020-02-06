using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerCenterLazada.Models
{
    public class Seller
    {
        public string userName { get; set; }
        public string verify { get; set; }
        public string portrait { get; set; }
        public string sellerId { get; set; }
    }

    public class Language
    {
        public string currentLanguage { get; set; }
        public string currentCountry { get; set; }
    }

    public class ItemList
    {
        public string title { get; set; }
        public string subTitle { get; set; }
        public string number { get; set; }
        public string filter { get; set; }
        public string tab { get; set; }
    }

    public class NewProducts
    {
        public string title { get; set; }
        public string total { get; set; }
        public List<ItemList> itemList { get; set; }
    }

    public class UrlConfigs
    {
        public string universityUrl { get; set; }
    }

    public class UserInfo
    {
        public Seller seller { get; set; }
        public Language language { get; set; }
        public object salesData { get; set; }
        public NewProducts newProducts { get; set; }
        public UrlConfigs urlConfigs { get; set; }
    }
}
