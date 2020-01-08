using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SellerCenterLazada.Models
{

    public class DataCd
    {
        public string updateWeek { get; set; }
        public string updateDay { get; set; }
        public string updateMonth { get; set; }
        public string updateNDay { get; set; }
    }

    public class CommonDate
    {
        public string traceId { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public DataCd data { get; set; }
    }
}
