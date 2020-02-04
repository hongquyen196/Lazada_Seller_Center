namespace SellerCenterLazada.Models
{
    public class ResultSD
    {
        public int shopId { get; set; }
        public int sellerId { get; set; }
        public string sellerKey { get; set; }
        public string shopLogo { get; set; }
        public string shopName { get; set; }
        public object officalIcon { get; set; }
        public object officalIconColor { get; set; }
        public object officalLabel { get; set; }
        public string ratingInfo { get; set; }
        public string shopUrl { get; set; }
        public bool isFollow { get; set; }
        public object iconLink { get; set; }
        public bool isLazMall { get; set; }
        public int followersNum { get; set; }
        public string type { get; set; }
        public bool isTestShop { get; set; }
        public bool isKOL { get; set; }
        public object tagName { get; set; }
        public object tagKey { get; set; }
        public int tagApprovalStatus { get; set; }
        public object tagApprovalStatusStr { get; set; }
    }

    public class ShopDetail
    {
        public int status { get; set; }
        public string message { get; set; }
        public ResultSD result { get; set; }
    }
}
