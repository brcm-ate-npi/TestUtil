namespace TesterUtil
{
	// Token: 0x02000002 RID: 2
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000039DC File Offset: 0x00001BDC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003A14 File Offset: 0x00001C14
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_QA = new System.Windows.Forms.Button();
            this.btn_NPI = new System.Windows.Forms.Button();
            this.btn_PM = new System.Windows.Forms.Button();
            this.btn_Inst = new System.Windows.Forms.Button();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lbl_ipaddr = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRdpLogoff = new System.Windows.Forms.Button();
            this.btnKillExcel = new System.Windows.Forms.Button();
            this.tbxLogs = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_QA
            // 
            this.btn_QA.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_QA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QA.Location = new System.Drawing.Point(6, 19);
            this.btn_QA.Name = "btn_QA";
            this.btn_QA.Size = new System.Drawing.Size(267, 156);
            this.btn_QA.TabIndex = 1;
            this.btn_QA.Text = "Qual / BO / FLT / QA";
            this.btn_QA.UseVisualStyleBackColor = false;
            this.btn_QA.Click += new System.EventHandler(this.btn_QA_Click);
            // 
            // btn_NPI
            // 
            this.btn_NPI.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_NPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NPI.Location = new System.Drawing.Point(288, 19);
            this.btn_NPI.Name = "btn_NPI";
            this.btn_NPI.Size = new System.Drawing.Size(267, 156);
            this.btn_NPI.TabIndex = 2;
            this.btn_NPI.Text = "NPI Test / Development";
            this.btn_NPI.UseVisualStyleBackColor = false;
            this.btn_NPI.Click += new System.EventHandler(this.btn_NPI_Click);
            // 
            // btn_PM
            // 
            this.btn_PM.BackColor = System.Drawing.Color.DarkOrange;
            this.btn_PM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PM.Location = new System.Drawing.Point(6, 181);
            this.btn_PM.Name = "btn_PM";
            this.btn_PM.Size = new System.Drawing.Size(267, 156);
            this.btn_PM.TabIndex = 3;
            this.btn_PM.Text = "Tester PM / Calibration";
            this.btn_PM.UseVisualStyleBackColor = false;
            this.btn_PM.Click += new System.EventHandler(this.btn_PM_Click);
            // 
            // btn_Inst
            // 
            this.btn_Inst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Inst.Location = new System.Drawing.Point(6, 343);
            this.btn_Inst.Name = "btn_Inst";
            this.btn_Inst.Size = new System.Drawing.Size(267, 76);
            this.btn_Inst.TabIndex = 9;
            this.btn_Inst.Text = "NI-MAX Report Generator";
            this.btn_Inst.UseVisualStyleBackColor = true;
            this.btn_Inst.Click += new System.EventHandler(this.btn_Inst_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(3, 431);
            this.lbl_msg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(14, 13);
            this.lbl_msg.TabIndex = 12;
            this.lbl_msg.Text = "\'.\'";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // lbl_ipaddr
            // 
            this.lbl_ipaddr.AutoSize = true;
            this.lbl_ipaddr.Location = new System.Drawing.Point(21, 431);
            this.lbl_ipaddr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_ipaddr.Name = "lbl_ipaddr";
            this.lbl_ipaddr.Size = new System.Drawing.Size(94, 13);
            this.lbl_ipaddr.TabIndex = 13;
            this.lbl_ipaddr.Text = "Tester IP Address:";
            // 
            // buttonPanel
            // 
            this.buttonPanel.AutoScroll = true;
            this.buttonPanel.Location = new System.Drawing.Point(285, 180);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(270, 239);
            this.buttonPanel.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPanel);
            this.groupBox1.Controls.Add(this.btn_QA);
            this.groupBox1.Controls.Add(this.lbl_ipaddr);
            this.groupBox1.Controls.Add(this.btn_NPI);
            this.groupBox1.Controls.Add(this.lbl_msg);
            this.groupBox1.Controls.Add(this.btn_PM);
            this.groupBox1.Controls.Add(this.btn_Inst);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 459);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tester Utilization";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnRdpLogoff);
            this.flowLayoutPanel1.Controls.Add(this.btnKillExcel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(615, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(84, 459);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // btnRdpLogoff
            // 
            this.btnRdpLogoff.Location = new System.Drawing.Point(3, 3);
            this.btnRdpLogoff.Name = "btnRdpLogoff";
            this.btnRdpLogoff.Size = new System.Drawing.Size(75, 40);
            this.btnRdpLogoff.TabIndex = 0;
            this.btnRdpLogoff.Text = "RDP Logoff";
            this.btnRdpLogoff.UseVisualStyleBackColor = true;
            this.btnRdpLogoff.Click += new System.EventHandler(this.btnRdpLogoff_Click);
            // 
            // btnKillExcel
            // 
            this.btnKillExcel.Location = new System.Drawing.Point(3, 49);
            this.btnKillExcel.Name = "btnKillExcel";
            this.btnKillExcel.Size = new System.Drawing.Size(75, 40);
            this.btnKillExcel.TabIndex = 1;
            this.btnKillExcel.Text = "Kill Excel";
            this.btnKillExcel.UseVisualStyleBackColor = true;
            this.btnKillExcel.Click += new System.EventHandler(this.btnKillExcel_Click);
            // 
            // tbxLogs
            // 
            this.tbxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxLogs.BackColor = System.Drawing.SystemColors.WindowText;
            this.tbxLogs.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.tbxLogs.ForeColor = System.Drawing.SystemColors.Window;
            this.tbxLogs.Location = new System.Drawing.Point(12, 477);
            this.tbxLogs.Multiline = true;
            this.tbxLogs.Name = "tbxLogs";
            this.tbxLogs.Size = new System.Drawing.Size(687, 61);
            this.tbxLogs.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 550);
            this.Controls.Add(this.tbxLogs);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "WSD Tester Utilization v.1.0.5.0 (Auto IP Address Update)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x04000015 RID: 21
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.Button btn_QA;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.Button btn_NPI;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.Button btn_PM;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.Button btn_Inst;

		// Token: 0x04000022 RID: 34
		private global::System.Windows.Forms.Label lbl_msg;

		// Token: 0x04000023 RID: 35
		private global::System.ComponentModel.BackgroundWorker backgroundWorker1;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.Label lbl_ipaddr;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRdpLogoff;
        private System.Windows.Forms.TextBox tbxLogs;
        private System.Windows.Forms.Button btnKillExcel;
    }
}
