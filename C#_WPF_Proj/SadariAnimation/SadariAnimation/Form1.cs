using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SadariAnimation
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        Pen DrawingSadari;
        Pen[] DrawingVisited = new Pen[8];

        int[,] Sadari;
        int[,] VisitedSadari;
        int SelectedUser;
        bool[] labelArrived = new bool[8];
        bool[] PlayerResertChecked = new bool[8];

        //사다리 시작지점
        const int STARTPOINT_X = 20;
        const int STARTPOINT_Y = 25; 
        const int WIDTH = 65; //1칸당 너비
        const int HEIGHT_SIZE = 143;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;

            // 해당 멤버가 지나갈 때마다 색을 다르게 칠해준다.
            DrawingSadari = new Pen(Color.Blue, 3);
            DrawingVisited[0] = new Pen(Color.FromArgb(255, 0, 0), 3);
            DrawingVisited[1] = new Pen(Color.FromArgb(0, 255, 0), 3);
            DrawingVisited[2] = new Pen(Color.FromArgb(255, 0, 255), 3);
            DrawingVisited[3] = new Pen(Color.FromArgb(255, 255, 0), 3);
            DrawingVisited[4] = new Pen(Color.FromArgb(0, 255, 255), 3);
            DrawingVisited[5] = new Pen(Color.FromArgb(125, 125, 125), 3);
            DrawingVisited[6] = new Pen(Color.FromArgb(125, 0, 0), 3);
            DrawingVisited[7] = new Pen(Color.FromArgb(125, 0, 125), 3);
            InitSadari();
            pictureBox1.Invalidate();
        }
        private void InitSadari()
        {
            MakeSadari((int)numericUpDown1.Value);

            MakeLabel((int)numericUpDown1.Value);
            pictureBox2.Location = GetPos(0, 0);
        }
        private void MakeSadari(int playerNum)
        //2차원 배열을 이용하여 플레이어의 명수 만큼의 사다리를 만든다.
        {
            int i, j, k;

            //배열 초기화
            Sadari = new int[playerNum, HEIGHT_SIZE];
            VisitedSadari = new int[playerNum, HEIGHT_SIZE];


            //
            // 플레이어만큼의 세로줄(mean 1)을 그어준다.
            for (i = 0; i < playerNum; i++)
            {
                for (j = 0; j < HEIGHT_SIZE; j++)
                {
                    Sadari[i, j] = 1;
                    VisitedSadari[i, j] = 0;
                }
            }

            // 난수를 발생시켜 가로줄(2 저장)을 만든다.
            // 하나의 세로줄에 7개의 가로줄을 만든다.
            int r;
            int cnt;

            Random rnd = new Random();

            for (i = 0; i <= playerNum - 2; i++)
            {

                cnt = 0;
                while (true)
                {
                    while (true)
                    {
                        r = rnd.Next(4, HEIGHT_SIZE-5); // 랜덤
                        //가로줄을 그을 수 있는지 체크
                        for (k = 0; k < 4; k++)
                        {
                            // 앞뒤로 3칸씩 검사
                            //옆 라인의 값이 1이면 가로줄이 없다는 뜻
                            if (Sadari[i, r + k] != 1 || Sadari[i, r - k] != 1) break;
                        }

                        if (k != 4) continue;

                        //가능할 경우
                        cnt += 1;
                        //2 = 오른쪽, 3= 왼쪽으로 선이 그어져있다.
                        Sadari[i, r] = 2; 
                        Sadari[i + 1, r] = 3;
                        break;
                    }

                    if (cnt == 7)//해당 라인에 가로줄을 그은 후 나간다.
                        break;
                }
            }
        }

        private Label GetLblCtrl(String strTag)
        // Tag 값을 이용해 원하는 Label의 컨트롤을 얻는다.
        {
            int i;
            for (i = 0; i < pictureBox1.Controls.Count; i++)
            {
                if (pictureBox1.Controls[i].Tag.ToString().Equals(strTag))
                    return (Label)pictureBox1.Controls[i];
            }

            return null;
        }
        private void MakeLabel(int playerNum)
        // 사다리 위와 아래에 플레이어와 밑에 결과에 해당하는 라벨 생성
        {
            int i;

            // 우선 panMain의 모든 컨트롤을 삭제한다.
            pictureBox1.Controls.Clear();
            List<string> ResultValue = new List<string>(new string[]
        {
            "꽝!",
            "꽝!",
            "당첨",
            "꽝!",
            "꽝!",
            "꽝!",
            "당첨",
            "꽝!"
        });

            for (i = 0; i < playerNum; i++)
            {
                Label lbl1, lbl2;

                //위쪽 label 생성
                lbl1 = new Label();
                pictureBox1.Controls.Add(lbl1);

                lbl1.Size = new Size(65, 20);
                lbl1.Location = new Point(5 + i * WIDTH, 5);

                lbl1.Text = "User " + (i + 1).ToString();
                lbl1.Tag = "0" + i.ToString();

                lbl1.BorderStyle = BorderStyle.FixedSingle;
                lbl1.TextAlign = ContentAlignment.MiddleCenter;
                lbl1.BackColor = Color.WhiteSmoke;

                if (i == 0)// 최초 선택 표시
                {
                    SelectedUser = 0;
                    lbl1.BackColor = Color.Yellow;
                }

                //아래 label 생성
                lbl2 = new Label();
                pictureBox1.Controls.Add(lbl2);

                lbl2.Size = new Size(65, 20);
                lbl2.Location = new Point(5 + i * WIDTH, 450 + STARTPOINT_X);

                lbl2.Text = ResultValue[i];
                lbl2.Tag = "1" + i.ToString();

                lbl2.BorderStyle = BorderStyle.FixedSingle;
                lbl2.TextAlign = ContentAlignment.MiddleCenter;
                lbl2.BackColor = Color.WhiteSmoke;

                // 클릭/더블클릭 이벤트와 연결한다.
                // 클릭/더블클릭 이벤트와 연결한다
                lbl1.Click += new System.EventHandler(lbl_Click);
                lbl1.DoubleClick += new System.EventHandler(lbl_DClick);
                lbl2.DoubleClick += new System.EventHandler(lbl_DClick);
            }
        }
     
        private Point GetPos(int nX, int nY)
        {
            return new Point(nX * WIDTH + STARTPOINT_X, nY * 3 + STARTPOINT_Y);
        }
        private void lbl_Click(object sender, System.EventArgs e)
        // 동적으로 생성된 컨트롤의 클릭 이벤트(라벨에 사용자가 원하는 값입력)
        {
            // 원래 선택된 유저 배경색을 WhiteSmoke(선택 안된 상태)로 만든다.

            Label lbl;

            //선택된 라벨의 색을 바꿔준다.
            lbl = GetLblCtrl("0" + SelectedUser);
            lbl.BackColor = Color.WhiteSmoke;

            // 선택한 유저 배경색을 Yellow로 만들고 저장한다.
            String strTag;
            ((Label)sender).BackColor = Color.Yellow;
            strTag = ((Label)sender).Tag.ToString();
            SelectedUser = Convert.ToInt32(strTag.Substring(1, 1));
            pictureBox2.Location = GetPos(SelectedUser, 0);
        }


        private void lbl_DClick(object sender, System.EventArgs e)
        //동적으로 생성한 컨트롤의 더블클릭 이벤트(라벨에 사용자가 원하는 값입력)
        {
            Form2 frm = new Form2();

            String strTag;
            strTag = ((Label)sender).Tag.ToString();

            if (strTag.Substring(0, 1) == "0")//사용자를 더블클릭한 경우
            {
                frm.Text = "사용자 입력";
                frm.label1.Text = "사용자의 이름을 입력하세요";
            }
            else
            {
                frm.Text = "당첨, 꽝!";
                frm.label1.Text = "사다리 타기로 내기한 \n내용을 입력하세요";
            }

            if (frm.ShowDialog(this) == DialogResult.OK) 
                ((Label)sender).Text = frm.textBox1.Text;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        //생성한 사다리 배열을 이용해 picturebox에 그려준다.
        {
            int i, j;
            int x1, y1, x2, y2;

            for (i = 0; i < (int)numericUpDown1.Value; i++)
            {
                for (j = 0; j < HEIGHT_SIZE; j++)
                {
                    // 가로줄 그리기
                    if (Sadari[i, j] == 2)
                    {
                        // 좌표 계산
                        x1 = i * WIDTH + STARTPOINT_X;
                        y1 = STARTPOINT_Y + j * 3;
                        x2 = i * WIDTH + STARTPOINT_X + WIDTH;
                        y2 = STARTPOINT_Y + j * 3;
                        x1 += 15; 
                        x2 += 15;
                        if (VisitedSadari[i, j] != 0)
                            e.Graphics.DrawLine(DrawingVisited[VisitedSadari[i, j] - 1], x1, y1, x2, y2);
                        else
                            e.Graphics.DrawLine(DrawingSadari, x1, y1, x2, y2);
                        
                    }

                    //세로줄(그리기)
                    if (j != HEIGHT_SIZE)
                    {
                        // 좌표계산
                        x1 = i * WIDTH + STARTPOINT_X;
                        y1 = STARTPOINT_Y + j * 3 - 1;
                        x2 = i * WIDTH + STARTPOINT_X;
                        y2 = STARTPOINT_Y + (j + 1) * 3;
                        x1 = x2 += 15;
                        if (VisitedSadari[i, j] != 0)
                            e.Graphics.DrawLine(DrawingVisited[VisitedSadari[i, j] - 1], x1, y1, x2, y2);
                        else
                            e.Graphics.DrawLine(DrawingSadari, x1, y1, x2, y2);
                    }
                }

            }
        }

        private void FindWay(int nStartX)
        //플레이어가 이동할 길을 찾는다.
        {
            int i, j = 0, k = 0;
            Point pos1, pos2;
            i = nStartX;
            

            while (true)
            {
                VisitedSadari[i, j] = SelectedUser + 1;
                pictureBox2.Location = GetPos(i, j); // 현재 좌표에 맞게 아이콘을 이동시킨다.
                if (Sadari[i, j] == 1)  // 1일 경우엔 계속 세로줄을 타고 내려간다.
                    j += 1;
                else if (Sadari[i, j] == 2)//2일 경우엔 오른쪽 세로줄로 이동한다.
                {
                    pos1 = GetPos(i, j); // 현재 좌표를 구한다.
                    i += 1; // 세로줄을 옮긴다
                    VisitedSadari[i, j] = SelectedUser + 1; // 방문 표시를 한다.
                    pos2 = GetPos(i, j); // 옮길 좌표를 구한다.

                    for (k = pos1.X; k <= pos2.X; k += 5)// 아이콘의 가로 이동을 보여준다.
                    {
                        Application.DoEvents();
                        Thread.Sleep(5);
                        pictureBox2.Location = new Point(k, pos1.Y);
                    }
                    j += 1;
                }
                else if (Sadari[i, j] == 3) //3일 경우엔 왼쪽 세로줄로 이동한다.
                {
                    pos2 = GetPos(i, j);
                    i -= 1;
                    pos1 = GetPos(i, j);
                    VisitedSadari[i, j] = SelectedUser + 1;

                    for (k = pos2.X; k >= pos1.X; k -= 5)
                    {
                        Application.DoEvents();
                        Thread.Sleep(5);
                        pictureBox2.Location = new Point(k, pos1.Y);
                    }
                    j += 1;
                }
                Application.DoEvents();
                Thread.Sleep(5);

                if (j >= HEIGHT_SIZE) break; // 이동 끝
            }
            Label label;
            Point point;
            for (i = 0; i < numericUpDown1.Value; i++)
            {
                label = GetLblCtrl("1" + i.ToString());
                point = label.Location;
                if ((point.X == (pictureBox2.Location.X - 15)))
                {
                    label.Visible = true;
                    labelArrived[i] = true;
                    PlayerResertChecked[SelectedUser] = true;
                    break;
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
         //플레이어의 수가 변경되면 사다리를 인원수에 맞게 재배치하고
         //관련 배열을 초기화해준다.
        {
            InitSadari();
            pictureBox1.Invalidate();
            Array.Clear(labelArrived, 0, labelArrived.Length);
            Array.Clear(PlayerResertChecked, 0, PlayerResertChecked.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        //시작버튼을 클릭하여 사다리를 탄다.
        {
            Label lbl;
            int i;

            //시작 버튼을 클릭하면 이미 결과가 나온 라벨을 제외하곤 전부 보이지 않게 처리한다.
            for(i = 0; i < numericUpDown1.Value;i++)
            {
                if (labelArrived[i]) continue;
                lbl = GetLblCtrl("1"+i.ToString());
                lbl.Visible = false;
            }
            for(i = 0; i < PlayerResertChecked.Length; i++)
            {
                if (!PlayerResertChecked[i])
                {
                    Label label;
                    label = GetLblCtrl("0" + SelectedUser);
                    label.BackColor = Color.WhiteSmoke;

                    SelectedUser = i;
                    PlayerResertChecked[i] = true;
                    label = GetLblCtrl("0" + SelectedUser);
                    label.BackColor = Color.Yellow;
                    break;
                }
            }
            if (i == PlayerResertChecked.Length) InitSadari();
            //해당 플레이어에 맞는 사다리를 따라가는 함수
            FindWay(SelectedUser);

        }

        private void button2_Click(object sender, EventArgs e)
            //사다리 초기화 버튼
        {
            InitSadari();
            pictureBox1.Invalidate();
            Array.Clear(labelArrived, 0, labelArrived.Length);
            Array.Clear(PlayerResertChecked, 0, PlayerResertChecked.Length);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rn = new Random();
            button1.ForeColor = Color.FromArgb(rn.Next(255),rn.Next(255), rn.Next(255));
            button1.Invalidate();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
