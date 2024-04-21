using GameStates;
using GameStates.Interfaces;
using Services;
using SO;
using TagComponents;
using UnityEngine;
using Zenject;

namespace DIInstallers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private ListOfAllViews _listOfAllViews;
        [SerializeField] private MainPlayer_Tag _mainPlayer;

        private IGameStatesManager _gameStatesManager;

        public override void InstallBindings()
        {
            BindServices();
            BindLists();
        }

        private void BindServices()
        {
            Container.Bind<SceneLoader>().FromInstance(_sceneLoader);
            Container.Bind<MainPlayer_Tag>().FromInstance(_mainPlayer);
            Container.Bind<IGameStatesManager>().FromInstance(new GameStatesManager());
        }

        private void BindLists()
        {
            Container.Bind<ListOfAllViews>().FromInstance(_listOfAllViews);
        }
    }
}