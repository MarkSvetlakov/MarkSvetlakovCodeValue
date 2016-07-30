namespace PrimesCalculator
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
            this.TbFirstNum = new System.Windows.Forms.TextBox();
            this.TbSecondNum = new System.Windows.Forms.TextBox();
            this.BtnCalculate = new System.Windows.Forms.Button();
            this.ListBoxResult = new System.Windows.Forms.ListBox();
            this.LbMessage = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TbFirstNum
            // 
            this.TbFirstNum.Location = new System.Drawing.Point(13, 13);
            this.TbFirstNum.Name = "TbFirstNum";
            this.TbFirstNum.Size = new System.Drawing.Size(100, 20);
            this.TbFirstNum.TabIndex = 0;
            // 
            // TbSecondNum
            // 
            this.TbSecondNum.Location = new System.Drawing.Point(172, 13);
            this.TbSecondNum.Name = "TbSecondNum";
            this.TbSecondNum.Size = new System.Drawing.Size(100, 20);
            this.TbSecondNum.TabIndex = 1;
            // 
            // BtnCalculate
            // 
            this.BtnCalculate.Location = new System.Drawing.Point(12, 198);
            this.BtnCalculate.Name = "BtnCalculate";
            this.BtnCalculate.Size = new System.Drawing.Size(198, 28);
            this.BtnCalculate.TabIndex = 2;
            this.BtnCalculate.Text = "Calculate";
            this.BtnCalculate.UseVisualStyleBackColor = true;
            this.BtnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // ListBoxResult
            // 
            this.ListBoxResult.FormattingEnabled = true;
            this.ListBoxResult.Location = new System.Drawing.Point(81, 39);
            this.ListBoxResult.Name = "ListBoxResult";
            this.ListBoxResult.Size = new System.Drawing.Size(120, 134);
            this.ListBoxResult.TabIndex = 3;
            // 
            // LbMessage
            // 
            this.LbMessage.AutoSize = true;
            this.LbMessage.Location = new System.Drawing.Point(111, 180);
            this.LbMessage.Name = "LbMessage";
            this.LbMessage.Size = new System.Drawing.Size(0, 13);
            this.LbMessage.TabIndex = 4;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(13, 230);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(197, 27);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(216, 198);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(65, 59);
            this.BtnClear.TabIndex = 6;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LbMessage);
            this.Controls.Add(this.ListBoxResult);
            this.Controls.Add(this.BtnCalculate);
            this.Controls.Add(this.TbSecondNum);
            this.Controls.Add(this.TbFirstNum);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbFirstNum;
        private System.Windows.Forms.TextBox TbSecondNum;
        private System.Windows.Forms.Button BtnCalculate;
        private System.Windows.Forms.ListBox ListBoxResult;
        private System.Windows.Forms.Label LbMessage;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnClear;
    }
}

