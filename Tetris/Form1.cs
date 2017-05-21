using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using TetrisLibrary;

namespace Tetris
{

    public partial class Form1 : Form
    {
        public class Game_Board  // 게임보드 내의 정보를 제어하는 클래스
        {
            public int[,] smap = new int[20, 10];      // 게임보드 내의 블록 분포를 나타내는 맵
            public Box[,] bmap = new Box[20, 10];       // 맵에 연동된 box를 제어
        }
        bool Now_Playing = false;                   //현재 게임이 진행 중임을 나타내는 플래그
        Game_Board g = new Game_Board();
        Block Nowfalling;                           //현재 떨어지고 있는 블록
        Block Nextfalling;                          //다음에 떨어질 블록

        int remain_minutes = 3;
        int remain_seconds = 60;
        public static Random r = new Random();

        public void Game_Init()                    //게임 시작시 초기화
        {
            this.Nowfalling = new Block(r.Next(1, 8), r.Next(1, 8));
            foreach (Box b in this.Nowfalling.blocks) //게임보드에 블록 추가
                this.BackGround.Controls.Add(b);
            Thread.Sleep(500);
            this.Nextfalling = new Block(r.Next(1, 8), r.Next(1, 8));
            this.Nextfalling.Location_Next();
            foreach (Box b in this.Nextfalling.blocks) //Next Block에 해당 블록 표시
                this.Next_Block.Controls.Add(b);
            this.Now_Playing = true;
        }
        public void Game_Over()
        {
            this.timer1.Stop();
            this.timer2.Stop();
            MessageBox.Show("Game Over!");
        }
        public void Block_Push()                       //Next Block에 있는 Block을 게임보드에 추가하고 다음 블록을 Next Block에 추가              
        {
            int i, j, sum;
            foreach (Box b in this.Nowfalling.blocks)   //Nowfalling의 위치정보를 Gameboard에 저장하고 처리한다
            {
                sum = 0;
                i = b.ypos / 30;
                j = b.xpos / 30;
                this.g.smap[i, j] = 1;
                this.g.bmap[i, j] = b;
                for (int n = 0; n < 10; n++)             //맨 윗줄에 블록이 닿았을 때 게임 오버
                {
                    if (this.g.smap[0, n] == 1)
                    {
                        this.Now_Playing = false;
                        return;
                    }
                }
                for (int n = 0; n < 10; n++)
                    sum += this.g.smap[i, n];
                if (sum == 10)         //한 줄이 꽉 찼을때
                {
                    for (int n = 0; n < 10; n++)   //해당 줄의 모든 블록 제거
                    {
                        Box x = this.g.bmap[i, n];
                        this.BackGround.Controls.Remove(x);
                        this.g.bmap[i, n] = null;
                        x.Dispose();
                    }
                    for (int n = i; n > 0; n--)            //윗줄의 블록들을 한 줄씩 아래로 내림
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            this.g.bmap[n, m] = this.g.bmap[n - 1, m]; //윗줄의 블록을 아랫줄에 복사
                            this.g.smap[n, m] = this.g.smap[n - 1, m]; //윗줄의 맵 정보를 아랫줄에 복사
                            Box x = this.g.bmap[n, m];
                            if (x == null)                             //x가 null일때 다음 루프 실행
                                continue;
                            x.Location = new Point(x.xpos, x.ypos + 30); //윗줄의 블록들을 한 줄 아래로 내림
                            x.ypos += 30;
                        }
                    }
                    for (int n = 0; n < 10; n++)  //맨 윗줄 초기화
                    {
                        this.g.smap[0, n] = 0;
                        this.g.bmap[0, n] = null;
                    }
                }
            }
            this.Nowfalling = this.Nextfalling;        //Next Block에 있던 block이 Nowfalling이 되게 함
            foreach (Box b in this.Nowfalling.blocks)  //Next Block에서 해당 block 제거
                this.Next_Block.Controls.Remove(b);
            this.Nowfalling.Location_Init();           //게임보드에서의 위치와 맞게 위치 초기화
            foreach (Box b in this.Nowfalling.blocks)  //게임보드에 해당 block 추가
                this.BackGround.Controls.Add(b);
            this.Nextfalling = new Block(r.Next(1, 8), r.Next(1, 8));//Next Block에 들어갈 block 생성
            this.Nextfalling.Location_Next();
            foreach (Box b in this.Nextfalling.blocks) //Next Block에 해당 블록 표시
                this.Next_Block.Controls.Add(b);
        }
        public void Block_Falling() // 블록이 떨어지게 함
        {
            int i, j;
            foreach (Box b in this.Nowfalling.blocks) //블록이 다른 블록에 닿거나 바닥에 닿았을 때 정지하고 다음 블록을 움직임
            {
                if (b.ypos % 30 == 0)                   //블록이 한 칸 안에 온전히 들어왔을 때
                {
                    i = b.ypos / 30;
                    j = b.xpos / 30;
                    if (i == 19)                      //Out of index를 피하기 위해 따로 처리, 바닥에 닿았을 때
                    {
                        this.Block_Push();           //다음 블록을 움직임
                        return;
                    }
                    else if (this.g.smap[i + 1, j] == 1) // 다른 블록에 닿았을 때
                    {
                        this.Block_Push();
                        return;
                    }
                }
            }
            this.Nowfalling.Move_Down(); //아래로 10만큼 이동
        }
        public Form1()
        {
            InitializeComponent();
            this.Game_Init();
            this.timer1.Start();
            this.timer2.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int i, j;
            if (e.KeyCode == Keys.Up)            //왼쪽으로 90도 회전
            {
                
                byte[,] temp = new byte[4, 4];
                for (i = 0; i < 4; i++)                   //block_status가 왼쪽으로 회전하였을 때 상태 저장
                {
                    for (j = 0; j < 4; j++)
                        temp[i, j] = this.Nowfalling.block_status[j, 3-i];
                }

                int xtemp = this.Nowfalling.originX;    // 왼쪽으로 회전하였을 때 x 위치를 알기 위한 변수
                int ytemp = this.Nowfalling.originY;    // 오른쪽으로 회전하였을 때 y 위치를 알기 위한 변수

                for (i = 0; i < 4; i++)               //temp를 원점부터 검사한다
                {
                    for (j = 0; j < 4; j++)
                    {
                        if (temp[i, j] == 1)         //temp에서 box를 발견하면
                        {
                            int x = xtemp / 30;
                            int y = ytemp / 30;
                            //회전하였을 때 블록이 게임판을 벗어났을때 명령 무시
                            if (x < 0 || x > 9 || y < 0 || y > 19)
                                return;
                            else if(ytemp % 30 == 0) // 블록이 정위치에 있을 때
                            {
                                if (this.g.smap[y, x] == 1) //회전한 위치에 블록이 있으면 명령 무시
                                    return;
                            }
                            else if(ytemp % 30 != 0) // 블록이 정위치에 위치하지 않을 때
                            {
                                if (this.g.smap[y, x] == 1) //회전한 위치 위에 블록이 있으면 명령 무시
                                    return;
                                else if (this.g.smap[y + 1, x] == 1) //회전한 위치 아래에 블록이 있으면 명령 무시
                                    return;
                            }
                        }
                        xtemp += 30;                       //temp 상태에 맞는 x값을 결정하기 위해 x값 증가    
                    }
                    xtemp = this.Nowfalling.originX;       //루프를 한 번 돌았으므로 x값 초기화
                    ytemp += 30;
                }
                this.Nowfalling.Rotation();
            }
            else if (e.KeyCode == Keys.Down)     //블록 하강속도를 50배 빠르게 함
                this.timer1.Interval = 10;
            else if (e.KeyCode == Keys.Left) //왼쪽 방향키 입력 시 블록을 한 칸 왼쪽으로 이동
            {
                foreach (Box b in this.Nowfalling.blocks)
                {
                    i = b.ypos / 30;
                    j = b.xpos / 30;
                    if (j == 0)  //out of index를 피하기 위해 따로 처리, 가장 왼쪽에 닿았을 때 명령 무시
                        return;
                    else if (b.ypos % 30 == 0) //블록이 정위치에 위치하였을 때
                    {
                        if (this.g.smap[i, j - 1] == 1) //왼쪽에 블록이 존재하면 명령 무시
                            return;
                    }
                    else                  //블록이 정위치에 위치하지 않을 때
                    {
                        if (this.g.smap[i, j - 1] == 1)         //왼쪽 상단에 블록이 있을 때 명령 무시
                            return;
                        else if (this.g.smap[i + 1, j - 1] == 1) // 왼쪽 하단에 블록이 있을 때 명령 무시
                            return;
                    }
                }
                this.Nowfalling.Move_Left(); //왼쪽으로 한 칸 이동
            }
            else if (e.KeyCode == Keys.Right) //왼쪽 방향키 입력 시 블록을 한 칸 왼쪽으로 이동          
            {
                foreach (Box b in this.Nowfalling.blocks)
                {
                    i = b.ypos / 30;
                    j = b.xpos / 30;
                    if (j == 9)  //out of index를 피하기 위해 따로 처리, 가장 오른쪽에 닿았을 때 명령 무시
                        return;
                    else if (b.ypos % 30 == 0) //블록이 정위치에 위치하였을 때
                    {
                        if (this.g.smap[i, j + 1] == 1) //오른쪽에 블록이 존재하면 명령 무시
                            return;
                    }
                    else                  //블록이 정위치에 위치하지 않을 때
                    {
                        if (this.g.smap[i, j + 1] == 1)         //오른쪽 상단에 블록이 있을 때 명령 무시
                            return;
                        else if (this.g.smap[i + 1, j + 1] == 1) // 오른쪽 하단에 블록이 있을 때 명령 무시
                            return;
                    }
                }
                this.Nowfalling.Move_Right();  //오른쪽으로 한 칸 이동
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Now_Playing == false) // now playing이 false일때 game over!
            {
                this.Game_Over();
                return;
            }
            this.Block_Falling();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                this.timer1.Interval = 500;
        }

        private void timer2_Tick(object sender, EventArgs e) // 남은 시간 표시
        {
            if (this.remain_minutes == 0 && this.remain_seconds == 0) // 남은 시간 0일때 게임 오버
            {
                this.Now_Playing = false;
                this.Game_Over();
                return;
            }
            else if (this.remain_minutes != 0 && this.remain_seconds == 60) // 0초일때 분과 초를 각각 1씩 깎는다
            {
                this.remain_minutes--;
                this.remain_seconds--;
            }
            else if (this.remain_minutes != 0 && this.remain_seconds == 1) // 0초가 됐을때 60초로 계산
            {
                this.remain_seconds = 60;
            }
            else                                                           // 1초당 남은 시간을 1초씩 깎는다
                this.remain_seconds--;
            if (this.remain_seconds / 10 != 0)                                // 남은 초가 10 이상 일때
            {
                this.Remain_Minutes.Text = "0" + this.remain_minutes;      // 라벨에 현재 남은 분 표시
                if (this.remain_seconds == 60)                             // 0초일때 라벨에 00으로 표시
                    this.Remain_Seconds.Text = "00";
                else                                                       // 라벨에 현재 남은 초 표시
                    this.Remain_Seconds.Text = this.remain_seconds.ToString();
            }
            else                                                           // 남은 초가 10 이하 일때
            {
                this.Remain_Minutes.Text = "0" + this.remain_minutes;
                this.Remain_Seconds.Text = "0" + this.remain_seconds;
            }
        }
    }
}
