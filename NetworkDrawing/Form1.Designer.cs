namespace NetworkDrawing
{
    partial class Form1
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
            this.serverCheck = new System.Windows.Forms.CheckBox();
            this.clientCheck = new System.Windows.Forms.CheckBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverCheck
            // 
            this.serverCheck.AutoSize = true;
            this.serverCheck.Location = new System.Drawing.Point(13, 13);
            this.serverCheck.Name = "serverCheck";
            this.serverCheck.Size = new System.Drawing.Size(57, 17);
            this.serverCheck.TabIndex = 0;
            this.serverCheck.Text = "Server";
            this.serverCheck.UseVisualStyleBackColor = true;
            // 
            // clientCheck
            // 
            this.clientCheck.AutoSize = true;
            this.clientCheck.Location = new System.Drawing.Point(12, 36);
            this.clientCheck.Name = "clientCheck";
            this.clientCheck.Size = new System.Drawing.Size(52, 17);
            this.clientCheck.TabIndex = 1;
            this.clientCheck.Text = "Client";
            this.clientCheck.UseVisualStyleBackColor = true;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 60);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 730);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.clientCheck);
            this.Controls.Add(this.serverCheck);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox serverCheck;
        private System.Windows.Forms.CheckBox clientCheck;
        private System.Windows.Forms.Button connectButton;
    }
}

