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

            // tbpHeader
            CtrlHelper.SetControlsEnabled(tbpHeader, state);
            CtrlHelper.ResetControls(tbpHeader);

            // tbpAnim
            CtrlHelper.SetControlsEnabled(tbpAnim, state);
            CtrlHelper.ResetControls(tbpAnim);
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
                    var desc = $"[{this.Text}]画像アドレス(ID:{_currentTilesetNo:D8})";

                    _tilesetManager.TilesetData.UpdateData(_undoManager, parsedValue, desc);
                });
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

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }

        private void LoadDataToUI(int tilsetNo)
        {
            _tilesetManager = new TilesetManager(_sharedData);
            _tilesetManager.ReadHeader(tilsetNo);

            // UIを有効化
            UpdateTabPageState(true);
            // UIに反映
            cmbImageCompType.SelectedIndex = _tilesetManager.TilesetData.ImageCompType;
            cmbPaletteType.SelectedIndex = (int)_tilesetManager.TilesetData.PaletteType;
            txtImageOffset.Text = _tilesetManager.TilesetData.ImageOffset.ParseIntToString();
            txtPaletteOffset.Text = _tilesetManager.TilesetData.PaletteOffset.ParseIntToString();
            txtBlockDataTableOffset.Text = _tilesetManager.TilesetData.BlockDataTableOffset.ParseIntToString();
            txtAnimHeaderOffset.Text = _tilesetManager.TilesetData.AnimHeaderOffset.ParseIntToString();
            txtBlockAttrTableOffset.Text = _tilesetManager.TilesetData.BlockAttrTableOffset.ParseIntToString();
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
