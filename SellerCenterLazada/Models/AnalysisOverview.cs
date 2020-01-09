using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerCenterLazada.Models
{

    public class ShortOfStock
    {
        public int value { get; set; }
    }

    public class ConversionDropping
    {
        public int value { get; set; }
    }

    public class RevenueLossSum
    {
        public double value { get; set; }
    }

    public class RevenueDropping
    {
        public int value { get; set; }
    }

    public class RevenueLossCount
    {
        public int value { get; set; }
    }

    public class NotSelling
    {
        public int value { get; set; }
    }

    public class PriceUncompetitive
    {
        public int value { get; set; }
    }

    public class DataAo
    {
        public ShortOfStock shortOfStock { get; set; }
        public string statDate { get; set; }
        public ConversionDropping conversionDropping { get; set; }
        public RevenueLossSum revenueLossSum { get; set; }
        public RevenueDropping revenueDropping { get; set; }
        public RevenueLossCount revenueLossCount { get; set; }
        public NotSelling notSelling { get; set; }
        public PriceUncompetitive priceUncompetitive { get; set; }
    }

    public class AnalysisOverview
    {
        public string traceId { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public DataAo data { get; set; }
    }
}
