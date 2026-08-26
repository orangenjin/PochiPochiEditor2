using System;
using System.Drawing;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Managers.Commands;
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
        private EntryManager _imageEntry = null;
        private EntryManager _paletteEntry = null;
        private EntryManager _yPosEntry = null;
        private EntryManager _animPointerEntry = null;
        // パイプライン用
        private PipelineBuilder _imageOffsetPipeline = null;
        private PipelineBuilder _paletteOffsetPipeline = null;
        private PipelineBuilder _yPosValuePipeline = null;
        // 可変長データ管理用
        private RefData _imageData = null;
        private RefData _paletteData = null;
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

        private enum SpriteData
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
            _imageEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

            // パレットテーブルを作成
            defFileName = IniKey.TrainerSpritePaletteEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerSpritePaletteTableOffset);
            _paletteEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

            // Y座標位置テーブルを作成
            defFileName = IniKey.TrainerSpriteYPosEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerSpriteYPosTableOffset);
            _yPosEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

            // アニメポインタテーブルを作成
            defFileName = IniKey.TrainerSpriteAnimationPointerEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerSpriteAnimPointerTableOffset);
            _animPointerEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
        }

        private void InitializeControls()
        {
            // nudの最大値
            int spriteCount = _sharedData.Config.ReadInt(IniKey.TrainerSpriteCount);
            nudSpriteIndex.Maximum = spriteCount - 1;

            // タグ設定
            btnImportImage.Tag = SpriteData.Image;
            btnImportPalette.Tag = SpriteData.Palette;
        }

        private void InitializePipelines()
        {
            // txtImageOffset
            _imageOffsetPipeline = new PipelineBuilder()
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
                    var desc = $"[{this.Text}]画像アドレス(ID:{_currentSpriteIndex:D4})";

                    _imageEntry.Entries[_currentSpriteIndex][FieldKey.ImageOffset]
                        .UpdateData(_undoManager, parsedValue, desc);
                });

            // txtPaletteOffset
            _paletteOffsetPipeline = new PipelineBuilder()
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
                    var desc = $"[{this.Text}]パレットアドレス(ID:{_currentSpriteIndex:D4})";

                    _paletteEntry.Entries[_currentSpriteIndex][FieldKey.PaletteOffset]
                        .UpdateData(_undoManager, parsedValue, desc);
                });

            // nudYPosValue
            _yPosValuePipeline = new PipelineBuilder()
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
                    var desc = $"[{this.Text}]Y座標位置(ID:{_currentSpriteIndex:D4})";

                    _yPosEntry.Entries[_currentSpriteIndex][FieldKey.YPosValue]
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
                (sender, e) =>
                {
                    _imageOffsetPipeline.Execute(new Context(sender, e));
                    
                });
            // パレットアドレス
            _eventBinder.BindCtrl(
                h => txtPaletteOffset.Validated += h,
                h => txtPaletteOffset.Validated -= h,
                (sender, e) =>
                {
                    _paletteOffsetPipeline.Execute(new Context(sender, e));

                });
            // Y座標位置
            _eventBinder.BindCtrl(
                h => nudYPosValue.ValueChanged += h,
                h => nudYPosValue.ValueChanged -= h,
                (sender, e) =>
                {
                    _yPosValuePipeline.Execute(new Context(sender, e));

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

            // エクスポート
            _eventBinder.BindCtrl(
                h => btnSpriteExport.Click += h,
                h => btnSpriteExport.Click -= h,
                (_, __) =>
                {
                    // 正規かどうかの判定
                    if (picSprite.Image == null) return;

                    using (var sfd = new SaveFileDialog())
                    {
                        sfd.Filter = Constants.ImageExportFilter;
                        sfd.FileName = $"trainer_sprite_{_currentSpriteIndex:D4}";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            // RefDataから生成
                            var imageData = ImageHelper.DecompressLZ77(
                                _imageData.BinaryData);
                            var paletteData = ImageHelper.DecompressPalette(
                                _paletteData.BinaryData);
                            var sprite = ImageHelper.CreateBitmap(
                                imageData,
                                paletteData,
                                Constants.SpriteSize,
                                Constants.SpriteSize,
                                showBackColor: true);

                            ImageHelper.ExportIndexedImage(
                                sprite, 
                                sfd.FileName);
                        }
                    }
                });
            // インポート
            _eventBinder.BindCtrl(
                h => btnImportImage.Click += h,
                h => btnImportImage.Click -= h,
                SpriteImport_Click);
            _eventBinder.BindCtrl(
                h => btnImportPalette.Click += h,
                h => btnImportPalette.Click -= h,
                SpriteImport_Click);

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
                _imageEntry.Entries[index][FieldKey.ImageOffset]
                .GetData<int>()
                .ParseIntToString();
            // パレットアドレス
            txtPaletteOffset.Text = 
                _paletteEntry.Entries[index][FieldKey.PaletteOffset]
                .GetData<int>()
                .ParseIntToString();
            // Y座標位置
            nudYPosValue.Value =
                _yPosEntry.Entries[index][FieldKey.YPosValue]
                .GetData<int>();

            // アニメーションポインタアドレス
            txtAnimPointerOffset.Text =
                _animPointerEntry.Entries[index][FieldKey.AnimPointerOffset]
                .GetData<int>()
                .ParseIntToString();
            // アニメーションデータアドレス
            int targetOffset = 
                _animPointerEntry.Entries[index][FieldKey.AnimPointerOffset]
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
            var imageOffsetStr = txtImageOffset.Text;
            var paletteOffsetStr = txtPaletteOffset.Text;
            var isImageInvalid = string.IsNullOrEmpty(imageOffsetStr);
            var isPaletteInValid = string.IsNullOrEmpty(paletteOffsetStr);

            // 無効なアドレスの場合は何も描画しない
            if (isImageInvalid || isPaletteInValid)
            {
                picSprite.Image?.Dispose();
                picSprite.Image = null;
                return;
            }

            try
            {
                // オフセットを取得
                var imageOffsetValue = imageOffsetStr.ParseStringToInt();
                var imageData = ImageHelper.DecompressLZ77(
                    _sharedData.RomData,
                    imageOffsetValue);
                // RefDataとして保持する
                var imageDataLz77 = ImageHelper.CompressLZ77(imageData);
                _imageData = new RefData(
                    SpriteData.Image,
                    imageOffsetValue,
                    imageDataLz77, 
                    _sharedData);

                // オフセットを取得
                var paletteOffsetValue = paletteOffsetStr.ParseStringToInt();
                var paletteData = ImageHelper.DecompressPalette(
                    _sharedData.RomData,
                    paletteOffsetValue);
                // RefDataとして保持する
                var paletteDataLz77 = ImageHelper.CompressPalette(paletteData);
                _paletteData = new RefData(
                    SpriteData.Palette,
                    paletteOffsetValue,
                    paletteDataLz77,
                    _sharedData);

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
            if (!(sender is Button btn) || !(btn.Tag is SpriteData importKind)) return;

            using (var popup = new QuickInput(
                defaultOffset: 0,
                fileFilter: Constants.ImageImportFilter))
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    int newOffset = popup.Offset;
                    string filePath = popup.FilePath;

                    using (Bitmap bmp = new Bitmap(filePath))
                    {
                        // バイト配列を抽出
                        if (!ImageHelper.ExtractImageAndPalette(
                            bmp,
                            Constants.SpriteSize,
                            Constants.SpriteSize,
                            out byte[] imageData,
                            out byte[] paletteData)) return;

                        if (importKind == SpriteData.Image)
                        {
                            // LZ77圧縮を適用
                            var compressedData = ImageHelper.CompressLZ77(imageData);
                            // コマンド表示名
                            string desc = $"[{this.Text}]画像インポート(ID:{_currentSpriteIndex:D4})";

                            // コマンドを統合する
                            var combine = new CombineCommand(desc);
                            // FieldValueの変更コマンド
                            combine.Add(
                                _imageEntry.Entries[_currentSpriteIndex][FieldKey.ImageOffset]
                                .CreateUpdateCommand(newOffset, desc));
                            // RefDataの変更コマンド
                            combine.Add(
                                _imageData.CreateUpdateCommand(
                                    newOffset,
                                    compressedData,
                                    desc));
                            // 要素数が0より大きければ
                            if (combine.HasCommands)
                            {
                                _undoManager.PushCommand(combine);
                            }
                        }
                        else
                        {
                            // LZ77圧縮を適用
                            var compressedData = ImageHelper.CompressPalette(paletteData);
                            // コマンド表示名
                            var desc = $"[{this.Text}]パレットインポート(ID:{_currentSpriteIndex:D4})";

                            // コマンドを統合する
                            var combine = new CombineCommand(desc);
                            // FieldValueの変更コマンド
                            combine.Add(
                                _paletteEntry.Entries[_currentSpriteIndex][FieldKey.PaletteOffset]
                                .CreateUpdateCommand(newOffset, desc));
                            // RefDataの変更コマンド
                            combine.Add(
                                _paletteData.CreateUpdateCommand(
                                    newOffset,
                                    compressedData,
                                    desc));
                            // 要素数が0より大きければ
                            if (combine.HasCommands)
                            {
                                _undoManager.PushCommand(combine);
                            }
                        }
                    }
                }
            }
        }
    }
}
