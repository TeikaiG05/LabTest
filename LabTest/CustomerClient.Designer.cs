namespace LabTest
{
    partial class CustomerClient
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtSoMon = new System.Windows.Forms.TextBox();
            this.txtBan = new System.Windows.Forms.TextBox();
            this.cboMon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDat = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rtbView = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(90, 40);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(138, 20);
            this.txtIP.TabIndex = 0;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(234, 40);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(340, 37);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Kết nối";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // btnMenu
            // 
            this.btnMenu.Location = new System.Drawing.Point(340, 122);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(75, 23);
            this.btnMenu.TabIndex = 3;
            this.btnMenu.Text = "MENU";
            this.btnMenu.UseVisualStyleBackColor = true;
            // 
            // txtSoMon
            // 
            this.txtSoMon.Location = new System.Drawing.Point(90, 177);
            this.txtSoMon.Name = "txtSoMon";
            this.txtSoMon.Size = new System.Drawing.Size(44, 20);
            this.txtSoMon.TabIndex = 5;
            // 
            // txtBan
            // 
            this.txtBan.Location = new System.Drawing.Point(90, 151);
            this.txtBan.Name = "txtBan";
            this.txtBan.Size = new System.Drawing.Size(44, 20);
            this.txtBan.TabIndex = 6;
            // 
            // cboMon
            // 
            this.cboMon.FormattingEnabled = true;
            this.cboMon.Location = new System.Drawing.Point(90, 124);
            this.cboMon.Name = "cboMon";
            this.cboMon.Size = new System.Drawing.Size(138, 21);
            this.cboMon.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Chọn món";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Số bàn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Số món";
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(646, 196);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 11;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // btnDat
            // 
            this.btnDat.Location = new System.Drawing.Point(481, 196);
            this.btnDat.Name = "btnDat";
            this.btnDat.Size = new System.Drawing.Size(75, 23);
            this.btnDat.TabIndex = 12;
            this.btnDat.Text = "Đặt";
            this.btnDat.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(87, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(231, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "PORT";
            // 
            // rtbView
            // 
            this.rtbView.Location = new System.Drawing.Point(445, 37);
            this.rtbView.Name = "rtbView";
            this.rtbView.Size = new System.Drawing.Size(316, 153);
            this.rtbView.TabIndex = 15;
            this.rtbView.Text = "";
            // 
            // CustomerClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 272);
            this.Controls.Add(this.rtbView);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDat);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboMon);
            this.Controls.Add(this.txtBan);
            this.Controls.Add(this.txtSoMon);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Name = "CustomerClient";
            this.Text = "CustomerClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnMenu;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtSoMon;
        private System.Windows.Forms.TextBox txtBan;
        private System.Windows.Forms.ComboBox cboMon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnDat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtbView;
    }
}