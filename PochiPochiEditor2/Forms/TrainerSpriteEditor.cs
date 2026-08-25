using System;
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
    public partial class TrainerSpriteEditor : Form, IEditorRefresh
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
        private PipelineBuilder _imageOffsetValidated = null;
        private PipelineBuilder _paletteOffsetValidated = null;
        private PipelineBuilder _yPosValueValidated = null;
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
            // txtImageOffset
            _imageOffsetValidated = new PipelineBuilder()
                // 入力値を取得
                .Then(ctx =>
                {
                    ctx.Set((TextBox)ctx.Sender); // テキストボックス
                    ctx.Set(ctx.Get<TextBox>().Text); // 入力されたテキスト
                })
                // データを更新
                .Then(ctx =>
                {
                    var parsedValue = CalcHelper.ParseStringToInt(ctx.Get<string>());
                    var desc = $"[{this.Text}]画像アドレス(ID:{_currentSpriteIndex})";

                    _image.Entries[_currentSpriteIndex][FieldKey.ImageOffset]
                        .UpdateData(_undoManager, parsedValue, desc);
                });

            // txtPaletteOffset
            _paletteOffsetValidated = new PipelineBuilder()
                // 入力値を取得
                .Then(ctx =>
                {
                    ctx.Set((TextBox)ctx.Sender); // テキストボックス
                    ctx.Set(ctx.Get<TextBox>().Text); // 入力されたテキスト
                })
                // データを更新
                .Then(ctx =>
                {
                    var parsedValue = CalcHelper.ParseStringToInt(ctx.Get<string>());
                    var desc = $"[{this.Text}]パレットアドレス(ID:{_currentSpriteIndex})";

                    _palette.Entries[_currentSpriteIndex][FieldKey.PaletteOffset]
                        .UpdateData(_undoManager, parsedValue, desc);
                });

            // nudYPosValue
            _yPosValueValidated = new PipelineBuilder()
                // 入力値を取得
                .Then(ctx =>
                {
                    ctx.Set((NumericUpDown)ctx.Sender); // ニューメリックアップダウン
                    ctx.Set(ctx.Get<NumericUpDown>().Value); // 入力された値
                })
                // データを更新
                .Then(ctx =>
                {
                    var parsedValue = (int)ctx.Get<decimal>();
                    var desc = $"[{this.Text}]Y座標位置(ID:{_currentSpriteIndex})";

                    _yPos.Entries[_currentSpriteIndex][FieldKey.YPosValue]
                        .UpdateData(_undoManager, parsedValue, desc);
                });
        }

        private void InitializeEventHandlers()
        {
            // 枠描画
            _eventBinder.BindCustom(
                () => CtrlHelper.AttachBorder(this, picSprite),
                () => CtrlHelper.DetachBorder(this));
            // nudにbtnを対応付ける
            _eventBinder.BindCustom(
                () => CtrlHelper.AttachBtnsToNud(
                    nudSpriteIndex, 
                    btnSpriteIndexPrev,
                    btnSpriteIndexNext),
                () => CtrlHelper.DetachBtnsToNud(
                    nudSpriteIndex,
                    btnSpriteIndexPrev,
                    btnSpriteIndexNext));

            // 画像アドレス
            _eventBinder.BindCtrl(
                h => txtImageOffset.Validated += h,
                h => txtImageOffset.Validated -= h,
                (s, e) =>
                {
                    _imageOffsetValidated.Execute(new Context(s, e));
                    
                });
            // パレットアドレス
            _eventBinder.BindCtrl(
                h => txtPaletteOffset.Validated += h,
                h => txtPaletteOffset.Validated -= h,
                (s, e) =>
                {
                    _paletteOffsetValidated.Execute(new Context(s, e));

                });
            // Y座標位置
            _eventBinder.BindCtrl(
                h => nudYPosValue.ValueChanged += h,
                h => nudYPosValue.ValueChanged -= h,
                (s, e) =>
                {
                    _yPosValueValidated.Execute(new Context(s, e));

                });

            // 画像インデックスnud
            _eventBinder.BindCtrl(
                h => nudSpriteIndex.ValueChanged += h,
                h => nudSpriteIndex.ValueChanged -= h,
                (_, __) =>
                {
                    int newIndex = (int)nudSpriteIndex.Value;
                    LoadDataToUI(newIndex);
                });

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }

        /// <summary>
        /// FormGroupManagerからのUI再描画用の処理。
        /// </summary>
        public void RefreshFromData()
        {
            // 現在のインデックスを再読み込み
            LoadDataToUI(_currentSpriteIndex);

            // カーソル位置
            CtrlHelper.MoveCursorToEnd(txtImageOffset);
            CtrlHelper.MoveCursorToEnd(txtPaletteOffset);
        }

        private void LoadDataToUI(int index)
        {
            _currentSpriteIndex = index;

            // 画像アドレス
            txtImageOffset.Text =
                _image.Entries[index][FieldKey.ImageOffset]
                .GetData<int>()
                .ParseIntToString();
            // パレットアドレス
            txtPaletteOffset.Text = 
                _palette.Entries[index][FieldKey.PaletteOffset]
                .GetData<int>()
                .ParseIntToString();
            // Y座標位置
            nudYPosValue.Value =
                _yPos.Entries[index][FieldKey.YPosValue]
                .GetData<int>();

            // アニメーションポインタアドレス
            txtAnimPointerOffset.Text =
                _animPointer.Entries[index][FieldKey.AnimPointerOffset]
                .GetData<int>()
                .ParseIntToString();
            // アニメーションデータアドレス
            int targetOffset = 
                _animPointer.Entries[index][FieldKey.AnimPointerOffset]
                .GetData<int>();
            if (IoHelper.TryReadPtr(_sharedData.RomData, targetOffset, out int result) 
                && result != Constants.InvalidValue)
            {
                txtAnimDataOffset.Text = result.ParseIntToString();
            }
            else
            {
                txtAnimDataOffset.Text = string.Empty;
            }

            // 画像の再描画
            DisplayTrainerSprite();
        }

        private void DisplayTrainerSprite()
        {
            var isImageValid = !string.IsNullOrEmpty(txtImageOffset.Text);
            var isPaletteValid = !string.IsNullOrEmpty(txtPaletteOffset.Text);

            // 無効なアドレスの場合は何も描画しない
            if (!isImageValid || !isPaletteValid)
            {
                picSprite.Image?.Dispose();
                picSprite.Image = null;
                return;
            }

            try
            {
                var imageData = ImageHelper.DecompressLZ77(
                    _sharedData.RomData,
                    txtImageOffset.Text.ParseStringToInt());
                var paletteData = ImageHelper.DecompressPalette(
                    _sharedData.RomData,
                    txtPaletteOffset.Text.ParseStringToInt(), 
                    isCompressed: true);

                var sprite = ImageHelper.CreateBitmap(
                    imageData,
                    paletteData,
                    Constants.SpriteSize,
                    Constants.SpriteSize,
                    showBackColor: true);
                var scaled = ImageHelper.ScaleBitmap(sprite);

                picSprite.Image?.Dispose();
                picSprite.Image = scaled;
                picSprite.Refresh();
            }
            catch
            {
                picSprite.Image?.Dispose();
                picSprite.Image = null;
            }
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
