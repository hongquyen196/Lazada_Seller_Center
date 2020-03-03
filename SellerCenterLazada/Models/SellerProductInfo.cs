using System;

namespace SellerCenterLazada.Models
{
    public class SellerProductInfo
    {
        public long SkuId { set; get; }
        public long ItemId { set; get; }
        public string Title { set; get; }
        public string DiscountPriceFormatted { set; get; }
        public string ImageUrl { set; get; }
        public DateTime? QueueDate { set; get; }
        public bool IsRunned { set; get; }
        public string SellerAccountID { set; get; }
        public string SellerAccount { set; get; }
        public int Type { set; get; }
        public int LicenseId { set; get; }
    }
}
