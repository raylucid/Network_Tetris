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
            this.GameBoard = new System.Windows.Forms.Panel();
            this.BackGround = new System.Windows.Forms.PictureBox();
            this.GameStatus = new System.Windows.Forms.Panel();
            this.Remain_Seconds = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Remain_Minutes = new System.Windows.Forms.Label();
            this.Next_Block = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.GameBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).BeginInit();
            this.GameStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Next_Block)).BeginInit();
            this.SuspendLayout();
            // 
            // GameBoard
            // 
            this.GameBoard.Controls.Add(this.BackGround);
            this.GameBoard.Dock = System.Windows.Forms.DockStyle.Left;
            this.GameBoard.Location = new System.Drawing.Point(0, 0);
            this.GameBoard.Name = "GameBoard";
            this.GameBoard.Size = new System.Drawing.Size(300, 600);
            this.GameBoard.TabIndex = 0;
            // 
            // BackGround
            // 
            this.BackGround.BackColor = System.Drawing.SystemColors.Control;
            this.BackGround.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackGround.Location = new System.Drawing.Point(0, 0);
            this.BackGround.Name = "BackGround";
            this.BackGround.Size = new System.Drawing.Size(300, 600);
            this.BackGround.TabIndex = 0;
            this.BackGround.TabStop = false;
            // 
            // GameStatus
            // 
            this.GameStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameStatus.Controls.Add(this.Remain_Seconds);
            this.GameStatus.Controls.Add(this.label2);
            this.GameStatus.Controls.Add(this.Remain_Minutes);
            this.GameStatus.Controls.Add(this.Next_Block);
            this.GameStatus.Controls.Add(this.label6);
            this.GameStatus.Controls.Add(this.listView1);
            this.GameStatus.Controls.Add(this.label5);
            this.GameStatus.Controls.Add(this.label4);
            this.GameStatus.Controls.Add(this.label3);
            this.GameStatus.Controls.Add(this.label1);
            this.GameStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.GameStatus.Location = new System.Drawing.Point(300, 0);
            this.GameStatus.Name = "GameStatus";
            this.GameStatus.Size = new System.Drawing.Size(178, 600);
            this.GameStatus.TabIndex = 1;
            // 
            // Remain_Seconds
            // 
            this.Remain_Seconds.AutoSize = true;
            this.Remain_Seconds.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Remain_Seconds.Location = new System.Drawing.Point(60, 69);
            this.Remain_Seconds.Name = "Remain_Seconds";
            this.Remain_Seconds.Size = new System.Drawing.Size(44, 27);
            this.Remain_Seconds.TabIndex = 10;
            this.Remain_Seconds.Text = "00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(49, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 27);
            this.label2.TabIndex = 9;
            this.label2.Text = ":";
            // 
            // Remain_Minutes
            // 
            this.Remain_Minutes.AutoSize = true;
            this.Remain_Minutes.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Remain_Minutes.Location = new System.Drawing.Point(10, 69);
            this.Remain_Minutes.Name = "Remain_Minutes";
            this.Remain_Minutes.Size = new System.Drawing.Size(44, 27);
            this.Remain_Minutes.TabIndex = 8;
            this.Remain_Minutes.Text = "03";
            // 
            // Next_Block
            // 
            this.Next_Block.Location = new System.Drawing.Point(15, 416);
            this.Next_Block.Name = "Next_Block";
            this.Next_Block.Size = new System.Drawing.Size(120, 120);
            this.Next_Block.TabIndex = 7;
            this.Next_Block.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(10, 377);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 27);
            this.label6.TabIndex = 6;
            this.label6.Text = "Next Block";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(10, 232);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(154, 112);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(10, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "Items";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(10, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(10, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "Score";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(10, 27);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(478, 600);
            this.Controls.Add(this.GameStatus);
            this.Controls.Add(this.GameBoard);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.GameBoard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BackGround)).EndInit();
            this.GameStatus.ResumeLayout(false);
            this.GameStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Next_Block)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GameBoard;
        private System.Windows.Forms.Panel GameStatus;
        private System.Windows.Forms.PictureBox Next_Block;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox BackGround;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label Remain_Seconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Remain_Minutes;
    }
}

