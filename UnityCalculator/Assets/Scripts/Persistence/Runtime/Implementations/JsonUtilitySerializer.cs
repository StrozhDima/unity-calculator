using UnityEngine;

namespace UnityCalculator.Persistence
{
    public sealed class JsonUtilitySerializer : ISerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonUtility.ToJson(data);
        }

        public T Deserialize<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }
    }
}
