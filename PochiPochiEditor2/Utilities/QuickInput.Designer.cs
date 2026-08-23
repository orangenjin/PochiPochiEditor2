
namespace PochiPochiEditor2.Utilities
{
    partial class QuickInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickInput));
            this.grpInputInfo = new System.Windows.Forms.GroupBox();
            this.lblTargetOffset = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.lblEntryCount = new System.Windows.Forms.Label();
            this.lblSelectFile = new System.Windows.Forms.Label();
            this.txtTargetOffset = new System.Windows.Forms.TextBox();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.nudEntryCount = new System.Windows.Forms.NumericUpDown();
            this.txtSelectFile = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.grpInputInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEntryCount)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInputInfo
            // 
            this.grpInputInfo.Controls.Add(this.btnSelectFile);
            this.grpInputInfo.Controls.Add(this.nudEntryCount);
            this.grpInputInfo.Controls.Add(this.cmbDataType);
            this.grpInputInfo.Controls.Add(this.txtSelectFile);
            this.grpInputInfo.Controls.Add(this.txtTargetOffset);
            this.grpInputInfo.Controls.Add(this.lblSelectFile);
            this.grpInputInfo.Controls.Add(this.lblEntryCount);
            this.grpInputInfo.Controls.Add(this.lblDataType);
            this.grpInputInfo.Controls.Add(this.lblTargetOffset);
            this.grpInputInfo.Location = new System.Drawing.Point(20, 16);
            this.grpInputInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grpInputInfo.Name = "grpInputInfo";
            this.grpInputInfo.Padding = new System.Windows.Forms.Padding(0);
            this.grpInputInfo.Size = new System.Drawing.Size(416, 156);
            this.grpInputInfo.TabIndex = 0;
            this.grpInputInfo.TabStop = false;
            this.grpInputInfo.Text = "入力情報";
            // 
            // lblTargetOffset
            // 
            this.lblTargetOffset.AutoSize = true;
            this.lblTargetOffset.Location = new System.Drawing.Point(20, 28);
            this.lblTargetOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblTargetOffset.Name = "lblTargetOffset";
            this.lblTargetOffset.Size = new System.Drawing.Size(104, 15);
            this.lblTargetOffset.TabIndex = 0;
            this.lblTargetOffset.Text = "書き込み先アドレス :";
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(20, 58);
            this.lblDataType.Margin = new System.Windows.Forms.Padding(0);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(66, 15);
            this.lblDataType.TabIndex = 0;
            this.lblDataType.Text = "データタイプ :";
            // 
            // lblEntryCount
            // 
            this.lblEntryCount.AutoSize = true;
            this.lblEntryCount.Location = new System.Drawing.Point(20, 88);
            this.lblEntryCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblEntryCount.Name = "lblEntryCount";
            this.lblEntryCount.Size = new System.Drawing.Size(67, 15);
            this.lblEntryCount.TabIndex = 0;
            this.lblEntryCount.Text = "エントリー数 :";
            // 
            // lblSelectFile
            // 
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(20, 118);
            this.lblSelectFile.Margin = new System.Windows.Forms.Padding(0);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(71, 15);
            this.lblSelectFile.TabIndex = 0;
            this.lblSelectFile.Text = "参照ファイル :";
            // 
            // txtTargetOffset
            // 
            this.txtTargetOffset.Location = new System.Drawing.Point(136, 24);
            this.txtTargetOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtTargetOffset.Name = "txtTargetOffset";
            this.txtTargetOffset.Size = new System.Drawing.Size(80, 23);
            this.txtTargetOffset.TabIndex = 1;
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(136, 54);
            this.cmbDataType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(152, 23);
            this.cmbDataType.TabIndex = 2;
            // 
            // nudEntryCount
            // 
            this.nudEntryCount.Location = new System.Drawing.Point(136, 84);
            this.nudEntryCount.Margin = new System.Windows.Forms.Padding(0);
            this.nudEntryCount.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEntryCount.Name = "nudEntryCount";
            this.nudEntryCount.Size = new System.Drawing.Size(56, 23);
            this.nudEntryCount.TabIndex = 3;
            // 
            // txtSelectFile
            // 
            this.txtSelectFile.Location = new System.Drawing.Point(136, 114);
            this.txtSelectFile.Margin = new System.Windows.Forms.Padding(0);
            this.txtSelectFile.Name = "txtSelectFile";
            this.txtSelectFile.ReadOnly = true;
            this.txtSelectFile.Size = new System.Drawing.Size(176, 23);
            this.txtSelectFile.TabIndex = 1;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(320, 114);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(72, 23);
            this.btnSelectFile.TabIndex = 4;
            this.btnSelectFile.Text = "選択";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(20, 180);
            this.btnApply.Margin = new System.Windows.Forms.Padding(0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(416, 31);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "適用";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // QuickInputPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 233);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.grpInputInfo);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "QuickInputPopup";
            this.Text = "入力画面";
            this.grpInputInfo.ResumeLayout(false);
            this.grpInputInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEntryCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInputInfo;
        private System.Windows.Forms.TextBox txtTargetOffset;
        private System.Windows.Forms.Label lblSelectFile;
        private System.Windows.Forms.Label lblEntryCount;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.Label lblTargetOffset;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.NumericUpDown nudEntryCount;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtSelectFile;
        private System.Windows.Forms.Button btnApply;
    }
}