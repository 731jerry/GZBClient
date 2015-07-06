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
    public partial class ClientWindow : DevExpress.XtraEditors.XtraForm
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void ClientWindow_Load(object sender, EventArgs e)
        {

        }

        private void clientSave_Click(object sender, EventArgs e)
        {
            if (UniversalFunctions.GetInstence().checkIfValdated(new List<BaseControl>() { client1,  client17 }))
            {

            }
            else {
                XtraMessageBox.Show( "警告","带(*)为必填项目!");
            }
        }


    }
}