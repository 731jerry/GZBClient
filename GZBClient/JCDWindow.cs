using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace GZBClient
{
    public partial class JCDWindow : DevExpress.XtraEditors.XtraForm
    {
        public JCDWindow()
        {
            InitializeComponent();
        }

        PrintControl printControl1 = new PrintControl();
        PrintableComponentLink pc = new PrintableComponentLink();
        private void printButton_Click(object sender, EventArgs e)
        {
            this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("custom", this.printDocument1.DefaultPageSettings.PaperSize.Width, 480);

            using (CoolPrintPreviewDialog dlg = new CoolPrintPreviewDialog())
            {
                dlg.Document = this.printDocument1;
                //dlg.WindowState = FormWindowState.Maximized;
                dlg.Size = new Size(800, 600);

                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    //SaveButton.PerformClick();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;   //先建立画布
            SizeF fontSize;

            Font f1 = new Font("黑体", 20, FontStyle.Bold);
            Font f2 = new Font("微软雅黑", 9);

            int x = 0, y = 0, tableX = 0, tableY = 0;
            foreach (BaseControl item in panelControl1.Controls)
            {
                if (item is PictureEdit)
                {
                    PictureEdit tx = (item as PictureEdit);
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(tx.Image, new Rectangle(tx.Location.X + x + tableX, tx.Location.Y + y + tableY - 20, tx.Width, tx.Height));
                }
                if (item is LabelControl)
                {
                    LabelControl tx = (item as LabelControl);
                    g.DrawString(tx.Text, tx.Font, new SolidBrush(tx.ForeColor), tx.Left + x + tableX, tx.Top + y + tableY - 20);
                }
                if (item is TextEdit)
                {
                    TextEdit tx = (item as TextEdit);
                    //g.DrawString(tx.Text, tx.Font, new SolidBrush(tx.ForeColor), tx.Location.X + x + tableX, tx.Location.Y + y + 3 + tableY);
                    g.DrawString(tx.Text, tx.Font, new SolidBrush(tx.ForeColor), tx.Location.X + x + tableX, tx.Location.Y + y + tableY - 20);
                }
               
            }
        }

    }

}