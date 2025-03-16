using di;
using UnityEngine;

namespace ui.lobby
{
    public class ScreenLobby : BaseScreen
    {
        [SerializeField] private WidgetImageSelectItem[] _selectImageButtons;
        private UiManager _uiManager;


        private void Awake()
        {
            //тут потом будет бесконечная прокрутка картинок с переиспользованием виджетов, чтобы не спавнить сразу 1000
            for (var i = 0; i < _selectImageButtons.Length; i++)
            {
                var iCache = i+1;
                _selectImageButtons[i].Init(()=> HandleStartPuzzleClick(iCache));
            }

            _uiManager = GlobalContext.Resolve<UiManager>();
        }

        private void HandleStartPuzzleClick(int iCache)
        {
            _uiManager.ShowDialogPuzzleStart(iCache.ToString());
        }
    }
}