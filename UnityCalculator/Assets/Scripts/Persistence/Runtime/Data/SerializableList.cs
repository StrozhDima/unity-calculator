using System;
using System.Collections.Generic;

namespace UnityCalculator.Persistence
{
    [Serializable]
    public sealed class SerializableList<T>
    {
        public List<T> Items = new();
    }
}
