namespace eMotoCellSimulator
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
            this.components = new System.ComponentModel.Container();
            this.listBoxASCII = new System.Windows.Forms.ListBox();
            this.listBoxHex = new System.Windows.Forms.ListBox();
            this.btnSend1 = new System.Windows.Forms.Button();
            this.btnSend2 = new System.Windows.Forms.Button();
            this.btnSend3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSendPreAmble = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tbSerialPortNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxASCII
            // 
            this.listBoxASCII.FormattingEnabled = true;
            this.listBoxASCII.ItemHeight = 20;
            this.listBoxASCII.Location = new System.Drawing.Point(18, 58);
            this.listBoxASCII.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxASCII.Name = "listBoxASCII";
            this.listBoxASCII.Size = new System.Drawing.Size(373, 384);
            this.listBoxASCII.TabIndex = 0;
            // 
            // listBoxHex
            // 
            this.listBoxHex.FormattingEnabled = true;
            this.listBoxHex.ItemHeight = 20;
            this.listBoxHex.Location = new System.Drawing.Point(402, 58);
            this.listBoxHex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxHex.Name = "listBoxHex";
            this.listBoxHex.Size = new System.Drawing.Size(390, 384);
            this.listBoxHex.TabIndex = 1;
            // 
            // btnSend1
            // 
            this.btnSend1.Location = new System.Drawing.Point(560, 529);
            this.btnSend1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend1.Name = "btnSend1";
            this.btnSend1.Size = new System.Drawing.Size(112, 35);
            this.btnSend1.TabIndex = 2;
            this.btnSend1.Text = "button1";
            this.btnSend1.UseVisualStyleBackColor = true;
            this.btnSend1.Click += new System.EventHandler(this.btnSend1_Click);
            // 
            // btnSend2
            // 
            this.btnSend2.Location = new System.Drawing.Point(560, 574);
            this.btnSend2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend2.Name = "btnSend2";
            this.btnSend2.Size = new System.Drawing.Size(112, 35);
            this.btnSend2.TabIndex = 3;
            this.btnSend2.Text = "button2";
            this.btnSend2.UseVisualStyleBackColor = true;
            // 
            // btnSend3
            // 
            this.btnSend3.Location = new System.Drawing.Point(560, 618);
            this.btnSend3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend3.Name = "btnSend3";
            this.btnSend3.Size = new System.Drawing.Size(112, 35);
            this.btnSend3.TabIndex = 4;
            this.btnSend3.Text = "button3";
            this.btnSend3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 532);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(530, 26);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(18, 578);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(530, 26);
            this.textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(18, 622);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(530, 26);
            this.textBox3.TabIndex = 7;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 230400;
            this.serialPort1.PortName = "COM5";
            this.serialPort1.ReadBufferSize = 50000;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(166, 12);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 35);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSendPreAmble
            // 
            this.btnSendPreAmble.Location = new System.Drawing.Point(680, 529);
            this.btnSendPreAmble.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendPreAmble.Name = "btnSendPreAmble";
            this.btnSendPreAmble.Size = new System.Drawing.Size(112, 78);
            this.btnSendPreAmble.TabIndex = 9;
            this.btnSendPreAmble.Text = "Send Preamble";
            this.btnSendPreAmble.UseVisualStyleBackColor = true;
            this.btnSendPreAmble.Click += new System.EventHandler(this.btnSendPreAmble_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 668);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 20);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status";
            // 
            // tbSerialPortNo
            // 
            this.tbSerialPortNo.Location = new System.Drawing.Point(60, 17);
            this.tbSerialPortNo.Name = "tbSerialPortNo";
            this.tbSerialPortNo.Size = new System.Drawing.Size(100, 26);
            this.tbSerialPortNo.TabIndex = 11;
            this.tbSerialPortNo.Text = "COM6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Port";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(18, 449);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(142, 35);
            this.btnClearLog.TabIndex = 13;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 709);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSerialPortNo);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSendPreAmble);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSend3);
            this.Controls.Add(this.btnSend2);
            this.Controls.Add(this.btnSend1);
            this.Controls.Add(this.listBoxHex);
            this.Controls.Add(this.listBoxASCII);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxASCII;
        private System.Windows.Forms.ListBox listBoxHex;
        private System.Windows.Forms.Button btnSend1;
        private System.Windows.Forms.Button btnSend2;
        private System.Windows.Forms.Button btnSend3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSendPreAmble;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox tbSerialPortNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearLog;
    }
}

