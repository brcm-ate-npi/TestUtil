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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::TesterUtil.Form1));
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_QA = new global::System.Windows.Forms.Button();
			this.btn_NPI = new global::System.Windows.Forms.Button();
			this.btn_PM = new global::System.Windows.Forms.Button();
			this.btn_Clotho01 = new global::System.Windows.Forms.Button();
			this.btn_Clotho02 = new global::System.Windows.Forms.Button();
			this.btn_Clotho03 = new global::System.Windows.Forms.Button();
			this.btn_Clotho04 = new global::System.Windows.Forms.Button();
			this.btn_Clotho05 = new global::System.Windows.Forms.Button();
			this.btn_Inst = new global::System.Windows.Forms.Button();
			this.lbl_message = new global::System.Windows.Forms.Label();
			this.btn_Clotho06 = new global::System.Windows.Forms.Button();
			this.lbl_msg = new global::System.Windows.Forms.Label();
			this.backgroundWorker1 = new global::System.ComponentModel.BackgroundWorker();
			this.lbl_ipaddr = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(18, 14);
			this.label1.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(193, 29);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tester Utilization";
			this.btn_QA.BackColor = global::System.Drawing.Color.PaleGreen;
			this.btn_QA.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_QA.Location = new global::System.Drawing.Point(60, 49);
			this.btn_QA.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_QA.Name = "btn_QA";
			this.btn_QA.Size = new global::System.Drawing.Size(400, 240);
			this.btn_QA.TabIndex = 1;
			this.btn_QA.Text = "Qual / BO / FLT / QA";
			this.btn_QA.UseVisualStyleBackColor = false;
			this.btn_QA.Click += new global::System.EventHandler(this.btn_QA_Click);
			this.btn_NPI.BackColor = global::System.Drawing.Color.DodgerBlue;
			this.btn_NPI.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_NPI.Location = new global::System.Drawing.Point(498, 49);
			this.btn_NPI.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_NPI.Name = "btn_NPI";
			this.btn_NPI.Size = new global::System.Drawing.Size(400, 240);
			this.btn_NPI.TabIndex = 2;
			this.btn_NPI.Text = "NPI Test / Development";
			this.btn_NPI.UseVisualStyleBackColor = false;
			this.btn_NPI.Click += new global::System.EventHandler(this.btn_NPI_Click);
			this.btn_PM.BackColor = global::System.Drawing.Color.DarkOrange;
			this.btn_PM.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_PM.Location = new global::System.Drawing.Point(60, 298);
			this.btn_PM.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_PM.Name = "btn_PM";
			this.btn_PM.Size = new global::System.Drawing.Size(400, 240);
			this.btn_PM.TabIndex = 3;
			this.btn_PM.Text = "Tester PM / Calibration";
			this.btn_PM.UseVisualStyleBackColor = false;
			this.btn_PM.Click += new global::System.EventHandler(this.btn_PM_Click);
			this.btn_Clotho01.BackColor = global::System.Drawing.Color.Snow;
			this.btn_Clotho01.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_Clotho01.Location = new global::System.Drawing.Point(498, 298);
			this.btn_Clotho01.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Clotho01.Name = "btn_Clotho01";
			this.btn_Clotho01.Size = new global::System.Drawing.Size(400, 45);
			this.btn_Clotho01.TabIndex = 4;
			this.btn_Clotho01.Text = "Clotho Version";
			this.btn_Clotho01.UseVisualStyleBackColor = false;
			this.btn_Clotho01.Click += new global::System.EventHandler(this.btn_Clotho01_Click);
			this.btn_Clotho02.BackColor = global::System.Drawing.Color.Snow;
			this.btn_Clotho02.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_Clotho02.Location = new global::System.Drawing.Point(498, 352);
			this.btn_Clotho02.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Clotho02.Name = "btn_Clotho02";
			this.btn_Clotho02.Size = new global::System.Drawing.Size(400, 45);
			this.btn_Clotho02.TabIndex = 5;
			this.btn_Clotho02.Text = "Clotho Version";
			this.btn_Clotho02.UseVisualStyleBackColor = false;
			this.btn_Clotho02.Visible = false;
			this.btn_Clotho02.Click += new global::System.EventHandler(this.btn_Clotho02_Click);
			this.btn_Clotho03.BackColor = global::System.Drawing.Color.Snow;
			this.btn_Clotho03.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_Clotho03.Location = new global::System.Drawing.Point(498, 409);
			this.btn_Clotho03.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Clotho03.Name = "btn_Clotho03";
			this.btn_Clotho03.Size = new global::System.Drawing.Size(400, 45);
			this.btn_Clotho03.TabIndex = 6;
			this.btn_Clotho03.Text = "Clotho Version";
			this.btn_Clotho03.UseVisualStyleBackColor = false;
			this.btn_Clotho03.Visible = false;
			this.btn_Clotho03.Click += new global::System.EventHandler(this.btn_Clotho03_Click);
			this.btn_Clotho04.BackColor = global::System.Drawing.Color.Snow;
			this.btn_Clotho04.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_Clotho04.Location = new global::System.Drawing.Point(498, 463);
			this.btn_Clotho04.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Clotho04.Name = "btn_Clotho04";
			this.btn_Clotho04.Size = new global::System.Drawing.Size(400, 45);
			this.btn_Clotho04.TabIndex = 7;
			this.btn_Clotho04.Text = "Clotho Version";
			this.btn_Clotho04.UseVisualStyleBackColor = false;
			this.btn_Clotho04.Visible = false;
			this.btn_Clotho04.Click += new global::System.EventHandler(this.btn_Clotho04_Click);
			this.btn_Clotho05.BackColor = global::System.Drawing.Color.Snow;
			this.btn_Clotho05.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_Clotho05.Location = new global::System.Drawing.Point(498, 517);
			this.btn_Clotho05.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Clotho05.Name = "btn_Clotho05";
			this.btn_Clotho05.Size = new global::System.Drawing.Size(400, 45);
			this.btn_Clotho05.TabIndex = 8;
			this.btn_Clotho05.Text = "Clotho Version";
			this.btn_Clotho05.UseVisualStyleBackColor = false;
			this.btn_Clotho05.Visible = false;
			this.btn_Clotho05.Click += new global::System.EventHandler(this.btn_Clotho05_Click);
			this.btn_Inst.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_Inst.Location = new global::System.Drawing.Point(60, 548);
			this.btn_Inst.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Inst.Name = "btn_Inst";
			this.btn_Inst.Size = new global::System.Drawing.Size(400, 117);
			this.btn_Inst.TabIndex = 9;
			this.btn_Inst.Text = "NI-MAX Report Generator";
			this.btn_Inst.UseVisualStyleBackColor = true;
			this.btn_Inst.Click += new global::System.EventHandler(this.btn_Inst_Click);
			this.lbl_message.AutoSize = true;
			this.lbl_message.Location = new global::System.Drawing.Point(494, 549);
			this.lbl_message.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_message.Name = "lbl_message";
			this.lbl_message.Size = new global::System.Drawing.Size(0, 20);
			this.lbl_message.TabIndex = 10;
			this.lbl_message.Click += new global::System.EventHandler(this.lbl_message_Click);
			this.btn_Clotho06.BackColor = global::System.Drawing.Color.Snow;
			this.btn_Clotho06.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_Clotho06.Location = new global::System.Drawing.Point(498, 571);
			this.btn_Clotho06.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Clotho06.Name = "btn_Clotho06";
			this.btn_Clotho06.Size = new global::System.Drawing.Size(400, 45);
			this.btn_Clotho06.TabIndex = 11;
			this.btn_Clotho06.Text = "Clotho Version";
			this.btn_Clotho06.UseVisualStyleBackColor = false;
			this.btn_Clotho06.Visible = false;
			this.btn_Clotho06.Click += new global::System.EventHandler(this.btn_Clotho06_Click);
			this.lbl_msg.AutoSize = true;
			this.lbl_msg.Location = new global::System.Drawing.Point(56, 683);
			this.lbl_msg.Name = "lbl_msg";
			this.lbl_msg.Size = new global::System.Drawing.Size(19, 20);
			this.lbl_msg.TabIndex = 12;
			this.lbl_msg.Text = "'.'";
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new global::System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new global::System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new global::System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.lbl_ipaddr.AutoSize = true;
			this.lbl_ipaddr.Location = new global::System.Drawing.Point(82, 683);
			this.lbl_ipaddr.Name = "lbl_ipaddr";
			this.lbl_ipaddr.Size = new global::System.Drawing.Size(140, 20);
			this.lbl_ipaddr.TabIndex = 13;
			this.lbl_ipaddr.Text = "Tester IP Address:";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(974, 723);
			base.Controls.Add(this.lbl_ipaddr);
			base.Controls.Add(this.lbl_msg);
			base.Controls.Add(this.btn_Clotho06);
			base.Controls.Add(this.lbl_message);
			base.Controls.Add(this.btn_Inst);
			base.Controls.Add(this.btn_Clotho05);
			base.Controls.Add(this.btn_Clotho04);
			base.Controls.Add(this.btn_Clotho03);
			base.Controls.Add(this.btn_Clotho02);
			base.Controls.Add(this.btn_Clotho01);
			base.Controls.Add(this.btn_PM);
			base.Controls.Add(this.btn_NPI);
			base.Controls.Add(this.btn_QA);
			base.Controls.Add(this.label1);
            base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
            base.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			base.Name = "Form1";
			this.Text = "WSD Tester Utilization v.1.0.5.0 (Auto IP Address Update)";
			base.Load += new global::System.EventHandler(this.Form1_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
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

		// Token: 0x0400001A RID: 26
		private global::System.Windows.Forms.Button btn_Clotho01;

		// Token: 0x0400001B RID: 27
		private global::System.Windows.Forms.Button btn_Clotho02;

		// Token: 0x0400001C RID: 28
		private global::System.Windows.Forms.Button btn_Clotho03;

		// Token: 0x0400001D RID: 29
		private global::System.Windows.Forms.Button btn_Clotho04;

		// Token: 0x0400001E RID: 30
		private global::System.Windows.Forms.Button btn_Clotho05;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.Button btn_Inst;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.Label lbl_message;

		// Token: 0x04000021 RID: 33
		private global::System.Windows.Forms.Button btn_Clotho06;

		// Token: 0x04000022 RID: 34
		private global::System.Windows.Forms.Label lbl_msg;

		// Token: 0x04000023 RID: 35
		private global::System.ComponentModel.BackgroundWorker backgroundWorker1;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.Label lbl_ipaddr;
	}
}
