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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellerCenterLazada
{
    public partial class Form2 : DevComponents.DotNetBar.Office2007RibbonForm
    {
        private readonly SellerAccountRepository _accountRepository = new SellerAccountRepository();
        private readonly SellerProductInfoRepository _sellerProductInfoRepository = new SellerProductInfoRepository();
        private bool IsClosed = false;
        public Form2()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            GetSellerAccount();
            
        }
        void GetSellerAccount()
        {
            var result = _accountRepository.GetSellerAccounts(1);
            userLoginBindingSource.DataSource = result?.Select(item => new UserLogin(item.Account, CryptoHelper.Decrypt(item.Password), item.Name, "", ""));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
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

        private void btnDangNhap_Click(object sender, EventArgs e)
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

                gridAccount.PrimaryGrid.BeginDataUpdate();
                gridAccount.PrimaryGrid.EndDataUpdate();

                btnDangDao.Enabled = true;
                tabManager.Enabled = true;
                //_accountRepository.AddSellerAccount(sellerAccounts, FormLicense.LicenseId);
                InsertAllProductInfo(sellerAccounts);
            }).Start();
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

        string currentUser = "";
        private async void gridAccount_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            
            var row = (GridRow)e.GridRow;
            UserLogin userLogin = (UserLogin)(row.DataItem);
            if (userLogin != null)
            {
                currentUser = userLogin.username;
                APIHelper.cookie = userLogin.cookie;
                lock(productInfoVoListBindingSource)
                {
                    var result = APIHelper.GetProductInfoVoList();
                    productInfoVoListBindingSource.DataSource = result;
                }
            }
            //btnDangDao.Enabled = true;
            //productInfo.PrimaryGrid.BeginDataUpdate();
            //productInfo.PrimaryGrid.EndDataUpdate();
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
    }
}
