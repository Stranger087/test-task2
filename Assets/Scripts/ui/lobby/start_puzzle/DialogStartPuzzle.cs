using System.Collections.Generic;
using addressable;
using di;
using game;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace ui.lobby.start_puzzle
{
    public class DialogStartPuzzle : BaseDialog
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private ButtonStartPuzzle _buttonStart;

        [SerializeField] private Image _imagePreview;

        [SerializeField] private Transform _detailLevelsContainer;

        private string _imageId;
        private MetaGameLogic _metaGameLogic;
        private int _selectedDetailLevelIndex;

        private List<WidgetPuzzleDetailLevel> _detailLevelWidgets = new();
        private UiManager _uiManager;
        private PuzzleConfig _config;

        private void Awake()
        {
            _closeButton.onClick.AddListener(HandleCloseClick);
            _buttonStart.onClick.AddListener(HandleStartClick);
            _uiManager = GlobalContext.Resolve<UiManager>();
            _metaGameLogic = GlobalContext.Resolve<MetaGameLogic>();
            _imagePreview.preserveAspect = true;
        }

        private void HandleStartClick()
        {
            if (_metaGameLogic.TryStartPuzzle(_imageId, _selectedDetailLevelIndex))
            {
                //todo start actual puzzle
            }
        }

        private void HandleCloseClick()
        {
            _uiManager.CloseDialog<DialogStartPuzzle>();
        }


        public void Open(string imageId)
        {
            _imageId = imageId;
            _selectedDetailLevelIndex = 0;
            
            var texture = AddressableUtils.LoadImmediately<Texture2D>(imageId);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            _config = _metaGameLogic.GetPuzzleConfig(imageId);

            var detailLevelPrefab =
                AddressableUtils.LoadImmediately<GameObject>(AddressableKeys.WidgetDetailLevel);

            _detailLevelsContainer.DestroyChildren(false);
            
            for (var i = 0; i < _config.DetailLevels.Count; i++)
            {
                var detailLevel = _config.DetailLevels[i];
                var detailLevelWidget = Instantiate(detailLevelPrefab, _detailLevelsContainer).GetComponent<WidgetPuzzleDetailLevel>();
                detailLevelWidget.SetData(detailLevel, i, HandleDetailLevelClick);
                _detailLevelWidgets.Add(detailLevelWidget);
            }

            _imagePreview.sprite = sprite;
            
            RefreshView();
        }

        private void HandleDetailLevelClick(int detailLevelIndex)
        {
            _selectedDetailLevelIndex = detailLevelIndex;
            RefreshView();
        }

        private void RefreshView()
        {
            for (var i = 0; i < _detailLevelWidgets.Count; i++)
            {
                _detailLevelWidgets[i].SetSelected(i == _selectedDetailLevelIndex);
            }
            
            _buttonStart.SetPrice(_config.DetailLevels[_selectedDetailLevelIndex].Price);
        }
    }
}