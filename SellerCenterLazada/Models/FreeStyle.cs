using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SellerCenterLazada.Models
{
    public class FreestyleObject
    {
        public long itemId { get; set; }
        public long skuId { get; set; }
        public string itemPicUrl { get; set; }

        public FreestyleObject(long skuId, long itemId, string itemPicUrl)
        {
            this.skuId = skuId;
            this.itemId = itemId;
            this.itemPicUrl = itemPicUrl;
        }
    }

    public class Title
    {
        public string en_US { get; set; }
        public string vi_VN { get; set; }
    }

    public class Description
    {
        public string en_US { get; set; }
        public string vi_VN { get; set; }
    }

    public class FreeStyle
    {
        public List<FreestyleObject> freestyleObjects { get; set; }
        public Title title { get; set; }
        public Description description { get; set; }
        public object feedThemeId { get; set; }
        public object publishTimestamp { get; set; }
        public int shopId { get; set; }
        public class Result
        {
            public int status { get; set; }
            public string message { get; set; }
            public string result { get; set; }
        }
    }
   
}
