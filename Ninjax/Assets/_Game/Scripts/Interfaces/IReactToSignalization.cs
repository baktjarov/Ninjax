namespace Interfaces
{
    public interface IReactToSignalization<T>
    {
        public void OnSignalization(T noticedObject);
    }
}