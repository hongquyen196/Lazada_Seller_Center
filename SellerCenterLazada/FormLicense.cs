using SellerCenterLazada.Helpers;
using SellerCenterLazada.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellerCenterLazada
{
    public partial class FormLicense : Form
    {
        public static int LicenseId;
        public FormLicense()
        {
            InitializeComponent();
            var key = HardDiskHelper.GenerateKey();
            textBox2.Text = key;
            LicenseId = new LicenseRepository().CheckLicense(key);
            if (!0.Equals(LicenseId))
            {
                this.Hide();
                Form form = new Form1();
                form.ShowDialog();
                this.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
            notify.ShowBalloonTip(1000, "Seller Lazada Tool", "Sao chép thành công", ToolTipIcon.Info);
        }
    }
}
