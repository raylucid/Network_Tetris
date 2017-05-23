namespace ScreenShot
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
            this.btnShot = new System.Windows.Forms.Button();
            this.pbSource = new System.Windows.Forms.PictureBox();
            this.pbTarget = new System.Windows.Forms.PictureBox();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.screenShotTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShot
            // 
            this.btnShot.Location = new System.Drawing.Point(789, 100);
            this.btnShot.Name = "btnShot";
            this.btnShot.Size = new System.Drawing.Size(75, 23);
            this.btnShot.TabIndex = 0;
            this.btnShot.Text = "shot!";
            this.btnShot.UseVisualStyleBackColor = true;
            this.btnShot.Click += new System.EventHandler(this.btnShot_Click);
            // 
            // pbSource
            // 
            this.pbSource.Image = global::ScreenShot.Properties.Resources.설현2;
            this.pbSource.Location = new System.Drawing.Point(24, 12);
            this.pbSource.Name = "pbSource";
            this.pbSource.Size = new System.Drawing.Size(519, 808);
            this.pbSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSource.TabIndex = 1;
            this.pbSource.TabStop = false;
            // 
            // pbTarget
            // 
            this.pbTarget.Location = new System.Drawing.Point(777, 619);
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(198, 201);
            this.pbTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTarget.TabIndex = 2;
            this.pbTarget.TabStop = false;
            // 
            // txtPos
            // 
            this.txtPos.Location = new System.Drawing.Point(789, 129);
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(100, 25);
            this.txtPos.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(789, 34);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(104, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "server_start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(789, 64);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // screenShotTimer
            // 
            this.screenShotTimer.Interval = 3000;
            this.screenShotTimer.Tick += new System.EventHandler(this.screenShotTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 864);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtPos);
            this.Controls.Add(this.pbTarget);
            this.Controls.Add(this.pbSource);
            this.Controls.Add(this.btnShot);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTarget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShot;
        private System.Windows.Forms.PictureBox pbSource;
        private System.Windows.Forms.PictureBox pbTarget;
        private System.Windows.Forms.TextBox txtPos;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Timer screenShotTimer;
    }
}

