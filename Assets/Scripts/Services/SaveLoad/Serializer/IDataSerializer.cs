public interface IDataSerializer {
    string Serialize<T>(T state);
    T Deserialize<T>(string json);
}
