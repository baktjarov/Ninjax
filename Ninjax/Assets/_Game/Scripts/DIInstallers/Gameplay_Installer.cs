using Gameplay;
using Interfaces;
using Services;
using TagComponents;
using UnityEngine;
using Zenject;

namespace DIInstallers
{
    public class Gameplay_Installer : MonoInstaller
    {
        [SerializeField] private VideoCameraSystem _videoCameraSystem;

        public override void InstallBindings()
        {
            InjectService.SetDIContainer(Container);

            Container.Bind<ISignalization<MainPlayer_TagComponent>>().FromInstance(_videoCameraSystem);
        }
    }
}