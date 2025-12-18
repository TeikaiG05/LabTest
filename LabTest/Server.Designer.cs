namespace LabTest
{
    partial class Server
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.ltlPort = new System.Windows.Forms.Label();
            this.txtNumPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(32, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 52);
            this.button1.TabIndex = 0;
            this.button1.Text = "START";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(32, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 52);
            this.button2.TabIndex = 1;
            this.button2.Text = "STOP";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(183, 73);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(935, 466);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(110, 24);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(264, 20);
            this.txtIP.TabIndex = 4;
            // 
            // ltlPort
            // 
            this.ltlPort.AutoSize = true;
            this.ltlPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltlPort.Location = new System.Drawing.Point(399, 24);
            this.ltlPort.Name = "ltlPort";
            this.ltlPort.Size = new System.Drawing.Size(47, 20);
            this.ltlPort.TabIndex = 5;
            this.ltlPort.Text = "Port:";
            // 
            // txtNumPort
            // 
            this.txtNumPort.Location = new System.Drawing.Point(452, 24);
            this.txtNumPort.Name = "txtNumPort";
            this.txtNumPort.Size = new System.Drawing.Size(104, 20);
            this.txtNumPort.TabIndex = 6;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 617);
            this.Controls.Add(this.txtNumPort);
            this.Controls.Add(this.ltlPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label ltlPort;
        private System.Windows.Forms.TextBox txtNumPort;
    }
}