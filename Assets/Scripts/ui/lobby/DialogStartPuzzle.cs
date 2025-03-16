using System;
using di;
using UnityEngine;
using UnityEngine.UI;

namespace ui.lobby
{
    public class DialogStartPuzzle : BaseDialog
    {
        [SerializeField] private Button _closeButton;

        private UiManager _uiManager;
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(HandleCloseClick);
            _uiManager = GlobalContext.Resolve<UiManager>();
        }

        private void HandleCloseClick()
        {
            _uiManager.CloseDialog<DialogStartPuzzle>();
        }


        public void Open(string pictureId)
        {
        }
    }
}