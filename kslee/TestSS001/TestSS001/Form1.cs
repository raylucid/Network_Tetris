using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using TetrisPacket;
namespace TestSS001
{
    public partial class Form1 : Form
    {
        private Bitmap bmp;
        private bool connect_flag = false;

        private TcpListener listener;
        private const int PORT = 7777;

        private TcpClient client;
        
        private Thread receiveThread;

        private NetworkStream netStream;
        private BinaryReader dataReader;
        private BinaryWriter dataWriter;
        private bool file_flag = false;

      //  private int screen_index = 0;
        //  string file_path;

        private byte[] readBuffer = new byte[1024 * 4];
        private byte[] writeBuffer = new byte[1024 * 4];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect_flag == false)
                {
                    listener = new TcpListener(PORT);
                    listener.Start();

                    Socket hClient = listener.AcceptSocket();
                    if (hClient.Connected)
                    {
                        btnStart.Text = "server_stop";
                        netStream = new NetworkStream(hClient);
                        dataReader = new BinaryReader(netStream);
                        dataWriter = new BinaryWriter(netStream);
                        connect_flag = true;
                        receiveThread = new Thread(new ThreadStart(Receive));
                        receiveThread.Start();
                        screenShotTimer.Start();
                    }
                }
                else
                {
                    btnStart.Text = "server_start";
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            try
            {
                client.Connect("localhost", PORT);
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

            receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.Start();

            screenShotTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pbTarget.Image = pbSource.Image;
        }

        private void btnShot_Click(object sender, EventArgs e)
        {
            Copy("send.jpg");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (screenShotTimer != null) { screenShotTimer.Stop(); screenShotTimer.Dispose(); }
            if (receiveThread != null) receiveThread.Abort();
            if (dataReader != null) dataReader.Close();
            if (dataWriter != null) dataWriter.Close();
            if (netStream != null) netStream.Close();

            if (listener != null) { listener.Stop(); listener = null; }
            if (client != null) { client.Close(); client = null; }
        }

        public void Copy(string outputFilename)
        {
            bmp = new Bitmap(this.pbSource.Width, this.pbSource.Height);

            // Bitmap 이미지 변경을 위해 Graphics 객체 생성
            Graphics gr = Graphics.FromImage(bmp);
            //copyfromscreen 메서드에 사용하기 위해 절대좌표로 변환.
            Point newPoint = PointToScreen(new Point(pbSource.Location.X, pbSource.Location.Y));
            //지정한 영역을 캡쳐
            gr.CopyFromScreen((newPoint.X), (newPoint.Y), 0, 0, pbSource.Size);

            FileInfo fi = new FileInfo(Environment.CurrentDirectory + "\\" + outputFilename);
            if (fi.Exists)
            {
                if (pbTarget.Image != null)
                    pbTarget.Image.Dispose();
                fi.Delete();
                fi = null;
            }
            //캡쳐한 이미지를 파일로 저장
            bmp.Save(outputFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
          //  pbTarget.Image = Image.FromFile(outputFilename);
            bmp.Dispose();
            bmp = null;
            gr.Dispose();
            gr = null;
            //파일을 패킷으로 보낸다.
            Send(outputFilename);

        }

        //수신용 쓰레드가 수행할 메서드.
        public void Receive()
        {
            while (connect_flag)
            {
                while (-1 != dataReader.Read(this.readBuffer, 0, this.readBuffer.Length))
                {
                    ReceiveData();
                }
            }
        }

        public void ReceiveData()
        {
           
            
            Packet packet = (Packet)Packet.Deserialize(this.readBuffer);
      
            switch ((int)packet.Type)
            {
                case (int)PacketType.스샷:
                    {
                        if (pbTarget.Image != null)
                        {
                            Invoke(new MethodInvoker(delegate ()
                            {
                                pbTarget.Image.Dispose();
                                
                            }));
                        }
                        ScreenShotPacket screenShotPacket = (ScreenShotPacket)ScreenShotPacket.Deserialize(this.readBuffer);

                        int p_length = screenShotPacket.Length;
                        string filePath = Environment.CurrentDirectory + "\\" + "receive.jpg";

                        FileStream fs;
                        if (p_length > 0)
                        {
                            if (!file_flag)
                            {
                                using (fs = new FileStream(filePath, FileMode.Create))
                                {
                                    file_flag = true;
                                    fs.Write(screenShotPacket.data, 0, 1024);
                                }
                            }
                            else
                            {
                                using (fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                                {
                                    fs.Write(screenShotPacket.data, 0, 1024);
                                }
                            }

                           
                            fs.Close();
                            fs = null;
                        }
                        else
                        {
                            file_flag = false;
                            PrintScreenShot(filePath);
                   
                           screenShotPacket = null;
                        }
                        break;
                    }
            }


        }

        private void PrintScreenShot(string filePath)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                string Fname = Path.GetFileName(filePath);
                 pbTarget.Image = Image.FromFile(Fname);
 


            }));
        }

        private void Send(string fileName)
        {
            //패킷 전송을 위한 클래스 
            ScreenShotPacket ssp = new ScreenShotPacket();
            byte[] sendData = new byte[1024];
            string sendPath = Environment.CurrentDirectory + "\\" + fileName;
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

        private void screenShotTimer_Tick(object sender, EventArgs e)
        {
           Copy("send.jpg");
        }


        //BinaryWriter를 사용하여 byte배열로 변환된 값을 전송한다.
        private void SendData(byte[] a_data)
        {
            this.dataWriter.Write(a_data, 0, a_data.Length);
            this.dataWriter.Flush();
            for (int i = 0; i < 1024 * 4; i++)
                a_data[i] = 0;
        }
        private bool image_flag = false;
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (image_flag == false)
            {
                pbSource.Image = Image.FromFile("설현3.jpg");
                image_flag = true;
            }
            else
            {
                pbSource.Image = Image.FromFile("설현4.jpg");
                image_flag = false;
            }

        }
    }


}



