using System;
using Services;
using SO;
using UI.Views;
using Zenject;
using Object = UnityEngine.Object;

namespace GameStates
{
    public class MainMenu_GameState_ViewsManager
    {
        public Action onPlayClicked;
        public Action onQuitClicked;

        [Inject] private ListOfAllViews _listOfAllViews;

        private MainMenu_View _mainMenuView;

        public void Initialize()
        {
            InjectService.Inject(this);

            MainMenu_View mainMenu_View_Prefab = _listOfAllViews.GetView<MainMenu_View>();
            _mainMenuView = Object.Instantiate(mainMenu_View_Prefab);

            _mainMenuView.Open();

            _mainMenuView.onPlayButtonClicked += OnPlayClicked;
            _mainMenuView.onQuitButtonClicked += OnQuitClicked;
        }

        private void OnPlayClicked()
        {
            onPlayClicked?.Invoke();
        }

        private void OnQuitClicked()
        {
            onQuitClicked?.Invoke();
        }
    }
}