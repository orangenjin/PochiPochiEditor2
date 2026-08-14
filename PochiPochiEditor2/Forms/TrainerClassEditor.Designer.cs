
namespace PochiPochiEditor2.Forms
{
    partial class TrainerClassEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainerClassEditor));
            this.lblClassNameIndex = new System.Windows.Forms.Label();
            this.nudClassNameIndex = new System.Windows.Forms.NumericUpDown();
            this.cmbClassNameIndex = new System.Windows.Forms.ComboBox();
            this.grpBasicData = new System.Windows.Forms.GroupBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lblPrizeMulti = new System.Windows.Forms.Label();
            this.nudPrizeMulti = new System.Windows.Forms.NumericUpDown();
            this.grpExtraData = new System.Windows.Forms.GroupBox();
            this.nudEncMusic = new System.Windows.Forms.NumericUpDown();
            this.lblEncMusic = new System.Windows.Forms.Label();
            this.lblBattleMusic = new System.Windows.Forms.Label();
            this.nudBattleMusic = new System.Windows.Forms.NumericUpDown();
            this.nudBaseIv = new System.Windows.Forms.NumericUpDown();
            this.nudPokeBall = new System.Windows.Forms.NumericUpDown();
            this.lblBaseIv = new System.Windows.Forms.Label();
            this.lblPokeBall = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassNameIndex)).BeginInit();
            this.grpBasicData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrizeMulti)).BeginInit();
            this.grpExtraData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEncMusic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBattleMusic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseIv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPokeBall)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClassNameIndex
            // 
            this.lblClassNameIndex.AutoSize = true;
            this.lblClassNameIndex.Location = new System.Drawing.Point(20, 20);
            this.lblClassNameIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lblClassNameIndex.Name = "lblClassNameIndex";
            this.lblClassNameIndex.Size = new System.Drawing.Size(29, 15);
            this.lblClassNameIndex.TabIndex = 0;
            this.lblClassNameIndex.Text = "No. ";
            // 
            // nudClassNameIndex
            // 
            this.nudClassNameIndex.Location = new System.Drawing.Point(56, 16);
            this.nudClassNameIndex.Margin = new System.Windows.Forms.Padding(0);
            this.nudClassNameIndex.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudClassNameIndex.Name = "nudClassNameIndex";
            this.nudClassNameIndex.Size = new System.Drawing.Size(56, 23);
            this.nudClassNameIndex.TabIndex = 1;
            // 
            // cmbClassNameIndex
            // 
            this.cmbClassNameIndex.FormattingEnabled = true;
            this.cmbClassNameIndex.Location = new System.Drawing.Point(124, 16);
            this.cmbClassNameIndex.Margin = new System.Windows.Forms.Padding(0);
            this.cmbClassNameIndex.Name = "cmbClassNameIndex";
            this.cmbClassNameIndex.Size = new System.Drawing.Size(144, 23);
            this.cmbClassNameIndex.TabIndex = 2;
            // 
            // grpBasicData
            // 
            this.grpBasicData.Controls.Add(this.nudPrizeMulti);
            this.grpBasicData.Controls.Add(this.lblPrizeMulti);
            this.grpBasicData.Controls.Add(this.txtClassName);
            this.grpBasicData.Controls.Add(this.lblClassName);
            this.grpBasicData.Location = new System.Drawing.Point(20, 50);
            this.grpBasicData.Margin = new System.Windows.Forms.Padding(0);
            this.grpBasicData.Name = "grpBasicData";
            this.grpBasicData.Padding = new System.Windows.Forms.Padding(0);
            this.grpBasicData.Size = new System.Drawing.Size(274, 102);
            this.grpBasicData.TabIndex = 3;
            this.grpBasicData.TabStop = false;
            this.grpBasicData.Text = "肩書きデータ";
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(20, 32);
            this.lblClassName.Margin = new System.Windows.Forms.Padding(0);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(58, 15);
            this.lblClassName.TabIndex = 0;
            this.lblClassName.Text = "肩書き名 :";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(104, 28);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(144, 23);
            this.txtClassName.TabIndex = 1;
            // 
            // lblPrizeMulti
            // 
            this.lblPrizeMulti.AutoSize = true;
            this.lblPrizeMulti.Location = new System.Drawing.Point(20, 62);
            this.lblPrizeMulti.Margin = new System.Windows.Forms.Padding(0);
            this.lblPrizeMulti.Name = "lblPrizeMulti";
            this.lblPrizeMulti.Size = new System.Drawing.Size(61, 15);
            this.lblPrizeMulti.TabIndex = 2;
            this.lblPrizeMulti.Text = "賞金倍率 :";
            // 
            // nudPrizeMulti
            // 
            this.nudPrizeMulti.Location = new System.Drawing.Point(104, 58);
            this.nudPrizeMulti.Margin = new System.Windows.Forms.Padding(0);
            this.nudPrizeMulti.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPrizeMulti.Name = "nudPrizeMulti";
            this.nudPrizeMulti.Size = new System.Drawing.Size(56, 23);
            this.nudPrizeMulti.TabIndex = 3;
            // 
            // grpExtraData
            // 
            this.grpExtraData.Controls.Add(this.nudBaseIv);
            this.grpExtraData.Controls.Add(this.nudPokeBall);
            this.grpExtraData.Controls.Add(this.lblBaseIv);
            this.grpExtraData.Controls.Add(this.lblPokeBall);
            this.grpExtraData.Controls.Add(this.nudBattleMusic);
            this.grpExtraData.Controls.Add(this.nudEncMusic);
            this.grpExtraData.Controls.Add(this.lblBattleMusic);
            this.grpExtraData.Controls.Add(this.lblEncMusic);
            this.grpExtraData.Location = new System.Drawing.Point(20, 164);
            this.grpExtraData.Margin = new System.Windows.Forms.Padding(0);
            this.grpExtraData.Name = "grpExtraData";
            this.grpExtraData.Padding = new System.Windows.Forms.Padding(0);
            this.grpExtraData.Size = new System.Drawing.Size(186, 162);
            this.grpExtraData.TabIndex = 4;
            this.grpExtraData.TabStop = false;
            this.grpExtraData.Text = "追加データ";
            // 
            // nudEncMusic
            // 
            this.nudEncMusic.Location = new System.Drawing.Point(104, 28);
            this.nudEncMusic.Margin = new System.Windows.Forms.Padding(0);
            this.nudEncMusic.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEncMusic.Name = "nudEncMusic";
            this.nudEncMusic.Size = new System.Drawing.Size(56, 23);
            this.nudEncMusic.TabIndex = 5;
            // 
            // lblEncMusic
            // 
            this.lblEncMusic.AutoSize = true;
            this.lblEncMusic.Location = new System.Drawing.Point(20, 32);
            this.lblEncMusic.Margin = new System.Windows.Forms.Padding(0);
            this.lblEncMusic.Name = "lblEncMusic";
            this.lblEncMusic.Size = new System.Drawing.Size(75, 15);
            this.lblEncMusic.TabIndex = 4;
            this.lblEncMusic.Text = "戦闘前BGM :";
            // 
            // lblBattleMusic
            // 
            this.lblBattleMusic.AutoSize = true;
            this.lblBattleMusic.Location = new System.Drawing.Point(20, 62);
            this.lblBattleMusic.Margin = new System.Windows.Forms.Padding(0);
            this.lblBattleMusic.Name = "lblBattleMusic";
            this.lblBattleMusic.Size = new System.Drawing.Size(75, 15);
            this.lblBattleMusic.TabIndex = 4;
            this.lblBattleMusic.Text = "戦闘中BGM :";
            // 
            // nudBattleMusic
            // 
            this.nudBattleMusic.Location = new System.Drawing.Point(104, 58);
            this.nudBattleMusic.Margin = new System.Windows.Forms.Padding(0);
            this.nudBattleMusic.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBattleMusic.Name = "nudBattleMusic";
            this.nudBattleMusic.Size = new System.Drawing.Size(56, 23);
            this.nudBattleMusic.TabIndex = 5;
            // 
            // nudBaseIv
            // 
            this.nudBaseIv.Location = new System.Drawing.Point(104, 118);
            this.nudBaseIv.Margin = new System.Windows.Forms.Padding(0);
            this.nudBaseIv.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBaseIv.Name = "nudBaseIv";
            this.nudBaseIv.Size = new System.Drawing.Size(56, 23);
            this.nudBaseIv.TabIndex = 8;
            // 
            // nudPokeBall
            // 
            this.nudPokeBall.Location = new System.Drawing.Point(104, 88);
            this.nudPokeBall.Margin = new System.Windows.Forms.Padding(0);
            this.nudPokeBall.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPokeBall.Name = "nudPokeBall";
            this.nudPokeBall.Size = new System.Drawing.Size(56, 23);
            this.nudPokeBall.TabIndex = 9;
            // 
            // lblBaseIv
            // 
            this.lblBaseIv.AutoSize = true;
            this.lblBaseIv.Location = new System.Drawing.Point(20, 122);
            this.lblBaseIv.Margin = new System.Windows.Forms.Padding(0);
            this.lblBaseIv.Name = "lblBaseIv";
            this.lblBaseIv.Size = new System.Drawing.Size(73, 15);
            this.lblBaseIv.TabIndex = 6;
            this.lblBaseIv.Text = "基礎個体値 :";
            // 
            // lblPokeBall
            // 
            this.lblPokeBall.AutoSize = true;
            this.lblPokeBall.Location = new System.Drawing.Point(20, 92);
            this.lblPokeBall.Margin = new System.Windows.Forms.Padding(0);
            this.lblPokeBall.Name = "lblPokeBall";
            this.lblPokeBall.Size = new System.Drawing.Size(76, 15);
            this.lblPokeBall.TabIndex = 7;
            this.lblPokeBall.Text = "使用ボールID :";
            // 
            // TrainerClassEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 343);
            this.Controls.Add(this.grpExtraData);
            this.Controls.Add(this.grpBasicData);
            this.Controls.Add(this.cmbClassNameIndex);
            this.Controls.Add(this.nudClassNameIndex);
            this.Controls.Add(this.lblClassNameIndex);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TrainerClassEditor";
            this.Text = "トレーナー肩書き";
            ((System.ComponentModel.ISupportInitialize)(this.nudClassNameIndex)).EndInit();
            this.grpBasicData.ResumeLayout(false);
            this.grpBasicData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrizeMulti)).EndInit();
            this.grpExtraData.ResumeLayout(false);
            this.grpExtraData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEncMusic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBattleMusic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseIv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPokeBall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblClassNameIndex;
        private System.Windows.Forms.NumericUpDown nudClassNameIndex;
        private System.Windows.Forms.ComboBox cmbClassNameIndex;
        private System.Windows.Forms.GroupBox grpBasicData;
        private System.Windows.Forms.NumericUpDown nudPrizeMulti;
        private System.Windows.Forms.Label lblPrizeMulti;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.GroupBox grpExtraData;
        private System.Windows.Forms.NumericUpDown nudBaseIv;
        private System.Windows.Forms.NumericUpDown nudPokeBall;
        private System.Windows.Forms.Label lblBaseIv;
        private System.Windows.Forms.Label lblPokeBall;
        private System.Windows.Forms.NumericUpDown nudBattleMusic;
        private System.Windows.Forms.NumericUpDown nudEncMusic;
        private System.Windows.Forms.Label lblBattleMusic;
        private System.Windows.Forms.Label lblEncMusic;
    }
}