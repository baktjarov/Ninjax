using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class Pause_View : ViewBase
    {
        public Action onMainMenuButtonClicked;

        [Header("Buttons")]
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _mainMenuButton;

        public override void Open()
        {
            base.Open();

            _backButton.onClick.AddListener(Close);
            _mainMenuButton.onClick.AddListener(Exit);
        }

        public override void Close()
        {
            base.Close();

            _backButton.onClick.RemoveListener(Close);
            _mainMenuButton.onClick.RemoveListener(Exit);
        }

        private void Exit()
        {
            onMainMenuButtonClicked?.Invoke();
        }
    }
}