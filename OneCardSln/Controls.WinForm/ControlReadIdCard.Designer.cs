namespace OneCardSln.Components.Controls.WinForm
{
    partial class ControlReadIdCard
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
            this.lblMsgIDCard = new System.Windows.Forms.Label();
            this.btnReadIDCard = new DevExpress.XtraEditors.SimpleButton();
            this.picIDCard = new System.Windows.Forms.PictureBox();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtIDCard = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblIDCard = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.picIDCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDCard.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsgIDCard
            // 
            this.lblMsgIDCard.AutoSize = true;
            this.lblMsgIDCard.ForeColor = System.Drawing.Color.Red;
            this.lblMsgIDCard.Location = new System.Drawing.Point(138, 134);
            this.lblMsgIDCard.Name = "lblMsgIDCard";
            this.lblMsgIDCard.Size = new System.Drawing.Size(11, 12);
            this.lblMsgIDCard.TabIndex = 5;
            this.lblMsgIDCard.Text = " ";
            // 
            // btnReadIDCard
            // 
            this.btnReadIDCard.Location = new System.Drawing.Point(191, 102);
            this.btnReadIDCard.Name = "btnReadIDCard";
            this.btnReadIDCard.Size = new System.Drawing.Size(93, 27);
            this.btnReadIDCard.TabIndex = 3;
            this.btnReadIDCard.Text = "读取身份证(&I)";
            this.btnReadIDCard.Click += new System.EventHandler(this.btnReadIDCard_Click);
            // 
            // picIDCard
            // 
            this.picIDCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIDCard.Location = new System.Drawing.Point(13, 16);
            this.picIDCard.Name = "picIDCard";
            this.picIDCard.Size = new System.Drawing.Size(106, 130);
            this.picIDCard.TabIndex = 2;
            this.picIDCard.TabStop = false;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(191, 69);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(150, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtIDCard
            // 
            this.txtIDCard.EditValue = "";
            this.txtIDCard.Location = new System.Drawing.Point(191, 32);
            this.txtIDCard.Name = "txtIDCard";
            this.txtIDCard.Properties.ReadOnly = true;
            this.txtIDCard.Size = new System.Drawing.Size(150, 20);
            this.txtIDCard.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(149, 69);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(36, 14);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "姓名：";
            // 
            // lblIDCard
            // 
            this.lblIDCard.Location = new System.Drawing.Point(125, 32);
            this.lblIDCard.Name = "lblIDCard";
            this.lblIDCard.Size = new System.Drawing.Size(60, 14);
            this.lblIDCard.TabIndex = 0;
            this.lblIDCard.Text = "身份证号：";
            // 
            // ControlReadIdCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMsgIDCard);
            this.Controls.Add(this.picIDCard);
            this.Controls.Add(this.btnReadIDCard);
            this.Controls.Add(this.lblIDCard);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtIDCard);
            this.Name = "ControlReadIdCard";
            this.Size = new System.Drawing.Size(369, 176);
            ((System.ComponentModel.ISupportInitialize)(this.picIDCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDCard.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsgIDCard;
        private DevExpress.XtraEditors.SimpleButton btnReadIDCard;
        private System.Windows.Forms.PictureBox picIDCard;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblIDCard;
        public DevExpress.XtraEditors.TextEdit txtIDCard;
        public DevExpress.XtraEditors.TextEdit txtName;
    }
}
