namespace OneCardSln.Components.WinForm
{
    partial class ControlReadNFCCard
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            DisposeThread();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCardNumber = new DevExpress.XtraEditors.TextEdit();
            this.btnRead = new DevExpress.XtraEditors.SimpleButton();
            this.lblMsg = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "一卡通号码：";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(88, 8);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Properties.ReadOnly = true;
            this.txtCardNumber.Size = new System.Drawing.Size(138, 20);
            this.txtCardNumber.TabIndex = 1;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(232, 7);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "手动读卡(&R)";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(23, 31);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(4, 14);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = " ";
            // 
            // ControlReadNFCCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.labelControl1);
            this.Name = "ControlReadNFCCard";
            this.Size = new System.Drawing.Size(310, 50);
            this.Load += new System.EventHandler(this.ControlReadCard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCardNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnRead;
        private DevExpress.XtraEditors.LabelControl lblMsg;
        public DevExpress.XtraEditors.TextEdit txtCardNumber;
    }
}
