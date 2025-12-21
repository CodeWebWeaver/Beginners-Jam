public interface ISaveStorage {
    void Save(string key, string value);
    string Load(string key);
    bool HasKey(string key);
    void Commit();
}
