using System.Collections.Generic;
using System.Linq;
using addressable;
using ui.lobby;
using UnityEngine;

namespace ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Transform _screensContainer;
        [SerializeField] private Transform _dialogsContainer;

        private readonly List<BaseDialog> _openedDialogs = new();
        private BaseScreen _currentScreen;

        public void Initialize()
        {
        }

        public void ShowDialogPuzzleStart()
        {
            var prefab = AddressableUtils.LoadImmediately<GameObject>(AddressableKeys.DialogStartPuzzle);
            var dialog = Instantiate(prefab, _dialogsContainer).GetComponent<DialogStartPuzzle>();
            dialog.Open(null);
            _openedDialogs.Add(dialog);
        }

        public void CloseDialog<T>() where T : BaseDialog
        {
            var dialog = _openedDialogs.FirstOrDefault(x => x is T);
            if (dialog != null)
            {
                //todo: pooling
                Destroy(dialog.gameObject);
                _openedDialogs.Remove(dialog);
            }
        }

        public void ShowMainScreen()
        {
            ShowScreenByAddressableKey(AddressableKeys.ScreenLobby);
        }

        private void ShowScreenByAddressableKey(string addressableKey)
        {
            if (_currentScreen != null) Destroy(_currentScreen.gameObject);

            var prefab = AddressableUtils.LoadImmediately<GameObject>(addressableKey);
            _currentScreen = Instantiate(prefab, _screensContainer).GetComponent<BaseScreen>();
        }
    }
}