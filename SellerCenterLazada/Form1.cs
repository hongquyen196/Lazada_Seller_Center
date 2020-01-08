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
            List<ProductSalesAnalysisModel> list = new List<ProductSalesAnalysisModel>();
            ProductSalesAnalysis data = APIHelper.GetProductSalesAnalysis(dateRange: dateTimePicker1.Text + "%7C" + dateTimePicker2.Text);
            int num = 0;
            data.data.data.ForEach(p =>
            {
                ProductSalesAnalysisModel model = new ProductSalesAnalysisModel();
                model.numId = ++num;
                model.skuId = p.skuId.value + "";
                model.image = p.image.value;
                model.productName = p.productName.value;
                model.uvValue = p.uv.value + "";
                model.uvCycleCrc = p.uv.cycleCrc != null ? p.uv.cycleCrc?.ToString("P", CultureInfo.InvariantCulture) : "-";
                list.Add(model);
            });
            productSalesAnalysisModelBindingSource.DataSource = list;
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
            DataGridViewColumn newColumn = productSalesAnalysisGridView.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = productSalesAnalysisGridView.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    productSalesAnalysisGridView.SortOrder == SortOrder.Ascending)
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
            productSalesAnalysisGridView.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
        }

        private void productSalesAnalysisGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in productSalesAnalysisGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
    }

}
