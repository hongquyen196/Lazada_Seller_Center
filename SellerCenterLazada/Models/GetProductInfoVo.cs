using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SellerCenterLazada.Models
{
    public class ProductInfoVoList
    {
        public string imageUrlString => "https:" + imageUrl;
        public string imageUrl { get; set; }
        public string priceFormatted { get; set; }
        public string discountPriceFormatted { get; set; }
        public long skuId { get; set; }
        public long itemId { get; set; }
        public string title { get; set; }
        public long feedStatus { get; set; }
        public long discountPrice { get; set; }
        public long price { get; set; }
        public long rating { get; set; }
        public long reviews { get; set; }
        public object createdTimestamp { get; set; }
        public string sellerSku { get; set; }
        public long stock { get; set; }
        public DateTime? QueueDate { get; set; }
        public List<long> categories { get; set; }
        public string Account { get; set; }
    }

    public class PageInfo
    {
        public long pageNum { get; set; }
        public long pageSize { get; set; }
        public bool hasMore { get; set; }
        public long count { get; set; }
        public long totalCount { get; set; }
        public long currentSystemTime { get; set; }
        public object algorithm { get; set; }
    }

    public class Result
    {
        public List<ProductInfoVoList> productInfoVoList { get; set; }
        public PageInfo pageInfo { get; set; }
    }

    public class GetProductInfoVo
    {
        public long status { get; set; }
        public string message { get; set; }
        public Result result { get; set; }
    }
}
