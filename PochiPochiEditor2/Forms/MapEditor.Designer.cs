
namespace PochiPochiEditor2.Forms
{
    partial class MapEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEditor));
            this.grpMapHeader = new System.Windows.Forms.GroupBox();
            this.lblMapFooterOffset = new System.Windows.Forms.Label();
            this.txtMapFooterOffset = new System.Windows.Forms.TextBox();
            this.lblEventScriptOffset = new System.Windows.Forms.Label();
            this.txtEventScriptOffset = new System.Windows.Forms.TextBox();
            this.lblLevelScriptOffset = new System.Windows.Forms.Label();
            this.lblConnHeaderOffset = new System.Windows.Forms.Label();
            this.txtLevelScriptOffset = new System.Windows.Forms.TextBox();
            this.txtConnHeaderOffset = new System.Windows.Forms.TextBox();
            this.lblMapTerrainIndex = new System.Windows.Forms.Label();
            this.nudMapTerrainIndex = new System.Windows.Forms.NumericUpDown();
            this.lblMapType = new System.Windows.Forms.Label();
            this.cmbMapType = new System.Windows.Forms.ComboBox();
            this.nudMapRelLayer = new System.Windows.Forms.NumericUpDown();
            this.lblMapRelLayer = new System.Windows.Forms.Label();
            this.cmbMapWthr = new System.Windows.Forms.ComboBox();
            this.lblMapWthr = new System.Windows.Forms.Label();
            this.cmbMapSight = new System.Windows.Forms.ComboBox();
            this.lblMapSight = new System.Windows.Forms.Label();
            this.cmbSpBg = new System.Windows.Forms.ComboBox();
            this.lblSpBg = new System.Windows.Forms.Label();
            this.cmbMapBike = new System.Windows.Forms.ComboBox();
            this.lblMapBike = new System.Windows.Forms.Label();
            this.cmbMapNameType = new System.Windows.Forms.ComboBox();
            this.lblMapNameType = new System.Windows.Forms.Label();
            this.cmbMapNameIndex = new System.Windows.Forms.ComboBox();
            this.lblMapNameIndex = new System.Windows.Forms.Label();
            this.nudBgmIndex = new System.Windows.Forms.NumericUpDown();
            this.lblBgmIndex = new System.Windows.Forms.Label();
            this.grpMapHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapTerrainIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapRelLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBgmIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMapHeader
            // 
            this.grpMapHeader.Controls.Add(this.nudBgmIndex);
            this.grpMapHeader.Controls.Add(this.lblBgmIndex);
            this.grpMapHeader.Controls.Add(this.cmbMapNameType);
            this.grpMapHeader.Controls.Add(this.lblMapNameType);
            this.grpMapHeader.Controls.Add(this.cmbMapNameIndex);
            this.grpMapHeader.Controls.Add(this.lblMapNameIndex);
            this.grpMapHeader.Controls.Add(this.cmbSpBg);
            this.grpMapHeader.Controls.Add(this.lblSpBg);
            this.grpMapHeader.Controls.Add(this.cmbMapBike);
            this.grpMapHeader.Controls.Add(this.lblMapBike);
            this.grpMapHeader.Controls.Add(this.cmbMapSight);
            this.grpMapHeader.Controls.Add(this.lblMapSight);
            this.grpMapHeader.Controls.Add(this.cmbMapWthr);
            this.grpMapHeader.Controls.Add(this.lblMapWthr);
            this.grpMapHeader.Controls.Add(this.nudMapRelLayer);
            this.grpMapHeader.Controls.Add(this.lblMapRelLayer);
            this.grpMapHeader.Controls.Add(this.cmbMapType);
            this.grpMapHeader.Controls.Add(this.nudMapTerrainIndex);
            this.grpMapHeader.Controls.Add(this.txtConnHeaderOffset);
            this.grpMapHeader.Controls.Add(this.txtEventScriptOffset);
            this.grpMapHeader.Controls.Add(this.txtLevelScriptOffset);
            this.grpMapHeader.Controls.Add(this.txtMapFooterOffset);
            this.grpMapHeader.Controls.Add(this.lblMapType);
            this.grpMapHeader.Controls.Add(this.lblMapTerrainIndex);
            this.grpMapHeader.Controls.Add(this.lblConnHeaderOffset);
            this.grpMapHeader.Controls.Add(this.lblEventScriptOffset);
            this.grpMapHeader.Controls.Add(this.lblLevelScriptOffset);
            this.grpMapHeader.Controls.Add(this.lblMapFooterOffset);
            this.grpMapHeader.Location = new System.Drawing.Point(20, 16);
            this.grpMapHeader.Margin = new System.Windows.Forms.Padding(0);
            this.grpMapHeader.Name = "grpMapHeader";
            this.grpMapHeader.Padding = new System.Windows.Forms.Padding(0);
            this.grpMapHeader.Size = new System.Drawing.Size(316, 462);
            this.grpMapHeader.TabIndex = 0;
            this.grpMapHeader.TabStop = false;
            this.grpMapHeader.Text = "マップヘッダー";
            // 
            // lblMapFooterOffset
            // 
            this.lblMapFooterOffset.AutoSize = true;
            this.lblMapFooterOffset.Location = new System.Drawing.Point(20, 32);
            this.lblMapFooterOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapFooterOffset.Name = "lblMapFooterOffset";
            this.lblMapFooterOffset.Size = new System.Drawing.Size(107, 15);
            this.lblMapFooterOffset.TabIndex = 0;
            this.lblMapFooterOffset.Text = "マップフッターアドレス :";
            // 
            // txtMapFooterOffset
            // 
            this.txtMapFooterOffset.Location = new System.Drawing.Point(156, 28);
            this.txtMapFooterOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtMapFooterOffset.Name = "txtMapFooterOffset";
            this.txtMapFooterOffset.Size = new System.Drawing.Size(80, 23);
            this.txtMapFooterOffset.TabIndex = 1;
            // 
            // lblEventScriptOffset
            // 
            this.lblEventScriptOffset.AutoSize = true;
            this.lblEventScriptOffset.Location = new System.Drawing.Point(20, 62);
            this.lblEventScriptOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblEventScriptOffset.Name = "lblEventScriptOffset";
            this.lblEventScriptOffset.Size = new System.Drawing.Size(119, 15);
            this.lblEventScriptOffset.TabIndex = 0;
            this.lblEventScriptOffset.Text = "イベントヘッダーアドレス :";
            // 
            // txtEventScriptOffset
            // 
            this.txtEventScriptOffset.Location = new System.Drawing.Point(156, 58);
            this.txtEventScriptOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtEventScriptOffset.Name = "txtEventScriptOffset";
            this.txtEventScriptOffset.Size = new System.Drawing.Size(80, 23);
            this.txtEventScriptOffset.TabIndex = 1;
            // 
            // lblLevelScriptOffset
            // 
            this.lblLevelScriptOffset.AutoSize = true;
            this.lblLevelScriptOffset.Location = new System.Drawing.Point(20, 92);
            this.lblLevelScriptOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblLevelScriptOffset.Name = "lblLevelScriptOffset";
            this.lblLevelScriptOffset.Size = new System.Drawing.Size(117, 15);
            this.lblLevelScriptOffset.TabIndex = 0;
            this.lblLevelScriptOffset.Text = "マップスクリプトアドレス :";
            // 
            // lblConnHeaderOffset
            // 
            this.lblConnHeaderOffset.AutoSize = true;
            this.lblConnHeaderOffset.Location = new System.Drawing.Point(20, 122);
            this.lblConnHeaderOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblConnHeaderOffset.Name = "lblConnHeaderOffset";
            this.lblConnHeaderOffset.Size = new System.Drawing.Size(107, 15);
            this.lblConnHeaderOffset.TabIndex = 0;
            this.lblConnHeaderOffset.Text = "接続ヘッダーアドレス :";
            // 
            // txtLevelScriptOffset
            // 
            this.txtLevelScriptOffset.Location = new System.Drawing.Point(156, 88);
            this.txtLevelScriptOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtLevelScriptOffset.Name = "txtLevelScriptOffset";
            this.txtLevelScriptOffset.Size = new System.Drawing.Size(80, 23);
            this.txtLevelScriptOffset.TabIndex = 1;
            // 
            // txtConnHeaderOffset
            // 
            this.txtConnHeaderOffset.Location = new System.Drawing.Point(156, 118);
            this.txtConnHeaderOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtConnHeaderOffset.Name = "txtConnHeaderOffset";
            this.txtConnHeaderOffset.Size = new System.Drawing.Size(80, 23);
            this.txtConnHeaderOffset.TabIndex = 1;
            // 
            // lblMapTerrainIndex
            // 
            this.lblMapTerrainIndex.AutoSize = true;
            this.lblMapTerrainIndex.Location = new System.Drawing.Point(20, 152);
            this.lblMapTerrainIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapTerrainIndex.Name = "lblMapTerrainIndex";
            this.lblMapTerrainIndex.Size = new System.Drawing.Size(74, 15);
            this.lblMapTerrainIndex.TabIndex = 0;
            this.lblMapTerrainIndex.Text = "マップ地形ID :";
            // 
            // nudMapTerrainIndex
            // 
            this.nudMapTerrainIndex.Location = new System.Drawing.Point(136, 148);
            this.nudMapTerrainIndex.Margin = new System.Windows.Forms.Padding(0);
            this.nudMapTerrainIndex.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudMapTerrainIndex.Name = "nudMapTerrainIndex";
            this.nudMapTerrainIndex.Size = new System.Drawing.Size(100, 23);
            this.nudMapTerrainIndex.TabIndex = 2;
            // 
            // lblMapType
            // 
            this.lblMapType.AutoSize = true;
            this.lblMapType.Location = new System.Drawing.Point(20, 182);
            this.lblMapType.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapType.Name = "lblMapType";
            this.lblMapType.Size = new System.Drawing.Size(66, 15);
            this.lblMapType.TabIndex = 0;
            this.lblMapType.Text = "マップタイプ :";
            // 
            // cmbMapType
            // 
            this.cmbMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapType.FormattingEnabled = true;
            this.cmbMapType.Location = new System.Drawing.Point(136, 178);
            this.cmbMapType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMapType.Name = "cmbMapType";
            this.cmbMapType.Size = new System.Drawing.Size(152, 23);
            this.cmbMapType.TabIndex = 3;
            // 
            // nudMapRelLayer
            // 
            this.nudMapRelLayer.Location = new System.Drawing.Point(136, 208);
            this.nudMapRelLayer.Margin = new System.Windows.Forms.Padding(0);
            this.nudMapRelLayer.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudMapRelLayer.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudMapRelLayer.Name = "nudMapRelLayer";
            this.nudMapRelLayer.Size = new System.Drawing.Size(100, 23);
            this.nudMapRelLayer.TabIndex = 5;
            // 
            // lblMapRelLayer
            // 
            this.lblMapRelLayer.AutoSize = true;
            this.lblMapRelLayer.Location = new System.Drawing.Point(20, 212);
            this.lblMapRelLayer.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapRelLayer.Name = "lblMapRelLayer";
            this.lblMapRelLayer.Size = new System.Drawing.Size(61, 15);
            this.lblMapRelLayer.TabIndex = 4;
            this.lblMapRelLayer.Text = "相対階層 :";
            // 
            // cmbMapWthr
            // 
            this.cmbMapWthr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapWthr.FormattingEnabled = true;
            this.cmbMapWthr.Location = new System.Drawing.Point(136, 238);
            this.cmbMapWthr.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMapWthr.Name = "cmbMapWthr";
            this.cmbMapWthr.Size = new System.Drawing.Size(152, 23);
            this.cmbMapWthr.TabIndex = 7;
            // 
            // lblMapWthr
            // 
            this.lblMapWthr.AutoSize = true;
            this.lblMapWthr.Location = new System.Drawing.Point(20, 242);
            this.lblMapWthr.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapWthr.Name = "lblMapWthr";
            this.lblMapWthr.Size = new System.Drawing.Size(37, 15);
            this.lblMapWthr.TabIndex = 6;
            this.lblMapWthr.Text = "天候 :";
            // 
            // cmbMapSight
            // 
            this.cmbMapSight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapSight.FormattingEnabled = true;
            this.cmbMapSight.Location = new System.Drawing.Point(136, 268);
            this.cmbMapSight.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMapSight.Name = "cmbMapSight";
            this.cmbMapSight.Size = new System.Drawing.Size(152, 23);
            this.cmbMapSight.TabIndex = 9;
            // 
            // lblMapSight
            // 
            this.lblMapSight.AutoSize = true;
            this.lblMapSight.Location = new System.Drawing.Point(20, 272);
            this.lblMapSight.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapSight.Name = "lblMapSight";
            this.lblMapSight.Size = new System.Drawing.Size(61, 15);
            this.lblMapSight.TabIndex = 8;
            this.lblMapSight.Text = "視界状況 :";
            // 
            // cmbSpBg
            // 
            this.cmbSpBg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpBg.FormattingEnabled = true;
            this.cmbSpBg.Location = new System.Drawing.Point(136, 328);
            this.cmbSpBg.Margin = new System.Windows.Forms.Padding(0);
            this.cmbSpBg.Name = "cmbSpBg";
            this.cmbSpBg.Size = new System.Drawing.Size(152, 23);
            this.cmbSpBg.TabIndex = 13;
            // 
            // lblSpBg
            // 
            this.lblSpBg.AutoSize = true;
            this.lblSpBg.Location = new System.Drawing.Point(20, 332);
            this.lblSpBg.Margin = new System.Windows.Forms.Padding(0);
            this.lblSpBg.Name = "lblSpBg";
            this.lblSpBg.Size = new System.Drawing.Size(93, 15);
            this.lblSpBg.TabIndex = 12;
            this.lblSpBg.Text = "戦闘背景(特殊) :";
            // 
            // cmbMapBike
            // 
            this.cmbMapBike.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapBike.FormattingEnabled = true;
            this.cmbMapBike.Location = new System.Drawing.Point(136, 298);
            this.cmbMapBike.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMapBike.Name = "cmbMapBike";
            this.cmbMapBike.Size = new System.Drawing.Size(152, 23);
            this.cmbMapBike.TabIndex = 11;
            // 
            // lblMapBike
            // 
            this.lblMapBike.AutoSize = true;
            this.lblMapBike.Location = new System.Drawing.Point(20, 302);
            this.lblMapBike.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapBike.Name = "lblMapBike";
            this.lblMapBike.Size = new System.Drawing.Size(73, 15);
            this.lblMapBike.TabIndex = 10;
            this.lblMapBike.Text = "自転車可否 :";
            // 
            // cmbMapNameType
            // 
            this.cmbMapNameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapNameType.FormattingEnabled = true;
            this.cmbMapNameType.Location = new System.Drawing.Point(136, 388);
            this.cmbMapNameType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMapNameType.Name = "cmbMapNameType";
            this.cmbMapNameType.Size = new System.Drawing.Size(152, 23);
            this.cmbMapNameType.TabIndex = 17;
            // 
            // lblMapNameType
            // 
            this.lblMapNameType.AutoSize = true;
            this.lblMapNameType.Location = new System.Drawing.Point(20, 392);
            this.lblMapNameType.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapNameType.Name = "lblMapNameType";
            this.lblMapNameType.Size = new System.Drawing.Size(99, 15);
            this.lblMapNameType.TabIndex = 16;
            this.lblMapNameType.Text = "マップ名表示設定 :";
            // 
            // cmbMapNameIndex
            // 
            this.cmbMapNameIndex.FormattingEnabled = true;
            this.cmbMapNameIndex.Location = new System.Drawing.Point(136, 358);
            this.cmbMapNameIndex.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMapNameIndex.Name = "cmbMapNameIndex";
            this.cmbMapNameIndex.Size = new System.Drawing.Size(152, 23);
            this.cmbMapNameIndex.TabIndex = 15;
            // 
            // lblMapNameIndex
            // 
            this.lblMapNameIndex.AutoSize = true;
            this.lblMapNameIndex.Location = new System.Drawing.Point(20, 362);
            this.lblMapNameIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapNameIndex.Name = "lblMapNameIndex";
            this.lblMapNameIndex.Size = new System.Drawing.Size(62, 15);
            this.lblMapNameIndex.TabIndex = 14;
            this.lblMapNameIndex.Text = "マップ名ID :";
            // 
            // nudBgmIndex
            // 
            this.nudBgmIndex.Location = new System.Drawing.Point(136, 418);
            this.nudBgmIndex.Margin = new System.Windows.Forms.Padding(0);
            this.nudBgmIndex.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudBgmIndex.Name = "nudBgmIndex";
            this.nudBgmIndex.Size = new System.Drawing.Size(100, 23);
            this.nudBgmIndex.TabIndex = 19;
            // 
            // lblBgmIndex
            // 
            this.lblBgmIndex.AutoSize = true;
            this.lblBgmIndex.Location = new System.Drawing.Point(20, 422);
            this.lblBgmIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lblBgmIndex.Name = "lblBgmIndex";
            this.lblBgmIndex.Size = new System.Drawing.Size(61, 15);
            this.lblBgmIndex.TabIndex = 18;
            this.lblBgmIndex.Text = "BGM No. :";
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 581);
            this.Controls.Add(this.grpMapHeader);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MapEditor";
            this.Text = "マップ";
            this.grpMapHeader.ResumeLayout(false);
            this.grpMapHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapTerrainIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapRelLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBgmIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMapHeader;
        private System.Windows.Forms.TextBox txtConnHeaderOffset;
        private System.Windows.Forms.TextBox txtEventScriptOffset;
        private System.Windows.Forms.TextBox txtLevelScriptOffset;
        private System.Windows.Forms.TextBox txtMapFooterOffset;
        private System.Windows.Forms.Label lblConnHeaderOffset;
        private System.Windows.Forms.Label lblEventScriptOffset;
        private System.Windows.Forms.Label lblLevelScriptOffset;
        private System.Windows.Forms.Label lblMapFooterOffset;
        private System.Windows.Forms.NumericUpDown nudMapRelLayer;
        private System.Windows.Forms.Label lblMapRelLayer;
        private System.Windows.Forms.ComboBox cmbMapType;
        private System.Windows.Forms.NumericUpDown nudMapTerrainIndex;
        private System.Windows.Forms.Label lblMapType;
        private System.Windows.Forms.Label lblMapTerrainIndex;
        private System.Windows.Forms.ComboBox cmbSpBg;
        private System.Windows.Forms.Label lblSpBg;
        private System.Windows.Forms.ComboBox cmbMapBike;
        private System.Windows.Forms.Label lblMapBike;
        private System.Windows.Forms.ComboBox cmbMapSight;
        private System.Windows.Forms.Label lblMapSight;
        private System.Windows.Forms.ComboBox cmbMapWthr;
        private System.Windows.Forms.Label lblMapWthr;
        private System.Windows.Forms.NumericUpDown nudBgmIndex;
        private System.Windows.Forms.Label lblBgmIndex;
        private System.Windows.Forms.ComboBox cmbMapNameType;
        private System.Windows.Forms.Label lblMapNameType;
        private System.Windows.Forms.ComboBox cmbMapNameIndex;
        private System.Windows.Forms.Label lblMapNameIndex;
    }
}