using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GZBClient
{
    public partial class MainWindow : DevExpress.XtraEditors.XtraForm
    {
        public static String UserID = "001";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Login lo = new Login();
            lo.ShowDialog();
        }

        private void clientAdd_Click(object sender, EventArgs e)
        {
            ClientWindow cw = new ClientWindow();
            cw.ShowDialog();
        }

        private void jcdAdd_Click(object sender, EventArgs e)
        {
            JCDWindow jcdW = new JCDWindow();
            jcdW.ShowDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            String sqlPara = "clientID,companyName,gzbID,address,contactName,tel,fax,postNumber,bankName,email,bankAccount,taxNumber,PrivateBank,mainContact,mainTel,mainPhone,beizhu,modifyName,modifyDate";
            String sql = "SELECT " + sqlPara + " FROM gzb_clients WHERE gzb_account ='" + MainWindow.UserID + "'";
            DataTable data = DatabaseManager.Ins.ExcuteDataTable(sql, null);

            this.clientGridControl.DataSource = data;
        }
    }
}
