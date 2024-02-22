using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class Louse_View : ViewBase
    {
        public Action onMainMenuButtonClicked;
        public Action onNextButtonClicked;

        [Header("Buttons")]
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _mainMenuButton;

        public override void Open()
        {
            base.Open();

            _nextButton.onClick.AddListener(Next);
            _mainMenuButton.onClick.AddListener(Exit);
        }

        public override void Close()
        {
            base.Close();

            _nextButton.onClick.RemoveListener(Next);
            _mainMenuButton.onClick.RemoveListener(Exit);
        }

        private void Exit()
        {
            onMainMenuButtonClicked?.Invoke();
        }

        private void Next()
        {
            onNextButtonClicked?.Invoke();
        }
    }
}