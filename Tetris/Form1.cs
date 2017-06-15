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
using System.IO;
using System.Net.Sockets;
using TetrisPacket;
using System.Media;
using WMPLib;

namespace Tetris
{
    public partial class Form1 : Form
    {

        //동현 구현 사항 -Start
        bool aitem = false;                         //속도 아이템이 사용 중인지 나타내는 플래그
        bool isblind = false;
        int tickCount = 0;                          //속도 아이템 적용시 timer의 tick을 계산
        int blindtc = 0;
        int nomalSpeed = 300;                       //기본 게임 속도
        int tempSpeed;                             //아이템 속도

        SoundPlayer effect = new SoundPlayer(Properties.Resources.Tetris_Explosion_Short);  //effectSound
        //동현 구현 사항 - End

        //가송 구현 사항 - start
        private WindowsMediaPlayer m_wmp; //bgm
        IWMPMedia m_Media;
        IWMPPlaylist PlayList;
        private uint tot_file_size = 0;
        private uint cur_file_size = 0;
        //가송 구현 사항 - END

        //연호 구현 사항 -Start
        public class Game_Board  // 게임보드 내의 정보를 제어하는 클래스 
        {
            public byte[,] smap = new byte[20, 10];     // 게임보드 내의 블록 분포를 나타내는 맵 
            public Box[,] bmap = new Box[20, 10];       // 맵에 연동된 box를 제어 
        }

        bool Now_Playing = false;                   //현재 게임이 진행 중임을 나타내는 플래그 
        Game_Board g;                               //게임보드 객체 
        Block Nowfalling;                           //현재 떨어지고 있는 블록 
        Block Nextfalling;                          //다음에 떨어질 블록 

        int score;                              //게임 점수 
        int combo;                              //콤보 횟수 
        int remain_minutes;                     //남은 분 
        int remain_seconds;                     //남은 초 
        byte game_mode;                             //게임이 싱글 모드인지 멀티 모드인지 판별 
        public static Random r = new Random();      //블록 생성을 위한 랜덤 객체 
        //연호 구현 사항 -End

        //가송 구현 사항 -Start
        private Bitmap bmp;
        //서버 - 클라이언트 연결 확인용 플래그.
        private bool connect_flag = false;
        private bool conn_over_flag = false;
        private TcpListener listener;
        private TcpClient client;
        //실시간 수신을 위한 쓰레드.
        private Thread receiveThread;
        //포트 번호는 고정.
        private const int PORT = 7777;
        //통신용 스트림.
        private NetworkStream netStream;
        //스트림에 연결하여 바이너리로 읽고 쓴다.
        private BinaryReader dataReader;
        private BinaryWriter dataWriter;
        //파일 캡쳐시 필요한 플래그(미확정)
        private bool file_flag = false;
        //송수신 데이터를 담을 byte 배열.
        private byte[] readBuffer = new byte[1024 * 4];
        private byte[] writeBuffer = new byte[1024 * 4];
        private void aa()
        {
            this.netStream.Write(this.writeBuffer, 0, this.writeBuffer.Length);
            this.netStream.Flush();
            for (int i = 0; i < 1024 * 4; i++)
                this.writeBuffer[i] = 0;
        }
        private int receive_number = 0;

