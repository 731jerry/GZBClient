using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;

namespace GZBClient
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

            //Properties.Settings.Default.AccountNameList = new System.Collections.Specialized.StringCollection();
            if (Properties.Settings.Default.AccountNameList != null)
            {
                foreach (String item in Properties.Settings.Default.AccountNameList)
                {
                    AccountICB.Properties.Items.Add(item);
                }
            }
        }
  
        private void hyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.vividapp.net/GZB_register.php");
        }

        private void hyperLinkEdit2_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.vividapp.net/GZB_product.php");
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            //AccountICB.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("ceshi",0) );
            Properties.Settings.Default.AccountNameList.Add(AccountICB.Text);
            Properties.Settings.Default.Save();             
        }
    }
}