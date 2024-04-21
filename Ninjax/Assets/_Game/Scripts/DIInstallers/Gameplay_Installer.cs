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
        [SerializeField] private PlayerStart_Tag _playerStart;
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private Finish _finish;

        private void Awake()
        {
            if (_videoCameraSystem == null) { _videoCameraSystem = FindAnyObjectByType<VideoCameraSystem>(FindObjectsInactive.Include); }
            if (_playerStart == null) { _playerStart = FindAnyObjectByType<PlayerStart_Tag>(FindObjectsInactive.Include); }
            if (_followCamera == null) { _followCamera = FindAnyObjectByType<FollowCamera>(FindObjectsInactive.Include); }
            if (_finish == null) { _finish = FindAnyObjectByType<Finish>(FindObjectsInactive.Include); }
        }

        public override void InstallBindings()
        {
            InjectService.SetDIContainer(Container);

            Container.Bind<ISignalization<MainPlayer_Tag>>().FromInstance(_videoCameraSystem);
            Container.Bind<PlayerStart_Tag>().FromInstance(_playerStart);
            Container.Bind<FollowCamera>().FromInstance(_followCamera);
            Container.Bind<Finish>().FromInstance(_finish);
        }
    }
}