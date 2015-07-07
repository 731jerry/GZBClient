using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;

namespace GZBClient
{
    public partial class ClientWindow : DevExpress.XtraEditors.XtraForm
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void ClientWindow_Load(object sender, EventArgs e)
        {
            client1.Text = UniversalFunctions.GetInstence().GetAutoincreaseID("gzb_clients", "clientID");
            client19.Text = "2015-07-16 12:00:00";
            String sqlPara = "clientID,companyName,gzbID,address,contactName,tel,fax,postNumber,bankName,email,bankAccount,taxNumber,PrivateBank,mainContact,mainTel,mainPhone,beizhu,modifyName,modifyDate";
            String sql = "SELECT " + sqlPara + " FROM gzb_clients WHERE gzb_account ='" + MainWindow.UserID + "'";
            DataTable data = DatabaseManager.Ins.ExcuteDataTable(sql, null);  
            List<BaseControl> basecontrols = new List<BaseControl>() { client1, client2, client3, client4, client5, client6, client7, client8, client9, client10, client11, client12, client13, client14, client15, client16, client17, client18, client19 };
            UniversalFunctions.GetInstence().SetValuesByBaseControls(UniversalFunctions.GetInstence().DataTableConvertToList<String>(data).ToList(), basecontrols);
        }

        private void clientSave_Click(object sender, EventArgs e)
        {
            if (UniversalFunctions.GetInstence().CheckIfValdated(new List<BaseControl>() {  client17 }))
            {
                String sqlPara = "gzb_account,clientID,companyName,gzbID,address,contactName,tel,fax,postNumber,bankName,email,bankAccount,taxNumber,PrivateBank,mainContact,mainTel,mainPhone,beizhu,modifyName,modifyDate";
                String sql = "INSERT INTO gzb_clients (" + sqlPara + ") VALUES (" + ("@" + sqlPara).Replace(",", ",@") + ");";
                List<String> basecontrols = new List<String>() {MainWindow.UserID, client1.Text, client2.Text, client3.Text, client4.Text, client5.Text, client6.Text, client7.Text, client8.Text, client9.Text, client10.Text, client11.Text, client12.Text, client13.Text, client14.Text, client15.Text, client16.Text, client17.Text, client18.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                List<MySqlParameter> Paramter =UniversalFunctions.GetInstence().CombineMysqlParametersByBaseControl(sqlPara.Split(',').ToList(), basecontrols);
                if (DatabaseManager.Ins.ExecuteNonquery(sql, Paramter.ToArray()) > 0)
                {
                    XtraMessageBox.Show("成功", "保存成功!");
                }
            }
            else
            {
                XtraMessageBox.Show("警告", "带(*)为必填项目!");
            }
        }


    }
}