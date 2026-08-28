
namespace PochiPochiEditor2.Forms.AssistantTools
{
    partial class TilesetCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetCalc));
            this.lblTilesetNo = new System.Windows.Forms.Label();
            this.nudTilesetNo = new System.Windows.Forms.NumericUpDown();
            this.btnToOffset = new System.Windows.Forms.Button();
            this.btnToNo = new System.Windows.Forms.Button();
            this.lblHeaderOffset = new System.Windows.Forms.Label();
            this.txtHeaderOffset = new System.Windows.Forms.TextBox();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblRecNoAndOffset = new System.Windows.Forms.Label();
            this.nudRecNo = new System.Windows.Forms.NumericUpDown();
            this.txtRecOffset = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesetNo)).BeginInit();
            this.grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecNo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTilesetNo
            // 
            this.lblTilesetNo.AutoSize = true;
            this.lblTilesetNo.Location = new System.Drawing.Point(20, 24);
            this.lblTilesetNo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTilesetNo.Name = "lblTilesetNo";
            this.lblTilesetNo.Size = new System.Drawing.Size(89, 15);
            this.lblTilesetNo.TabIndex = 0;
            this.lblTilesetNo.Text = "タイルセット No. :";
            // 
            // nudTilesetNo
            // 
            this.nudTilesetNo.Location = new System.Drawing.Point(120, 20);
            this.nudTilesetNo.Margin = new System.Windows.Forms.Padding(0);
            this.nudTilesetNo.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudTilesetNo.Name = "nudTilesetNo";
            this.nudTilesetNo.Size = new System.Drawing.Size(104, 23);
            this.nudTilesetNo.TabIndex = 1;
            // 
            // btnToOffset
            // 
            this.btnToOffset.Location = new System.Drawing.Point(20, 50);
            this.btnToOffset.Margin = new System.Windows.Forms.Padding(0);
            this.btnToOffset.Name = "btnToOffset";
            this.btnToOffset.Size = new System.Drawing.Size(96, 31);
            this.btnToOffset.TabIndex = 2;
            this.btnToOffset.Text = "▼アドレス変換";
            this.btnToOffset.UseVisualStyleBackColor = true;
            // 
            // btnToNo
            // 
            this.btnToNo.Location = new System.Drawing.Point(128, 50);
            this.btnToNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnToNo.Name = "btnToNo";
            this.btnToNo.Size = new System.Drawing.Size(96, 31);
            this.btnToNo.TabIndex = 2;
            this.btnToNo.Text = "▲番号変換";
            this.btnToNo.UseVisualStyleBackColor = true;
            // 
            // lblHeaderOffset
            // 
            this.lblHeaderOffset.AutoSize = true;
            this.lblHeaderOffset.Location = new System.Drawing.Point(20, 96);
            this.lblHeaderOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblHeaderOffset.Name = "lblHeaderOffset";
            this.lblHeaderOffset.Size = new System.Drawing.Size(83, 15);
            this.lblHeaderOffset.TabIndex = 0;
            this.lblHeaderOffset.Text = "ヘッダーアドレス :";
            // 
            // txtHeaderOffset
            // 
            this.txtHeaderOffset.Location = new System.Drawing.Point(120, 92);
            this.txtHeaderOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtHeaderOffset.Name = "txtHeaderOffset";
            this.txtHeaderOffset.Size = new System.Drawing.Size(104, 23);
            this.txtHeaderOffset.TabIndex = 3;
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.lblRecNoAndOffset);
            this.grpResult.Controls.Add(this.txtRecOffset);
            this.grpResult.Controls.Add(this.lblResult);
            this.grpResult.Controls.Add(this.nudRecNo);
            this.grpResult.Location = new System.Drawing.Point(240, 16);
            this.grpResult.Margin = new System.Windows.Forms.Padding(0);
            this.grpResult.Name = "grpResult";
            this.grpResult.Padding = new System.Windows.Forms.Padding(0);
            this.grpResult.Size = new System.Drawing.Size(144, 158);
            this.grpResult.TabIndex = 4;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "変換結果";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblResult.Location = new System.Drawing.Point(20, 28);
            this.lblResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(39, 20);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "結果";
            // 
            // lblRecNoAndOffset
            // 
            this.lblRecNoAndOffset.AutoSize = true;
            this.lblRecNoAndOffset.Location = new System.Drawing.Point(20, 60);
            this.lblRecNoAndOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblRecNoAndOffset.Name = "lblRecNoAndOffset";
            this.lblRecNoAndOffset.Size = new System.Drawing.Size(102, 15);
            this.lblRecNoAndOffset.TabIndex = 1;
            this.lblRecNoAndOffset.Text = "代替の組み合わせ :";
            // 
            // nudRecNo
            // 
            this.nudRecNo.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRecNo.Location = new System.Drawing.Point(20, 84);
            this.nudRecNo.Margin = new System.Windows.Forms.Padding(0);
            this.nudRecNo.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudRecNo.Name = "nudRecNo";
            this.nudRecNo.ReadOnly = true;
            this.nudRecNo.Size = new System.Drawing.Size(104, 23);
            this.nudRecNo.TabIndex = 1;
            // 
            // txtRecOffset
            // 
            this.txtRecOffset.Location = new System.Drawing.Point(20, 114);
            this.txtRecOffset.Margin = new System.Windows.Forms.Padding(0);
            this.txtRecOffset.Name = "txtRecOffset";
            this.txtRecOffset.ReadOnly = true;
            this.txtRecOffset.Size = new System.Drawing.Size(104, 23);
            this.txtRecOffset.TabIndex = 3;
            // 
            // TilesetCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 195);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.txtHeaderOffset);
            this.Controls.Add(this.btnToNo);
            this.Controls.Add(this.btnToOffset);
            this.Controls.Add(this.nudTilesetNo);
            this.Controls.Add(this.lblHeaderOffset);
            this.Controls.Add(this.lblTilesetNo);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TilesetCalc";
            this.Text = "タイルセット番号計算";
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesetNo)).EndInit();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTilesetNo;
        private System.Windows.Forms.NumericUpDown nudTilesetNo;
        private System.Windows.Forms.Button btnToOffset;
        private System.Windows.Forms.Button btnToNo;
        private System.Windows.Forms.Label lblHeaderOffset;
        private System.Windows.Forms.TextBox txtHeaderOffset;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.Label lblRecNoAndOffset;
        private System.Windows.Forms.TextBox txtRecOffset;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.NumericUpDown nudRecNo;
    }
}