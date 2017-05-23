using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//연호 구현 사항 -Start
namespace TetrisLibrary
{
    public class Box : PictureBox  // 테트리스 블록을 구성하는 박스 
    {
        public int xpos;
        public int ypos;
        public int item;
    }
    public class Block            // 테트리스 블록 
    {
        public byte[,] block_status;                // 테트리스 블록을 구성하는 box의 배치 상태 
        public int originX;                         //블록을 둘러싼 가상의 사각형의 x좌표 원점 
        public int originY;                         //블록을 둘러싼 가상의 사각형의 y좌표 원점 
        public List<Box> blocks = new List<Box>();  // 블록을 구성하는 box의 list 


        public Block(int random1, int random2)      // 블록 초기화 
        {
            block_status = new byte[4, 4];
            this.Status_Init(random1);              // 블록 Status 초기화 
            Box temp;                               // box를 추가하기 위한 임시 객체 
            Color c = new Color();                  // block의 색상 
            Random r = new Random();                // 블록에 아이템을 추가할 랜덤 객체 
            switch (random2)
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
                temp.item = r.Next(1, 101);                //1~100 중에 91~100만 아이템(10%확률) 
                this.blocks.Add(temp);
            }
            this.Location_Init();
        }
        public void Location_Init()                   //블록 위치 초기화 
        {
            originX = 90;                          //블록을 둘러싼 영역 x 좌표 원점 초기화 
            originY = -30;                         //블록을 둘러싼 영역 y 좌표 원점 초기화 
            int xtemp = originX;                   // box의 Location.x를 설정하기 위한 변수 
            int ytemp = originY;                   // box의 Location.y를 설정하기 위한 변수 
            int index = 0;                         // blocks에 들어갈 box의 인덱스 값 

            for (int i = 0; i < 4; i++)                  //block_status에 따른 배치대로 box를 배치한다.(원점부터 탐색) 
            {
                for (int j = 0; j < 4; j++)
                {
                    if (block_status[i, j] == 1)         //block_status에서 box를 발견하면 위치를 지정한다. 
                    {
                        this.blocks[index].Location = new Point(xtemp, ytemp);
                        this.blocks[index].xpos = xtemp;
                        this.blocks[index].ypos = ytemp;
                        index++;
                    }
                    xtemp += 30;                       //block_status에 맞는 x값을 부여하기 위해 x값 증가 
                }
                xtemp = 90;                            //루프를 한 번 돌았으므로 x값 초기화 
                ytemp += 30;
            }
        }
        public void Location_Next()                   //Next block창 안에서의 위치를 지정 
        {
            originX = 0;                         //블록을 둘러싼 영역 x 좌표 원점 초기화 
            originY = 0;                         //블록을 둘러싼 영역 y 좌표 원점 초기화 
            int xtemp = 0;                       // box의 Location.x를 설정하기 위한 변수 
            int ytemp = 0;                       // box의 Location.y를 설정하기 위한 변수 
            int index = 0;                       // blocks에 들어갈 box의 인덱스 값 

            for (int i = 0; i < 4; i++)                  //block_status에 따른 배치대로 box를 배치한다.(원점부터 탐색) 
            {
                for (int j = 0; j < 4; j++)
                {
                    if (block_status[i, j] == 1)         //block_status에서 box를 발견하면 위치를 지정한다. 
                    {
                        this.blocks[index].Location = new Point(xtemp, ytemp);
                        this.blocks[index].xpos = xtemp;
                        this.blocks[index].ypos = ytemp;
                        index++;
                    }
                    xtemp += 30;                       //block_status에 맞는 x값을 부여하기 위해 x값 증가 
                }
                xtemp = 0;                            //루프를 한 번 돌았으므로 x값 초기화 
                ytemp += 30;
            }
        }
        public void Move_Right()
        {
            foreach (Box b in this.blocks)        //오른쪽으로 블록 한 칸 이동 
            {
                b.Location = new Point(b.xpos + 30, b.ypos);
                b.xpos += 30;
            }
            originX += 30;                       //블록을 둘러싼 영역 X좌표 원점도 이동 
        }
        public void Move_Left()
        {
            foreach (Box b in this.blocks)        //왼쪽으로 블록 한 칸 이동 
            {
                b.Location = new Point(b.xpos - 30, b.ypos);
                b.xpos -= 30;
            }
            originX -= 30;                       //블록을 둘러싼 영역 X좌표 원점도 이동 
        }
        public void Move_Down()
        {
            foreach (Box b in this.blocks)   //10씩 아래로 이동                               
            {
                b.Location = new Point(b.xpos, b.ypos + 10);
                b.ypos += 10;
            }
            originY += 10;
        }
        public void Rotation()  //왼쪽으로 90도 회전 
        {
            byte[,] temp = new byte[4, 4];
            for(int i=0; i<4;i++)                   //block_status 값 임시 저장 
            {
                for (int j = 0; j < 4; j++)
                    temp[i, j] = block_status[i, j];
            }
            for(int i=0; i<4;i++)                   //block_status 왼쪽으로 회전 
            {
                for (int j = 0; j < 4; j++)
                    block_status[i, j] = temp[j,3-i];
            }

            int xtemp = originX;                   // box의 Location.x를 설정하기 위한 변수 
            int ytemp = originY;                   // box의 Location.y를 설정하기 위한 변수 
            int index = 0;                         // blocks에 들어갈 box의 인덱스 값 

            for (int i = 0; i < 4; i++)                  //block_status에 따른 배치대로 box를 배치한다.(원점부터 탐색) 
            {
                for (int j = 0; j < 4; j++)
                {
                    if (block_status[i, j] == 1)         //block_status에서 box를 발견하면 위치를 지정한다. 
                    {
                        this.blocks[index].Location = new Point(xtemp, ytemp);
                        this.blocks[index].xpos = xtemp;
                        this.blocks[index].ypos = ytemp;
                        index++;
                    }
                    xtemp += 30;                       //block_status에 맞는 x값을 부여하기 위해 x값 증가  
                }
                xtemp = originX;                       //루프를 한 번 돌았으므로 x값 초기화 
                ytemp += 30;
            }
        }
        private void Status_Init(int random1) //블록 생성 시 모양을 결정 
        {
            switch (random1)
            {
                case 1:
                    {   
                        /*
                         ㅁㅁㅁㅁ
                         */
                        this.block_status[1, 0] = 1;
                        this.block_status[1, 1] = 1;
                        this.block_status[1, 2] = 1;
                        this.block_status[1, 3] = 1;
                        break;
                    }
                case 2:
                    {   
                        /*
                         ㅁ
                         ㅁㅁㅁ
                         */
                        this.block_status[1, 1] = 1;
                        this.block_status[2, 1] = 1;
                        this.block_status[2, 2] = 1;
                        this.block_status[2, 3] = 1;
                        break;
                    }
                case 3:
                    {   
                        /*
                         ㅁㅁ
                           ㅁㅁ
                         */ 
                        this.block_status[1, 1] = 1;
                        this.block_status[1, 2] = 1;
                        this.block_status[2, 2] = 1;
                        this.block_status[2, 3] = 1;
                        break;
                    }
                case 4:
                    {
                        /*
                         ㅁㅁ
                         ㅁㅁ
                         */ 
                        this.block_status[1, 1] = 1;
                        this.block_status[1, 2] = 1;
                        this.block_status[2, 1] = 1;
                        this.block_status[2, 2] = 1;
                        break;
                    }
                case 5:
                    {   
                        /*
                             ㅁ
                         ㅁㅁㅁ
                         */
                        this.block_status[1, 3] = 1;
                        this.block_status[2, 3] = 1;
                        this.block_status[2, 2] = 1;
                        this.block_status[2, 1] = 1;
                        break;
                    }
                case 6:
                    {
                        /*
                           ㅁㅁ
                         ㅁㅁ
                         */ 
                        this.block_status[1, 2] = 1;
                        this.block_status[1, 3] = 1;
                        this.block_status[2, 1] = 1;
                        this.block_status[2, 2] = 1;
                        break;
                    }
                case 7:
                    {
                        /* 
                           ㅁ
                         ㅁㅁㅁ
                         */ 
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
//연호 구현 사항 -End