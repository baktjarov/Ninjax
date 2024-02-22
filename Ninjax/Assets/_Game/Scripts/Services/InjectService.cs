using Zenject;

namespace Services
{
    public class InjectService
    {
        public static DiContainer diContainer { get; private set; }

        public static void SetDIContainer(DiContainer diContainer)
        {
            InjectService.diContainer = diContainer;
        }

        public static void Inject(object toInject)
        {
            diContainer.Inject(toInject);
        }
    }
}