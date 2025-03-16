using System;
using di;
using UnityEngine;
using UnityEngine.UI;

namespace ui.lobby
{
    public class ScreenLobby : BaseScreen
    {
        [SerializeField] private Button _buttonStartPuzzle;
        private UiManager _uiManager;

        private void Awake()
        {
            _buttonStartPuzzle.onClick.AddListener(HandleStartPuzzleClick);
            _uiManager = GlobalContext.Resolve<UiManager>();
        }

        private void HandleStartPuzzleClick()
        {
            _uiManager.ShowDialogPuzzleStart();
        }
    }
}