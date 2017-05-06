using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisLibrary
{
    public class Box : PictureBox  // 테트리스 블록을 구성하는 박스
    {
        public int xpos;
        public int ypos;
    }
    public class Block            // 테트리스 블록
    {
        public byte[,] block_status;                    // 테트리스 블록을 구성하는 box의 배치 상태
        public bool is_focus = false;
        public List<Box> blocks = new List<Box>();      // 블록을 구성하는 box의 list

        public Block()                              // 블록 초기화
        {
            block_status = new byte[4, 4];
            this.Status_Init();                     // 블록 Status 초기화
            Box temp;                               // box를 추가하기 위한 임시 객체
            
            Color c = new Color();                  // block의 색상
            int n;                                  // block의 색상을 랜덤하게 결정하기 위한 값
            Random r = new Random();
            n = r.Next(1, 7);

            switch (n)
            {
                case 1:
                    {
                        c = Color.Red;
                        break;
                    }
                case 2:
                    {
                        c = Color.Orange;
                        break;
                    }
                case 3:
                    {
                        c = Color.Yellow;
                        break;
                    }
                case 4:
                    {
                        c = Color.Green;
                        break;
                    }
                case 5:
                    {
                        c = Color.Blue;
                        break;
                    }
                case 6:
                    {
                        c = Color.Navy;
                        break;
                    }
                case 7:
                    {
                        c = Color.Purple;
                        break;
                    }
                default:
                    break;
            }
            for (int i = 0; i < 4; i++)                    //boxlist에 4개의 box를 생성하여 추가한다.
            {
                temp = new Box();
                temp.Size = new Size(30, 30);
                temp.BackColor = c;
                temp.BorderStyle = BorderStyle.FixedSingle;
                this.blocks.Add(temp);
            }
            this.Location_Init();
        }
        public void Location_Init()                   //블록 위치 초기화
        {
            int xpos = 90;                          // box의 Location.x를 설정하기 위한 변수
            int ypos = -30;                         // box의 Location.y를 설정하기 위한 변수
            int index = 0;                          // blocks에 들어갈 box의 인덱스 값
            for (int i = 0; i < 4; i++)                     //block_status에 따른 배치대로 box를 배치한다.
            {
                for (int j = 0; j < 4; j++)
                {
                    if (block_status[i, j] == 1)         //block_status에서 box를 발견하면 위치를 지정한다.
                    {
                        this.blocks[index].Location = new Point(xpos, ypos);
                        this.blocks[index].xpos = xpos;
                        this.blocks[index].ypos = ypos;
                        index++;
                    }
                    xpos += 30;                       //block_status에 맞는 x값을 부여하기 위해 x값 증가    
                }
                xpos = 90;                            //루프를 한 번 돌았으므로 x값 초기화
                ypos += 30;
            }
        }
        public void Location_Next()                   //Next block창 안에서의 위치를 지정
        {
            int xpos = 0;                          // box의 Location.x를 설정하기 위한 변수
            int ypos = 0;                         // box의 Location.y를 설정하기 위한 변수
            int index = 0;                          // blocks에 들어갈 box의 인덱스 값
            for (int i = 0; i < 4; i++)                     //block_status에 따른 배치대로 box를 배치한다.
            {
                for (int j = 0; j < 4; j++)
                {
                    if (block_status[i, j] == 1)         //block_status에서 box를 발견하면 위치를 지정한다.
                    {
                        this.blocks[index].Location = new Point(xpos, ypos);
                        this.blocks[index].xpos = xpos;
                        this.blocks[index].ypos = ypos;
                        index++;
                    }
                    xpos += 30;                       //block_status에 맞는 x값을 부여하기 위해 x값 증가    
                }
                xpos = 0;                            //루프를 한 번 돌았으므로 x값 초기화
                ypos += 30;
            }
        }
        public void Status_Init()                     //블록 생성 시 모양을 결정
        {
            Random r = new Random();
            int i = r.Next(1, 7);
            switch (i)
            {
                case 1:
                    {
                        this.block_status[1, 0] = 1;
                        this.block_status[1, 1] = 1;
                        this.block_status[1, 2] = 1;
                        this.block_status[1, 3] = 1;
                        break;
                    }
                case 2:
                    {
                        this.block_status[1, 1] = 1;
                        this.block_status[2, 1] = 1;
                        this.block_status[2, 2] = 1;
                        this.block_status[2, 3] = 1;
                        break;
                    }
                case 3:
                    {
                        this.block_status[1, 1] = 1;
                        this.block_status[1, 2] = 1;
                        this.block_status[2, 2] = 1;
                        this.block_status[2, 3] = 1;
                        break;
                    }
                case 4:
                    {
                        this.block_status[1, 1] = 1;
                        this.block_status[1, 2] = 1;
                        this.block_status[2, 1] = 1;
                        this.block_status[2, 2] = 1;
                        break;
                    }
                case 5:
                    {
                        this.block_status[1, 3] = 1;
                        this.block_status[2, 3] = 1;
                        this.block_status[2, 2] = 1;
                        this.block_status[2, 1] = 1;
                        break;
                    }
                case 6:
                    {
                        this.block_status[1, 1] = 1;
                        this.block_status[1, 2] = 1;
                        this.block_status[2, 2] = 1;
                        this.block_status[2, 3] = 1;
                        break;
                    }
                case 7:
                    {
                        this.block_status[1, 1] = 1;
                        this.block_status[2, 0] = 1;
                        this.block_status[2, 1] = 1;
                        this.block_status[2, 2] = 1;
                        break;
                    }
                default:
                    break;
            }


        }
    }
}
