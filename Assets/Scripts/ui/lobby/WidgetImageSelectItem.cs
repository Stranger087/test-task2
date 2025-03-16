using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ui.lobby
{
    public class WidgetImageSelectItem : MonoBehaviour, IPointerClickHandler
    {
        private Action _clickCallback;

        public void OnPointerClick(PointerEventData eventData)
        {
            _clickCallback?.Invoke();
        }

        public void Init(Action clickCallback)
        {
            _clickCallback = clickCallback;
        }
    }
}