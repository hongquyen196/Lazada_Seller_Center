using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SellerCenterLazada.Models
{
    public class FeedContent
    {
        public FeedContent(long skuId, long itemId, string picUrl)
        {
            this.skuId = skuId;
            this.itemId = itemId;
            this.picUrl = picUrl;
        }
        public long skuId { get; set; }
        public long itemId { get; set; }
        public string picUrl { get; set; }

    }

    public class ViEn
    {
        public ViEn(string title, string desc)
        {
            this.title = title;
            this.desc = desc;
        }
        public string title { get; set; }
        public string desc { get; set; }
    }

    public class FeedDesc
    {
        public ViEn vi { get; set; }
        public ViEn en { get; set; }
    }

    public class Feed
    {
        public List<FeedContent> feedContent { get; set; }
        public FeedDesc feedDesc { get; set; }
        public class Result
        {
            public int status { get; set; }
            public string message { get; set; }
            public string result { get; set; }
        }
    }
}
