using Newtonsoft.Json;
using System.Collections.Generic;
using SellerCenterLazada.Models;
using System;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.IO;
using SellerCenterLazada.Repositories;
using SellerCenterLazada.Helpers;
using System.Threading.Tasks;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.Web;

namespace SellerCenterLazada
{
    public partial class Form1 : Form
    {

        private readonly SellerAccountRepository _accountRepository = new SellerAccountRepository();
        private readonly SellerProductInfoRepository _sellerProductInfoRepository = new SellerProductInfoRepository();
        DateTimePicker dtp = new DateTimePicker();
        Rectangle rectangle;
        private bool IsClosed = false;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            productInfoVoListDataGridView.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            dtp.TextChanged += new EventHandler(dtp_TextChange);
            productInfoVoListDataGridView.ColumnWidthChanged += ProductInfoVoListDataGridView_ColumnWidthChanged;
            productInfoVoListDataGridView.Scroll += ProductInfoVoListDataGridView_Scroll;
        }

        private void ProductInfoVoListDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }

        private void ProductInfoVoListDataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không được để trống!");
                return;
            }
            userLoginBindingSource.Add(new UserLogin(username, password));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            labelStatus.ResetText();
            foreach (ProductInfoVoList p in productInfoVoListBindingSource)
            {
                //hẹn giờ
                if (p.feedStatus == 0)
                {
                    var result = APIHelper.CreateFeed(p);
                    if (result.IndexOf("success") > -1)
                    {
                        labelStatus.Text = "Đăng dạo sản phẩm " + p.title + " thành công!";
                        p.feedStatus = 1;
                        count++;
                        break;
                    }
                    else if (result.IndexOf("has been posted") > -1)
                    {
                        labelStatus.Text = "Sản phẩm này đã được đăng dạo rồi!";
                    }
                    else
                    {
                        labelStatus.Text = "Không đăng dạo được";
                    }
                }
            }
            labelStatus.Text = "Đã đăng dạo " + count + " sản phẩm.";
            productInfoVoListDataGridView.Update();
            productInfoVoListDataGridView.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
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
                    List<UserLogin> userLogins = new List<UserLogin>();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            string[] a = line.Split("|".ToCharArray());
                            string username = a[0];
                            string password = a[1];
                            userLoginBindingSource.Add(new UserLogin(username, password));

                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Task(() =>
            {
                UserInfo userInfo = null;
                var sellerAccounts = new List<SellerAccount>();
                foreach (UserLogin userLogin in userLoginBindingSource)
                {
                    if (APIHelper.Login(userLogin.username, userLogin.password))
                    {
                        userLogin.status = "Đã đăng nhập";
                        userLogin.cookie = APIHelper.cookie;
                        userInfo = APIHelper.GetUserInfo();
                        userLogin.name = userInfo.seller.userName;

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
                dataGridView1.Update();
                dataGridView1.Refresh();
                button2.Enabled = true;
                tabControl1.Enabled = true;
                _accountRepository.AddSellerAccount(sellerAccounts);
                InsertAllProductInfo(sellerAccounts);
            }).Start();
        }
        private string currentUser = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgv.CurrentRow.Selected = true;
                currentUser = (dgv.Rows[e.RowIndex].Cells[0].FormattedValue).ToString();
                APIHelper.cookie = dgv.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                Console.WriteLine(APIHelper.cookie);
                productInfoVoListBindingSource.DataSource = APIHelper.GetProductInfoVoList();
                productInfoVoListDataGridView.Update();
                productInfoVoListDataGridView.Refresh();
                button5.Enabled = true;
            }
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
                        APIHelper.GetProductInfoVoList().Select(p => new SellerProductInfo
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



        //---Add datepicker
        private void dtp_TextChange(object sender, EventArgs e)
        {
            productInfoVoListDataGridView.CurrentCell.Value = dtp.Text.ToString();
            dtp.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("\r\nBắt đầu hẹn giờ và đăng dạo...");
            button2.Enabled = false;
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
                                            });
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

                                            var result = APIHelper.CreateFeed(q);
                                            if (result.IndexOf("success") > -1)
                                            {
                                                richTextBox1.SelectionColor = Color.Green;
                                                richTextBox1.AppendText("\r\nĐăng dạo sản phẩm " + p.Title + " thành công.");
                                                _sellerProductInfoRepository.UpdateStatus(p.ItemId, p.SkuId, true);
                                            }
                                            else if (result.IndexOf("has been posted") > -1)
                                            {
                                                richTextBox1.SelectionColor = Color.Green;
                                                richTextBox1.AppendText("\r\nSản phẩm này đã được đăng dạo rồi.");
                                            }
                                            else
                                            {
                                                richTextBox1.SelectionColor = Color.Green;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosed = true;
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void productInfoVoListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0: // Column index of needed dateTimePicker cell

                    rectangle = productInfoVoListDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
                    dtp.Size = new Size(rectangle.Width, rectangle.Height); //  
                    dtp.Location = new Point(rectangle.X, rectangle.Y); //  
                    dtp.Visible = true;
                    break;
            }
        }

        private void productSalesAnalysisGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //http://csharp.net-informations.com/datagridview/csharp-datagridview-filter.htm
            DataGridViewColumn newColumn = uvGridView.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = uvGridView.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    uvGridView.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            uvGridView.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
        }

        private void productSalesAnalysisGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in uvGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
        CommonDate commDate = null;
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1)
            {
                commDate = APIHelper.GetCommonDate();
            }
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 2)
            {
                get(0);
            }
        }
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
            int tab = tabControl2.SelectedIndex;
            switch (tab)
            {
                case 0:
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
                            model.skuId = p.skuId.value.ToString();
                            model.image = p.image.value;
                            model.productName = HttpUtility.HtmlDecode(p.productName.value);
                            model.uvValue = p.payAmount.value.ToString("#,###");
                            model.uvCycleCrc = p.payAmount.cycleCrc != null ? p.payAmount.cycleCrc?.ToString("P", CultureInfo.InvariantCulture) : "";
                            list.Add(model);
                        });
                        if (pageNum > 1)
                        {
                            List<ProductSalesAnalysisModel> listConcat = (List<ProductSalesAnalysisModel>)payAmountlDataGridView.DataSource;
                            list = listConcat.Concat(list).ToList();
                        }
                        payAmountlDataGridView.DataSource = list;
                        payAmountlDataGridView.Update();
                        payAmountlDataGridView.Refresh();
                    }
                    break;
                case 1:
                    indexCode = "uv";
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
                            model.skuId = p.skuId.value.ToString();
                            model.image = p.image.value;
                            model.productName = HttpUtility.HtmlDecode(p.productName.value);
                            model.uvValue = p.uv.value.ToString();
                            model.uvCycleCrc = p.uv.cycleCrc != null ? p.uv.cycleCrc?.ToString("P", CultureInfo.InvariantCulture) : "";
                            list.Add(model);
                        });
                        if (pageNum > 1)
                        {
                            List<ProductSalesAnalysisModel> listConcat = (List<ProductSalesAnalysisModel>)uvGridView.DataSource;
                            list = listConcat.Concat(list).ToList();
                        }
                        uvGridView.DataSource = list;
                        uvGridView.Update();
                        uvGridView.Refresh();
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
        private void button6_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(0);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(3);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            countPageNum = 0;
            get(4);
        }

        int countPageNum = 0;
        private void payAmountlDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.DisplayedRowCount(false) + grid.FirstDisplayedScrollingRowIndex >= grid.RowCount)
            {
                get(buttonSaved, ++countPageNum);
            }
        }

        private void uvGridView_Scroll(object sender, ScrollEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.DisplayedRowCount(false) + grid.FirstDisplayedScrollingRowIndex >= grid.RowCount)
            {
                get(buttonSaved, ++countPageNum);
            }
        }

        private void payAmountlDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //var grid = sender as DataGridView;
            //var rowIdx = (e.RowIndex + 1).ToString();
            //var centerFormat = new StringFormat()
            //{
            //    // right alignment might actually make more sense for numbers
            //    Alignment = StringAlignment.Center,
            //    LineAlignment = StringAlignment.Center
            //};

            //var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            //e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void uvGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //var grid = sender as DataGridView;
            //var rowIdx = (e.RowIndex + 1).ToString();
            //var centerFormat = new StringFormat()
            //{
            //    // right alignment might actually make more sense for numbers
            //    Alignment = StringAlignment.Center,
            //    LineAlignment = StringAlignment.Center
            //};

            //var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            //e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        void getProductAnalysis(int button = 0, int pageNum = 1)
        {
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
                    model.skuId = p.skuId.value.ToString();
                    model.image = p.image.value;
                    model.productName = HttpUtility.HtmlDecode(p.productName.value);
                    switch (button)
                    {
                        case 0:
                            model.uvValue = p.skuPrice?.value.ToString("#,###");
                            model.uvCycleCrc = p.competiterLowestPrice?.value.ToString("#,###");
                            break;
                        case 1:
                            model.uvValue = p.avgPayQuantity30d?.value.ToString();
                            model.uvCycleCrc = p.stockCnt1d?.value.ToString();
                            break;
                        case 2:
                            model.uvValue = p.crtOrdAmt7d?.value.ToString("#,###");
                            model.uvCycleCrc = p.lastCycleRevenue7d?.value.ToString("#,###");
                            break;
                        case 3:
                            model.uvValue =  p.avgConversion7d?.value?.ToString("P", CultureInfo.InvariantCulture);
                            model.uvCycleCrc = p.lowConversionGap?.value?.ToString("P", CultureInfo.InvariantCulture);
                            break;
                        case 4:
                            model.uvValue = p.lastCycleByr7d?.value.ToString();
                            model.uvCycleCrc = p.uv7d?.value.ToString();
                            break;
                    }
                    list.Add(model);
                });
                if (pageNum > 1)
                {
                    List<ProductSalesAnalysisModel> listConcat = (List<ProductSalesAnalysisModel>)productAnalysisDataGridView.DataSource;
                    list = listConcat.Concat(list).ToList();
                }
                productAnalysisDataGridView.DataSource = list;
                productAnalysisDataGridView.Update();
                productAnalysisDataGridView.Refresh();
            }
        }

        private void buttonPriceUncompetitive_Click(object sender, EventArgs e)
        {
            getProductAnalysis(0);
        }

        private void buttonShortOfStock_Click(object sender, EventArgs e)
        {
            getProductAnalysis(1);
        }

        private void buttonRevenueDropping_Click(object sender, EventArgs e)
        {
            getProductAnalysis(2);
        }

        private void buttonConversionDropping_Click(object sender, EventArgs e)
        {
            getProductAnalysis(3);
        }

        private void buttonNotSelling_Click(object sender, EventArgs e)
        {
            getProductAnalysis(4);
        }
    }

}
