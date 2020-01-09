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

    //ProductAnalysis

    public class CompetiterLowestPrice
    {
        public double value { get; set; }
    }

    public class SkuPrice
    {
        public double value { get; set; }
    }

    public class AvgPayQuantity30d
    {
        public int value { get; set; }
    }

    public class StockCnt1d
    {
        public int value { get; set; }
    }

    public class CrtOrdAmt7d
    {
        public double value { get; set; }
    }

    public class LastCycleRevenue7d
    {
        public double value { get; set; }
    }

    public class AvgConversion7d
    {
        public double? value { get; set; }
    }

    public class LowConversionGap
    {
        public double? value { get; set; }
    }

    public class LastCycleByr7d
    {
        public int value { get; set; }
    }

    public class Uv7d
    {
        public int value { get; set; }
    }

    public class DatumPa
    {
        public SellerSKU sellerSKU { get; set; }
        public Image image { get; set; }
        public ProductName productName { get; set; }
        public CompetiterLowestPrice competiterLowestPrice { get; set; }
        public SkuPrice skuPrice { get; set; }
        public AvgConversion7d avgConversion7d { get; set; }
        public LowConversionGap lowConversionGap { get; set; }
        public CrtOrdAmt7d crtOrdAmt7d { get; set; }
        public LastCycleRevenue7d lastCycleRevenue7d { get; set; }
        public AvgPayQuantity30d avgPayQuantity30d { get; set; }
        public StockCnt1d stockCnt1d { get; set; }       
        public LastCycleByr7d lastCycleByr7d { get; set; }
        public Uv7d uv7d { get; set; }
        public Link link { get; set; }
    }

    public class ProductAnalysis
    {
        public string traceId { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<DatumPa> data { get; set; }
    }


    public class ProductSalesAnalysisModel
    {
        public string sellerSKU { get; set; }
        public string image { get; set; }
        public string productName { get; set; }
        public string uvValue { get; set; }
        public string uvCycleCrc { get; set; }
        public string link { get; set; }

    }
}
