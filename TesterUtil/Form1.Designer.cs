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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_QA = new System.Windows.Forms.Button();
            this.btn_NPI = new System.Windows.Forms.Button();
            this.btn_PM = new System.Windows.Forms.Button();
            this.btn_Inst = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lbl_ipaddr = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tester Utilization";
            // 
            // btn_QA
            // 
            this.btn_QA.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_QA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QA.Location = new System.Drawing.Point(40, 32);
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
            this.btn_NPI.Location = new System.Drawing.Point(332, 32);
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
            this.btn_PM.Location = new System.Drawing.Point(40, 194);
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
            this.btn_Inst.Location = new System.Drawing.Point(40, 356);
            this.btn_Inst.Name = "btn_Inst";
            this.btn_Inst.Size = new System.Drawing.Size(267, 76);
            this.btn_Inst.TabIndex = 9;
            this.btn_Inst.Text = "NI-MAX Report Generator";
            this.btn_Inst.UseVisualStyleBackColor = true;
            this.btn_Inst.Click += new System.EventHandler(this.btn_Inst_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Location = new System.Drawing.Point(329, 357);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 13);
            this.lbl_message.TabIndex = 10;
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(37, 444);
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
            this.lbl_ipaddr.Location = new System.Drawing.Point(55, 444);
            this.lbl_ipaddr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_ipaddr.Name = "lbl_ipaddr";
            this.lbl_ipaddr.Size = new System.Drawing.Size(94, 13);
            this.lbl_ipaddr.TabIndex = 13;
            this.lbl_ipaddr.Text = "Tester IP Address:";
            // 
            // buttonPanel
            // 
            this.buttonPanel.AutoScroll = true;
            this.buttonPanel.Location = new System.Drawing.Point(329, 193);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(308, 239);
            this.buttonPanel.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 470);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.lbl_ipaddr);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Inst);
            this.Controls.Add(this.btn_PM);
            this.Controls.Add(this.btn_NPI);
            this.Controls.Add(this.btn_QA);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "WSD Tester Utilization v.1.0.5.0 (Auto IP Address Update)";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x04000015 RID: 21
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.Button btn_QA;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.Button btn_NPI;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.Button btn_PM;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.Button btn_Inst;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.Label lbl_message;

		// Token: 0x04000022 RID: 34
		private global::System.Windows.Forms.Label lbl_msg;

		// Token: 0x04000023 RID: 35
		private global::System.ComponentModel.BackgroundWorker backgroundWorker1;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.Label lbl_ipaddr;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    }
}
