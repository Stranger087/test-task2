using System;
using game;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ui.lobby.start_puzzle
{
    public class WidgetPuzzleDetailLevel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _detailsCountText;
        [SerializeField] private GameObject _selectedIndicator;
        private int _index;
        private Action<int> _onClick;

        public void SetData(DetailLevelConfig detailLevel, int index, Action<int> onClick)
        {
            _index = index;
            _onClick = onClick;
            _detailsCountText.text = detailLevel.PiecesCount.ToString();
        }
        
        public void SetSelected(bool selected)
        {
            _selectedIndicator.SetActive(selected);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick?.Invoke(_index);
        }
    }
}