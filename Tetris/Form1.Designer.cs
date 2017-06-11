namespace Tetris
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("상대 속도 증가");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("상대 줄 추가");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("상대 화면 가리기");
            this.GameBoard = new System.Windows.Forms.Panel();
            this.Exit_btn = new System.Windows.Forms.Button();
            this.Restart_btn = new System.Windows.Forms.Button();
            this.BackGround = new System.Windows.Forms.PictureBox();
            this.Game_Over_Msg = new System.Windows.Forms.Label();
            this.Combo_label = new System.Windows.Forms.Label();
            this.blind_pbox = new System.Windows.Forms.PictureBox();
            this.GameStatus = new System.Windows.Forms.Panel();
            this.Enemy_Screen = new System.Windows.Forms.PictureBox();
            this.Enemy_Text = new System.Windows.Forms.Label();
            this.Remain_Seconds = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Remain_Minutes = new System.Windows.Forms.Label();
            this.Next_Block = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Item_List = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.Score_text = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.captureTimer = new System.Windows.Forms.Timer(this.components);
            this.GameBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).BeginInit();
            this.BackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blind_pbox)).BeginInit();
            this.GameStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Enemy_Screen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Next_Block)).BeginInit();
            this.SuspendLayout();
            // 
            // GameBoard
            // 
            this.GameBoard.BackgroundImage = global::Tetris.Properties.Resources.lego_2383089_1920;
            this.GameBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GameBoard.Controls.Add(this.Combo_label);
            this.GameBoard.Controls.Add(this.Exit_btn);
            this.GameBoard.Controls.Add(this.Game_Over_Msg);
            this.GameBoard.Controls.Add(this.Restart_btn);
            this.GameBoard.Controls.Add(this.BackGround);
            this.GameBoard.Dock = System.Windows.Forms.DockStyle.Left;
            this.GameBoard.Location = new System.Drawing.Point(0, 0);
            this.GameBoard.Name = "GameBoard";
            this.GameBoard.Size = new System.Drawing.Size(300, 600);
            this.GameBoard.TabIndex = 0;
            // 
            // Exit_btn
            // 
            this.Exit_btn.BackColor = System.Drawing.Color.White;
            this.Exit_btn.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit_btn.Location = new System.Drawing.Point(157, 266);
            this.Exit_btn.Name = "Exit_btn";
            this.Exit_btn.Size = new System.Drawing.Size(127, 38);
            this.Exit_btn.TabIndex = 3;
            this.Exit_btn.Text = "Exit";
            this.Exit_btn.UseVisualStyleBackColor = false;
            this.Exit_btn.Visible = false;
            this.Exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // Restart_btn
            // 
            this.Restart_btn.BackColor = System.Drawing.Color.White;
            this.Restart_btn.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Restart_btn.Location = new System.Drawing.Point(12, 266);
            this.Restart_btn.Name = "Restart_btn";
            this.Restart_btn.Size = new System.Drawing.Size(127, 38);
            this.Restart_btn.TabIndex = 3;
            this.Restart_btn.Text = "Restart";
            this.Restart_btn.UseVisualStyleBackColor = false;
            this.Restart_btn.Visible = false;
            this.Restart_btn.Click += new System.EventHandler(this.Restart_btn_Click);
            // 
            // BackGround
            // 
            this.BackGround.BackColor = System.Drawing.Color.Transparent;
            this.BackGround.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackGround.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackGround.Controls.Add(this.blind_pbox);
            this.BackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackGround.Location = new System.Drawing.Point(0, 0);
            this.BackGround.Name = "BackGround";
            this.BackGround.Size = new System.Drawing.Size(300, 600);
            this.BackGround.TabIndex = 0;
            this.BackGround.TabStop = false;
            // 
            // Game_Over_Msg
            // 
            this.Game_Over_Msg.AutoSize = true;
            this.Game_Over_Msg.BackColor = System.Drawing.Color.Transparent;
            this.Game_Over_Msg.Font = new System.Drawing.Font("굴림", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Game_Over_Msg.ForeColor = System.Drawing.Color.Red;
            this.Game_Over_Msg.Location = new System.Drawing.Point(10, 205);
            this.Game_Over_Msg.Name = "Game_Over_Msg";
            this.Game_Over_Msg.Size = new System.Drawing.Size(284, 45);
            this.Game_Over_Msg.TabIndex = 3;
            this.Game_Over_Msg.Text = "Game Over!";
            this.Game_Over_Msg.Visible = false;
            // 
            // Combo_label
            // 
            this.Combo_label.AutoSize = true;
            this.Combo_label.BackColor = System.Drawing.Color.Transparent;
            this.Combo_label.Font = new System.Drawing.Font("굴림", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Combo_label.ForeColor = System.Drawing.Color.Red;
            this.Combo_label.Location = new System.Drawing.Point(100, 266);
            this.Combo_label.Name = "Combo_label";
            this.Combo_label.Size = new System.Drawing.Size(87, 19);
            this.Combo_label.TabIndex = 3;
            this.Combo_label.Text = "Combo!";
            this.Combo_label.Visible = false;
            // 
            // blind_pbox
            // 
            this.blind_pbox.BackColor = System.Drawing.SystemColors.Control;
            this.blind_pbox.Image = global::Tetris.Properties.Resources.멜롱;
            this.blind_pbox.Location = new System.Drawing.Point(0, 0);
            this.blind_pbox.Name = "blind_pbox";
            this.blind_pbox.Size = new System.Drawing.Size(300, 201);
            this.blind_pbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.blind_pbox.TabIndex = 4;
            this.blind_pbox.TabStop = false;
            this.blind_pbox.Visible = false;
            // 
            // GameStatus
            // 
            this.GameStatus.BackColor = System.Drawing.Color.Black;
            this.GameStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameStatus.Controls.Add(this.Enemy_Screen);
            this.GameStatus.Controls.Add(this.Enemy_Text);
            this.GameStatus.Controls.Add(this.Remain_Seconds);
            this.GameStatus.Controls.Add(this.label2);
            this.GameStatus.Controls.Add(this.Remain_Minutes);
            this.GameStatus.Controls.Add(this.Next_Block);
            this.GameStatus.Controls.Add(this.label6);
            this.GameStatus.Controls.Add(this.Item_List);
            this.GameStatus.Controls.Add(this.label5);
            this.GameStatus.Controls.Add(this.Score_text);
            this.GameStatus.Controls.Add(this.label3);
            this.GameStatus.Controls.Add(this.label1);
            this.GameStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.GameStatus.Location = new System.Drawing.Point(300, 0);
            this.GameStatus.Name = "GameStatus";
            this.GameStatus.Size = new System.Drawing.Size(178, 600);
            this.GameStatus.TabIndex = 1;
            // 
            // Enemy_Screen
            // 
            this.Enemy_Screen.Location = new System.Drawing.Point(15, 475);
            this.Enemy_Screen.Name = "Enemy_Screen";
            this.Enemy_Screen.Size = new System.Drawing.Size(120, 112);
            this.Enemy_Screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Enemy_Screen.TabIndex = 12;
            this.Enemy_Screen.TabStop = false;
            this.Enemy_Screen.Visible = false;
            // 
            // Enemy_Text
            // 
            this.Enemy_Text.AutoSize = true;
            this.Enemy_Text.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Enemy_Text.ForeColor = System.Drawing.Color.White;
            this.Enemy_Text.Location = new System.Drawing.Point(11, 444);
            this.Enemy_Text.Name = "Enemy_Text";
            this.Enemy_Text.Size = new System.Drawing.Size(97, 27);
            this.Enemy_Text.TabIndex = 11;
            this.Enemy_Text.Text = "Enemy";
            this.Enemy_Text.Visible = false;
            // 
            // Remain_Seconds
            // 
            this.Remain_Seconds.AutoSize = true;
            this.Remain_Seconds.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Remain_Seconds.ForeColor = System.Drawing.Color.White;
            this.Remain_Seconds.Location = new System.Drawing.Point(60, 42);
            this.Remain_Seconds.Name = "Remain_Seconds";
            this.Remain_Seconds.Size = new System.Drawing.Size(44, 27);
            this.Remain_Seconds.TabIndex = 10;
            this.Remain_Seconds.Text = "00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(49, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 27);
            this.label2.TabIndex = 9;
            this.label2.Text = ":";
            // 
            // Remain_Minutes
            // 
            this.Remain_Minutes.AutoSize = true;
            this.Remain_Minutes.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Remain_Minutes.ForeColor = System.Drawing.Color.White;
            this.Remain_Minutes.Location = new System.Drawing.Point(10, 42);
            this.Remain_Minutes.Name = "Remain_Minutes";
            this.Remain_Minutes.Size = new System.Drawing.Size(44, 27);
            this.Remain_Minutes.TabIndex = 8;
            this.Remain_Minutes.Text = "03";
            // 
            // Next_Block
            // 
            this.Next_Block.Location = new System.Drawing.Point(15, 317);
            this.Next_Block.Name = "Next_Block";
            this.Next_Block.Size = new System.Drawing.Size(120, 120);
            this.Next_Block.TabIndex = 7;
            this.Next_Block.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(10, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 27);
            this.label6.TabIndex = 6;
            this.label6.Text = "Next Block";
            // 
            // Item_List
            // 
            this.Item_List.BackColor = System.Drawing.Color.White;
            this.Item_List.Enabled = false;
            this.Item_List.ForeColor = System.Drawing.Color.Black;
            this.Item_List.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14,
            listViewItem15});
            this.Item_List.Location = new System.Drawing.Point(11, 172);
            this.Item_List.Name = "Item_List";
            this.Item_List.Size = new System.Drawing.Size(154, 112);
            this.Item_List.TabIndex = 5;
            this.Item_List.UseCompatibleStateImageBehavior = false;
            this.Item_List.View = System.Windows.Forms.View.List;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "Items";
            // 
            // Score_text
            // 
            this.Score_text.AutoSize = true;
            this.Score_text.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Score_text.ForeColor = System.Drawing.Color.White;
            this.Score_text.Location = new System.Drawing.Point(76, 105);
            this.Score_text.Name = "Score_text";
            this.Score_text.Size = new System.Drawing.Size(28, 27);
            this.Score_text.TabIndex = 3;
            this.Score_text.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "Score";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Time";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // captureTimer
            // 
            this.captureTimer.Tick += new System.EventHandler(this.captureTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(478, 600);
            this.Controls.Add(this.GameBoard);
            this.Controls.Add(this.GameStatus);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Tetris";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.GameBoard.ResumeLayout(false);
            this.GameBoard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).EndInit();
            this.BackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blind_pbox)).EndInit();
            this.GameStatus.ResumeLayout(false);
            this.GameStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Enemy_Screen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Next_Block)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GameBoard;
        private System.Windows.Forms.Panel GameStatus;
        private System.Windows.Forms.PictureBox Next_Block;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView Item_List;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Score_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox BackGround;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label Remain_Seconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Remain_Minutes;
        public System.Windows.Forms.PictureBox Enemy_Screen;
        public System.Windows.Forms.Label Enemy_Text;
        private System.Windows.Forms.Label Combo_label;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button Exit_btn;
        private System.Windows.Forms.Button Restart_btn;
        private System.Windows.Forms.Label Game_Over_Msg;
        private System.Windows.Forms.Timer captureTimer;
        private System.Windows.Forms.PictureBox blind_pbox;
    }
}

