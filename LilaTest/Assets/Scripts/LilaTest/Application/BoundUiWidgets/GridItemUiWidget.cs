using Core.UI.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LilaTest
{
    internal class GridItemUiWidget : BaseBoundComponent<GridItemModel>
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUgui;
        [SerializeField] private Image _image;
        
        public void Reorient()
        {
            base.Reorient((transform.GetSiblingIndex() + 1).ToString());
        }

        protected override void OnModelUpdated()
        {
            if (BoundModel == null) SetDefault();
            else if (!_uiKey.Equals(BoundModel.Id.ToString())) Reorient(BoundModel.Id.ToString());
            else if (!BoundModel.IsSelected) SetDefault();
            else if (BoundModel.IsSelected) SetSelected();
        }

        protected override void OnEnable()
        {
            _uiKey = BoundModel?.Id.ToString();
            base.OnEnable();
        }

        private void SetDefault()
        {
            _textMeshProUgui.text = string.Empty;
            _image.color = Color.white;
        }

        private void SetSelected()
        {
            _textMeshProUgui.text = BoundModel.Id.ToString();
            _image.color = BoundModel.Color;
        }
    }
}