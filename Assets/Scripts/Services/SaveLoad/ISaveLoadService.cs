public interface ISaveLoadService {
    void LoadAll();
    void Register<T>(ISaveable<T> saveable);
    void SaveAll();
}