using UnityEngine;

public class JsonSerializer : IDataSerializer {
    public T Deserialize<T>(string json) {
        return JsonUtility.FromJson<T>(json);
    }

    public string Serialize<T>(T state) {
        return JsonUtility.ToJson(state);
    }
}