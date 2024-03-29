namespace TCPChatClientTest
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnect = new Button();
            btnDisconnect = new Button();
            txtOutput = new TextBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 10);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(117, 38);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(189, 12);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(117, 38);
            btnDisconnect.TabIndex = 1;
            btnDisconnect.Text = "Disconect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Visible = false;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // txtOutput
            // 
            txtOutput.Enabled = false;
            txtOutput.Location = new Point(13, 57);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(293, 151);
            txtOutput.TabIndex = 2;
            // 
            // txtMessage
            // 
            txtMessage.Enabled = false;
            txtMessage.Location = new Point(13, 231);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(219, 23);
            txtMessage.TabIndex = 3;
            // 
            // btnSend
            // 
            btnSend.Enabled = false;
            btnSend.Location = new Point(245, 229);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(61, 26);
            btnSend.TabIndex = 4;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click_1;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 266);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(txtOutput);
            Controls.Add(btnDisconnect);
            Controls.Add(btnConnect);
            Name = "Client";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private Button btnDisconnect;
        private TextBox txtOutput;
        private TextBox txtMessage;
        private Button btnSend;
    }
}