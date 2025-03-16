using System.Collections.Generic;
using game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace ui.lobby.start_puzzle
{
    public class ButtonStartPuzzle : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<PriceType, List<GameObject>> _states;

        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _layoutContainer;

        public Button.ButtonClickedEvent onClick => _button.onClick;

        public void SetPrice(PriceData priceData)
        {
            //эту штуку потом вынести в универсальный WidgetPrice
            foreach (var state in _states.Values)
            {
                foreach (var stateChild in state)
                {
                    stateChild.SetActive(false);
                }
            }

            var currentState = _states[priceData.Type];
            foreach (var childState in currentState)
            {
                childState.SetActive(true);
            }
            
            _priceText.text = priceData.Amount.ToString();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutContainer);
        }
        
        
    }
}