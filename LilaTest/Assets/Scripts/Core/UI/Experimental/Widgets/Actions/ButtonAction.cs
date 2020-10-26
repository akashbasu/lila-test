using Core.Command;
using Core.IoC;
using Core.UI.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Ui.Binders
{
    [RequireComponent(typeof(Button))]
    internal class ButtonAction : MonoBehaviour
    {
        [SerializeField] private string _onClickEvent;
        
        private IBoundComponent _data;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _data = GetComponent<IBoundComponent>();
        }

        private void Start()
        {
            Injector.ResolveDependencies(this);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if(string.IsNullOrEmpty(_onClickEvent)) return;

            if (_data == null) new EventCommand(_onClickEvent).Execute();
            else new EventCommand(_onClickEvent, _data.FullKey).Execute();
        }
    }
}