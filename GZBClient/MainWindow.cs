﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Detailed de = new Detailed();
            de.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Danzi dz = new Danzi();
            dz.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Login lo = new Login();
            lo.ShowDialog();
        }
    }
}