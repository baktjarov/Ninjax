using System;

namespace Interfaces
{
    public interface ISignalizer<T>
    {
        public Action<T> onSignalize { get; set; }
        public void Signalize(T noticedObject);
    }
}