using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerCenterLazada.Models
{
    public class VoucherInfoList
    {
        public object voucherId { get; set; }
        public int voucherType { get; set; }
        public object voucherCode { get; set; }
        public string title { get; set; }
        public string subTitle { get; set; }
        public string timeline { get; set; }
        public int sellerId { get; set; }
        public string spreadId { get; set; }
        public string summary { get; set; }
        public string buttonValue { get; set; }
        public int adminPortalStatus { get; set; }
        public int followersOnly { get; set; }
        public object labelImg { get; set; }
        public List<object> voucherItemList { get; set; }
        public List<object> voucherNewItemVoList { get; set; }
    }

    public class ResultVC
    {
        public string productListTitle { get; set; }
        public List<VoucherInfoList> voucherInfoList { get; set; }
        public object pageInfo { get; set; }
    }
    public class GetVoucherList
    {
        public int status { get; set; }
        public string message { get; set; }
        public ResultVC result { get; set; }
    }
}
