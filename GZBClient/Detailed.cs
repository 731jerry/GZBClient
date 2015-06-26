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
    public partial class Detailed : DevExpress.XtraEditors.XtraForm
    {
        public Detailed()
        {
            InitializeComponent();
        }
         bool formMove = false;//窗体是否移动
        Point formPoint;//记录窗体的位置

private void Form4_MouseDown(object sender, MouseEventArgs e)//鼠标按下
        {
            formPoint = new Point();
            int xOffset;
            int yOffset;
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height;
                formPoint = new Point(xOffset, yOffset);
                formMove = true;//开始移动
            }
        }

private void Form4_MouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            if (formMove == true)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(formPoint.X, formPoint.Y);
                Location = mousePos;
            }
        }

private void Form4_MouseUp(object sender, MouseEventArgs e)//鼠标松开
        {
            if (e.Button == MouseButtons.Left)//按下的是鼠标左键
            {
                formMove = false;//停止移动
            }
        }

//窗体的最大化、最小化和关闭：

////需要自己拖三个picturebox控件到窗体的右上角，最大化、最小化和关闭的图标就自己去找了。添加picturebox的单击事件。如果要实现一些特效可以加入picturebox的MouseEnter、MouseLeave事件来更换图片。

private void pictureBox1_Click(object sender, EventArgs e)
        {
                 this.WindowState=FormWindowState.Minimized;//最小化
        }

private void pictureBox2_Click(object sender, EventArgs e)
        {

                      WindowState = FormWindowState.Maximized;//最大化

        }

private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}