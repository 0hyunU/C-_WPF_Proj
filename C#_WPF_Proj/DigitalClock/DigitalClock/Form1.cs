using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace DigitalClock
{
    public partial class DigitalClock : Form
    {

        public static string s;
        public DigitalClock()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
            //타이머에서 현재 시간을 받아와 시간을 drawing클래스를 이용해 그려줍니다.
        {
            Font font = new Font("",15, FontStyle.Bold, GraphicsUnit.Point);
            Brush b = Brushes.Blue;
            s = DateTime.Now.ToString();
            s = s.Remove(0, 11);
            if (s.IndexOf("오후") !=-1)
            {
                s = s.Remove(0, 2);
                s = "p.m" + s;
            }
            else if(s.IndexOf("오전") != -1){
                s = s.Remove(0, 2);
                s = "a.m" + s;
            }
            Point p = new Point(pictureBox1.Width*1/6, pictureBox1.Height * 2 / 5);
            
            e.Graphics.DrawString(s, font, b, p.X, p.Y);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