        //캡쳐할 화면 : BackGround , 상대방 화면 : Enemy_Screen
        public void Copy(string outputFilename)
        {
            bmp = new Bitmap(this.BackGround.Width, this.BackGround.Height);

            // Bitmap 이미지 변경을 위해 Graphics 객체 생성
            Graphics gr = Graphics.FromImage(bmp);
            //copyfromscreen 메서드에 사용하기 위해 절대좌표로 변환.
            Point newPoint = PointToScreen(new Point(BackGround.Location.X, BackGround.Location.Y));
            //지정한 영역을 캡쳐
            gr.CopyFromScreen((newPoint.X), (newPoint.Y), 0, 0, BackGround.Size);

            //캡쳐한 이미지를 파일로 저장
            bmp.Save(outputFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
            gr.Dispose();
            //파일을 패킷으로 보낸다.
            Send(outputFilename);

        }
        //가송 구현 사항 - End
        //연호 구현 사항 - Start

        public void Game_Init()                    //게임 시작시 초기화 
        {
            this.BackGround.Controls.Clear();      //게임 보드 내부 블록 초기화(다시 시작할 때 대비) 
            this.Next_Block.Controls.Clear();      //Next block 내부 블록 초기화(다시 시작할 때 대비)
            this.Nowfalling = new Block(r.Next(1, 8), r.Next(1, 8));
            foreach (Box b in this.Nowfalling.blocks) //게임보드에 블록 추가 
                this.BackGround.Controls.Add(b);
            Thread.Sleep(500);
            this.Nextfalling = new Block(r.Next(1, 8), r.Next(1, 8));
            this.Nextfalling.Location_Next();
            foreach (Box b in this.Nextfalling.blocks) //Next Block에 해당 블록 표시 
                this.Next_Block.Controls.Add(b);
            this.Now_Playing = true;
            this.score = 0;
            this.Score_text.Text = "0";
            this.combo = 0;
            this.remain_minutes = 3;
            this.remain_seconds = 60;
            this.Remain_Minutes.Text = "03";
            this.Remain_Seconds.Text = "00";
            this.g = new Game_Board();
            this.timer1.Start();
            this.timer2.Start();
            //가송 구현 사항 - start
            m_wmp = new WindowsMediaPlayer();
            m_wmp.settings.autoStart = true; //auto start true 
            PlayList = m_wmp.newPlaylist("BGM", "");
            string path;
            path = System.Reflection.Assembly.GetExecutingAssembly().Location; //실행 파일 이름과 절대 경로 위치를 가져온다. 즉 debug/*.exe
            path = System.IO.Path.GetDirectoryName(path); //파일의 이름을 제외한 경로만을 추출한다.
            path += "\\Tetris_Bgm2.wav"; //debug 폴더에 존재하는 배경 음악용 wav 파일의 이름.
            m_Media = m_wmp.newMedia(@path); //windows media player 재생을 위한 미디어를 만든다.
            m_wmp.currentPlaylist = PlayList; //플레이어에 재생할 플레이리스트를 설정한다.
            PlayList.appendItem(m_Media); //미디어 파일을 추가한다.
            m_wmp.controls.play(); //재생한다.
            m_wmp.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange); //반복재생을 위한 이벤트를 추가한다.
            //가송 구현 사항 - End
        }
        public void Game_Over() //게임 오버 되었을 때 
        {
            m_wmp.controls.stop(); //배경음악 중지
            this.timer1.Stop(); //게임 중지
            this.timer2.Stop(); //남은 시간 체크 중지 
            this.Game_Over_Msg.Visible = true; //게임 오버 메시지 보이기 
            this.Restart_btn.Visible = true;   //재시작 버튼 보이기 
            this.Exit_btn.Visible = true;      //종료 버튼 보이기 
            if(connect_flag == true)
            {
                Send("게임오버"); 
                connect_flag = false;
                
            }     
        }
        public void Block_Push()                       //Next Block에 있는 Block을 게임보드에 추가하고 다음 블록을 Next Block에 추가           
        {
            int i, j, sum;
            this.combo = 0;
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
                    //  double m_cur_pos = m_wmp.controls.currentPosition;
                    m_wmp.controls.pause(); //BGM 일시정지

                    effect.Play();  //효과음 재생
                                    //  Thread.Sleep(100);
                    for (int n = 0; n < 10; n++)   //해당 줄의 모든 블록 제거 
                    {

                        Box x = this.g.bmap[i, n];
                        this.BackGround.Controls.Remove(x);
                        this.g.bmap[i, n] = null;
                        if (x.item > 98)           //블록에 아이템이 있을 때 
                        {
                            AppendItem();          //아이템 추가
                        }
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
                    this.score += 300 * (int)Math.Pow(combo, combo); //점수 = 300 * 콤보^2
                    this.Score_text.Text = score.ToString();
                    this.combo++;
                    this.Combo_label.Text = combo + " Combo!";
                    if (!Combo_label.Visible)
                        this.Combo_label.Visible = true;
                    m_wmp.controls.play(); //BGM 다시 재생
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
            if (this.Combo_label.Visible)//콤보 라벨을 표시할 때
            {
                this.timer3.Start();//타이머 작동
            }
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
                    else if (this.g.smap[i + 1, j] != 0) // 다른 블록에 닿았을 때 
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
            this.game_mode = 0;
            this.Game_Init();
        }
        public Form1(byte mode, string input)
        {
            InitializeComponent();
            this.Enemy_Screen.Visible = true;
            this.Enemy_Text.Visible = true;
            this.game_mode = mode;
            if (this.game_mode == 1) // 클라이언트 모드 
            {
                //클라이언트 시작 - 가송 구현
                ClientStart(input);
            }
            else if (this.game_mode == 2) // 서버 모드 
            {
                //서버 시작 - 가송 구현
                ServerStart();
            }
            this.Game_Init();
        }
        //연호 구현 사항 - End
        //가송 구현 사항 - Start
        private void ServerStart()
        {
            try
            {
                if (connect_flag == false)
                {
                    //서버 리스너 등록
                    listener = new TcpListener(PORT);
                    listener.Start();

                    Socket hClient = listener.AcceptSocket();
                    if (hClient.Connected)
                    {
                        netStream = new NetworkStream(hClient);
                        dataReader = new BinaryReader(netStream);
                        dataWriter = new BinaryWriter(netStream);
                        connect_flag = true;

                        //수신을 위한 쓰레드를 등록한다.
                        receiveThread = new Thread(new ThreadStart(Receive));
                        receiveThread.Start();
                        //0.1초마다 캡쳐하기 위해 타이머 등록.
                        captureTimer.Start();
                    }
                }
                else
                {
                    connect_flag = false;
                    receiveThread.Abort();
                    netStream.Close();
                    listener.Stop();
                }
            }
            catch
            {
                MessageBox.Show("서버 시작 도중 오류!");
                return;
            }
        }
        public void Receive()
        {
            //수신용 쓰레드가 수행할 메서드.
            while (connect_flag)
            {
                try
                {
                    while (-1 != dataReader.Read(this.readBuffer, 0, this.readBuffer.Length))
                    {
                        ReceiveData();                   
                    }
                }
                catch
                {
                    if(connect_flag == false)
                    {
                 
                        captureTimer.Stop();
                        MessageBox.Show("연결이 해제되었습니다.");
                        receiveThread.Abort();
                    }
                   
                    
                }

            }
        }

        public void ReceiveData()
        {
            Packet packet = (Packet)Packet.Deserialize(this.readBuffer);
            //패킷 타입에 따라 처리할 코드를 달리한다.
            //추후에 아이템 같은 타입에 대한 처리도 추가할 예정.
            switch ((int)packet.Type)
            {
                case (int)PacketType.스샷:
                    {
                        ScreenShotPacket screenShotPacket = (ScreenShotPacket)ScreenShotPacket.Deserialize(this.readBuffer);

                        int p_length = screenShotPacket.Length;
                        //저장될 파일 이름은 receive.jpg로 고정한다.
                        string filePath = Environment.CurrentDirectory + "\\" + "receive" + receive_number + ".jpg";

                        FileStream fs;
                        //p_length가 음수이면 데이터 완료를 의미한다.(Send 메서드 참고.)
                        if (p_length > 0)
                        {
                            if (!file_flag)
                            {
                                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                                file_flag = true;
                            }
                            else
                            {
                                fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                            }

                                                 
                            fs.Write(screenShotPacket.data, 0, 1024);
                            fs.Close();
                        }
                        else
                        {
                           
                            PrintScreenShot(filePath);
                            file_flag = false;
                        }
                        break;
                    }
                case (int)PacketType.상대속도증가:
                    {
                        //원본
                        EnemySpeedUpPacket esp = (EnemySpeedUpPacket)EnemySpeedUpPacket.Deserialize(this.readBuffer);
                        //         int m_esp_data = Convert.ToInt32((byte)esp.data[0]);
                       
                        Invoke((MethodInvoker)delegate ()
                        {
                            speedUp();
                        });

                        //받은 값에 따라 아이템 적용.
                        break;
                    }
                case (int)PacketType.상대줄추가:
                    {
                        //원본
                        EnemyAddLinePacket esp = (EnemyAddLinePacket)EnemyAddLinePacket.Deserialize(this.readBuffer);
                        //       int m_esp_data = Convert.ToInt32((byte)esp.data[0]);

                        Invoke((MethodInvoker)delegate ()
                        {
                            appendBottomLine();
                        });
                        //받은 값에 따라 아이템 적용.
                        break;
                    }
                case (int)PacketType.상대화면가리기:
                    {
                        //원본
                        EnemyBlindPacket esp = (EnemyBlindPacket)EnemyBlindPacket.Deserialize(this.readBuffer);
                        //     int m_esp_data = Convert.ToInt32((byte)esp.data[0]);
                        Invoke((MethodInvoker)delegate ()
                        {
                            Blind();
                        });
                    
                        //받은 값에 따라 아이템 적용.
                        break;
                    }
                case (int)PacketType.연결종료:
                    {
                        DisconnectPacket dp = (DisconnectPacket)DisconnectPacket.Deserialize(this.readBuffer);
                        Invoke((MethodInvoker)delegate ()
                        {
                            
                            connect_flag = false;
                            this.Close();
                        });
                        break;
                    }
                case (int)PacketType.게임오버:
                    {
                        GameOverPacket gop = (GameOverPacket)GameOverPacket.Deserialize(this.readBuffer);
                      
                        Invoke((MethodInvoker)delegate ()
                        {
                            MessageBox.Show("이겼습니다.");
                            Send("disconnect");
                            connect_flag = false;
                            conn_over_flag = true;
                            Game_Over();
                            this.Close();
                        });
                        break;
                    }
                  
            }


        }

        //수신된 이미지 파일을 픽쳐 박스에 로드한다.
        private void PrintScreenShot(string filePath)
        {

            Invoke(new MethodInvoker(delegate ()
            {
                if (Enemy_Screen.Image != null)
                {
                    Enemy_Screen.Image.Dispose();
                    Enemy_Screen.Image = null;
                }
                string Fname = Path.GetFileName(filePath);
                Enemy_Screen.Image = Image.FromFile(Fname);
                receive_number++;
                receive_number = receive_number % 2;
            
            }));

        }

        private void Send(string dataname)
        {
            if (connect_flag == false) return;
            if (dataname == "send.jpg")
            {
                //패킷 전송을 위한 클래스 
                ScreenShotPacket ssp = new ScreenShotPacket();
                byte[] sendData = new byte[1024];
                string sendPath = Environment.CurrentDirectory + "\\" + dataname;
                FileStream fs = new FileStream(sendPath, FileMode.Open, FileAccess.Read);
                FileInfo fi = new FileInfo(fs.Name);
                long LengthQ = fi.Length / 1024;
                long LengthR = fi.Length % 1024;
                long n = 0;
                //파일을 1024byte만큼 나눠서 보낸다.
                for (n = 0; n < LengthQ; n++)
                {
                    fs.Seek(1024 * n, SeekOrigin.Begin);
                    fs.Read(sendData, 0, sendData.Length);
                    ssp.Type = (int)PacketType.스샷;
                    ssp.Length = sendData.Length;
                    ssp.data = sendData;
                    Packet.Serialize(ssp).CopyTo(this.writeBuffer, 0);
                    SendData(writeBuffer);
                }
                //남은 부분을 보낸다.
                if ((int)LengthR != 0)
                {
                    fs.Seek(1024 * n, SeekOrigin.Begin);
                    fs.Read(sendData, 0, 1024);
                    ssp.Type = (int)PacketType.스샷;
                    ssp.Length = sendData.Length;
                    ssp.data = sendData;
                    Packet.Serialize(ssp).CopyTo(this.writeBuffer, 0);
                    SendData(writeBuffer);
                }
                //데이터 전송 완료를 알려주기 위해 Length에 -1을 넣는다.
                ssp.Type = (int)PacketType.스샷;
                ssp.Length = -1;
                Packet.Serialize(ssp).CopyTo(this.writeBuffer, 0);
                SendData(writeBuffer);

                fs.Close();
            }
            else if (dataname == "1")
            {
                //상대 속도 증가
                EnemySpeedUpPacket esp = new EnemySpeedUpPacket();
                esp.Type = (int)PacketType.상대속도증가;
                esp.Length = sizeof(int);
                //       byte[] m_speed_up = new byte[4];
                //     m_speed_up[0] = 1;
                //   esp.data = m_speed_up;
                Packet.Serialize(esp).CopyTo(this.writeBuffer, 0);
                SendData(this.writeBuffer);

            }
            else if (dataname == "2")
            {
                //상대 줄 추가
                EnemyAddLinePacket ealp = new EnemyAddLinePacket();
                ealp.Type = (int)PacketType.상대줄추가;
                ealp.Length = sizeof(int);
                //   byte[] m_speed_up = new byte[4];
                // m_speed_up[0] = 1;
                // ealp.data = m_speed_up;
                Packet.Serialize(ealp).CopyTo(this.writeBuffer, 0);
                SendData(this.writeBuffer);

            }
            else if (dataname == "3")
            {
                //상대 화면 가리기
                EnemyBlindPacket ebp = new EnemyBlindPacket();
                ebp.Type = (int)PacketType.상대화면가리기;
                ebp.Length = sizeof(int);
                //   byte[] m_speed_up = new byte[4];
                // m_speed_up[0] = 1;
                // ealp.data = m_speed_up;
                Packet.Serialize(ebp).CopyTo(this.writeBuffer, 0);
                SendData(this.writeBuffer);
            }
            else if(dataname == "disconnect")
            {
                DisconnectPacket dp = new DisconnectPacket();
                dp.Type = (int)PacketType.연결종료;
                dp.Length = sizeof(int);
                Packet.Serialize(dp).CopyTo(this.writeBuffer, 0);
                SendData(this.writeBuffer);
            }
            else if(dataname == "게임오버")
            {
                GameOverPacket gop = new GameOverPacket();
                gop.Type = (int)PacketType.게임오버;
                gop.Length = sizeof(int);
                Packet.Serialize(gop).CopyTo(this.writeBuffer, 0);
                SendData(this.writeBuffer);
            }
        }
        //BinaryWriter를 사용하여 byte배열로 변환된 값을 전송한다.
        private void SendData(byte[] a_data)
        {

            this.dataWriter.Write(a_data, 0, a_data.Length);
            this.dataWriter.Flush();
            for (int i = 0; i < 1024 * 4; i++)
                a_data[i] = 0;
        }

        private void ClientStart(string a_input)
        {
            client = new TcpClient();
            try
            {
                //form2에서 전달받은 ip값으로 연결.
                client.Connect(a_input, PORT);
            }
            catch
            {
                connect_flag = false;
                return;
            }
            connect_flag = true;
            netStream = client.GetStream();
            dataReader = new BinaryReader(netStream);
            dataWriter = new BinaryWriter(netStream);

            //수신을 위한 쓰레드를 등록한다.
            receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.Start();
            captureTimer.Start();
        }
        //가송 구현 사항 -End
        //연호 구현 사항 -Start
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int i, j;
            if (e.KeyCode == Keys.Up)            //왼쪽으로 90도 회전 
            {

                byte[,] temp = new byte[4, 4];
                for (i = 0; i < 4; i++)                   //block_status가 왼쪽으로 회전하였을 때 상태 저장 
                {
                    for (j = 0; j < 4; j++)
                        temp[i, j] = this.Nowfalling.block_status[j, 3 - i];
                }

                int xtemp = this.Nowfalling.originX;    // 왼쪽으로 회전하였을 때 x 위치를 알기 위한 변수 
                int ytemp = this.Nowfalling.originY;    // 오른쪽으로 회전하였을 때 y 위치를 알기 위한 변수 

                for (i = 0; i < 4; i++)               //temp를 원점부터 검사한다 
                {
                    for (j = 0; j < 4; j++)
                    {
                        if (temp[i, j] != 0)         //temp에서 box를 발견하면 
                        {
                            int x = xtemp / 30;
                            int y = ytemp / 30;
                            //회전하였을 때 블록이 게임판을 벗어났을때 명령 무시 
                            if (x < 0 || x > 9 || y < 0 || y > 19)
                                return;
                            else if (ytemp % 30 == 0) // 블록이 정위치에 있을 때 
                            {
                                if (this.g.smap[y, x] != 0) //회전한 위치에 블록이 있으면 명령 무시 
                                    return;
                            }
                            else if (ytemp % 30 != 0) // 블록이 정위치에 위치하지 않을 때 
                            {
                                if (this.g.smap[y, x] != 0) //회전한 위치 위에 블록이 있으면 명령 무시 
                                    return;
                                if (y != 19) //y가 19가 아닐 때(out of index 방지)
                                {
                                    if (this.g.smap[y + 1, x] != 0) //회전한 위치 아래에 블록이 있으면 명령 무시 
                                        return;
                                }
                            }
                        }
                        xtemp += 30;                       //temp 상태에 맞는 x값을 결정하기 위해 x값 증가   
                    }
                    xtemp = this.Nowfalling.originX;       //루프를 한 번 돌았으므로 x값 초기화 
                    ytemp += 30;
                }
                this.Nowfalling.Rotation();
            }
            else if (e.KeyCode == Keys.Down)     //블록 하강속도를 빠르게 함 
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
                        if (this.g.smap[i, j - 1] != 0) //왼쪽에 블록이 존재하면 명령 무시 
                            return;
                    }
                    else                  //블록이 정위치에 위치하지 않을 때 
                    {
                        if (this.g.smap[i, j - 1] != 0)         //왼쪽 상단에 블록이 있을 때 명령 무시 
                            return;
                        else if (this.g.smap[i + 1, j - 1] != 0) // 왼쪽 하단에 블록이 있을 때 명령 무시 
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
                        if (this.g.smap[i, j + 1] != 0) //오른쪽에 블록이 존재하면 명령 무시 
                            return;
                    }
                    else                  //블록이 정위치에 위치하지 않을 때 
                    {
                        if (this.g.smap[i, j + 1] != 0)         //오른쪽 상단에 블록이 있을 때 명령 무시 
                            return;
                        else if (this.g.smap[i + 1, j + 1] != 0) // 오른쪽 하단에 블록이 있을 때 명령 무시 
                            return;
                    }
                }
                this.Nowfalling.Move_Right();  //오른쪽으로 한 칸 이동 
            }
            else if (e.KeyCode == Keys.A) //A키 눌렀을 때 아이템 사용 - 동현 구현 사항
            {
                usingItem();
            }
        }
        private void usingItem()//아이템 리스트에 있는 아이템을 사용
        {
            if (!(Item_List.TopItem == null))
            {
                if (Item_List.TopItem.Text == "속도 감소")
                {
                    speedDown();
                    Item_List.TopItem.Remove();

                }
                else if (Item_List.TopItem.Text == "한 줄 제거")
                {
                    remove_endLine();
                    Item_List.TopItem.Remove();

                }
                else if (Item_List.TopItem.Text == "상대 속도 증가")
                {
                    Send("1");
                    Item_List.TopItem.Remove();
                }
                else if (Item_List.TopItem.Text == "상대 줄 추가")
                {
                    Send("2");
                    Item_List.TopItem.Remove();
                }
                else if (Item_List.TopItem.Text == "상대 화면 가리기")
                {
                    Send("3");
                   // Blind();
                    Item_List.TopItem.Remove();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //게임을 실행하는 Main Timer 
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
            if (e.KeyCode == Keys.Down) //아래 방향 키를 떼었을 때 원래 속도로 변경
            {
                if (this.aitem == true)
                    this.timer1.Interval = tempSpeed; //아이템이 적용중 일 때는 아이템이 적용 된 속도로 변경
                else
                    this.timer1.Interval = nomalSpeed;
            }
        }

        private void timer2_Tick(object sender, EventArgs e) // 남은 시간 표시 
        {
            if (this.aitem)       //속도 조절 아이템이 사용 됐을 경우 8초 동안 변경 된 속도를 유지- 동현 구현 사항
            {
                returnToNSpeed();

            }
            if (this.isblind)        //화면이 가려지면 4초 후 해제
            {
                if (this.blindtc < 5)
                    blindtc++;
                else
                {
                    blind_pbox.Visible = false;
                    isblind = false;
                  
                    blindtc = 0;
                }
            }
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
                nomalSpeedUp();
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
        private void Remove_Combo() //콤보 라벨이 표시 중이면 제거 
        {
            if (this.Combo_label.Visible)
                this.Combo_label.Visible = false;
            else                     //없으면 타이머 종료 
                this.timer3.Stop();
        }

        private void timer3_Tick(object sender, EventArgs e) // 콤보 메시지 제거 타이머 
        {
            this.Remove_Combo();
        }

        private void Restart_btn_Click(object sender, EventArgs e) //재시작 버튼 눌렀을 때 
        {
            this.Game_Over_Msg.Visible = false;     //게임 오버 메시지 및 선택 버튼 제거 
            this.Restart_btn.Visible = false;
            this.Exit_btn.Visible = false;
            this.Item_List.Clear();
            this.blind_pbox.Visible = false;
            nomalSpeed = 300;
            this.Game_Init();                           //게임 시작 
        }

        private void Exit_btn_Click(object sender, EventArgs e) //종료 버튼 눌렀을 때 
        {
            this.Dispose();                 //게임 종료 

        }
        //연호 구현 사항 -End
        //가송 구현 사항 -Start
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(connect_flag)
                Send("disconnect");
        }

        private void captureTimer_Tick(object sender, EventArgs e)
        {
            //캡쳐하기
            Copy("send.jpg");
        }

        private void wplayer_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPLib.WMPPlayState.wmppsMediaEnded) //재싱 시간이 끝에 도달하면
            {
                //다시 재생한다.
                m_wmp.controls.play();
                m_wmp.controls.previous();
                m_wmp.controls.currentPosition = 0;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Enemy_Screen.Image = null;
       //     this.blind_pbox.Visible = true;

        }
        //가송 구현 사항 -End
        private void AppendItem()//아이템 획득
        {
            string itemName = null;
            int itemNum;
            if (this.game_mode == 0)        //아이템을 갖고있는 블록이 깨지면 item_list에 랜덤한 아이템이 추가 됨
            {
                Random r = new Random();

                itemNum = r.Next(1, 3);

                switch (itemNum)
                {
                    case 1:
                        {
                            itemName = "속도 감소";
                            break;
                        }
                    case 2:
                        {
                            itemName = "한 줄 제거";
                            break;
                        }
                }
            }
            else
            {

                Random r = new Random();
                itemNum = r.Next(1, 6);
                  // itemNum = 5;
                switch (itemNum)
                {
                    case 1:
                        {
                            itemName = "속도 감소";
                            break;
                        }
                    case 2:
                        {
                            itemName = "한 줄 제거";
                            break;
                        }
                    case 3:
                        {
                            itemName = "상대 속도 증가";
                            break;
                        }
                    case 4:
                        {
                            itemName = "상대 줄 추가";
                            break;
                        }
                    case 5:
                        {
                            itemName = "상대 화면 가리기";
                            break;
                        }
                }
            }
            ListViewItem newitem = new ListViewItem(itemName);
            Item_List.Items.Add(newitem);
        }
        private void speedDown()// 속도 감소 아이템 
        {
            this.tempSpeed = this.nomalSpeed * 4; //현재 속도의 4배 느리게
            timer1.Interval = tempSpeed;
            this.aitem = true;

        }
        private void speedUp() //속도 증가 아이템
        {
           
                timer1.Stop();
                this.tempSpeed = this.nomalSpeed / 4;//현재 속도의 4배 빠르게
                timer1.Interval = this.tempSpeed;

                timer1.Start();

      
        }
        private void remove_endLine() //한 줄 제거 아이템 사용 시 마지막 줄 제거
        {
            for (int n = 0; n < 10; n++)
            {
                Box x = this.g.bmap[19, n];
                if (x != null)
                {
                    this.BackGround.Controls.Remove(x);
                    if (x.item > 98)           //블록에 아이템이 있을 때 
                    {
                        AppendItem();          //아이템 추가
                    }
                    this.g.bmap[19, n] = null;
                    x.Dispose();
                }
            }

            for (int n = 19; n > 0; n--)
            {
                for (int m = 0; m < 10; m++)
                {
                    this.g.bmap[n, m] = this.g.bmap[n - 1, m];
                    this.g.smap[n, m] = this.g.smap[n - 1, m];
                    Box x = this.g.bmap[n, m];
                    if (x == null)
                        continue;
                    x.Location = new Point(x.xpos, x.ypos + 30);
                    x.ypos += 30;
                }
            }
            for (int n = 0; n < 10; n++)
            {
                this.g.smap[0, n] = 0;
                this.g.bmap[0, n] = null;
            }
            this.score += 300;
        }
        //연호 구현 사항 - Start
        private void appendBottomLine()
        {
            for (int n = 0; n < 19; n++)//한 줄 씩 올림
            {
                for (int m = 0; m < 10; m++)
                {
                    this.g.bmap[n, m] = this.g.bmap[n + 1, m];
                    this.g.smap[n, m] = this.g.smap[n + 1, m];
                    Box x = this.g.bmap[n, m];
                    if (x == null)
                        continue;
                    x.Location = new Point(x.xpos, x.ypos - 30);
                    x.ypos -= 30;
                }
            }
            int xpos = 0; //x값 위치 지정하는 변수
            for (int n = 0; n < 10; n++)//가장 밑에 한 줄 추가
            {
                Box temp = new Box();
                temp.Size = new Size(30, 30);
                temp.BackColor = Color.Gray;
                temp.BorderStyle = BorderStyle.FixedSingle;
                temp.item = 0;  //아이템 없음
                temp.xpos = xpos;
                temp.ypos = 570;
                temp.Location = new Point(temp.xpos, temp.ypos); //생성한 박스 좌표 설정
                xpos += 30; //x좌표 증가
                this.BackGround.Controls.Add(temp); //생성한 박스 게임판에 추가
                this.g.bmap[19, n] = temp;          //박스 제어맵에 추가
                this.g.smap[19, n] = 2;             //제거 아이템 외에는 제거할 수 없도록 상태 2로 설정
            }

            for (int n = 0; n < 10; n++) //천장에 닿았을 경우 게임 오버
            {
                if (this.g.smap[0, n] == 1)
                {
                    this.Now_Playing = false;
                    return;
                }
            }
        }
        //연호 구현 사항 - END
        private void Blind()                                    //화면이 가려짐
        {
            this.blind_pbox.Visible = true;
        //    this.blind_pbox.BringToFront();
            this.isblind = true;
        }
        private void returnToNSpeed()                           //8초가 되면 속도아이템 효과 해제
        {
            if (this.tickCount < 9)
                this.tickCount++;

            else
            {

                this.timer1.Interval = this.nomalSpeed;
                this.aitem = false;
                this.tickCount = 0;
                return;
            }
        }
        private void nomalSpeedUp()
        {
            this.nomalSpeed -= 100;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (captureTimer != null) { captureTimer.Stop(); captureTimer.Dispose(); }
            if (receiveThread != null) receiveThread.Abort();
            if (dataReader != null) dataReader.Close();
            if (dataWriter != null) dataWriter.Close();
            if (netStream != null) netStream.Close();

            if (listener != null) { listener.Stop(); listener = null; }
            if (client != null) { client.Close(); client = null; }
        }
    }
}


