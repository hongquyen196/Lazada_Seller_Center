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
    public partial class License : Form
    {
        public License()
        {
            InitializeComponent();
            var key = HardDiskHelper.GenerateKey();
            textBox1.Text = key;
            if(new LicenseRepository().CheckLicense(key))
            {
                this.Hide();
                Form form = new Form1(this);
                form.Show();
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
