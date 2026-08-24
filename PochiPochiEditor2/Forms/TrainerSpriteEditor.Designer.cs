
namespace PochiPochiEditor2.Forms
{
    partial class TrainerSpriteEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainerSpriteEditor));
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.nudSpriteIndex = new System.Windows.Forms.NumericUpDown();
            this.btnSpriteIndexPrev = new System.Windows.Forms.Button();
            this.btnSpriteIndexNext = new System.Windows.Forms.Button();
            this.btnSpriteExport = new System.Windows.Forms.Button();
            this.lblImageOffset = new System.Windows.Forms.Label();
            this.txtImageOffset = new System.Windows.Forms.TextBox();
            this.lblPaletteOffset = new System.Windows.Forms.Label();
            this.txtPaletteOffset = new System.Windows.Forms.TextBox();
            this.lblYPosValue = new System.Windows.Forms.Label();
            this.nudYPosValue = new System.Windows.Forms.NumericUpDown();
            this.btnImportImage = new System.Windows.Forms.Button();
            this.btnImportPalette = new System.Windows.Forms.Button();
            this.grpAnim = new System.Windows.Forms.GroupBox();
            this.txtAnimDataOffset = new System.Windows.Forms.TextBox();
            this.txtAnimPointerOffset = new System.Windows.Forms.TextBox();
            this.lblAnimDataOffset = new System.Windows.Forms.Label();
            this.lblAnimPointerOffset = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpriteIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYPosValue)).BeginInit();
            this.grpAnim.SuspendLayout();
            this.SuspendLayout();
            // 
            // picSprite
            // 
            this.picSprite.Location = new System.Drawing.Point(20, 20);
            this.picSprite.Margin = new System.Windows.Forms.Padding(0);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(128, 128);
            this.picSprite.TabIndex = 0;
            this.picSprite.TabStop = false;
            // 
            // nudSpriteIndex
            // 
            this.nudSpriteIndex.Location = new System.Drawing.Point(56, 156);
            this.nudSpriteIndex.Margin = new System.Windows.Forms.Padding(0);
            this.nudSpriteIndex.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSpriteIndex.Name = "nudSpriteIndex";
            this.nudSpriteIndex.Size = new System.Drawing.Size(56, 23);
            this.nudSpriteIndex.TabIndex = 1;
            // 
            // btnSpriteIndexPrev
            // 
            this.btnSpriteIndexPrev.Location = new System.Drawing.Point(20, 156);
            this.btnSpriteIndexPrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnSpriteIndexPrev.Name = "btnSpriteIndexPrev";
            this.btnSpriteIndexPrev.Size = new System.Drawing.Size(30, 23);
            this.btnSpriteIndexPrev.TabIndex = 2;
            this.btnSpriteIndexPrev.Text = "<";
            this.btnSpriteIndexPrev.UseVisualStyleBackColor = true;
            // 
            // btnSpriteIndexNext
            // 
            this.btnSpriteIndexNext.Location = new System.Drawing.Point(118, 156);
            this.btnSpriteIndexNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnSpriteIndexNext.Name = "btnSpriteIndexNext";
            this.btnSpriteIndexNext.Size = new System.Drawing.Size(30, 23);
            this.btnSpriteIndexNext.TabIndex = 2;
            this.btnSpriteIndexNext.Text = ">";
            this.btnSpriteIndexNext.UseVisualStyleBackColor = true;
            // 
            // btnSpriteExport
            // 
            this.btnSpriteExport.Location = new System.Drawing.Point(20, 186);
            this.btnSpriteExport.Margin = new System.Windows.Forms.Padding(0);
            this.btnSpriteExport.Name = "btnSpriteExport";
            this.btnSpriteExport.Size = new System.Drawing.Size(128, 23);
            this.btnSpriteExport.TabIndex = 3;
            this.btnSpriteExport.Text = "画像をエクスポート";
            this.btnSpriteExport.UseVisualStyleBackColor = true;
            // 
            // lblImageOffset
            // 
            this.lblImageOffset.AutoSize = true;
            this.lblImageOffset.Location = new System.Drawing.Point(168, 24);
            this.lblImageOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblImageOffset.Name = "lblImageOffset";
            this.lblImageOffset.Size = new System.Drawing.Size(72, 15);
            this.lblImageOffset.TabIndex = 4;
            this.lblImageOffset.Text = "画像アドレス :";
            // 
            // txtImageOffset
            // 
            this.txtImageOffset.Location = new System.Drawing.Point(256, 20);
            this.txtImageOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtImageOffset.Name = "txtImageOffset";
            this.txtImageOffset.Size = new System.Drawing.Size(80, 23);
            this.txtImageOffset.TabIndex = 5;
            // 
            // lblPaletteOffset
            // 
            this.lblPaletteOffset.AutoSize = true;
            this.lblPaletteOffset.Location = new System.Drawing.Point(168, 54);
            this.lblPaletteOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblPaletteOffset.Name = "lblPaletteOffset";
            this.lblPaletteOffset.Size = new System.Drawing.Size(83, 15);
            this.lblPaletteOffset.TabIndex = 4;
            this.lblPaletteOffset.Text = "パレットアドレス :";
            // 
            // txtPaletteOffset
            // 
            this.txtPaletteOffset.Location = new System.Drawing.Point(256, 50);
            this.txtPaletteOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtPaletteOffset.Name = "txtPaletteOffset";
            this.txtPaletteOffset.Size = new System.Drawing.Size(80, 23);
            this.txtPaletteOffset.TabIndex = 5;
            // 
            // lblYPosValue
            // 
            this.lblYPosValue.AutoSize = true;
            this.lblYPosValue.Location = new System.Drawing.Point(168, 84);
            this.lblYPosValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblYPosValue.Name = "lblYPosValue";
            this.lblYPosValue.Size = new System.Drawing.Size(68, 15);
            this.lblYPosValue.TabIndex = 4;
            this.lblYPosValue.Text = "Y座標位置 :";
            // 
            // nudYPosValue
            // 
            this.nudYPosValue.Location = new System.Drawing.Point(256, 80);
            this.nudYPosValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudYPosValue.Name = "nudYPosValue";
            this.nudYPosValue.Size = new System.Drawing.Size(80, 23);
            this.nudYPosValue.TabIndex = 6;
            // 
            // btnImportImage
            // 
            this.btnImportImage.Location = new System.Drawing.Point(348, 20);
            this.btnImportImage.Margin = new System.Windows.Forms.Padding(0);
            this.btnImportImage.Name = "btnImportImage";
            this.btnImportImage.Size = new System.Drawing.Size(128, 23);
            this.btnImportImage.TabIndex = 7;
            this.btnImportImage.Text = "画像をインポート";
            this.btnImportImage.UseVisualStyleBackColor = true;
            // 
            // btnImportPalette
            // 
            this.btnImportPalette.Location = new System.Drawing.Point(348, 50);
            this.btnImportPalette.Margin = new System.Windows.Forms.Padding(0);
            this.btnImportPalette.Name = "btnImportPalette";
            this.btnImportPalette.Size = new System.Drawing.Size(128, 23);
            this.btnImportPalette.TabIndex = 7;
            this.btnImportPalette.Text = "パレットをインポート";
            this.btnImportPalette.UseVisualStyleBackColor = true;
            // 
            // grpAnim
            // 
            this.grpAnim.Controls.Add(this.txtAnimDataOffset);
            this.grpAnim.Controls.Add(this.txtAnimPointerOffset);
            this.grpAnim.Controls.Add(this.lblAnimDataOffset);
            this.grpAnim.Controls.Add(this.lblAnimPointerOffset);
            this.grpAnim.Location = new System.Drawing.Point(168, 112);
            this.grpAnim.Margin = new System.Windows.Forms.Padding(0);
            this.grpAnim.Name = "grpAnim";
            this.grpAnim.Padding = new System.Windows.Forms.Padding(0);
            this.grpAnim.Size = new System.Drawing.Size(218, 98);
            this.grpAnim.TabIndex = 8;
            this.grpAnim.TabStop = false;
            this.grpAnim.Text = "アニメーション?";
            // 
            // txtAnimDataOffset
            // 
            this.txtAnimDataOffset.Location = new System.Drawing.Point(112, 56);
            this.txtAnimDataOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtAnimDataOffset.Name = "txtAnimDataOffset";
            this.txtAnimDataOffset.ReadOnly = true;
            this.txtAnimDataOffset.Size = new System.Drawing.Size(80, 23);
            this.txtAnimDataOffset.TabIndex = 8;
            // 
            // txtAnimPointerOffset
            // 
            this.txtAnimPointerOffset.Location = new System.Drawing.Point(112, 26);
            this.txtAnimPointerOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtAnimPointerOffset.Name = "txtAnimPointerOffset";
            this.txtAnimPointerOffset.ReadOnly = true;
            this.txtAnimPointerOffset.Size = new System.Drawing.Size(80, 23);
            this.txtAnimPointerOffset.TabIndex = 9;
            // 
            // lblAnimDataOffset
            // 
            this.lblAnimDataOffset.AutoSize = true;
            this.lblAnimDataOffset.Location = new System.Drawing.Point(20, 60);
            this.lblAnimDataOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblAnimDataOffset.Name = "lblAnimDataOffset";
            this.lblAnimDataOffset.Size = new System.Drawing.Size(74, 15);
            this.lblAnimDataOffset.TabIndex = 6;
            this.lblAnimDataOffset.Text = "データアドレス :";
            // 
            // lblAnimPointerOffset
            // 
            this.lblAnimPointerOffset.AutoSize = true;
            this.lblAnimPointerOffset.Location = new System.Drawing.Point(20, 30);
            this.lblAnimPointerOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblAnimPointerOffset.Name = "lblAnimPointerOffset";
            this.lblAnimPointerOffset.Size = new System.Drawing.Size(85, 15);
            this.lblAnimPointerOffset.TabIndex = 7;
            this.lblAnimPointerOffset.Text = "ポインタアドレス :";
            // 
            // TrainerSpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 229);
            this.Controls.Add(this.grpAnim);
            this.Controls.Add(this.btnImportPalette);
            this.Controls.Add(this.btnImportImage);
            this.Controls.Add(this.nudYPosValue);
            this.Controls.Add(this.txtPaletteOffset);
            this.Controls.Add(this.txtImageOffset);
            this.Controls.Add(this.lblYPosValue);
            this.Controls.Add(this.lblPaletteOffset);
            this.Controls.Add(this.lblImageOffset);
            this.Controls.Add(this.btnSpriteExport);
            this.Controls.Add(this.btnSpriteIndexNext);
            this.Controls.Add(this.btnSpriteIndexPrev);
            this.Controls.Add(this.nudSpriteIndex);
            this.Controls.Add(this.picSprite);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TrainerSpriteEditor";
            this.Text = "トレーナー画像";
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpriteIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYPosValue)).EndInit();
            this.grpAnim.ResumeLayout(false);
            this.grpAnim.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picSprite;
        private System.Windows.Forms.NumericUpDown nudSpriteIndex;
        private System.Windows.Forms.Button btnSpriteIndexPrev;
        private System.Windows.Forms.Button btnSpriteIndexNext;
        private System.Windows.Forms.Button btnSpriteExport;
        private System.Windows.Forms.Label lblImageOffset;
        private System.Windows.Forms.TextBox txtImageOffset;
        private System.Windows.Forms.Label lblPaletteOffset;
        private System.Windows.Forms.TextBox txtPaletteOffset;
        private System.Windows.Forms.Label lblYPosValue;
        private System.Windows.Forms.NumericUpDown nudYPosValue;
        private System.Windows.Forms.Button btnImportImage;
        private System.Windows.Forms.Button btnImportPalette;
        private System.Windows.Forms.GroupBox grpAnim;
        private System.Windows.Forms.TextBox txtAnimDataOffset;
        private System.Windows.Forms.TextBox txtAnimPointerOffset;
        private System.Windows.Forms.Label lblAnimDataOffset;
        private System.Windows.Forms.Label lblAnimPointerOffset;
    }
}