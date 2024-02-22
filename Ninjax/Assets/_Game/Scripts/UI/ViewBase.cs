using UnityEngine;
using System;

namespace UI.Views
{
    public abstract class ViewBase : MonoBehaviour
    {
        private static Action<ViewBase> _onAViewOpen;
        private ViewBase _openOnClose;

        protected virtual void Awake()
        {
            _onAViewOpen += OnAViewOpen;
            TurnOff();
        }

        protected void OnDestroy()
        {
            _onAViewOpen -= OnAViewOpen;
        }

        private void OnAViewOpen(ViewBase view)
        {
            if (view != this)
            {
                TurnOff();
            }
        }

        public void SetOpenOnCloseView(ViewBase view)
        {
            _openOnClose = view;
        }

        private void TurnOff()
        {
            gameObject.SetActive(false);
        }

        private void TurnOn()
        {
            gameObject.SetActive(true);
        }

        public virtual void Open()
        {
            TurnOn();
            _onAViewOpen?.Invoke(this);
        }

        public virtual void Close()
        {
            TurnOff();
            if (_openOnClose != null)
            {
                _openOnClose.Open();
            }
        }
    }
}