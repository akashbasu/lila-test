using System.Collections.Generic;
using Core.UI.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace LilaTest
{
    internal class GridUiWidget : BaseBoundComponent<GridModel>
    {
        [SerializeField] private GridLayoutGroup _layout;
        [SerializeField] private GameObject _cache;
        
        private GameObject _gridRoot;

        private int ManagedCount => _cachedWidgets.Count + _activeWidgets.Count; 
        private readonly List<GridItemUiWidget> _cachedWidgets = new List<GridItemUiWidget>();
        private readonly List<GridItemUiWidget> _activeWidgets = new List<GridItemUiWidget>();
        
        protected override void Awake()
        {
            base.Awake();
            
            Initialize();
            CacheAll();
        }

        protected override void OnModelUpdated()
        {
            UpdateGrid();
        }

        private void Initialize()
        {
            _gridRoot = _layout.gameObject;
            
            _cache.SetActive(false);
            _gridRoot.SetActive(true);
            
            _cachedWidgets.Clear();
            _activeWidgets.Clear();
            
            foreach (Transform child in _cache.transform)
            {
                if (child.TryGetComponent<GridItemUiWidget>(out var gridItemUiWidget))
                {
                    _cachedWidgets.Add(gridItemUiWidget);
                }
            }

            foreach (Transform child in _gridRoot.transform)
            {
                if (child.TryGetComponent<GridItemUiWidget>(out var gridItemUiWidget))
                {
                    _activeWidgets.Add(gridItemUiWidget);
                }
            }
        }

        private void CacheAll()
        {
            while (_activeWidgets.Count > 0) CacheElement(_activeWidgets[0]);
        }

        private void UpdateGrid()
        {
            _gridRoot.SetActive(false);
            
            if(BoundModel == null) return;

            CacheAll();
            SetLayout();
            if (NeedsCachePadding()) PadCache();
            PopulateGridWithData();
            
            _gridRoot.SetActive(true);
        }

        private void SetLayout()
        {
            _layout.constraint = BoundModel.Constraint;
            _layout.constraintCount = BoundModel.Columns;
        }

        private bool NeedsCachePadding() => BoundModel.GridCount > ManagedCount;

        private void PadCache()
        {
            var template = _cachedWidgets.Count > 0 ? _cachedWidgets[0] : _activeWidgets[0];
            var difference = BoundModel.GridCount - ManagedCount;

            while (difference > 0)
            {
                var newCacheEntry = Instantiate(template.gameObject, _cache.transform);
                newCacheEntry.SetActive(true);
                CacheElement(newCacheEntry.GetComponent<GridItemUiWidget>());
                difference--;
            }
        }

        private void PopulateGridWithData()
        {
            var itemsToPopulate = BoundModel.GridCount;

            while (itemsToPopulate > 0)
            {
                UnCacheElement(_cachedWidgets[0]);
                itemsToPopulate--;
            }
        }
        
        private void CacheElement(GridItemUiWidget widget)
        {
            widget.transform.SetParent(_cache.transform);
            widget.transform.SetAsLastSibling();
            _cachedWidgets.Remove(widget);
            _activeWidgets.Remove(widget);
            _cachedWidgets.Add(widget);
        }

        private void UnCacheElement(GridItemUiWidget widget)
        {
            widget.transform.SetParent(_gridRoot.transform);
            widget.transform.SetAsLastSibling();
            widget.Reorient();
            _cachedWidgets.Remove(widget);
            _activeWidgets.Remove(widget);
            _activeWidgets.Add(widget);
        }
    }
}