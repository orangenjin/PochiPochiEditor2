
namespace PochiPochiEditor2.Forms
{
    partial class TilesetEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetEditor));
            this.lblTilesetNo = new System.Windows.Forms.Label();
            this.nudTilesetNo = new System.Windows.Forms.NumericUpDown();
            this.btnLoadTileset = new System.Windows.Forms.Button();
            this.tncMain = new System.Windows.Forms.TabControl();
            this.tbpHeader = new System.Windows.Forms.TabPage();
            this.tbpAnim = new System.Windows.Forms.TabPage();
            this.lblImageCompType = new System.Windows.Forms.Label();
            this.cmbImageCompType = new System.Windows.Forms.ComboBox();
            this.lblPaletteType = new System.Windows.Forms.Label();
            this.cmbPaletteType = new System.Windows.Forms.ComboBox();
            this.lblImageOffset = new System.Windows.Forms.Label();
            this.lblPaletteOffset = new System.Windows.Forms.Label();
            this.txtImageOffset = new System.Windows.Forms.TextBox();
            this.txtPaletteOffset = new System.Windows.Forms.TextBox();
            this.lblBlockDataTableOffset = new System.Windows.Forms.Label();
            this.lblAnimHeaderOffset = new System.Windows.Forms.Label();
            this.txtBlockDataTableOffset = new System.Windows.Forms.TextBox();
            this.txtAnimHeaderOffset = new System.Windows.Forms.TextBox();
            this.lblBlockAttrTableOffset = new System.Windows.Forms.Label();
            this.txtBlockAttrTableOffset = new System.Windows.Forms.TextBox();
            this.grpTilesetView = new System.Windows.Forms.GroupBox();
            this.lblTileIndex = new System.Windows.Forms.Label();
            this.nudTileIndex = new System.Windows.Forms.NumericUpDown();
            this.txtTileIndex = new System.Windows.Forms.TextBox();
            this.lblSelectedTileCount = new System.Windows.Forms.Label();
            this.nudSelectedTileCount = new System.Windows.Forms.NumericUpDown();
            this.btnSelectTileMinus = new System.Windows.Forms.Button();
            this.btnSelectTilePlus = new System.Windows.Forms.Button();
            this.pnlTilesetImage = new System.Windows.Forms.Panel();
            this.cmbSelectedPalette = new System.Windows.Forms.ComboBox();
            this.lblSelectedPalette = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesetNo)).BeginInit();
            this.tncMain.SuspendLayout();
            this.tbpHeader.SuspendLayout();
            this.grpTilesetView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedTileCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTilesetNo
            // 
            this.lblTilesetNo.AutoSize = true;
            this.lblTilesetNo.Location = new System.Drawing.Point(20, 20);
            this.lblTilesetNo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTilesetNo.Name = "lblTilesetNo";
            this.lblTilesetNo.Size = new System.Drawing.Size(91, 15);
            this.lblTilesetNo.TabIndex = 0;
            this.lblTilesetNo.Text = "タイルセット番号 :";
            // 
            // nudTilesetNo
            // 
            this.nudTilesetNo.Location = new System.Drawing.Point(120, 16);
            this.nudTilesetNo.Margin = new System.Windows.Forms.Padding(0);
            this.nudTilesetNo.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudTilesetNo.Name = "nudTilesetNo";
            this.nudTilesetNo.Size = new System.Drawing.Size(120, 23);
            this.nudTilesetNo.TabIndex = 1;
            // 
            // btnLoadTileset
            // 
            this.btnLoadTileset.Location = new System.Drawing.Point(252, 16);
            this.btnLoadTileset.Margin = new System.Windows.Forms.Padding(0);
            this.btnLoadTileset.Name = "btnLoadTileset";
            this.btnLoadTileset.Size = new System.Drawing.Size(96, 23);
            this.btnLoadTileset.TabIndex = 2;
            this.btnLoadTileset.Text = "読み込み";
            this.btnLoadTileset.UseVisualStyleBackColor = true;
            // 
            // tncMain
            // 
            this.tncMain.Controls.Add(this.tbpHeader);
            this.tncMain.Controls.Add(this.tbpAnim);
            this.tncMain.Location = new System.Drawing.Point(20, 52);
            this.tncMain.Margin = new System.Windows.Forms.Padding(0);
            this.tncMain.Name = "tncMain";
            this.tncMain.SelectedIndex = 0;
            this.tncMain.Size = new System.Drawing.Size(618, 490);
            this.tncMain.TabIndex = 3;
            // 
            // tbpHeader
            // 
            this.tbpHeader.Controls.Add(this.grpTilesetView);
            this.tbpHeader.Controls.Add(this.txtBlockAttrTableOffset);
            this.tbpHeader.Controls.Add(this.txtAnimHeaderOffset);
            this.tbpHeader.Controls.Add(this.txtPaletteOffset);
            this.tbpHeader.Controls.Add(this.txtBlockDataTableOffset);
            this.tbpHeader.Controls.Add(this.txtImageOffset);
            this.tbpHeader.Controls.Add(this.cmbPaletteType);
            this.tbpHeader.Controls.Add(this.cmbImageCompType);
            this.tbpHeader.Controls.Add(this.lblBlockAttrTableOffset);
            this.tbpHeader.Controls.Add(this.lblAnimHeaderOffset);
            this.tbpHeader.Controls.Add(this.lblPaletteOffset);
            this.tbpHeader.Controls.Add(this.lblPaletteType);
            this.tbpHeader.Controls.Add(this.lblBlockDataTableOffset);
            this.tbpHeader.Controls.Add(this.lblImageOffset);
            this.tbpHeader.Controls.Add(this.lblImageCompType);
            this.tbpHeader.Location = new System.Drawing.Point(4, 24);
            this.tbpHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tbpHeader.Name = "tbpHeader";
            this.tbpHeader.Size = new System.Drawing.Size(610, 462);
            this.tbpHeader.TabIndex = 0;
            this.tbpHeader.Text = "ヘッダー";
            this.tbpHeader.UseVisualStyleBackColor = true;
            // 
            // tbpAnim
            // 
            this.tbpAnim.Location = new System.Drawing.Point(4, 24);
            this.tbpAnim.Margin = new System.Windows.Forms.Padding(0);
            this.tbpAnim.Name = "tbpAnim";
            this.tbpAnim.Size = new System.Drawing.Size(756, 397);
            this.tbpAnim.TabIndex = 1;
            this.tbpAnim.Text = "タイルアニメ";
            this.tbpAnim.UseVisualStyleBackColor = true;
            // 
            // lblImageCompType
            // 
            this.lblImageCompType.AutoSize = true;
            this.lblImageCompType.Location = new System.Drawing.Point(20, 24);
            this.lblImageCompType.Margin = new System.Windows.Forms.Padding(0);
            this.lblImageCompType.Name = "lblImageCompType";
            this.lblImageCompType.Size = new System.Drawing.Size(85, 15);
            this.lblImageCompType.TabIndex = 0;
            this.lblImageCompType.Text = "画像圧縮設定 :";
            // 
            // cmbImageCompType
            // 
            this.cmbImageCompType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageCompType.FormattingEnabled = true;
            this.cmbImageCompType.Location = new System.Drawing.Point(148, 20);
            this.cmbImageCompType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbImageCompType.Name = "cmbImageCompType";
            this.cmbImageCompType.Size = new System.Drawing.Size(120, 23);
            this.cmbImageCompType.TabIndex = 1;
            // 
            // lblPaletteType
            // 
            this.lblPaletteType.AutoSize = true;
            this.lblPaletteType.Location = new System.Drawing.Point(20, 54);
            this.lblPaletteType.Margin = new System.Windows.Forms.Padding(0);
            this.lblPaletteType.Name = "lblPaletteType";
            this.lblPaletteType.Size = new System.Drawing.Size(118, 15);
            this.lblPaletteType.TabIndex = 0;
            this.lblPaletteType.Text = "パレット読み込み設定 :";
            // 
            // cmbPaletteType
            // 
            this.cmbPaletteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaletteType.FormattingEnabled = true;
            this.cmbPaletteType.Location = new System.Drawing.Point(148, 50);
            this.cmbPaletteType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPaletteType.Name = "cmbPaletteType";
            this.cmbPaletteType.Size = new System.Drawing.Size(120, 23);
            this.cmbPaletteType.TabIndex = 1;
            // 
            // lblImageOffset
            // 
            this.lblImageOffset.AutoSize = true;
            this.lblImageOffset.Location = new System.Drawing.Point(20, 84);
            this.lblImageOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblImageOffset.Name = "lblImageOffset";
            this.lblImageOffset.Size = new System.Drawing.Size(72, 15);
            this.lblImageOffset.TabIndex = 0;
            this.lblImageOffset.Text = "画像アドレス :";
            // 
            // lblPaletteOffset
            // 
            this.lblPaletteOffset.AutoSize = true;
            this.lblPaletteOffset.Location = new System.Drawing.Point(20, 114);
            this.lblPaletteOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblPaletteOffset.Name = "lblPaletteOffset";
            this.lblPaletteOffset.Size = new System.Drawing.Size(83, 15);
            this.lblPaletteOffset.TabIndex = 0;
            this.lblPaletteOffset.Text = "パレットアドレス :";
            // 
            // txtImageOffset
            // 
            this.txtImageOffset.Location = new System.Drawing.Point(148, 80);
            this.txtImageOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtImageOffset.Name = "txtImageOffset";
            this.txtImageOffset.Size = new System.Drawing.Size(120, 23);
            this.txtImageOffset.TabIndex = 2;
            // 
            // txtPaletteOffset
            // 
            this.txtPaletteOffset.Location = new System.Drawing.Point(148, 110);
            this.txtPaletteOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtPaletteOffset.Name = "txtPaletteOffset";
            this.txtPaletteOffset.Size = new System.Drawing.Size(120, 23);
            this.txtPaletteOffset.TabIndex = 2;
            // 
            // lblBlockDataTableOffset
            // 
            this.lblBlockDataTableOffset.AutoSize = true;
            this.lblBlockDataTableOffset.Location = new System.Drawing.Point(20, 144);
            this.lblBlockDataTableOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblBlockDataTableOffset.Name = "lblBlockDataTableOffset";
            this.lblBlockDataTableOffset.Size = new System.Drawing.Size(110, 15);
            this.lblBlockDataTableOffset.TabIndex = 0;
            this.lblBlockDataTableOffset.Text = "ブロックデータテーブル :";
            // 
            // lblAnimHeaderOffset
            // 
            this.lblAnimHeaderOffset.AutoSize = true;
            this.lblAnimHeaderOffset.Location = new System.Drawing.Point(20, 174);
            this.lblAnimHeaderOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblAnimHeaderOffset.Name = "lblAnimHeaderOffset";
            this.lblAnimHeaderOffset.Size = new System.Drawing.Size(109, 15);
            this.lblAnimHeaderOffset.TabIndex = 0;
            this.lblAnimHeaderOffset.Text = "アニメヘッダーアドレス :";
            // 
            // txtBlockDataTableOffset
            // 
            this.txtBlockDataTableOffset.Location = new System.Drawing.Point(148, 140);
            this.txtBlockDataTableOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtBlockDataTableOffset.Name = "txtBlockDataTableOffset";
            this.txtBlockDataTableOffset.Size = new System.Drawing.Size(120, 23);
            this.txtBlockDataTableOffset.TabIndex = 2;
            // 
            // txtAnimHeaderOffset
            // 
            this.txtAnimHeaderOffset.Location = new System.Drawing.Point(148, 170);
            this.txtAnimHeaderOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtAnimHeaderOffset.Name = "txtAnimHeaderOffset";
            this.txtAnimHeaderOffset.Size = new System.Drawing.Size(120, 23);
            this.txtAnimHeaderOffset.TabIndex = 2;
            // 
            // lblBlockAttrTableOffset
            // 
            this.lblBlockAttrTableOffset.AutoSize = true;
            this.lblBlockAttrTableOffset.Location = new System.Drawing.Point(20, 204);
            this.lblBlockAttrTableOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblBlockAttrTableOffset.Name = "lblBlockAttrTableOffset";
            this.lblBlockAttrTableOffset.Size = new System.Drawing.Size(108, 15);
            this.lblBlockAttrTableOffset.TabIndex = 0;
            this.lblBlockAttrTableOffset.Text = "ブロック属性テーブル :";
            // 
            // txtBlockAttrTableOffset
            // 
            this.txtBlockAttrTableOffset.Location = new System.Drawing.Point(148, 200);
            this.txtBlockAttrTableOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtBlockAttrTableOffset.Name = "txtBlockAttrTableOffset";
            this.txtBlockAttrTableOffset.Size = new System.Drawing.Size(120, 23);
            this.txtBlockAttrTableOffset.TabIndex = 2;
            // 
            // grpTilesetView
            // 
            this.grpTilesetView.Controls.Add(this.cmbSelectedPalette);
            this.grpTilesetView.Controls.Add(this.lblSelectedPalette);
            this.grpTilesetView.Controls.Add(this.pnlTilesetImage);
            this.grpTilesetView.Controls.Add(this.btnSelectTilePlus);
            this.grpTilesetView.Controls.Add(this.btnSelectTileMinus);
            this.grpTilesetView.Controls.Add(this.txtTileIndex);
            this.grpTilesetView.Controls.Add(this.nudSelectedTileCount);
            this.grpTilesetView.Controls.Add(this.nudTileIndex);
            this.grpTilesetView.Controls.Add(this.lblSelectedTileCount);
            this.grpTilesetView.Controls.Add(this.lblTileIndex);
            this.grpTilesetView.Location = new System.Drawing.Point(288, 16);
            this.grpTilesetView.Margin = new System.Windows.Forms.Padding(0);
            this.grpTilesetView.Name = "grpTilesetView";
            this.grpTilesetView.Padding = new System.Windows.Forms.Padding(0);
            this.grpTilesetView.Size = new System.Drawing.Size(298, 425);
            this.grpTilesetView.TabIndex = 3;
            this.grpTilesetView.TabStop = false;
            this.grpTilesetView.Text = "閲覧用";
            // 
            // lblTileIndex
            // 
            this.lblTileIndex.AutoSize = true;
            this.lblTileIndex.Location = new System.Drawing.Point(20, 32);
            this.lblTileIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lblTileIndex.Name = "lblTileIndex";
            this.lblTileIndex.Size = new System.Drawing.Size(65, 15);
            this.lblTileIndex.TabIndex = 0;
            this.lblTileIndex.Text = "タイル番号 :";
            // 
            // nudTileIndex
            // 
            this.nudTileIndex.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTileIndex.Location = new System.Drawing.Point(106, 28);
            this.nudTileIndex.Margin = new System.Windows.Forms.Padding(0);
            this.nudTileIndex.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.nudTileIndex.Name = "nudTileIndex";
            this.nudTileIndex.ReadOnly = true;
            this.nudTileIndex.Size = new System.Drawing.Size(72, 23);
            this.nudTileIndex.TabIndex = 1;
            // 
            // txtTileIndex
            // 
            this.txtTileIndex.Location = new System.Drawing.Point(188, 28);
            this.txtTileIndex.Margin = new System.Windows.Forms.Padding(0);
            this.txtTileIndex.Name = "txtTileIndex";
            this.txtTileIndex.ReadOnly = true;
            this.txtTileIndex.Size = new System.Drawing.Size(72, 23);
            this.txtTileIndex.TabIndex = 2;
            // 
            // lblSelectedTileCount
            // 
            this.lblSelectedTileCount.AutoSize = true;
            this.lblSelectedTileCount.Location = new System.Drawing.Point(20, 62);
            this.lblSelectedTileCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblSelectedTileCount.Name = "lblSelectedTileCount";
            this.lblSelectedTileCount.Size = new System.Drawing.Size(77, 15);
            this.lblSelectedTileCount.TabIndex = 0;
            this.lblSelectedTileCount.Text = "選択タイル数 :";
            // 
            // nudSelectedTileCount
            // 
            this.nudSelectedTileCount.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSelectedTileCount.Location = new System.Drawing.Point(106, 58);
            this.nudSelectedTileCount.Margin = new System.Windows.Forms.Padding(0);
            this.nudSelectedTileCount.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudSelectedTileCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSelectedTileCount.Name = "nudSelectedTileCount";
            this.nudSelectedTileCount.ReadOnly = true;
            this.nudSelectedTileCount.Size = new System.Drawing.Size(72, 23);
            this.nudSelectedTileCount.TabIndex = 1;
            this.nudSelectedTileCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSelectTileMinus
            // 
            this.btnSelectTileMinus.Location = new System.Drawing.Point(188, 58);
            this.btnSelectTileMinus.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectTileMinus.Name = "btnSelectTileMinus";
            this.btnSelectTileMinus.Size = new System.Drawing.Size(34, 23);
            this.btnSelectTileMinus.TabIndex = 3;
            this.btnSelectTileMinus.Text = "-";
            this.btnSelectTileMinus.UseVisualStyleBackColor = true;
            // 
            // btnSelectTilePlus
            // 
            this.btnSelectTilePlus.Location = new System.Drawing.Point(226, 58);
            this.btnSelectTilePlus.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectTilePlus.Name = "btnSelectTilePlus";
            this.btnSelectTilePlus.Size = new System.Drawing.Size(34, 23);
            this.btnSelectTilePlus.TabIndex = 3;
            this.btnSelectTilePlus.Text = "+";
            this.btnSelectTilePlus.UseVisualStyleBackColor = true;
            // 
            // pnlTilesetImage
            // 
            this.pnlTilesetImage.Location = new System.Drawing.Point(20, 96);
            this.pnlTilesetImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTilesetImage.Name = "pnlTilesetImage";
            this.pnlTilesetImage.Size = new System.Drawing.Size(256, 276);
            this.pnlTilesetImage.TabIndex = 4;
            // 
            // cmbSelectedPalette
            // 
            this.cmbSelectedPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectedPalette.FormattingEnabled = true;
            this.cmbSelectedPalette.Location = new System.Drawing.Point(106, 384);
            this.cmbSelectedPalette.Margin = new System.Windows.Forms.Padding(0);
            this.cmbSelectedPalette.Name = "cmbSelectedPalette";
            this.cmbSelectedPalette.Size = new System.Drawing.Size(96, 23);
            this.cmbSelectedPalette.TabIndex = 6;
            // 
            // lblSelectedPalette
            // 
            this.lblSelectedPalette.AutoSize = true;
            this.lblSelectedPalette.Location = new System.Drawing.Point(20, 388);
            this.lblSelectedPalette.Margin = new System.Windows.Forms.Padding(0);
            this.lblSelectedPalette.Name = "lblSelectedPalette";
            this.lblSelectedPalette.Size = new System.Drawing.Size(72, 15);
            this.lblSelectedPalette.TabIndex = 5;
            this.lblSelectedPalette.Text = "選択パレット :";
            // 
            // TilesetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 557);
            this.Controls.Add(this.tncMain);
            this.Controls.Add(this.btnLoadTileset);
            this.Controls.Add(this.nudTilesetNo);
            this.Controls.Add(this.lblTilesetNo);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TilesetEditor";
            this.Text = "タイルセット";
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesetNo)).EndInit();
            this.tncMain.ResumeLayout(false);
            this.tbpHeader.ResumeLayout(false);
            this.tbpHeader.PerformLayout();
            this.grpTilesetView.ResumeLayout(false);
            this.grpTilesetView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedTileCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTilesetNo;
        private System.Windows.Forms.NumericUpDown nudTilesetNo;
        private System.Windows.Forms.Button btnLoadTileset;
        private System.Windows.Forms.TabControl tncMain;
        private System.Windows.Forms.TabPage tbpHeader;
        private System.Windows.Forms.TabPage tbpAnim;
        private System.Windows.Forms.Label lblImageCompType;
        private System.Windows.Forms.ComboBox cmbImageCompType;
        private System.Windows.Forms.ComboBox cmbPaletteType;
        private System.Windows.Forms.Label lblPaletteType;
        private System.Windows.Forms.TextBox txtImageOffset;
        private System.Windows.Forms.Label lblPaletteOffset;
        private System.Windows.Forms.Label lblImageOffset;
        private System.Windows.Forms.TextBox txtPaletteOffset;
        private System.Windows.Forms.TextBox txtBlockAttrTableOffset;
        private System.Windows.Forms.TextBox txtAnimHeaderOffset;
        private System.Windows.Forms.TextBox txtBlockDataTableOffset;
        private System.Windows.Forms.Label lblBlockAttrTableOffset;
        private System.Windows.Forms.Label lblAnimHeaderOffset;
        private System.Windows.Forms.Label lblBlockDataTableOffset;
        private System.Windows.Forms.GroupBox grpTilesetView;
        private System.Windows.Forms.TextBox txtTileIndex;
        private System.Windows.Forms.NumericUpDown nudTileIndex;
        private System.Windows.Forms.Label lblTileIndex;
        private System.Windows.Forms.Button btnSelectTilePlus;
        private System.Windows.Forms.Button btnSelectTileMinus;
        private System.Windows.Forms.NumericUpDown nudSelectedTileCount;
        private System.Windows.Forms.Label lblSelectedTileCount;
        private System.Windows.Forms.ComboBox cmbSelectedPalette;
        private System.Windows.Forms.Label lblSelectedPalette;
        private System.Windows.Forms.Panel pnlTilesetImage;
    }
}