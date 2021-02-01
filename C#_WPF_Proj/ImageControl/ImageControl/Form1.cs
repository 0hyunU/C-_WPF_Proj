using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace ImageControl
{
    public partial class Form1 : Form
    {
        List<Point> pointsForMovingToCorner = new List<Point>(); //list for moving to corner(시계 혹은 반시계방향)
        bool turnReverseList = true; //set list 시계방향 or 반시계방향
        KeyValuePair<Point, Image> original = new KeyValuePair<Point, Image>();
        private bool mouseActivate = false;

        public Form1()
        {
            InitializeComponent();
            setPointList();
            //set original value
            original = new KeyValuePair<Point, Image>(pictureBox2.Location, pictureBox2.Image);
            
        }

 
        private static DateTime Delay(int MS)
        //delay action
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
        public static Bitmap RotateImg(Bitmap bmp, float angle)
        //이미지 회전 메소드
        {

            int w = bmp.Width;

            int h = bmp.Height;

            Bitmap tempImg = new Bitmap(w, h);

            Graphics g = Graphics.FromImage(tempImg);

            g.DrawImageUnscaled(bmp, 1, 1);

            g.Dispose();

            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(new RectangleF(0f, 0f, w, h));

            Matrix mtrx = new Matrix();

            mtrx.Rotate(angle);

            RectangleF rct = path.GetBounds(mtrx);

            Bitmap newImg = new Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height));

            g = Graphics.FromImage(newImg);

            g.TranslateTransform(-rct.X, -rct.Y);

            g.RotateTransform(angle);

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            g.DrawImageUnscaled(tempImg, 0, 0);

            g.Dispose();

            tempImg.Dispose();

            return newImg;

        }
        private void RotateImage()
            //버튼이 눌리면 이미지를 회전시킵니다.
        {
            Bitmap bitmap = new Bitmap(pictureBox2.Image);
            Image original = pictureBox2.Image;
            Image im = original;
            for (int i = 1; i <= 72; i++)
            {
                Bitmap b = RotateImg(bitmap, 5 * i);
                pictureBox2.Image = (Image)b;
                pictureBox2.Refresh();
            }
            Delay(1000);
            for (int i = 1; i <= 72; i++)
            {
                Bitmap b = RotateImg(bitmap, -5 * i);
                pictureBox2.Image = (Image)b;
                pictureBox2.Refresh();
            }
        }
       
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (radioButton5.Checked&&mouseActivate) {

                    this.pictureBox2.Location = new Point(e.X - pictureBox2.Width-2, e.Y - pictureBox2.Height-2);
            }
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton5.Checked)
            {
                /*if ((this.pictureBox2.Left <= e.Location.X) && (e.Location.X <= this.pictureBox2.Right)
                    && (this.pictureBox2.Top <= e.Location.Y) && (e.Location.Y <= this.pictureBox2.Bottom)) { 
                this.pictureBox2.Location = new Point(e.X + pictureBox2.Width, e.Y - pictureBox2.Height);   
                }*/
                this.pictureBox2.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void setPointList()
        //회전을 위한 리스트 setting
        {
            pointsForMovingToCorner.Add(new Point(this.ClientRectangle.Left + 12
                                            , this.ClientRectangle.Top + 12));
            pointsForMovingToCorner.Add(new Point(this.ClientRectangle.Left + 12
                                            , this.ClientRectangle.Bottom - this.pictureBox2.Height - 12));
            pointsForMovingToCorner.Add(new Point(this.ClientRectangle.Right - this.pictureBox2.Width - 12
                                            , this.ClientRectangle.Bottom - this.pictureBox2.Height - 12));
            pointsForMovingToCorner.Add(new Point(this.ClientRectangle.Right - this.pictureBox2.Width - 12
                                            , this.ClientRectangle.Top + 12));
            pointsForMovingToCorner.Add(new Point(this.ClientRectangle.Left + 12
                                            , this.ClientRectangle.Top + 12));
        }
        private void FlipImage()
        //버튼 1번을 눌렀을때 이미지를 반전시킵니다.
        {
            Bitmap bitmap = new Bitmap(pictureBox2.Image);
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox2.Image = (Image)bitmap;
            pictureBox2.Refresh();
            Delay(1000);
            pictureBox2.Image = original.Value;
            pictureBox2.Refresh();
            Delay(1000);

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox2.Image = (Image)bitmap;
            pictureBox2.Refresh();
            Delay(1000);
            pictureBox2.Image = original.Value;
            pictureBox2.Refresh();
            Delay(1000);
        }
        private void TrunRightDirection()
        //이미지를 구석에서 구석으로 시계방향으로 이동시킵니다.
        {
            //시계방향으로 리스트 setting
            if (turnReverseList)
            {
                pointsForMovingToCorner.Reverse();
                turnReverseList = false;
            }

            foreach (Point p in pointsForMovingToCorner)
            {
                pictureBox2.Location = p;
                Delay(500);
            }
            pictureBox2.Location = original.Key;
        }
        private void TrunReverseDirection()
        //이미지를 구석에서 구석으로 반시계방향으로 이동시킵니다.
        {
            //반시계방향으로 리스트 setting
            if (!turnReverseList) pointsForMovingToCorner.Reverse();

            foreach (Point p in pointsForMovingToCorner)
            {
                pictureBox2.Location = p;
                Delay(500);
            }
            pictureBox2.Location = original.Key;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Location = original.Key;
            pictureBox2.Image = original.Value;
            if (radioButton1.Checked) //이미지회전
            {
                FlipImage();
            }else if (radioButton2.Checked)
            {
                RotateImage();
            }
            else if (radioButton3.Checked)//이미지시계방향
            {
                TrunRightDirection();
            }else if (radioButton4.Checked)//이미지반시계방향
            {
                TrunReverseDirection();
            }
            else if (radioButton5.Checked)//이미지 마우스 팔로우
            {
                mouseActivate = true;
                pictureBox2.Location = original.Key;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton5.Checked) { 
                pictureBox2.Location = original.Key;
                mouseActivate = false;
            }
        }
    }
}
