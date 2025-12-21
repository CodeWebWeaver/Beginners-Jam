public class PlayerPrefsStorage : ISaveStorage {
    public void Save(string key, string value) {
        UnityEngine.PlayerPrefs.SetString(key, value);
    }
    public string Load(string key) {
        return UnityEngine.PlayerPrefs.GetString(key);
    }
    public bool HasKey(string key) {
        return UnityEngine.PlayerPrefs.HasKey(key);
    }
    public void Commit() {
        UnityEngine.PlayerPrefs.Save();
    }
}