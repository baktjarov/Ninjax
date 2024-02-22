using GameStates;
using GameStates.Interfaces;
using Services;
using SO;
using UnityEngine;
using Zenject;

namespace DIInstallers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private ListOfAllViews _listOfAllViews;

        public override void InstallBindings()
        {
            BindServices();
            BindLists();
        }

        private void BindServices()
        {
            Container.Bind<IGameStatesManager>().FromInstance(new GameStatesManager(Container));
            Container.Bind<SceneLoader>().FromInstance(_sceneLoader);

            InjectService.SetDIContainer(Container);
        }

        private void BindLists()
        {
            Container.Bind<ListOfAllViews>().FromInstance(_listOfAllViews);

        }
    }
}