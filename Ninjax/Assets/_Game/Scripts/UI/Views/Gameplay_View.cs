using Characters;
using Gameplay;
using Interfaces;
using TagComponents;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class Gameplay_View : ViewBase
    {
        [Header("Buttons")]
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Slider _playerHealthSlider;

        private Pause_View _pauseView;
        private Win_View _winView;
        private Louse_View _louseView;

        private IHealth _player;
        private Finish _finish;

        public void Construct(Pause_View pauseView,
            Win_View winView,
            Louse_View louseView,
            MainPlayer_Tag player,
            Finish finish)
        {
            _pauseView = pauseView;
            _winView = winView;
            _louseView = louseView;
            _player = player.GetComponent<PlayerHealth>();
            _finish = finish;

            if (_playerHealthSlider == null) { _playerHealthSlider = GetComponentInChildren<Slider>(true); }

            _playerHealthSlider.maxValue = _player.maxHealth;
        }

        public override void Open()
        {
            base.Open();

            _pauseButton.onClick.AddListener(OpenPause);
            _player.onDie += OpenLoose;
            _finish.isFinish += OpenWin;

            _player.onHealthChanged += UpdateHealthBar;

            UpdateHealthBar(_player.currentHealth);
        }

        public override void Close()
        {
            base.Close();

            _pauseButton.onClick.RemoveListener(OpenPause);
            _player.onDie -= OpenLoose;
            _finish.isFinish -= OpenWin;

            _player.onHealthChanged -= UpdateHealthBar;
        }

        private void OpenPause()
        {
            _pauseView?.Open();
        }

        private void OpenWin()
        {
            _winView?.Open();
        }

        private void OpenLoose()
        {
            _louseView?.Open();
        }

        private void UpdateHealthBar(float healthValue)
        {
            _playerHealthSlider.value = healthValue;
        }
    }
}
