using Services;
using System;
using UI.Views;
using Object = UnityEngine.Object;

namespace GameStates
{
    public class Gameplay_GameState_View
    {
        public Action onMainMenuClicked;
        public Action onNextClicked;

        private Gameplay_GameState_Model _model;

        private Gameplay_View _gameplayView;
        private Pause_View _pauseView;
        private Win_View _winView;
        private Louse_View _louseView;

        public Gameplay_GameState_View(Gameplay_GameState_Model model)
        {
            _model = model;
        }

        public void Initialize()
        {
            InjectService.Inject(this);

            _gameplayView = Object.Instantiate(_model.listOfAllViews.GetView<Gameplay_View>());
            _pauseView = Object.Instantiate(_model.listOfAllViews.GetView<Pause_View>());
            _winView = Object.Instantiate(_model.listOfAllViews.GetView<Win_View>());
            _louseView = Object.Instantiate(_model.listOfAllViews.GetView<Louse_View>());

            _gameplayView.Construct(_pauseView, _winView, _louseView, _model.mainPlayer, _model.finish);
            _pauseView.SetOpenOnCloseView(_gameplayView);

            _gameplayView.Open();

            _pauseView.onMainMenuButtonClicked += OnMainMenuClicked;
            _winView.onMainMenuButtonClicked += OnMainMenuClicked;
            _winView.onNextButtonClicked += OnNextClicked;
            _louseView.onMainMenuButtonClicked += OnMainMenuClicked;
            _louseView.onNextButtonClicked += OnNextClicked;
        }

        private void OnMainMenuClicked()
        {
            onMainMenuClicked?.Invoke();
        }

        private void OnNextClicked()
        {
            onNextClicked?.Invoke();
        }
    }
}