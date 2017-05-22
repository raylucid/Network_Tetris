using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Single_btn_Click(object sender, EventArgs e) //싱글모드 선택
        {
            this.Hide();              //현재 창을 숨기고
            Form1 fr1 = new Form1();
            fr1.ShowDialog();         //게임 창을 연다
            this.Close();             //게임 창이 종료되면 현재 창도 닫는다
        }

        private void Multi_btn_Click(object sender, EventArgs e) //멀티모드 선택
        {
            Client_btn.Visible = true;   //클라이언트 버튼 보이기
            Server_btn.Visible = true;   //서버 버튼 보이기
            Single_btn.Dispose();        //싱글모드 버튼 제거
            Multi_btn.Dispose();         //멀티모드 버튼 제거
        }

        private void Client_btn_Click(object sender, EventArgs e) //클라이언트 선택
        {
            Server_btn.Dispose();       //서버 버튼 제거
            Input_label.Visible = true; //Input label 보이기 
            Input_label.Text = "IP";    //Input label text IP로 변경
            OK_btn.Visible = true;      //OK버튼 보이기
            textBox1.Visible = true;    //텍스트 박스 보이기
            Client_btn.Enabled = false; //클라이언트 버튼 비활성화
        }

        private void Server_btn_Click(object sender, EventArgs e) //서버 선택
        {
            Client_btn.Text = "Server";  //클라이언트 버튼 텍스트 서버로 변경
            Client_btn.Enabled = false;  //클라이언트 버튼 비활성화
            Input_label.Visible = true;  //Input label 보이기
            Input_label.Text = "PORT";   //Input label text Port로 변경
            OK_btn.Visible = true;       //OK버튼 보이기
            textBox1.Visible = true;     //텍스트 박스 보이기
            Server_btn.Dispose();        //서버 버튼 제거
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e) 
        {
            if(e.KeyCode == Keys.Enter) // 텍스트박스에서 엔터키 입력 시
            {
                string s = textBox1.Text;
                if(Input_label.Text == "IP") //클라이언트이면
                {
                    this.Hide();                 //현재 창 숨기기
                    Form1 fr1 = new Form1(0, s); //모드 0과 IP를 게임창 생성자에 전달
                    fr1.ShowDialog();            //게임창 열기
                    this.Close();
                }
                else if(Input_label.Text == "PORT") //서버이면
                {
                    this.Hide();                 //현재 창 숨기기
                    Form1 fr1 = new Form1(1, s); //모드 0과 IP를 게임창 생성자에 전달
                    fr1.ShowDialog();            //게임창 열기
                    this.Close();
                }
            }
        }

        private void OK_btn_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            if (Input_label.Text == "IP") //클라이언트이면
            {
                this.Hide();                 //현재 창 숨기기
                Form1 fr1 = new Form1(0, s); //모드 0과 IP를 게임창 생성자에 전달
                fr1.ShowDialog();            //게임창 열기
                this.Close();
            }
            else if (Input_label.Text == "PORT") //서버이면
            {
                this.Hide();                 //현재 창 숨기기
                Form1 fr1 = new Form1(1, s); //모드 0과 IP를 게임창 생성자에 전달
                fr1.ShowDialog();            //게임창 열기
                this.Close();
            }
        }
    }
}
