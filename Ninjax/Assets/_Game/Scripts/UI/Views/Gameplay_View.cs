using Characters;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class Gameplay_View : ViewBase
    {
        [Header("Buttons")]
        [SerializeField] private Button _pauseButton;

        private Pause_View _pauseView;
        private Win_View _winView;
        private Louse_View _louseView;

        private Player _player;
        private Finish _finish;

        public void Construct(Pause_View pauseView, Win_View winView, Louse_View louseView, Player player, Finish finish)
        {
            _pauseView = pauseView;
            _winView = winView;
            _louseView = louseView;
            _player = player;
            _finish = finish;
        }

        public override void Open()
        {
            base.Open();

            _pauseButton.onClick.AddListener(OpenPause);
            _player.onDie += OpenLouse;
            _finish.isFinish += OpenWin;
        }

        public override void Close()
        {
            base.Close();

            _pauseButton.onClick.RemoveListener(OpenPause);
            _player.onDie -= OpenLouse;
            _finish.isFinish -= OpenWin;
        }

        private void OpenPause()
        {
            _pauseView?.Open();
        }

        private void OpenWin()
        {
            _winView?.Open();
        }

        private void OpenLouse()
        {
            _louseView?.Open();
        }
    }
}
