namespace LJSheng.App
{
    partial class DY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DY));
            this.dybt = new System.Windows.Forms.Button();
            this.biaotilb = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.kehubt = new System.Windows.Forms.Button();
            this.KHCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dybt
            // 
            this.dybt.Location = new System.Drawing.Point(665, 296);
            this.dybt.Name = "dybt";
            this.dybt.Size = new System.Drawing.Size(75, 23);
            this.dybt.TabIndex = 0;
            this.dybt.Text = "打印测试";
            this.dybt.UseVisualStyleBackColor = true;
            this.dybt.Click += new System.EventHandler(this.dybt_Click);
            // 
            // biaotilb
            // 
            this.biaotilb.AutoSize = true;
            this.biaotilb.BackColor = System.Drawing.Color.Transparent;
            this.biaotilb.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.biaotilb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.biaotilb.Location = new System.Drawing.Point(149, 4);
            this.biaotilb.Name = "biaotilb";
            this.biaotilb.Size = new System.Drawing.Size(500, 19);
            this.biaotilb.TabIndex = 1;
            this.biaotilb.Text = "请一定按步骤一步一步往下操作,否则打印会出现异常哦\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(34, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择模板:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(93, 230);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(141, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // kehubt
            // 
            this.kehubt.BackColor = System.Drawing.Color.Black;
            this.kehubt.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kehubt.ForeColor = System.Drawing.Color.White;
            this.kehubt.Location = new System.Drawing.Point(18, 33);
            this.kehubt.Name = "kehubt";
            this.kehubt.Size = new System.Drawing.Size(216, 56);
            this.kehubt.TabIndex = 4;
            this.kehubt.Text = "导入客户";
            this.kehubt.UseVisualStyleBackColor = false;
            this.kehubt.Click += new System.EventHandler(this.kehubt_Click);
            // 
            // KHCB
            // 
            this.KHCB.FormattingEnabled = true;
            this.KHCB.Location = new System.Drawing.Point(93, 116);
            this.KHCB.Name = "KHCB";
            this.KHCB.Size = new System.Drawing.Size(141, 20);
            this.KHCB.TabIndex = 6;
            this.KHCB.SelectedIndexChanged += new System.EventHandler(this.KHCB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(34, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择客户:";
            // 
            // tb
            // 
            this.tb.Location = new System.Drawing.Point(538, 52);
            this.tb.Multiline = true;
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(234, 142);
            this.tb.TabIndex = 7;
            // 
            // DY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tb);
            this.Controls.Add(this.KHCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.kehubt);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.biaotilb);
            this.Controls.Add(this.dybt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DY";
            this.Text = "票据打印程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dybt;
        private System.Windows.Forms.Label biaotilb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button kehubt;
        private System.Windows.Forms.ComboBox KHCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb;
    }
}