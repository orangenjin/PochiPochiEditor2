
namespace PochiPochiEditor2
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.grpLoadRom = new System.Windows.Forms.GroupBox();
            this.btnClearRom = new System.Windows.Forms.Button();
            this.btnSelectRom = new System.Windows.Forms.Button();
            this.cmbConfig = new System.Windows.Forms.ComboBox();
            this.lblConfig = new System.Windows.Forms.Label();
            this.grpSaveRom = new System.Windows.Forms.GroupBox();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnSaveOver = new System.Windows.Forms.Button();
            this.grpSelectEditor = new System.Windows.Forms.GroupBox();
            this.btnBattleBackground = new System.Windows.Forms.Button();
            this.btnTrainerList = new System.Windows.Forms.Button();
            this.btnTrade = new System.Windows.Forms.Button();
            this.btnTrainerSprite = new System.Windows.Forms.Button();
            this.btnTrainerClass = new System.Windows.Forms.Button();
            this.btnMailData = new System.Windows.Forms.Button();
            this.btnItem = new System.Windows.Forms.Button();
            this.btnRegion = new System.Windows.Forms.Button();
            this.btnTileset = new System.Windows.Forms.Button();
            this.btnOverworld = new System.Windows.Forms.Button();
            this.btnMap = new System.Windows.Forms.Button();
            this.btnEggmove = new System.Windows.Forms.Button();
            this.btnDexSearch = new System.Windows.Forms.Button();
            this.btnRoaming = new System.Windows.Forms.Button();
            this.btnDexNational = new System.Windows.Forms.Button();
            this.btnTmHmTutor = new System.Windows.Forms.Button();
            this.btnDexHabitat = new System.Windows.Forms.Button();
            this.btnSwarm = new System.Windows.Forms.Button();
            this.btnDexRegional = new System.Windows.Forms.Button();
            this.btnWildEnc = new System.Windows.Forms.Button();
            this.btnPokeData = new System.Windows.Forms.Button();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.picPoke = new System.Windows.Forms.PictureBox();
            this.btnEditor1 = new System.Windows.Forms.Button();
            this.btnEditor2 = new System.Windows.Forms.Button();
            this.btnEditor3 = new System.Windows.Forms.Button();
            this.btnFreeSpaceFinder = new System.Windows.Forms.Button();
            this.grpAssistantTool = new System.Windows.Forms.GroupBox();
            this.btnTool1 = new System.Windows.Forms.Button();
            this.btnTool3 = new System.Windows.Forms.Button();
            this.btnTool2 = new System.Windows.Forms.Button();
            this.btnTool5 = new System.Windows.Forms.Button();
            this.btnTool4 = new System.Windows.Forms.Button();
            this.grpLoadRom.SuspendLayout();
            this.grpSaveRom.SuspendLayout();
            this.grpSelectEditor.SuspendLayout();
            this.grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPoke)).BeginInit();
            this.grpAssistantTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLoadRom
            // 
            this.grpLoadRom.Controls.Add(this.btnClearRom);
            this.grpLoadRom.Controls.Add(this.btnSelectRom);
            this.grpLoadRom.Controls.Add(this.cmbConfig);
            this.grpLoadRom.Controls.Add(this.lblConfig);
            this.grpLoadRom.Location = new System.Drawing.Point(20, 16);
            this.grpLoadRom.Margin = new System.Windows.Forms.Padding(0);
            this.grpLoadRom.Name = "grpLoadRom";
            this.grpLoadRom.Padding = new System.Windows.Forms.Padding(0);
            this.grpLoadRom.Size = new System.Drawing.Size(254, 126);
            this.grpLoadRom.TabIndex = 0;
            this.grpLoadRom.TabStop = false;
            this.grpLoadRom.Text = "ROMを読み込み";
            // 
            // btnClearRom
            // 
            this.btnClearRom.Location = new System.Drawing.Point(20, 84);
            this.btnClearRom.Margin = new System.Windows.Forms.Padding(0);
            this.btnClearRom.Name = "btnClearRom";
            this.btnClearRom.Size = new System.Drawing.Size(212, 23);
            this.btnClearRom.TabIndex = 2;
            this.btnClearRom.Text = "読み込んだROMを破棄";
            this.btnClearRom.UseVisualStyleBackColor = true;
            // 
            // btnSelectRom
            // 
            this.btnSelectRom.Location = new System.Drawing.Point(20, 54);
            this.btnSelectRom.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectRom.Name = "btnSelectRom";
            this.btnSelectRom.Size = new System.Drawing.Size(212, 23);
            this.btnSelectRom.TabIndex = 2;
            this.btnSelectRom.Text = "ROMを選択";
            this.btnSelectRom.UseVisualStyleBackColor = true;
            // 
            // cmbConfig
            // 
            this.cmbConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConfig.FormattingEnabled = true;
            this.cmbConfig.Location = new System.Drawing.Point(112, 24);
            this.cmbConfig.Margin = new System.Windows.Forms.Padding(0);
            this.cmbConfig.Name = "cmbConfig";
            this.cmbConfig.Size = new System.Drawing.Size(120, 23);
            this.cmbConfig.TabIndex = 1;
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Location = new System.Drawing.Point(20, 28);
            this.lblConfig.Margin = new System.Windows.Forms.Padding(0);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(83, 15);
            this.lblConfig.TabIndex = 0;
            this.lblConfig.Text = "読み込み設定 :";
            // 
            // grpSaveRom
            // 
            this.grpSaveRom.Controls.Add(this.btnSaveAs);
            this.grpSaveRom.Controls.Add(this.btnSaveOver);
            this.grpSaveRom.Location = new System.Drawing.Point(20, 152);
            this.grpSaveRom.Margin = new System.Windows.Forms.Padding(0);
            this.grpSaveRom.Name = "grpSaveRom";
            this.grpSaveRom.Padding = new System.Windows.Forms.Padding(0);
            this.grpSaveRom.Size = new System.Drawing.Size(254, 100);
            this.grpSaveRom.TabIndex = 1;
            this.grpSaveRom.TabStop = false;
            this.grpSaveRom.Text = "ROMを保存";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(20, 58);
            this.btnSaveAs.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(212, 23);
            this.btnSaveAs.TabIndex = 3;
            this.btnSaveAs.Text = "名前を付けて保存";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            // 
            // btnSaveOver
            // 
            this.btnSaveOver.Location = new System.Drawing.Point(20, 28);
            this.btnSaveOver.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveOver.Name = "btnSaveOver";
            this.btnSaveOver.Size = new System.Drawing.Size(212, 23);
            this.btnSaveOver.TabIndex = 4;
            this.btnSaveOver.Text = "上書き保存";
            this.btnSaveOver.UseVisualStyleBackColor = true;
            // 
            // grpSelectEditor
            // 
            this.grpSelectEditor.Controls.Add(this.btnEditor3);
            this.grpSelectEditor.Controls.Add(this.btnBattleBackground);
            this.grpSelectEditor.Controls.Add(this.btnTrainerList);
            this.grpSelectEditor.Controls.Add(this.btnTrade);
            this.grpSelectEditor.Controls.Add(this.btnTrainerSprite);
            this.grpSelectEditor.Controls.Add(this.btnTrainerClass);
            this.grpSelectEditor.Controls.Add(this.btnMailData);
            this.grpSelectEditor.Controls.Add(this.btnItem);
            this.grpSelectEditor.Controls.Add(this.btnEditor2);
            this.grpSelectEditor.Controls.Add(this.btnRegion);
            this.grpSelectEditor.Controls.Add(this.btnTileset);
            this.grpSelectEditor.Controls.Add(this.btnOverworld);
            this.grpSelectEditor.Controls.Add(this.btnMap);
            this.grpSelectEditor.Controls.Add(this.btnEditor1);
            this.grpSelectEditor.Controls.Add(this.btnEggmove);
            this.grpSelectEditor.Controls.Add(this.btnDexSearch);
            this.grpSelectEditor.Controls.Add(this.btnRoaming);
            this.grpSelectEditor.Controls.Add(this.btnDexNational);
            this.grpSelectEditor.Controls.Add(this.btnTmHmTutor);
            this.grpSelectEditor.Controls.Add(this.btnDexHabitat);
            this.grpSelectEditor.Controls.Add(this.btnSwarm);
            this.grpSelectEditor.Controls.Add(this.btnDexRegional);
            this.grpSelectEditor.Controls.Add(this.btnWildEnc);
            this.grpSelectEditor.Controls.Add(this.btnPokeData);
            this.grpSelectEditor.Location = new System.Drawing.Point(294, 16);
            this.grpSelectEditor.Margin = new System.Windows.Forms.Padding(0);
            this.grpSelectEditor.Name = "grpSelectEditor";
            this.grpSelectEditor.Padding = new System.Windows.Forms.Padding(0);
            this.grpSelectEditor.Size = new System.Drawing.Size(458, 340);
            this.grpSelectEditor.TabIndex = 2;
            this.grpSelectEditor.TabStop = false;
            this.grpSelectEditor.Text = "編集項目を選択";
            // 
            // btnBattleBackground
            // 
            this.btnBattleBackground.Location = new System.Drawing.Point(308, 238);
            this.btnBattleBackground.Margin = new System.Windows.Forms.Padding(0);
            this.btnBattleBackground.Name = "btnBattleBackground";
            this.btnBattleBackground.Size = new System.Drawing.Size(128, 23);
            this.btnBattleBackground.TabIndex = 7;
            this.btnBattleBackground.Text = "戦闘背景";
            this.btnBattleBackground.UseVisualStyleBackColor = true;
            // 
            // btnTrainerList
            // 
            this.btnTrainerList.Location = new System.Drawing.Point(308, 178);
            this.btnTrainerList.Margin = new System.Windows.Forms.Padding(0);
            this.btnTrainerList.Name = "btnTrainerList";
            this.btnTrainerList.Size = new System.Drawing.Size(128, 23);
            this.btnTrainerList.TabIndex = 8;
            this.btnTrainerList.Text = "トレーナーデータ";
            this.btnTrainerList.UseVisualStyleBackColor = true;
            // 
            // btnTrade
            // 
            this.btnTrade.Location = new System.Drawing.Point(308, 208);
            this.btnTrade.Margin = new System.Windows.Forms.Padding(0);
            this.btnTrade.Name = "btnTrade";
            this.btnTrade.Size = new System.Drawing.Size(128, 23);
            this.btnTrade.TabIndex = 9;
            this.btnTrade.Text = "ゲーム内交換";
            this.btnTrade.UseVisualStyleBackColor = true;
            // 
            // btnTrainerSprite
            // 
            this.btnTrainerSprite.Location = new System.Drawing.Point(308, 148);
            this.btnTrainerSprite.Margin = new System.Windows.Forms.Padding(0);
            this.btnTrainerSprite.Name = "btnTrainerSprite";
            this.btnTrainerSprite.Size = new System.Drawing.Size(128, 23);
            this.btnTrainerSprite.TabIndex = 10;
            this.btnTrainerSprite.Text = "トレーナー画像";
            this.btnTrainerSprite.UseVisualStyleBackColor = true;
            // 
            // btnTrainerClass
            // 
            this.btnTrainerClass.Location = new System.Drawing.Point(308, 118);
            this.btnTrainerClass.Margin = new System.Windows.Forms.Padding(0);
            this.btnTrainerClass.Name = "btnTrainerClass";
            this.btnTrainerClass.Size = new System.Drawing.Size(128, 23);
            this.btnTrainerClass.TabIndex = 11;
            this.btnTrainerClass.Text = "トレーナー肩書き";
            this.btnTrainerClass.UseVisualStyleBackColor = true;
            // 
            // btnMailData
            // 
            this.btnMailData.Location = new System.Drawing.Point(308, 58);
            this.btnMailData.Margin = new System.Windows.Forms.Padding(0);
            this.btnMailData.Name = "btnMailData";
            this.btnMailData.Size = new System.Drawing.Size(128, 23);
            this.btnMailData.TabIndex = 5;
            this.btnMailData.Text = "メール内容";
            this.btnMailData.UseVisualStyleBackColor = true;
            // 
            // btnItem
            // 
            this.btnItem.Location = new System.Drawing.Point(308, 28);
            this.btnItem.Margin = new System.Windows.Forms.Padding(0);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(128, 23);
            this.btnItem.TabIndex = 6;
            this.btnItem.Text = "アイテム";
            this.btnItem.UseVisualStyleBackColor = true;
            // 
            // btnRegion
            // 
            this.btnRegion.Location = new System.Drawing.Point(164, 238);
            this.btnRegion.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegion.Name = "btnRegion";
            this.btnRegion.Size = new System.Drawing.Size(128, 23);
            this.btnRegion.TabIndex = 1;
            this.btnRegion.Text = "タウンマップ";
            this.btnRegion.UseVisualStyleBackColor = true;
            // 
            // btnTileset
            // 
            this.btnTileset.Location = new System.Drawing.Point(164, 178);
            this.btnTileset.Margin = new System.Windows.Forms.Padding(0);
            this.btnTileset.Name = "btnTileset";
            this.btnTileset.Size = new System.Drawing.Size(128, 23);
            this.btnTileset.TabIndex = 2;
            this.btnTileset.Text = "タイルセット";
            this.btnTileset.UseVisualStyleBackColor = true;
            // 
            // btnOverworld
            // 
            this.btnOverworld.Location = new System.Drawing.Point(164, 208);
            this.btnOverworld.Margin = new System.Windows.Forms.Padding(0);
            this.btnOverworld.Name = "btnOverworld";
            this.btnOverworld.Size = new System.Drawing.Size(128, 23);
            this.btnOverworld.TabIndex = 3;
            this.btnOverworld.Text = "歩行グラフィック";
            this.btnOverworld.UseVisualStyleBackColor = true;
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(164, 148);
            this.btnMap.Margin = new System.Windows.Forms.Padding(0);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(128, 23);
            this.btnMap.TabIndex = 4;
            this.btnMap.Text = "マップ";
            this.btnMap.UseVisualStyleBackColor = true;
            // 
            // btnEggmove
            // 
            this.btnEggmove.Location = new System.Drawing.Point(20, 238);
            this.btnEggmove.Margin = new System.Windows.Forms.Padding(0);
            this.btnEggmove.Name = "btnEggmove";
            this.btnEggmove.Size = new System.Drawing.Size(128, 23);
            this.btnEggmove.TabIndex = 0;
            this.btnEggmove.Text = "タマゴ技";
            this.btnEggmove.UseVisualStyleBackColor = true;
            // 
            // btnDexSearch
            // 
            this.btnDexSearch.Location = new System.Drawing.Point(20, 148);
            this.btnDexSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnDexSearch.Name = "btnDexSearch";
            this.btnDexSearch.Size = new System.Drawing.Size(128, 23);
            this.btnDexSearch.TabIndex = 0;
            this.btnDexSearch.Text = "図鑑索引";
            this.btnDexSearch.UseVisualStyleBackColor = true;
            // 
            // btnRoaming
            // 
            this.btnRoaming.Location = new System.Drawing.Point(164, 88);
            this.btnRoaming.Margin = new System.Windows.Forms.Padding(0);
            this.btnRoaming.Name = "btnRoaming";
            this.btnRoaming.Size = new System.Drawing.Size(128, 23);
            this.btnRoaming.TabIndex = 0;
            this.btnRoaming.Text = "徘徊位置";
            this.btnRoaming.UseVisualStyleBackColor = true;
            // 
            // btnDexNational
            // 
            this.btnDexNational.Location = new System.Drawing.Point(20, 88);
            this.btnDexNational.Margin = new System.Windows.Forms.Padding(0);
            this.btnDexNational.Name = "btnDexNational";
            this.btnDexNational.Size = new System.Drawing.Size(128, 23);
            this.btnDexNational.TabIndex = 0;
            this.btnDexNational.Text = "図鑑番号（全国）";
            this.btnDexNational.UseVisualStyleBackColor = true;
            // 
            // btnTmHmTutor
            // 
            this.btnTmHmTutor.Location = new System.Drawing.Point(20, 208);
            this.btnTmHmTutor.Margin = new System.Windows.Forms.Padding(0);
            this.btnTmHmTutor.Name = "btnTmHmTutor";
            this.btnTmHmTutor.Size = new System.Drawing.Size(128, 23);
            this.btnTmHmTutor.TabIndex = 0;
            this.btnTmHmTutor.Text = "技マシン / 教え技";
            this.btnTmHmTutor.UseVisualStyleBackColor = true;
            // 
            // btnDexHabitat
            // 
            this.btnDexHabitat.Location = new System.Drawing.Point(20, 118);
            this.btnDexHabitat.Margin = new System.Windows.Forms.Padding(0);
            this.btnDexHabitat.Name = "btnDexHabitat";
            this.btnDexHabitat.Size = new System.Drawing.Size(128, 23);
            this.btnDexHabitat.TabIndex = 0;
            this.btnDexHabitat.Text = "図鑑生息地";
            this.btnDexHabitat.UseVisualStyleBackColor = true;
            // 
            // btnSwarm
            // 
            this.btnSwarm.Location = new System.Drawing.Point(164, 58);
            this.btnSwarm.Margin = new System.Windows.Forms.Padding(0);
            this.btnSwarm.Name = "btnSwarm";
            this.btnSwarm.Size = new System.Drawing.Size(128, 23);
            this.btnSwarm.TabIndex = 0;
            this.btnSwarm.Text = "大量発生";
            this.btnSwarm.UseVisualStyleBackColor = true;
            // 
            // btnDexRegional
            // 
            this.btnDexRegional.Location = new System.Drawing.Point(20, 58);
            this.btnDexRegional.Margin = new System.Windows.Forms.Padding(0);
            this.btnDexRegional.Name = "btnDexRegional";
            this.btnDexRegional.Size = new System.Drawing.Size(128, 23);
            this.btnDexRegional.TabIndex = 0;
            this.btnDexRegional.Text = "図鑑番号（地方）";
            this.btnDexRegional.UseVisualStyleBackColor = true;
            // 
            // btnWildEnc
            // 
            this.btnWildEnc.Location = new System.Drawing.Point(164, 28);
            this.btnWildEnc.Margin = new System.Windows.Forms.Padding(0);
            this.btnWildEnc.Name = "btnWildEnc";
            this.btnWildEnc.Size = new System.Drawing.Size(128, 23);
            this.btnWildEnc.TabIndex = 0;
            this.btnWildEnc.Text = "野生設定";
            this.btnWildEnc.UseVisualStyleBackColor = true;
            // 
            // btnPokeData
            // 
            this.btnPokeData.Location = new System.Drawing.Point(20, 28);
            this.btnPokeData.Margin = new System.Windows.Forms.Padding(0);
            this.btnPokeData.Name = "btnPokeData";
            this.btnPokeData.Size = new System.Drawing.Size(128, 23);
            this.btnPokeData.TabIndex = 0;
            this.btnPokeData.Text = "ポケモン";
            this.btnPokeData.UseVisualStyleBackColor = true;
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.lstHistory);
            this.grpHistory.Controls.Add(this.btnRedo);
            this.grpHistory.Controls.Add(this.btnUndo);
            this.grpHistory.Location = new System.Drawing.Point(20, 264);
            this.grpHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Padding = new System.Windows.Forms.Padding(0);
            this.grpHistory.Size = new System.Drawing.Size(254, 232);
            this.grpHistory.TabIndex = 3;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "編集履歴";
            // 
            // lstHistory
            // 
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.ItemHeight = 15;
            this.lstHistory.Location = new System.Drawing.Point(20, 70);
            this.lstHistory.Margin = new System.Windows.Forms.Padding(0);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.ScrollAlwaysVisible = true;
            this.lstHistory.Size = new System.Drawing.Size(212, 139);
            this.lstHistory.TabIndex = 1;
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(132, 28);
            this.btnRedo.Margin = new System.Windows.Forms.Padding(0);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(100, 23);
            this.btnRedo.TabIndex = 0;
            this.btnRedo.Text = "やり直し";
            this.btnRedo.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(20, 28);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(0);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(100, 23);
            this.btnUndo.TabIndex = 0;
            this.btnUndo.Text = "元に戻す";
            this.btnUndo.UseVisualStyleBackColor = true;
            // 
            // picPoke
            // 
            this.picPoke.Location = new System.Drawing.Point(616, 384);
            this.picPoke.Margin = new System.Windows.Forms.Padding(0);
            this.picPoke.Name = "picPoke";
            this.picPoke.Size = new System.Drawing.Size(136, 104);
            this.picPoke.TabIndex = 4;
            this.picPoke.TabStop = false;
            // 
            // btnEditor1
            // 
            this.btnEditor1.Location = new System.Drawing.Point(20, 298);
            this.btnEditor1.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditor1.Name = "btnEditor1";
            this.btnEditor1.Size = new System.Drawing.Size(128, 23);
            this.btnEditor1.TabIndex = 0;
            this.btnEditor1.Text = "エディタ1";
            this.btnEditor1.UseVisualStyleBackColor = true;
            // 
            // btnEditor2
            // 
            this.btnEditor2.Location = new System.Drawing.Point(164, 298);
            this.btnEditor2.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditor2.Name = "btnEditor2";
            this.btnEditor2.Size = new System.Drawing.Size(128, 23);
            this.btnEditor2.TabIndex = 1;
            this.btnEditor2.Text = "エディタ2";
            this.btnEditor2.UseVisualStyleBackColor = true;
            // 
            // btnEditor3
            // 
            this.btnEditor3.Location = new System.Drawing.Point(308, 298);
            this.btnEditor3.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditor3.Name = "btnEditor3";
            this.btnEditor3.Size = new System.Drawing.Size(128, 23);
            this.btnEditor3.TabIndex = 7;
            this.btnEditor3.Text = "エディタ3";
            this.btnEditor3.UseVisualStyleBackColor = true;
            // 
            // btnFreeSpaceFinder
            // 
            this.btnFreeSpaceFinder.Location = new System.Drawing.Point(20, 28);
            this.btnFreeSpaceFinder.Name = "btnFreeSpaceFinder";
            this.btnFreeSpaceFinder.Size = new System.Drawing.Size(128, 23);
            this.btnFreeSpaceFinder.TabIndex = 5;
            this.btnFreeSpaceFinder.Text = "空き領域検索";
            this.btnFreeSpaceFinder.UseVisualStyleBackColor = true;
            // 
            // grpAssistantTool
            // 
            this.grpAssistantTool.Controls.Add(this.btnTool5);
            this.grpAssistantTool.Controls.Add(this.btnTool4);
            this.grpAssistantTool.Controls.Add(this.btnTool3);
            this.grpAssistantTool.Controls.Add(this.btnTool2);
            this.grpAssistantTool.Controls.Add(this.btnTool1);
            this.grpAssistantTool.Controls.Add(this.btnFreeSpaceFinder);
            this.grpAssistantTool.Location = new System.Drawing.Point(294, 366);
            this.grpAssistantTool.Margin = new System.Windows.Forms.Padding(0);
            this.grpAssistantTool.Name = "grpAssistantTool";
            this.grpAssistantTool.Padding = new System.Windows.Forms.Padding(0);
            this.grpAssistantTool.Size = new System.Drawing.Size(312, 130);
            this.grpAssistantTool.TabIndex = 6;
            this.grpAssistantTool.TabStop = false;
            this.grpAssistantTool.Text = "補助ツール";
            // 
            // btnTool1
            // 
            this.btnTool1.Location = new System.Drawing.Point(164, 28);
            this.btnTool1.Name = "btnTool1";
            this.btnTool1.Size = new System.Drawing.Size(128, 23);
            this.btnTool1.TabIndex = 6;
            this.btnTool1.Text = "ツール1";
            this.btnTool1.UseVisualStyleBackColor = true;
            // 
            // btnTool3
            // 
            this.btnTool3.Location = new System.Drawing.Point(164, 58);
            this.btnTool3.Name = "btnTool3";
            this.btnTool3.Size = new System.Drawing.Size(128, 23);
            this.btnTool3.TabIndex = 8;
            this.btnTool3.Text = "ツール3";
            this.btnTool3.UseVisualStyleBackColor = true;
            // 
            // btnTool2
            // 
            this.btnTool2.Location = new System.Drawing.Point(20, 58);
            this.btnTool2.Name = "btnTool2";
            this.btnTool2.Size = new System.Drawing.Size(128, 23);
            this.btnTool2.TabIndex = 7;
            this.btnTool2.Text = "ツール2";
            this.btnTool2.UseVisualStyleBackColor = true;
            // 
            // btnTool5
            // 
            this.btnTool5.Location = new System.Drawing.Point(164, 88);
            this.btnTool5.Name = "btnTool5";
            this.btnTool5.Size = new System.Drawing.Size(128, 23);
            this.btnTool5.TabIndex = 10;
            this.btnTool5.Text = "ツール5";
            this.btnTool5.UseVisualStyleBackColor = true;
            // 
            // btnTool4
            // 
            this.btnTool4.Location = new System.Drawing.Point(20, 88);
            this.btnTool4.Name = "btnTool4";
            this.btnTool4.Size = new System.Drawing.Size(128, 23);
            this.btnTool4.TabIndex = 9;
            this.btnTool4.Text = "ツール4";
            this.btnTool4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 515);
            this.Controls.Add(this.grpAssistantTool);
            this.Controls.Add(this.picPoke);
            this.Controls.Add(this.grpHistory);
            this.Controls.Add(this.grpSelectEditor);
            this.Controls.Add(this.grpSaveRom);
            this.Controls.Add(this.grpLoadRom);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "メイン画面";
            this.grpLoadRom.ResumeLayout(false);
            this.grpLoadRom.PerformLayout();
            this.grpSaveRom.ResumeLayout(false);
            this.grpSelectEditor.ResumeLayout(false);
            this.grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPoke)).EndInit();
            this.grpAssistantTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLoadRom;
        private System.Windows.Forms.Button btnClearRom;
        private System.Windows.Forms.Button btnSelectRom;
        private System.Windows.Forms.ComboBox cmbConfig;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.GroupBox grpSaveRom;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnSaveOver;
        private System.Windows.Forms.GroupBox grpSelectEditor;
        private System.Windows.Forms.Button btnDexSearch;
        private System.Windows.Forms.Button btnDexNational;
        private System.Windows.Forms.Button btnDexHabitat;
        private System.Windows.Forms.Button btnDexRegional;
        private System.Windows.Forms.Button btnPokeData;
        private System.Windows.Forms.Button btnEggmove;
        private System.Windows.Forms.Button btnTmHmTutor;
        private System.Windows.Forms.Button btnRoaming;
        private System.Windows.Forms.Button btnSwarm;
        private System.Windows.Forms.Button btnWildEnc;
        private System.Windows.Forms.Button btnRegion;
        private System.Windows.Forms.Button btnTileset;
        private System.Windows.Forms.Button btnOverworld;
        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Button btnMailData;
        private System.Windows.Forms.Button btnItem;
        private System.Windows.Forms.Button btnBattleBackground;
        private System.Windows.Forms.Button btnTrainerList;
        private System.Windows.Forms.Button btnTrade;
        private System.Windows.Forms.Button btnTrainerSprite;
        private System.Windows.Forms.Button btnTrainerClass;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.PictureBox picPoke;
        private System.Windows.Forms.Button btnEditor3;
        private System.Windows.Forms.Button btnEditor2;
        private System.Windows.Forms.Button btnEditor1;
        private System.Windows.Forms.Button btnFreeSpaceFinder;
        private System.Windows.Forms.GroupBox grpAssistantTool;
        private System.Windows.Forms.Button btnTool5;
        private System.Windows.Forms.Button btnTool4;
        private System.Windows.Forms.Button btnTool3;
        private System.Windows.Forms.Button btnTool2;
        private System.Windows.Forms.Button btnTool1;
    }
}

