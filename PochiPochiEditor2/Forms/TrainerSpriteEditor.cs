using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Forms
{
    [FormGroup(FormGroup.TrainerSprite)]
    public partial class TrainerSpriteEditor : Form
    {
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // 共有データ用
        private SharedData _sharedData = null;
        // 変更履歴用
        private UndoManager _undoManager = null;
        // 各テーブル用
        private EntryManager _image = null;
        private EntryManager _palette = null;
        private EntryManager _yPos = null;
        private EntryManager _animPointer = null;
        // パイプライン用
        private UiPipelineBuilder _imagePipeline = null;
        private UiPipelineBuilder _palettePipeline = null;
        private UiPipelineBuilder _yPosPipeline = null;

        // UI制御用
        private int _currentSpriteIndex = 0;

        private enum FieldKey
        {
            ImageOffset,
            ImageDecompSize,
            ImageIndex,
            ImageUnk1,

            PaletteOffset,
            PaletteIndex,
            PaletteUnk1,
            PaletteUnk2,
            PaletteUnk3,

            YPosTileCount,
            YPosValue,
            YPosUnk1,
            YPosUnk2,

            AnimPointerOffset
        }

        private static class IniKey
        {
            public static string TrainerSpriteImageEntry = nameof(TrainerSpriteImageEntry);
            public static string TrainerSpriteImageTableOffset = nameof(TrainerSpriteImageTableOffset);

            public static string TrainerSpritePaletteEntry = nameof(TrainerSpritePaletteEntry);
            public static string TrainerSpritePaletteTableOffset = nameof(TrainerSpritePaletteTableOffset);

            public static string TrainerSpriteYPosEntry = nameof(TrainerSpriteYPosEntry);
            public static string TrainerSpriteYPosTableOffset = nameof(TrainerSpriteYPosTableOffset);

            public static string TrainerSpriteAnimationPointerEntry = nameof(TrainerSpriteAnimationPointerEntry);
            public static string TrainerSpriteAnimPointerTableOffset = nameof(TrainerSpriteAnimPointerTableOffset);

            public static string TrainerSpriteCount = nameof(TrainerSpriteCount);
        }

        private enum ImportKind
        {
            Image,
            Palette
        }

        public TrainerSpriteEditor(SharedData sharedData, UndoManager undoManager)
        {
            InitializeComponent();
            _sharedData = sharedData;
            _undoManager = undoManager;

            InitializeEntries();
            InitializeControls();
            InitializePipelines();
            InitializeEventHandlers();

            LoadDataToUI(_currentSpriteIndex);
        }

        private void InitializeEntries()
        {
            // 画像テーブルを作成
            string defFileName = IniKey.TrainerSpriteImageEntry;
            int tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerSpriteImageTableOffset);
            int entrycount = _sharedData.Config.ReadInt(IniKey.TrainerSpriteCount);
            _image = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

            // パレットテーブルを作成
            defFileName = IniKey.TrainerSpritePaletteEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerSpritePaletteTableOffset);
            _palette = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

            // Y座標位置テーブルを作成
            defFileName = IniKey.TrainerSpriteYPosEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerSpriteYPosTableOffset);
            _yPos = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

            // アニメポインタテーブルを作成
            defFileName = IniKey.TrainerSpriteAnimationPointerEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerSpriteAnimPointerTableOffset);
            _animPointer = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
        }

        private void InitializeControls()
        {
            // nudの最大値
            int spriteCount = _sharedData.Config.ReadInt(IniKey.TrainerSpriteCount);
            nudSpriteIndex.Maximum = spriteCount - 1;

            // タグ設定
            btnImportImage.Tag = ImportKind.Image;
            btnImportPalette.Tag = ImportKind.Palette;
        }

        private void InitializePipelines()
        {
            // txtClassName
            _imagePipeline = new UiPipelineBuilder()
                // 入力値を取得
                .Then(ctx =>
                {
                    ctx.Set((TextBox)ctx.Sender); // テキストボックス
                    ctx.Set(ctx.Get<TextBox>().Text); // 入力されたテキスト
                })
                // 整形
                .Then(ctx =>
                {
                    ctx.Get<TextBox>().Text = CalcHelper.FormatValueText(ctx.Get<string>());
                    CtrlHelper.MoveCursorToEnd(ctx.Get<TextBox>()); // カーソル位置
                })
                // データを更新
                .Then(ctx =>
                {
                    var targetField = _image.Entries[_currentSpriteIndex][FieldKey.ImageOffset];

                    // 変更前のバイナリデータ
                    byte[] oldBinary = targetField.BinaryData;

                    // データ更新
                    targetField.SetData(ctx.Get<int>());

                    // 変更後のバイナリデータ
                    byte[] newBinary = targetField.BinaryData;

                    // 異なればスタックに追加
                    if (!oldBinary.SequenceEqual(newBinary))
                    {
                        var cmd = new FieldChangeCommand(
                            targetField,
                            oldBinary,
                            newBinary,
                            $"[{this.Text}]画像アドレス(ID:{_currentSpriteIndex})");
                        _undoManager.PushCommand(cmd);
                    }
                });
        }

        private void InitializeEventHandlers()
        {
            // 枠描画
            _eventBinder.BindCustom(
                () => CtrlHelper.AttachBorder(this, picSprite),
                () => CtrlHelper.DetachBorder(this)
            );

            // nudにbtnを対応付ける
            _eventBinder.BindCustom(
                () => CtrlHelper.AttachBtnsToNud(
                    nudSpriteIndex, 
                    btnSpriteIndexPrev,
                    btnSpriteIndexNext),
                () => CtrlHelper.DetachBtnsToNud(
                    nudSpriteIndex,
                    btnSpriteIndexPrev,
                    btnSpriteIndexNext)
            );

            // 画像アドレス
            _eventBinder.BindCtrl(
                h => txtImageOffset.TextChanged += h,
                h => txtImageOffset.TextChanged -= h,
                (s, e) =>
                {
                    _imagePipeline.Execute(new UiContext(s, e));
                    // DisplayTrainerSprite();
                });

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }

        private void LoadDataToUI(int index)
        {
            _currentSpriteIndex = index;

            // 画像アドレス
            txtImageOffset.Text = _image.Entries[index][FieldKey.ImageOffset].GetData<string>();
            // パレットアドレス
            txtPaletteOffset.Text = _palette.Entries[index][FieldKey.PaletteOffset].GetData<string>();



        }













        private void SpriteImport_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn) || !(btn.Tag is ImportKind importKind)) return;

            using (var popup = new QuickInput(
                defaultOffset: 0,
                fileFilter: Constants.ImageImportFilter))
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    int offset = popup.Offset;
                    string filePath = popup.FilePath;

                    using (Bitmap bmp = new Bitmap(filePath))
                    {
                        TextBox targetTextBox;

                        if (!ImageHelper.ExtractImageAndPalette(
                            bmp,
                            Constants.SpriteSize,
                            Constants.SpriteSize,
                            out byte[] imageData,
                            out byte[] paletteData)) return;

                        if (importKind == ImportKind.Image)
                        {
                            targetTextBox = txtImageOffset;
                        }
                        else if(importKind == ImportKind.Palette)
                        {
                            targetTextBox = txtPaletteOffset;
                        }
                    }

                    // DisplayTrainerSprite();
                }
            }
        }
    }
}
