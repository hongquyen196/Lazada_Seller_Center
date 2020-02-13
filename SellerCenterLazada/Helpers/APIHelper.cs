using Newtonsoft.Json;
using SellerCenterLazada.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SellerCenterLazada
{
    public class APIHelper
    {
        const string LOGIN_URL = "https://m.sellercenter.lazada.vn/m/seller/login.html";
        const string COOKIE_URL = "https://uac.lazada.com/tbpass/jump?group=lazada-seller&target=" + LOGIN_URL;
        const string USER_INFO_URL = "https://m.sellercenter.lazada.vn/m/index/info";
        const string SHOP_DETAIL_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.get.contentGenerator.getShopDetail";
        // Thông tin mới
        const string SHOP_NEW_ARRIVAL_PRODUCT_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.get.contentGenerator.getShopNewArrivalProducts&pageSize={0}&pageNum={1}";
        // Phong cách tự do
        const string SHOP_PRODUCT_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.get.contentGenerator.searchShopProducts&pageSize={0}&pageNum={1}";
        // Mã giảm giá
        const string VOUCHERS_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.get.contentGenerator.getVoucherList&ua=&umid=";
        // Deal nổi bật
        const string CAMPAIGN_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.get.contentGenerator.getShopCampaignList";
        const string CAMPAIGN_PRODUCTS_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.get.contentGenerator.getCampaignProducts&pageSize=20&pageNum=1&campaignId={0}";
        // Ảnh từ khách hàng
        const string REVIEWS_CUSTOMER_URL = "https://sellercenter.lazada.vn/asc-review/product-reviews/getList?pageSize=10&pageNo=1&contentType=with_images&productName=&orderNumber=&sellerSku=&shopSku=&fromDate=&toDate=&replyState=&rating=5&sourceAppName=&isExternal=false";
        // Đăng dạo Phong cách tự do
        const string CREATE_FREE_STYLE = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.post.contentGenerator.createFreeStyle";
        // Đăng dạo Thông tin mới
        // const string CHECK_FEED_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.post.contentGenerator.checkFeedDesc";
        const string CREATE_FEED_URL = "https://nest.sellercenter.lazada.vn/api/gateway?__gateway_method_id=api.post.contentGenerator.createFeed";


        // Đăng dạo reviews
        //https://sellercenter.lazada.vn/asc-review/seller-manage-reviews/updateShareStatus , post data = "type=feed&reviewRateId=228442819291426&isActive=true"
        // Phân tích bán hàng nâng cao sản phẩm
        const string COMMON_DATE_URL = "https://m.sellercenter.lazada.vn/sycm/lazada/common/commDate.json";
        const string PRODUCT_SALES_ANALYSIS_URL = "https://m.sellercenter.lazada.vn/sycm/lazada/mobile/dashboard/product/rankList/dateRange.json?pageSize={0}&page={1}&dateType={2}&dateRange={3}&indexCode={4}";
        const string ANALYSIS_OVERVIEW_URL = "https://m.sellercenter.lazada.vn/sycm/lazada/product/diagnosis/overview.json?dateType={0}&dateRange={0}";
        const string PRODUCT_ANALYSIS_URL = "https://m.sellercenter.lazada.vn/sycm/lazada/product/diagnosis/pagedAnomalyList.json?pageSize={0}&page={1}&indexCode={2}";


        public static string cookie = "";
        public static int shopId;

        public static bool Login(string username, string password)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(COOKIE_URL);
                request.Method = "GET";
                request.AllowAutoRedirect = false;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/66.4.120 Chrome/60.4.3112.120 Safari/537.36";
                request.Headers.Add("Accept-Language:vi-VN,vi;q=0.8,fr-FR;q=0.6,fr;q=0.4,en-US;q=0.2,en;q=0.2");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cookie = response.GetResponseHeader("Set-Cookie");
                Match JSID = Regex.Match(cookie, "JSID=(.+?);");
                Match TID = Regex.Match(cookie, "TID=(.+?);");
                Match CSRFT = Regex.Match(cookie, "CSRFT=(.+?);");
                cookie = JSID.Value + TID.Value + CSRFT.Value;
                request = (HttpWebRequest)WebRequest.Create(LOGIN_URL);
                request.Method = "POST";
                request.AllowAutoRedirect = false;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/66.4.120 Chrome/60.4.3112.120 Safari/537.36";
                request.Headers.Add("Accept-Language:vi-VN,vi;q=0.8,fr-FR;q=0.6,fr;q=0.4,en-US;q=0.2,en;q=0.2");
                request.Headers.Add("Cookie", cookie);
                var postData = $"TPL_username={HttpUtility.UrlEncode(username)}&TPL_password={HttpUtility.UrlEncode(password)}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Flush();
                dataStream.Close();
                response = (HttpWebResponse)request.GetResponse();
                return HttpStatusCode.Found.Equals(response.StatusCode);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return false;
            }
        }
        public static UserInfo GetUserInfo()
        {
            try
            {
                var data = Get(USER_INFO_URL);
                return JsonConvert.DeserializeObject<UserInfo>(data);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
        public static ShopDetail GetShopDetail()
        {
            try
            {
                var data = Get(SHOP_DETAIL_URL);
                return JsonConvert.DeserializeObject<ShopDetail>(data);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
        public static List<ProductInfoVoList> GetShopNewArrivalProducts()
        {
            List<ProductInfoVoList> productInfoVos = new List<ProductInfoVoList>();
            try
            {
                int page = 1;
                GetProductInfoVo productInfo = null;
                List<ProductInfoVoList> productInfoVos1 = null;
                while (true)
                {
                    var data = Get(string.Format(SHOP_NEW_ARRIVAL_PRODUCT_URL, 20, page));
                    productInfo = JsonConvert.DeserializeObject<GetProductInfoVo>(data);
                    productInfoVos1 = productInfo.result.productInfoVoList.FindAll(p => p.feedStatus == 0);
                    productInfoVos.AddRange(productInfoVos1);
                    if (productInfo.result.productInfoVoList.Count < 20)
                    {
                        break;
                    }
                    page++;
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return productInfoVos;
        }

        public static List<ProductInfoVoList> SearchShopProducts(int page = 1)
        {
            List<ProductInfoVoList> productInfoVos = new List<ProductInfoVoList>();
            try
            {
                GetProductInfoVo productInfo = null;
                List<ProductInfoVoList> productInfoVos1 = null;
                var data = Get(string.Format(SHOP_PRODUCT_URL, 100, page));
                productInfo = JsonConvert.DeserializeObject<GetProductInfoVo>(data);
                productInfoVos1 = productInfo.result.productInfoVoList.FindAll(p => p.feedStatus == 0);
                productInfoVos.AddRange(productInfoVos1);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return productInfoVos;
        }

        public static CommonDate GetCommonDate()
        {
            var data = Get(COMMON_DATE_URL);
            return JsonConvert.DeserializeObject<CommonDate>(data);
        }

        public static ProductSalesAnalysis GetProductSalesAnalysis(int pageSize = 100, int pageNum = 1, string dateType = "", string dateRange = "", string indexCode = "")
        {
            var data = Get(string.Format(PRODUCT_SALES_ANALYSIS_URL, pageSize, pageNum, dateType, dateRange, indexCode));
            return JsonConvert.DeserializeObject<ProductSalesAnalysis>(data);
        }
        public static ProductAnalysis GetProductAnalysis(int pageSize = 100, int pageNum = 1, string indexCode = "")
        {
            var data = Get(string.Format(PRODUCT_ANALYSIS_URL, pageSize, pageNum, indexCode));
            return JsonConvert.DeserializeObject<ProductAnalysis>(data);
        }
        public static AnalysisOverview GetAnalysisOverview(string dateType = "", string dateRange = "")
        {
            var data = Get(string.Format(ANALYSIS_OVERVIEW_URL, dateType, dateRange));
            return JsonConvert.DeserializeObject<AnalysisOverview>(data);
        }

        public static Feed.Result CreateFeed(ProductInfoVoList productInfoVo)
        {
            try
            {
                //if (productInfoVo.title.Length > 100)
                //{
                //    productInfoVo.title = productInfoVo.title.Substring(0, 90) + "...";
                //}
                List<FeedContent> feedContent = new List<FeedContent>();
                feedContent.Add(new FeedContent(
                    productInfoVo.skuId,
                    productInfoVo.itemId,
                    productInfoVo.imageUrl
                ));
                FeedDesc feedDesc = new FeedDesc();
                feedDesc.vi = new ViEn(
                    productInfoVo.title,
                    productInfoVo.title + "\n" + "Giá " + productInfoVo.discountPriceFormatted
                );
                feedDesc.en = feedDesc.vi;
                Feed feed = new Feed();
                feed.feedContent = feedContent;
                feed.feedDesc = feedDesc;
                var data = Post(CREATE_FEED_URL, JsonConvert.SerializeObject(feed));
                return JsonConvert.DeserializeObject<Feed.Result>(data);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
        public static FreeStyle.Result CreateFreeStyle(List<FreestyleObject> freestyleObject)
        {
            try
            {
                FreeStyle freeStyle = new FreeStyle();
                freeStyle.freestyleObjects = freestyleObject;
                freeStyle.description = new Description();
                freeStyle.description.vi_VN = "";
                freeStyle.shopId = shopId;
                var data = Post(CREATE_FREE_STYLE, JsonConvert.SerializeObject(freeStyle));
                return JsonConvert.DeserializeObject<FreeStyle.Result>(data);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
        private static string Get(string URL, bool isAllowRedirect = false)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "GET";
                request.AllowAutoRedirect = isAllowRedirect;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/66.4.120 Chrome/60.4.3112.120 Safari/537.36";
                request.Headers.Add("Upgrade-Insecure-Requests:1");
                request.Headers.Add("Accept-Language:vi-VN,vi;q=0.8,fr-FR;q=0.6,fr;q=0.4,en-US;q=0.2,en;q=0.2");
                request.Headers.Add("Cookie", cookie);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (!HttpStatusCode.OK.Equals(response.StatusCode)) return null;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                var result = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(result)) return null;
                reader.Close();
                responseStream.Close();
                response.Close();
                return result;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
        private static string Post(string URL, string postData, bool isAllowRedirect = false)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                request.AllowAutoRedirect = isAllowRedirect;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/66.4.120 Chrome/60.4.3112.120 Safari/537.36";
                request.ContentType = "application/json";
                request.Headers.Add("Upgrade-Insecure-Requests: 1");
                request.Headers.Add("Accept-Language:vi-VN,vi;q=0.8,fr-FR;q=0.6,fr;q=0.4,en-US;q=0.2,en;q=0.2");
                request.Headers.Add("Cookie", cookie);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Flush();
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (!HttpStatusCode.OK.Equals(response.StatusCode)) return null;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                var result = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(result)) return null;
                reader.Close();
                responseStream.Close();
                response.Close();
                return result;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
    }
}
