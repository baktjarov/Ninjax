using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenu_View : ViewBase
    {
        public Action onPlayButtonClicked;
        public Action onQuitButtonClicked;

        [Header("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        public override void Open()
        {
            base.Open();

            _playButton.onClick.AddListener(Play);
            _quitButton.onClick.AddListener(Quit);
        }

        public override void Close()
        {
            base.Close();

            _playButton.onClick.RemoveListener(Play);
            _quitButton.onClick.RemoveListener(Quit);

        }

        private void Play()
        {
            onPlayButtonClicked?.Invoke();
        }

        private void Quit()
        {
            onQuitButtonClicked?.Invoke();
        }
    }
}