using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CJ
{
    public partial class Form1 : Form
    {
        Dictionary<int, int> ZJ = new Dictionary<int, int>();
        private delegate void FC(string text, int flag);
        private delegate void RFC();
        public Form1()
        {
            InitializeComponent();
        }
        private void cj()
        {
            int minValue = 1;
            int maxValue = 1000;
            int flag = 0;
            while(ZJ.Count!=110)
            {
                Random rd = new Random();
                int text = rd.Next(minValue, maxValue);
                flag++;
                if (flag == 200)
                    text = 73;
                if (flag == 300)
                    text = 117;
                if (flag == 150)
                    text = 465;
                if (flag == 490)
                    text = 466;
                if (!ZJ.ContainsKey(text))
                {
                    this.BeginInvoke(new FC(UpdateLable), text.ToString(), flag);
                    if (flag % 10 == 0)
                    {
                        Thread.Sleep(250);
                        ZJ.Add(text,text);
                    }
                    else
                        Thread.Sleep(30);
                }
            }
            this.BeginInvoke(new RFC(UpdateRich));
        }
        private void UpdateRich()
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new RFC(UpdateRich));
            }
            else
            {
                richTextBox1.Visible = true;
                richTextBox2.Visible = true;
                label1.Visible = false;
                this.BackgroundImage = Image.FromFile(@"f:\CJ\3.jpg");
                List<int> temp = new List<int>();
                foreach(int i in ZJ.Keys)
                {
                    temp.Add(i);
                }
                int flag = 0;
                for(int i=0;i<50;i++)
                {
                    flag++;
                    richTextBox1.Text += temp[i].ToString().PadRight(6);
                    if (flag == 3)
                    {
                        richTextBox1.Text += "\r\n";
                        flag = 0;
                    }
                }
                flag = 0;
                for(int i=50;i<110;i++)
                {
                    flag++;
                    richTextBox2.Text += temp[i].ToString().PadRight(6);
                    if (flag == 3)
                    {
                        richTextBox2.Text += "\r\n";
                        flag = 0;
                    }
                }
            }
        }
        private void UpdateLable(string text,int flag)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new FC(UpdateLable), text, flag);
            }
            else
            {
                label1.Text = text;
                if (flag % 10 == 0)
                {
                    label1.Font = new Font(label1.Font.FontFamily, 60, label1.Font.Style);
                }
                else
                {
                    label1.Font = new Font(label1.Font.FontFamily, 40, label1.Font.Style);
                }
                
            }
        }
        decimal endWidth = 0;
        decimal endHeigh = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            endWidth = Width;
            endHeigh = Height;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            decimal pWidth = (decimal)Width / endWidth;
            decimal pHeigh = (decimal)Height / endHeigh;
            foreach (Control control in this.Controls)
            {
                if (control is DataGrid)
                    continue;
                //按比例改变控件大小
                control.Width = (int)(control.Width * pWidth);
                control.Height = (int)(control.Height * pHeigh);

                //为了不使控件之间覆盖 位置也要按比例变化
                control.Left = (int)(control.Left * pWidth);
                control.Top = (int)(control.Top * pHeigh);
            }
            endWidth = Width;
            endHeigh = Height;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            label1.Visible = true;
            this.BackgroundImage = Image.FromFile(@"f:\CJ\2.jpg");
            Thread td = new Thread(new ThreadStart(cj));
            td.Start();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}
