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
    [FormGroup(FormGroup.Tileset)]
    public partial class TilesetEditor : Form, IEditorRefresh
    {
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // 共有データ用
        private SharedData _sharedData = null;
        // 変更履歴用
        private UndoManager _undoManager = null;
        // 各テーブル用
        private TilesetManager _tilesetManager = null;
        // パイプライン用
        private PipelineBuilder _imageCompTypePipeline = null;
        private PipelineBuilder _paletteTypePipeline = null;
        private PipelineBuilder _imageOffsetPipeline = null;
        private PipelineBuilder _paletteOffsetPipeline = null;
        private PipelineBuilder _blockDataOffsetPipeline = null;
        private PipelineBuilder _animOffsetPipeline = null;
        private PipelineBuilder _blockAttrOffsetPipeline = null;
        // UI制御用
        private int _currentTilesetNo = default;

        public TilesetEditor(SharedData sharedData, UndoManager undoManager)
        {
            InitializeComponent();
            _sharedData = sharedData;
            _undoManager = undoManager;

            InitializeControls();
            InitializePipelines();
            InitializeEventHandlers();

            _tilesetManager = new TilesetManager(_sharedData);
        }

        private void InitializeControls()
        {
            // コンボボックスのアイテムを追加
            CtrlHelper.LoadComboBoxFromFile(
                (cmbImageCompType, "txt/Tileset/TilesetImageCompType.txt"),
                (cmbPaletteType, "txt/Tileset/TilesetPaletteype.txt"),
                (cmbSelectedPalette, "txt/Tileset/TilesetPaletteIndex.txt"));

            // 一時的に全コントロールを無効化
            UpdateTabPageState(false);
        }

        private void UpdateTabPageState(bool state)
        {
            // pnlのクリア処理が必要
            //

            // tncMain
            CtrlHelper.SetControlsEnabled(tncMain, state);
            CtrlHelper.ResetControls(tncMain);
        }

        private void InitializePipelines()
        {
            _imageCompTypePipeline = BuildCmbPipeline(
                TilesetManager.FieldKey.ImageCompType,
                lblImageCompType.Text);
            _paletteTypePipeline = BuildCmbPipeline(
                TilesetManager.FieldKey.PaletteType,
                lblPaletteType.Text);
            // ヘルパー
            PipelineBuilder BuildCmbPipeline(TilesetManager.FieldKey fieldKey, string label)
            {
                return new PipelineBuilder()
                    // 入力値を取得
                    .Then(ctx =>
                    {
                        ctx.Set((ComboBox)ctx.Sender); // コンボボックス
                        ctx.Set((byte)ctx.Get<ComboBox>().SelectedValue); // 選択されたインデックスの値
                    })
                    // データを更新
                    .Then(ctx =>
                    {
                        var value = ctx.Get<byte>();
                        var desc = $"[{this.Text}]{label}(ID:{_currentTilesetNo:D8})";

                        _tilesetManager.HeaderEntry[fieldKey]
                            .UpdateData(_undoManager, value, desc);
                    });
            }

            _imageOffsetPipeline = BuildOffsetPipeline(
                TilesetManager.FieldKey.ImageOffset,
                lblImageOffset.Text);
            _paletteOffsetPipeline = BuildOffsetPipeline(
                TilesetManager.FieldKey.PaletteOffset,
                lblPaletteOffset.Text);
            _blockDataOffsetPipeline = BuildOffsetPipeline(
                TilesetManager.FieldKey.BlockDataTableOffset,
                lblBlockDataTableOffset.Text);
            _animOffsetPipeline = BuildOffsetPipeline(
                TilesetManager.FieldKey.AnimHeaderOffset,
                lblAnimHeaderOffset.Text);
            _blockAttrOffsetPipeline = BuildOffsetPipeline(
                TilesetManager.FieldKey.BlockAttrTableOffset,
                lblBlockAttrTableOffset.Text);
            // ヘルパー
            PipelineBuilder BuildOffsetPipeline(TilesetManager.FieldKey fieldKey, string label)
            {
                return new PipelineBuilder()
                    // 入力値を取得
                    .Then(ctx =>
                    {
                        ctx.Set((TextBox)ctx.Sender); // テキストボックス
                        ctx.Set(ctx.Get<TextBox>().Text); // 入力されたテキスト
                    })
                    // データを更新
                    .Then(ctx =>
                    {
                        var value = CalcHelper.ParseStringToInt(ctx.Get<string>());
                        var desc = $"[{this.Text}]{label}(ID:{_currentTilesetNo:D8})";

                        _tilesetManager.HeaderEntry[fieldKey]
                            .UpdateData(_undoManager, value, desc);
                    });
            }
        }

        private void InitializeEventHandlers()
        {
            // 枠描画
            _eventBinder.BindCustom(
                () => CtrlHelper.AttachBorder(grpTilesetView, pnlTilesetImage),
                () => CtrlHelper.DetachBorder(grpTilesetView));
            // nudにbtnを対応付ける
            _eventBinder.BindCustom(
                () => CtrlHelper.AttachBtnsToNud(
                    nudSelectedTileCount,
                    btnSelectTileMinus,
                    btnSelectTilePlus),
                () => CtrlHelper.DetachBtnsToNud(
                    nudSelectedTileCount,
                    btnSelectTileMinus,
                    btnSelectTilePlus));

            // タイルセット番号
            _eventBinder.BindCtrl(
                h => btnLoadTileset.Click += h,
                h => btnLoadTileset.Click -= h,
                (sender, e) =>
                {
                    _currentTilesetNo = (int)nudTilesetNo.Value;
                    LoadDataToUI(_currentTilesetNo);
                });

            // 画像圧縮設定
            _eventBinder.BindCtrl(
                h => cmbImageCompType.SelectionChangeCommitted += h,
                h => cmbImageCompType.SelectionChangeCommitted -= h,
                (sender, e) =>
                {
                    _imageCompTypePipeline.Execute(new Context(sender, e));
                });
            // パレット読み込み設定
            _eventBinder.BindCtrl(
                h => cmbPaletteType.SelectionChangeCommitted += h,
                h => cmbPaletteType.SelectionChangeCommitted -= h,
                (sender, e) =>
                {
                    _paletteTypePipeline.Execute(new Context(sender, e));
                });
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
            // ブロックデータテーブル
            _eventBinder.BindCtrl(
                h => txtBlockDataTableOffset.Validated += h,
                h => txtBlockDataTableOffset.Validated -= h,
                (sender, e) =>
                {
                    _blockDataOffsetPipeline.Execute(new Context(sender, e));
                });
            // アニメヘッダーアドレス
            _eventBinder.BindCtrl(
                h => txtAnimHeaderOffset.Validated += h,
                h => txtAnimHeaderOffset.Validated -= h,
                (sender, e) =>
                {
                    _animOffsetPipeline.Execute(new Context(sender, e));
                });
            // ブロック属性テーブル
            _eventBinder.BindCtrl(
                h => txtBlockAttrTableOffset.Validated += h,
                h => txtBlockAttrTableOffset.Validated -= h,
                (sender, e) =>
                {
                    _blockAttrOffsetPipeline.Execute(new Context(sender, e));
                });






            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }

        private void LoadDataToUI(int tilsetNo)
        {
            // ヘッダーを再読み込み
            _tilesetManager.ReadHeader(tilsetNo, _sharedData);

            // UIを有効化
            UpdateTabPageState(true);

            // UIに反映
            cmbImageCompType.SelectedValue = GetHeaderData<byte>(TilesetManager.FieldKey.ImageCompType);
            cmbPaletteType.SelectedValue = GetHeaderData<byte>(TilesetManager.FieldKey.PaletteType);
            txtImageOffset.Text = GetHeaderData<int>(TilesetManager.FieldKey.ImageOffset).ParseIntToString();
            txtPaletteOffset.Text = GetHeaderData<int>(TilesetManager.FieldKey.PaletteOffset).ParseIntToString();
            txtBlockDataTableOffset.Text = GetHeaderData<int>(TilesetManager.FieldKey.BlockDataTableOffset).ParseIntToString();
            txtAnimHeaderOffset.Text = GetHeaderData<int>(TilesetManager.FieldKey.AnimHeaderOffset).ParseIntToString();
            txtBlockAttrTableOffset.Text = GetHeaderData<int>(TilesetManager.FieldKey.BlockAttrTableOffset).ParseIntToString();
            // ヘルパー
            T GetHeaderData<T>(TilesetManager.FieldKey key) => _tilesetManager.HeaderEntry[key].GetData<T>();



        }
        










        /// <summary>
        /// FormGroupManagerからのUI再描画用の処理。
        /// </summary>
        public void RefreshFromData()
        {
            LoadDataToUI(_currentTilesetNo);
        }
    }
}
