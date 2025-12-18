namespace LabTest
{
    partial class Menu
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
            this.Server = new System.Windows.Forms.Button();
            this.CustomerClient = new System.Windows.Forms.Button();
            this.StaffClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Server
            // 
            this.Server.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Server.Location = new System.Drawing.Point(48, 54);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(214, 55);
            this.Server.TabIndex = 0;
            this.Server.Text = "Server";
            this.Server.UseVisualStyleBackColor = true;
            this.Server.Click += new System.EventHandler(this.Server_Click);
            // 
            // CustomerClient
            // 
            this.CustomerClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerClient.Location = new System.Drawing.Point(48, 144);
            this.CustomerClient.Name = "CustomerClient";
            this.CustomerClient.Size = new System.Drawing.Size(214, 55);
            this.CustomerClient.TabIndex = 1;
            this.CustomerClient.Text = "Customer Client";
            this.CustomerClient.UseVisualStyleBackColor = true;
            this.CustomerClient.Click += new System.EventHandler(this.CustomerClient_Click);
            // 
            // StaffClient
            // 
            this.StaffClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StaffClient.Location = new System.Drawing.Point(48, 234);
            this.StaffClient.Name = "StaffClient";
            this.StaffClient.Size = new System.Drawing.Size(214, 55);
            this.StaffClient.TabIndex = 2;
            this.StaffClient.Text = "Staff Client";
            this.StaffClient.UseVisualStyleBackColor = true;
            this.StaffClient.Click += new System.EventHandler(this.StaffClient_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 330);
            this.Controls.Add(this.StaffClient);
            this.Controls.Add(this.CustomerClient);
            this.Controls.Add(this.Server);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Server;
        private System.Windows.Forms.Button CustomerClient;
        private System.Windows.Forms.Button StaffClient;
    }
}

