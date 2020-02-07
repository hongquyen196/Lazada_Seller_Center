using DevComponents.DotNetBar.SuperGrid;
using Newtonsoft.Json;
using SellerCenterLazada.Helpers;
using SellerCenterLazada.Models;
using SellerCenterLazada.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SellerCenterLazada
{
    public partial class Form2 : DevComponents.DotNetBar.Office2007RibbonForm
    {
        private readonly SellerAccountRepository _accountRepository = new SellerAccountRepository();
        private readonly SellerProductInfoRepository _sellerProductInfoRepository = new SellerProductInfoRepository();
        private bool IsClosed = false;
        Form formLicense;
        public Form2(Form form)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            GetSellerAccount();
            tabManager.Enabled = false;
            formLicense = form;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccount.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Thông tin đăng nhập không hợp lệ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            userLoginBindingSource.Add(new UserLogin(txtAccount.Text, txtPassword.Text));
        }

        private void btnDocFile_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    var userLogins = new List<UserLogin>();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            string[] a = line.Split("|".ToCharArray());
                            string username = a[0];
                            string password = a[1];
                            userLogins.Add(new UserLogin(username, password));
                        }
                        userLoginBindingSource.DataSource = userLogins;
                    }
                }
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            new Task(() =>
            {
                ShopDetail shopDetail = null;
                var sellerAccounts = new List<SellerAccount>();
                foreach (UserLogin userLogin in userLoginBindingSource)
                {
                    if (APIHelper.Login(userLogin.username, userLogin.password))
                    {
                        userLogin.status = "Đã đăng nhập";
                        userLogin.cookie = APIHelper.cookie;
                        shopDetail = APIHelper.GetShopDetail();
                        userLogin.shopId = shopDetail.result.shopId;
                        userLogin.name = shopDetail.result.shopName;
                        sellerAccounts.Add(new SellerAccount
                        {
                            Account = userLogin.username,
                            Cookie = userLogin.cookie,
                            Name = userLogin.name,
                            Password = CryptoHelper.Encrypt(userLogin.password)
                        });
                    }
                    else
                    {
                        userLogin.status = "Đăng nhập thất bại";
                    }
                }

                gridAccount.PrimaryGrid.BeginDataUpdate();
                gridAccount.PrimaryGrid.EndDataUpdate();

                btnDangDao.Enabled = true;
                tabManager.Enabled = true;
                _accountRepository.AddSellerAccount(sellerAccounts, FormLicense.LicenseId);
                InsertAllProductInfo(sellerAccounts);
            }).Start();
        }



        string currentUser = "";
        private void gridAccount_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            var row = (GridRow)e.GridRow;
            UserLogin userLogin = (UserLogin)(row.DataItem);
            if (userLogin != null)
            {
                currentUser = userLogin.username;
                APIHelper.cookie = userLogin.cookie;
                APIHelper.shopId = userLogin.shopId;
                //lock (productInfoVoListBindingSource)
                //{
                //    var result = APIHelper.GetShopNewArrivalProducts();
                //    result?.ForEach(item => item.Account = userLogin.username);
                //    productInfoVoListBindingSource.DataSource = result;
                //}
                richTextBox1.AppendText("\r\n>>>>>" + userLogin.username);
            }
            btnDangDao.Enabled = true;
        }

        private void productInfo_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            var row = (GridRow)e.GridRow;
            var item = (ProductInfoVoList)row.DataItem;
        }

        private void btnHenGio_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("\r\nBắt đầu hẹn giờ và đăng dạo...");
            btnDangDao.Enabled = false;
            new Task(() =>
            {
                var sellerProductInfo = new List<SellerProductInfo>();
                var data = (List<ProductInfoVoList>)(productInfoVoListBindingSource.DataSource);
                sellerProductInfo.AddRange(data.Select(item => new SellerProductInfo
                {
                    SkuId = item.skuId,
                    SellerAccount = currentUser,
                    DiscountPriceFormatted = item.discountPriceFormatted,
                    ImageUrl = item.imageUrl,
                    ItemId = item.itemId,
                    Title = item.title,
                    QueueDate = item.QueueDate.HasValue ? item.QueueDate.Value.ToUniversalTime() : (DateTime?)null
                }).ToList());
                _sellerProductInfoRepository.InsertSellerProductInfos(sellerProductInfo);

                new Task(() =>
                {
                    try
                    {
                        while (!IsClosed)
                        {
                            var productInfos = _sellerProductInfoRepository.GetAllQueue();
                            if (productInfos != null && productInfos.Count > 0)
                            {
                                var listGroup = productInfos.GroupBy(item => item.SellerAccountID).ToList();
                                listGroup.ForEach(item =>
                                {
                                    var account = JsonConvert.DeserializeObject<List<SellerAccount>>(item.FirstOrDefault().SellerAccount).FirstOrDefault();
                                    APIHelper.cookie = account.Cookie;
                                    if (APIHelper.GetUserInfo() == null)
                                    {
                                        if (!APIHelper.Login(account.Account, CryptoHelper.Decrypt(account.Password)))
                                        {
                                            account.Cookie = APIHelper.cookie;
                                            _accountRepository.AddSellerAccount(new List<SellerAccount>
                                            {
                                                account
                                            }, FormLicense.LicenseId);
                                        }
                                    }
                                    else
                                    {
                                        item.ToList().ForEach(p =>
                                        {
                                            var q = new ProductInfoVoList
                                            {
                                                title = p.Title,
                                                imageUrl = p.ImageUrl,
                                                discountPriceFormatted = p.DiscountPriceFormatted,
                                                itemId = p.ItemId,
                                                skuId = p.SkuId
                                            };

                                            Feed.Result nResult = APIHelper.CreateFeed(q);
                                            if ("success".Equals(nResult.message))
                                            {
                                                richTextBox1.AppendText("\r\n>> Đã đăng dạo sản phẩm: " + q.skuId + " => https://pages.lazada.vn/wow/i/vn/LandingPage/feed?feedId=" + nResult.result);
                                                richTextBox1.SelectionColor = Color.Green;
                                                _sellerProductInfoRepository.UpdateStatus(p.ItemId, p.SkuId, true);
                                            }
                                            else if (nResult.message.IndexOf("has been posted") > -1)
                                            {
                                                richTextBox1.SelectionColor = Color.Red;
                                                richTextBox1.AppendText("\r\nSản phẩm này đã được đăng dạo rồi.");
                                            }
                                            else
                                            {
                                                richTextBox1.SelectionColor = Color.Red;
                                                richTextBox1.AppendText("\r\nKhông đăng dạo được.");
                                            }
                                        });
                                    }
                                });
                            }
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(10));
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.SelectionColor = Color.Red;
                        richTextBox1.AppendText("\r\n" + ex.Message);
                    }

                }).Start();
            }).Start();
        }

        void GetSellerAccount()
        {
            var result = _accountRepository.GetSellerAccounts(FormLicense.LicenseId);
            userLoginBindingSource.DataSource = result?.Select(item => new UserLogin(item.Account, CryptoHelper.Decrypt(item.Password), item.Name, "", "")).ToList();
        }
        void InsertAllProductInfo(List<SellerAccount> sellerAccounts)
        {
            new Task(() =>
            {
                var sellerProductInfo = new List<SellerProductInfo>();
                sellerAccounts.ForEach(item =>
                {
                    APIHelper.cookie = item.Cookie;
                    Console.WriteLine(APIHelper.cookie);
                    sellerProductInfo.AddRange(
                        APIHelper.GetShopNewArrivalProducts().Select(p => new SellerProductInfo
                        {
                            SkuId = p.skuId,
                            SellerAccount = item.Account,
                            DiscountPriceFormatted = p.discountPriceFormatted,
                            ImageUrl = p.imageUrl,
                            ItemId = p.itemId,
                            Title = p.title
                        }).ToArray()
                    );
                });
                _sellerProductInfoRepository.InsertSellerProductInfos(sellerProductInfo);
            }).Start();
        }

        private void btnTimeOneDay_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(0);
        }
        int countPageNum = 0;
        CommonDate commDate = null;
        int buttonSaved = 1;
        void get(int button, int pageNum = 1)
        {
            buttonSaved = button;
            if (commDate == null)
            {
                return;
            }
            string dateType = "";
            string dateRange = "";
            string indexCode = "";
            switch (button)
            {
                case 0:
                    dateType = "day";
                    dateRange = commDate.data.updateDay + "%7C" + commDate.data.updateDay;
                    break;
                case 1:
                    dateType = "recent7";
                    dateRange = DateTime.Parse(commDate.data.updateDay).AddDays(-6).ToString("yyyy-MM-dd") + "%7C" + commDate.data.updateDay;
                    break;
                case 2:
                    dateType = "recent30";
                    dateRange = DateTime.Parse(commDate.data.updateDay).AddDays(-29).ToString("yyyy-MM-dd") + "%7C" + commDate.data.updateDay;
                    break;
                case 3:
                    dateType = "week";
                    dateRange = DateTime.Parse(commDate.data.updateWeek).AddDays(-6).ToString("yyyy-MM-dd") + "%7C" + commDate.data.updateWeek;
                    break;
                case 4:
                    dateType = "month";
                    dateRange = DateTime.Parse(commDate.data.updateMonth).AddDays(-30).ToString("yyyy-MM-dd") + "%7C" + commDate.data.updateMonth;
                    break;
            }
            List<ProductSalesAnalysisModel> list = new List<ProductSalesAnalysisModel>();
            ProductSalesAnalysis productSalesAnalysis = null;
            int tab = tabControl1.SelectedTabIndex;
            switch (tab)
            {
                case 0:
                    if (gridPhanTichDoanhThu.PrimaryGrid.Rows.Count > 1 && gridPhanTichDoanhThu.PrimaryGrid.Rows.Count < 100)
                    {
                        return;
                    }
                    indexCode = "payAmount";
                    productSalesAnalysis = APIHelper.GetProductSalesAnalysis(
                        pageNum: pageNum,
                        dateType: dateType,
                        dateRange: dateRange,
                        indexCode: indexCode
                        );
                    if (productSalesAnalysis.code == 0 && productSalesAnalysis.data != null)
                    {
                        //int num = 0;
                        productSalesAnalysis.data.data.ForEach(p =>
                        {
                            ProductSalesAnalysisModel model = new ProductSalesAnalysisModel();
                            //model.numId = ++num;
                            model.sellerSKU = HttpUtility.HtmlDecode(p.sellerSKU.value);
                            model.image = p.image.value;
                            model.productName = HttpUtility.HtmlDecode(p.productName.value);
                            model.uvValue = p.payAmount.value;
                            model.uvCycleCrc = p.payAmount.cycleCrc;
                            model.link = "https://www.lazada.vn/products/" + p.link.value;
                            model.uvCycleCrcValue = p.payAmount.cycleCrc;
                            list.Add(model);
                        });
                        if (pageNum > 1)
                        {
                            List<ProductSalesAnalysisModel> listConcat = (List<ProductSalesAnalysisModel>)gridPhanTichDoanhThu.PrimaryGrid.DataSource;
                            list = listConcat.Concat(list).ToList();
                        }
                        gridPhanTichDoanhThu.PrimaryGrid.DataSource = list;
                        gridPhanTichDoanhThu.Update();
                        gridPhanTichDoanhThu.Refresh();
                    }
                    break;
                case 1:
                    if (gridPhanTichLuotXem.PrimaryGrid.Rows.Count > 1 && gridPhanTichLuotXem.PrimaryGrid.Rows.Count < 100)
                    {
                        return;
                    }
                    indexCode = "uv";
                    productSalesAnalysis = APIHelper.GetProductSalesAnalysis(
                        pageNum: pageNum,
                        dateType: dateType,
                        dateRange: dateRange,
                        indexCode: indexCode
                        );
                    if (productSalesAnalysis.code == 0 && productSalesAnalysis.data != null)
                    {
                        productSalesAnalysis.data.data.ForEach(p =>
                        {
                            ProductSalesAnalysisModel model = new ProductSalesAnalysisModel();
                            model.sellerSKU = HttpUtility.HtmlDecode(p.sellerSKU.value);
                            model.image = p.image.value;
                            model.productName = HttpUtility.HtmlDecode(p.productName.value);
                            model.uvValue = p.uv.value;
                            model.uvCycleCrc = p.uv.cycleCrc;
                            model.link = "https://www.lazada.vn/products/" + p.link.value;
                            model.uvCycleCrcValue = p.uv?.cycleCrc;
                            list.Add(model);
                        });
                        if (pageNum > 1)
                        {
                            List<ProductSalesAnalysisModel> listConcat = (List<ProductSalesAnalysisModel>)(gridPhanTichLuotXem.PrimaryGrid.DataSource);
                            list = listConcat.Concat(list).ToList();
                        }
                        gridPhanTichLuotXem.PrimaryGrid.DataSource = list;
                    }
                    break;
                case 2:
                    AnalysisOverview analysisOverview = APIHelper.GetAnalysisOverview(dateType, dateRange);
                    if (analysisOverview.code == 0 && analysisOverview.data != null)
                    {
                        buttonShortOfStock.Text = string.Format(buttonShortOfStock.Text, analysisOverview.data.shortOfStock.value);
                        buttonConversionDropping.Text = string.Format(buttonConversionDropping.Text, analysisOverview.data.conversionDropping.value);
                        buttonRevenueDropping.Text = string.Format(buttonRevenueDropping.Text, analysisOverview.data.revenueDropping.value);
                        buttonNotSelling.Text = string.Format(buttonNotSelling.Text, analysisOverview.data.notSelling.value);
                        buttonPriceUncompetitive.Text = string.Format(buttonPriceUncompetitive.Text, analysisOverview.data.priceUncompetitive.value);
                    }
                    break;
            }
        }

        int buttonSavedPA = 0;
        void getProductAnalysis(int button = 0, int pageNum = 1)
        {
            buttonSavedPA = button;
            string dateType = "";
            switch (button)
            {
                case 0:
                    dateType = "priceUncompetitive";
                    break;
                case 1:
                    dateType = "shortOfStock";
                    break;
                case 2:
                    dateType = "revenueDropping";
                    break;
                case 3:
                    dateType = "conversionDropping";
                    break;
                case 4:
                    dateType = "notSelling";
                    break;
            }
            List<ProductSalesAnalysisModel> list = new List<ProductSalesAnalysisModel>();
            var productAnalysis = APIHelper.GetProductAnalysis(100, pageNum, dateType);
            if (productAnalysis.code == 0 && productAnalysis.data != null)
            {
                productAnalysis.data.ForEach(p =>
                {
                    ProductSalesAnalysisModel model = new ProductSalesAnalysisModel();
                    model.sellerSKU = HttpUtility.HtmlDecode(p.sellerSKU.value);
                    model.image = p.image.value;
                    model.productName = HttpUtility.HtmlDecode(p.productName.value);
                    switch (button)
                    {
                        case 0:
                            model.uvValue = p.skuPrice?.value;
                            model.uvCycleCrc = p.competiterLowestPrice?.value;
                            model.uvCycleCrcValue = p.competiterLowestPrice?.value;
                            break;
                        case 1:
                            model.uvValue = p.avgPayQuantity30d?.value;
                            model.uvCycleCrc = p.stockCnt1d?.value;
                            model.uvCycleCrcValue = p.stockCnt1d?.value;
                            break;
                        case 2:
                            model.uvValue = p.crtOrdAmt7d?.value;
                            model.uvCycleCrc = p.lastCycleRevenue7d?.value;
                            model.uvCycleCrcValue = p.lastCycleRevenue7d?.value;
                            break;
                        case 3:
                            model.uvValue = p.avgConversion7d?.value;
                            model.uvCycleCrc = p.lowConversionGap?.value;
                            model.uvCycleCrcValue = p.lastCycleRevenue7d?.value;
                            break;
                        case 4:
                            model.uvValue = p.lastCycleByr7d?.value;
                            model.uvCycleCrc = p.uv7d?.value;
                            model.uvCycleCrcValue = p.uv7d?.value;
                            break;
                    }
                    model.link = "https://www.lazada.vn/products/" + p.link.value;
                    list.Add(model);
                });
                if (pageNum > 1)
                {
                    List<ProductSalesAnalysisModel> listConcat = (List<ProductSalesAnalysisModel>)(gridPhanTichSanPham.PrimaryGrid.DataSource);
                    list = listConcat.Concat(list).ToList();
                }
                gridPhanTichSanPham.PrimaryGrid.DataSource = list;
            }
        }

        private void buttonPriceUncompetitive_Click(object sender, EventArgs e)
        {
            gridPhanTichSanPham.PrimaryGrid.Columns[3].HeaderText = "Giá khuyến mãi";
            gridPhanTichSanPham.PrimaryGrid.Columns[4].HeaderText = "Giá thấp nhất tìm thấy trên sản giao dịch khác";
            getProductAnalysis(0);
        }
        private void buttonShortOfStock_Click(object sender, EventArgs e)
        {
            gridPhanTichSanPham.PrimaryGrid.Columns[3].HeaderText = "Số lượng sản phẩm trung bình bán 30 ngày gần nhất";
            gridPhanTichSanPham.PrimaryGrid.Columns[4].HeaderText = "Hàng tồn ngày hôm qua";
            getProductAnalysis(1);
        }

        private void buttonRevenueDropping_Click(object sender, EventArgs e)
        {
            gridPhanTichSanPham.PrimaryGrid.Columns[3].HeaderText = "Doanh thu 7 ngày gần nhất";
            gridPhanTichSanPham.PrimaryGrid.Columns[4].HeaderText = "Lượng khách hàng 7 ngày trước đó";
            getProductAnalysis(2);
        }

        private void buttonConversionDropping_Click(object sender, EventArgs e)
        {
            gridPhanTichSanPham.PrimaryGrid.Columns[3].HeaderText = "Tỉ lệ mua hàng 7 ngày gần nhất";
            gridPhanTichSanPham.PrimaryGrid.Columns[4].HeaderText = "Chênh lệch tỉ lệ mua hàng";
            getProductAnalysis(3);
        }

        private void buttonNotSelling_Click(object sender, EventArgs e)
        {
            gridPhanTichSanPham.PrimaryGrid.Columns[3].HeaderText = "Lượng khách hàng trong 7 ngày gần nhất";
            gridPhanTichSanPham.PrimaryGrid.Columns[4].HeaderText = "Khách truy cập trong 7 ngày gần nhất";
            getProductAnalysis(4);
        }

        private void btnSevenDay_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(1);
        }

        private void btnOneMonth_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(2);
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(3);
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            get(4);
        }

        private void gridPhanTichDoanhThu_GetRowCellStyle(object sender, GridGetRowCellStyleEventArgs e)
        {
            var a = (GridRow)e.GridRow;
            var item = (ProductSalesAnalysisModel)a?.DataItem;
            if (item != null && item.uvCycleCrcValue != null)
            {
                if (item.uvCycleCrcValue > 0)
                    e.Style.TextColor = Color.Green;
                else
                    e.Style.TextColor = Color.Red;
            }
        }

        private void gridPhanTichDoanhThu_Scroll(object sender, GridScrollEventArgs e)
        {
            var obj = sender as SuperGridControl;
            if (e.ScrollEventArgs.ScrollOrientation == ScrollOrientation.VerticalScroll && (obj.VScrollMaximum <= obj.VScrollBar.LargeChange + obj.VScrollOffset + 1))
            {
                new Task(() =>
                {
                    get(buttonSaved, ++countPageNum);
                    obj.VScrollOffset = e.ScrollEventArgs.OldValue;
                }).Start();
            }
        }

        private void sideNavItem3_Click(object sender, EventArgs e)
        {
            commDate = APIHelper.GetCommonDate();
            AnalysisOverview analysisOverview = APIHelper.GetAnalysisOverview();
            if (analysisOverview.code == 0 && analysisOverview.data != null)
            {
                buttonShortOfStock.Text = Regex.Replace(buttonShortOfStock.Text, @"\(.+?\)", "") + "(" + analysisOverview.data.shortOfStock.value + ")";
                buttonConversionDropping.Text = Regex.Replace(buttonShortOfStock.Text, @"\(.+?\)", "") + "(" + analysisOverview.data.conversionDropping.value + ")";
                buttonRevenueDropping.Text = Regex.Replace(buttonShortOfStock.Text, @"\(.+?\)", "") + "(" + analysisOverview.data.revenueDropping.value + ")";
                buttonNotSelling.Text = Regex.Replace(buttonShortOfStock.Text, @"\(.+?\)", "") + "(" + analysisOverview.data.notSelling.value + ")";
                buttonPriceUncompetitive.Text = Regex.Replace(buttonShortOfStock.Text, @"\(.+?\)", "") + "(" + analysisOverview.data.priceUncompetitive.value + ")";
            }
        }

        private void gridPhanTichLuotXem_Scroll(object sender, GridScrollEventArgs e)
        {
            var obj = sender as SuperGridControl;
            if (e.ScrollEventArgs.ScrollOrientation == ScrollOrientation.VerticalScroll && (obj.VScrollMaximum <= obj.VScrollBar.LargeChange + obj.VScrollOffset + 1))
            {
                new Task(() =>
                {
                    get(buttonSaved, ++countPageNum);
                    obj.VScrollOffset = e.ScrollEventArgs.OldValue;
                }).Start();
            }
        }

        private void gridPhanTichSanPham_Scroll(object sender, GridScrollEventArgs e)
        {
            var obj = sender as SuperGridControl;
            if (e.ScrollEventArgs.ScrollOrientation == ScrollOrientation.VerticalScroll && (obj.VScrollMaximum <= obj.VScrollBar.LargeChange + obj.VScrollOffset + 1))
            {
                new Task(() =>
                {
                    get(buttonSaved, ++countPageNum);
                    obj.VScrollOffset = e.ScrollEventArgs.OldValue;
                }).Start();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosed = true;
            formLicense?.Dispose();
        }

        private void btnDangDao_Click(object sender, EventArgs e)
        {
            switch (btnDangDao.Text)
            {
                case "Đăng dạo Phong cách tự do":
                    List<FreestyleObject> freestyleObjects = new List<FreestyleObject>();
                    string tempSkuIds = "";
                    foreach (ProductInfoVoList p in productInfoVoListBindingSource)
                    {
                        if (p.feedStatus == 0)
                        {
                            freestyleObjects.Add(new FreestyleObject(p.skuId, p.itemId, p.imageUrl));
                            if (freestyleObjects.Count > 8)
                            {
                                tempSkuIds += p.skuId;
                                FreeStyle.Result fsResult = APIHelper.CreateFreeStyle(freestyleObjects);
                                if ("success".Equals(fsResult.message))
                                {
                                    richTextBox1.AppendText("\r\n>> Đã đăng dạo " + freestyleObjects.Count + " sản phẩm: " + tempSkuIds + " => https://pages.lazada.vn/wow/i/vn/LandingPage/feed?feedId=" + fsResult.result);
                                }
                                else if ("You can only publish maximum 20 feeds in a day".Equals(fsResult.message))
                                {
                                    richTextBox1.AppendText("\r\n>> Bạn chỉ có thể đăng dạo tối đa 20 bài mới trong 1 ngày.");
                                    richTextBox1.SelectionColor = Color.Red;
                                    break;
                                }
                                tempSkuIds = "";
                                freestyleObjects.Clear();
                            }
                            else
                            {
                                tempSkuIds += p.skuId + ", ";
                            }
                        }
                    }
                    break;
                case "Đăng dạo Thông tin mới":
                    foreach (ProductInfoVoList p in productInfoVoListBindingSource)
                    {
                        if (p.feedStatus == 0)
                        {
                            Feed.Result nResult = APIHelper.CreateFeed(p);
                            if ("success".Equals(nResult.message))
                            {
                                richTextBox1.AppendText("\r\n>> Đã đăng dạo sản phẩm: " + p.skuId + " => https://pages.lazada.vn/wow/i/vn/LandingPage/feed?feedId=" + nResult.result);
                            }
                            //break;
                        }
                    }
                    break;
            }
            richTextBox1.AppendText("\r\n>>>> Đăng dạo hoàn tất.\r\n");
            richTextBox1.SelectionColor = Color.Green;
            productInfo.Update();
            productInfo.Refresh();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            var openFileSave = new SaveFileDialog();
            openFileSave.Filter = "xlsx files (*.xlsx)|";
            openFileSave.DefaultExt = "xlsx";
            openFileSave.AddExtension = true;
            openFileSave.ShowDialog();
            if (!string.IsNullOrWhiteSpace(openFileSave.FileName))
            {
                if (tabControl1.SelectedTabIndex == 0)
                {
                    using (FileStream file = new FileStream(openFileSave.FileName, FileMode.Create, FileAccess.Write))
                    {
                        ExcelExportHelper.Export(gridPhanTichDoanhThu.PrimaryGrid, "Doanh thu").WriteTo(file);
                    }
                }

                if (tabControl1.SelectedTabIndex == 1)
                {
                    using (FileStream file = new FileStream(openFileSave.FileName, FileMode.Create, FileAccess.Write))
                    {
                        ExcelExportHelper.Export(gridPhanTichLuotXem.PrimaryGrid, "Lượt xem").WriteTo(file);
                    }
                }

                if (tabControl1.SelectedTabIndex == 2)
                {
                    using (FileStream file = new FileStream(openFileSave.FileName, FileMode.Create, FileAccess.Write))
                    {
                        ExcelExportHelper.Export(gridPhanTichSanPham.PrimaryGrid, "Phân tích").WriteTo(file);
                    }
                }
                MessageBox.Show("Đã xuất file excel tại đường dẫn: " + openFileSave.FileName, "Xuất thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void productInfo_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            var productInfor = (ProductInfoVoList)(e.GridCell.GridRow.DataItem);
            if (productInfor.QueueDate.HasValue && productInfor.QueueDate.Value.Year > 2019)
            {
                _sellerProductInfoRepository.UpdateSellerProductInfos(new SellerProductInfo
                {
                    DiscountPriceFormatted = productInfor.discountPriceFormatted,
                    QueueDate = productInfor.QueueDate,
                    ItemId = productInfor.itemId,
                    ImageUrl = productInfor.imageUrl,
                    IsRunned = false,
                    SellerAccount = productInfor.Account,
                    SkuId = productInfor.skuId,
                    Title = productInfor.title
                });
            }
            else
            {
                _sellerProductInfoRepository.UpdateSellerProductInfos(new SellerProductInfo
                {
                    DiscountPriceFormatted = productInfor.discountPriceFormatted,
                    QueueDate = null,
                    ItemId = productInfor.itemId,
                    ImageUrl = productInfor.imageUrl,
                    IsRunned = false,
                    SellerAccount = productInfor.Account,
                    SkuId = productInfor.skuId,
                    Title = productInfor.title
                });
            }
        }
        int pagePhongCachTuDo = 1;
        private void btnPhongCachTuDo_Click(object sender, EventArgs e)
        {
            btnDangDao.Text = "Đăng dạo Phong cách tự do";
            lock (productInfoVoListBindingSource)
            {
                var result = APIHelper.SearchShopProducts(pagePhongCachTuDo);
                if (result.Count < 100)
                {
                    btnPhongCachTuDo.Enabled = false;
                }
                if (pagePhongCachTuDo > 1)
                {
                    var listConcat = (List<ProductInfoVoList>)productInfoVoListBindingSource.DataSource;
                    result = listConcat.Concat(result).ToList();
                }
                result?.ForEach(item => { item.Account = currentUser; });
                productInfoVoListBindingSource.DataSource = result;
                productInfo.Update();
                productInfo.Refresh();
                pagePhongCachTuDo++;
            }
        }
        private void btnThongTinMoi_Click(object sender, EventArgs e)
        {
            btnDangDao.Text = "Đăng dạo Thông tin mới";
            lock (productInfoVoListBindingSource)
            {
                var result = APIHelper.GetShopNewArrivalProducts();
                result?.ForEach(item => item.Account = currentUser);
                productInfoVoListBindingSource.DataSource = result;
            }
        }
    }
}