using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

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
            if (Properties.Settings.Default.AccountNameList != null)
            {
                foreach (String item in Properties.Settings.Default.AccountNameList)
                {
                    AccountCB.Text = item;
                }
            }
            else
            {
                AccountCB.Text = Properties.Settings.Default.AccountNames;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.AccountNameList.Add(AccountCB.Text);
            Properties.Settings.Default.AccountNames = AccountCB.Text;
        }
    }
}