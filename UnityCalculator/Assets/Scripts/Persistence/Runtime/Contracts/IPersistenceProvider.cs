namespace UnityCalculator.Persistence
{
    public interface IPersistenceProvider
    {
        void Save(string key, string value);
        string Load(string key, string defaultValue);
        bool HasKey(string key);
    }
}
