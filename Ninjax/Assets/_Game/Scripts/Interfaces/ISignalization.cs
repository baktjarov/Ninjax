using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ISignalization<T>
    {
        public IReadOnlyList<T> noticedObjects { get; }

        public Action<T> onSignalizationTriggerred { get; set; }
    }
}