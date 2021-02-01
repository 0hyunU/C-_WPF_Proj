using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace tictactoe
{
    public partial class tictactoeeee : Form
    {
        public static string winner; //승리 or 무승부 체크를 위한 string
        private bool turn = false;
        Winner win = new Winner(); //승리자 메세지창
        int[] winCheck = { 5,5,5,5,5,5,5,5,5 }; // 1 -> X,0 -> O
        public tictactoeeee()
        {
            InitializeComponent();
        }

        private void DrawCircleInPictureBox(PictureBox PB,PaintEventArgs e)
        //pictureBox안에 원을 그려줍니다.
        {
            PointF center = new PointF(PB.Width / 2F, PB.Height / 2F);
            float radius = PB.Width / 5 * 2;
            PointF rectOrigin = new PointF(center.X - radius, center.Y - radius);
            RectangleF r = new RectangleF(rectOrigin, new SizeF(radius * 2F, radius * 2F));

            using (Pen p = new Pen(Color.Red, 4))
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                e.Graphics.DrawEllipse(p, r);
            }
        }
        private void DrawXInPictureBox(PictureBox PB, PaintEventArgs e)
        //pictureBox안에 X를 그려줍니다.
        {
            PointF center = new PointF(PB.Width / 2F, PB.Height / 2F);
            float radius = PB.Width/5*2;
            using (Pen p = new Pen(Color.Red, 4))
            {
                e.Graphics.DrawLine(p, center.X - radius, center.Y - radius,
                    center.X + radius, center.Y + radius);
                e.Graphics.DrawLine(p, center.X + radius, center.Y - radius,
                    center.X - radius, center.Y + radius);
            }
        }
        private void DrawPicture(Button b,PictureBox PB, PaintEventArgs e,int i)
        //해당플레이어의 차례에따라 맞는 그림을 그려주고 게임이 끝났는지 체크합니다.
        {
            if (!b.Visible)
            {
                if (turn)
                {
                    DrawCircleInPictureBox(PB, e);
                    textBox1.Text = "Who's Turn Now : X";
                    turn = false;
                    winCheck[i-1] = 0;
                }
                else
                {
                    DrawXInPictureBox(PB, e);
                    textBox1.Text = "Who's Turn Now : O";
                    turn = true;
                    winCheck[i-1] = 1;
                }
            }
            if (winnerCheck())
             //경기 결과 체크
            {
                this.Enabled = false;
               win.Show();
            }
            else
            //무승부 체크
            {
                int j;
                for (j = 0; j < 9; j++)
                {
                    //아직 남아있는 버튼이 있으면
                    if (winCheck[j] == 5) break;
                }
                //모든 버튼이 눌렸는데 승리자가 없다면
                if(j == 9)
                {
                winner = "OX";
                    this.Enabled = false;
                    win.Show();
                }
            }
        }
        private bool PrintWinnerText(int temp)
        {
            if (temp == 3)
            {
                textBox1.Text = "WINNER IS 'X'";
                winner = "X";
                return true;
            }
            else if (temp == 0)
            {
                textBox1.Text = "WINNER IS 'O'";
                winner = "O";
                return true;
            }
            return false;
        }
        private bool winnerCheck()
        {
            int temp;
            //가로로 승리할 때
            for (int i = 0;i<7;i += 3)
            {
                temp=winCheck[i]+winCheck[i + 1] + winCheck[i + 2];
                if (PrintWinnerText(temp)) return true;
            }
            //세로로 승리할 때
            for(int i = 0; i < 3; i++)
            {
                temp = winCheck[i] + winCheck[i + 3] + winCheck[i + 6];
                if (PrintWinnerText(temp)) return true;
            }
            //대각선으로 승리할 때
            temp = winCheck[0] + winCheck[4] + winCheck[8];
            if (PrintWinnerText(temp)) return true;
            temp = winCheck[2] + winCheck[4] + winCheck[6];
            if(PrintWinnerText(temp)) return true;

            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button1, pictureBox1, e,1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Visible = false;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button2, pictureBox2, e,2);
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button3, pictureBox3, e,3);
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button4, pictureBox4, e,4);
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button5, pictureBox5, e,5);
        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button6, pictureBox6, e,6);
        }

        private void pictureBox7_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button7, pictureBox7, e,7);
        }

        private void pictureBox8_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button8, pictureBox8, e,8);
        }

        private void pictureBox9_Paint(object sender, PaintEventArgs e)
        {
            DrawPicture(button9, pictureBox9, e,9);
        }
    }
}

