namespace Tetris
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Title = new System.Windows.Forms.Label();
            this.Single_btn = new System.Windows.Forms.Button();
            this.Multi_btn = new System.Windows.Forms.Button();
            this.Client_btn = new System.Windows.Forms.Button();
            this.Server_btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Input_label = new System.Windows.Forms.Label();
            this.OK_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("굴림", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Title.Location = new System.Drawing.Point(26, 21);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(221, 29);
            this.Title.TabIndex = 0;
            this.Title.Text = "Network Tetris";
            // 
            // Single_btn
            // 
            this.Single_btn.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Single_btn.Location = new System.Drawing.Point(31, 74);
            this.Single_btn.Name = "Single_btn";
            this.Single_btn.Size = new System.Drawing.Size(216, 52);
            this.Single_btn.TabIndex = 1;
            this.Single_btn.Text = "Single Mode";
            this.Single_btn.UseVisualStyleBackColor = true;
            this.Single_btn.Click += new System.EventHandler(this.Single_btn_Click);
            // 
            // Multi_btn
            // 
            this.Multi_btn.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Multi_btn.Location = new System.Drawing.Point(31, 154);
            this.Multi_btn.Name = "Multi_btn";
            this.Multi_btn.Size = new System.Drawing.Size(216, 52);
            this.Multi_btn.TabIndex = 1;
            this.Multi_btn.Text = "Multi Mode";
            this.Multi_btn.UseVisualStyleBackColor = true;
            this.Multi_btn.Click += new System.EventHandler(this.Multi_btn_Click);
            // 
            // Client_btn
            // 
            this.Client_btn.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Client_btn.Location = new System.Drawing.Point(31, 74);
            this.Client_btn.Name = "Client_btn";
            this.Client_btn.Size = new System.Drawing.Size(216, 52);
            this.Client_btn.TabIndex = 1;
            this.Client_btn.Text = "Client";
            this.Client_btn.UseVisualStyleBackColor = true;
            this.Client_btn.Visible = false;
            this.Client_btn.Click += new System.EventHandler(this.Client_btn_Click);
            // 
            // Server_btn
            // 
            this.Server_btn.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Server_btn.Location = new System.Drawing.Point(31, 154);
            this.Server_btn.Name = "Server_btn";
            this.Server_btn.Size = new System.Drawing.Size(216, 52);
            this.Server_btn.TabIndex = 1;
            this.Server_btn.Text = "Server";
            this.Server_btn.UseVisualStyleBackColor = true;
            this.Server_btn.Visible = false;
            this.Server_btn.Click += new System.EventHandler(this.Server_btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 154);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // Input_label
            // 
            this.Input_label.AutoSize = true;
            this.Input_label.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Input_label.Location = new System.Drawing.Point(28, 135);
            this.Input_label.Name = "Input_label";
            this.Input_label.Size = new System.Drawing.Size(48, 16);
            this.Input_label.TabIndex = 3;
            this.Input_label.Text = "Input";
            this.Input_label.Visible = false;
            // 
            // OK_btn
            // 
            this.OK_btn.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OK_btn.Location = new System.Drawing.Point(172, 154);
            this.OK_btn.Name = "OK_btn";
            this.OK_btn.Size = new System.Drawing.Size(75, 23);
            this.OK_btn.TabIndex = 4;
            this.OK_btn.Text = "OK";
            this.OK_btn.UseVisualStyleBackColor = true;
            this.OK_btn.Visible = false;
            this.OK_btn.Click += new System.EventHandler(this.OK_btn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.OK_btn);
            this.Controls.Add(this.Input_label);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Server_btn);
            this.Controls.Add(this.Client_btn);
            this.Controls.Add(this.Single_btn);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Multi_btn);
            this.Name = "Form2";
            this.Text = "Tetris";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Single_btn;
        private System.Windows.Forms.Button Multi_btn;
        private System.Windows.Forms.Button Client_btn;
        private System.Windows.Forms.Button Server_btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Input_label;
        private System.Windows.Forms.Button OK_btn;
    }
}