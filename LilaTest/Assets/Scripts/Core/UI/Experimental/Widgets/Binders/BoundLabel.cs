using Core.UI.Framework;
using Core.UI.Models;
using TMPro;
using UnityEngine;

namespace Core.UI.Binders
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    internal class BoundLabel : BaseBoundComponent<StringModel>
    {
        [SerializeField] private string _format;
        
        private TextMeshProUGUI _label;

        protected override void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
            
            base.Awake();
        }

        protected override void OnModelUpdated()
        {
            if(_label.text == BoundModel.Value) return;
            
            _label.text = !string.IsNullOrWhiteSpace(_format) ? string.Format(_format, BoundModel.Value) : BoundModel.Value;
        }
    }
}