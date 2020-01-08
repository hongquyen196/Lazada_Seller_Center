using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerCenterLazada.Models
{
    public class Uv
    {
        public double? cycleCrc { get; set; }
        public int value { get; set; }
    }

    public class PayAmount
    {
        public double? cycleCrc { get; set; }
        public double value { get; set; }
    }

    public class StatDate
    {
        public string value { get; set; }
    }

    public class Image
    {
        public string value { get; set; }
    }

    public class SellerId
    {
        public string value { get; set; }
    }

    public class ProductId
    {
        public string value { get; set; }
    }

    public class Link
    {
        public string value { get; set; }
    }

    public class SellerSKU
    {
        public string value { get; set; }
    }

    public class SkuId
    {
        public int value { get; set; }
    }

    public class ProductName
    {
        public string value { get; set; }
    }

    public class Datum
    {
        public Uv uv { get; set; }
        public PayAmount payAmount { get; set; }
        public StatDate statDate { get; set; }
        public Image image { get; set; }
        public SellerId sellerId { get; set; }
        public ProductId productId { get; set; }
        public Link link { get; set; }
        public SellerSKU sellerSKU { get; set; }
        public SkuId skuId { get; set; }
        public ProductName productName { get; set; }
    }

    public class Data
    {
        public int recordCount { get; set; }
        public List<Datum> data { get; set; }
    }

    public class ProductSalesAnalysis
    {
        public string traceId { get; set; }
        public int code { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
    }

    public class ProductSalesAnalysisModel
    {
        public int numId { get; set; }
        public string skuId { get; set; }
        public string image { get; set; }
        public string productName { get; set; }
        public string uvValue { get; set; }
        public string uvCycleCrc { get; set; }

    }
}
