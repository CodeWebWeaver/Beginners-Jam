using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadService : ISaveLoadService {
    private readonly IDataSerializer _serializer;
    private readonly ISaveStorage _storage;
    private readonly List<(string key, Action save, Action load)> _operations = new();

    public SaveLoadService(IDataSerializer serializer, ISaveStorage storage) {
        _serializer = serializer;
        _storage = storage;
    }

    public void Register<T>(ISaveable<T> saveable) {
        Action saveAction = () => {
            try {
                T state = saveable.CaptureState();
                string json = _serializer.Serialize(state);
                _storage.Save(saveable.SaveKey, json);
            } catch (Exception ex) {
                Debug.LogError($"Save failed for '{saveable.SaveKey}': {ex}");
            }
        };

        Action loadAction = () => {
            try {
                if (!_storage.HasKey(saveable.SaveKey)) return;

                string json = _storage.Load(saveable.SaveKey);
                T state = _serializer.Deserialize<T>(json);
                saveable.ApplyState(state);
            } catch (Exception ex) {
                Debug.LogError($"Load failed for '{saveable.SaveKey}': {ex}");
            }
        };

        _operations.Add((saveable.SaveKey, saveAction, loadAction));
    }

    public void SaveAll() {
        foreach (var (key, save, _) in _operations)
            save();

        _storage.Commit();
        Debug.Log($"Saved {_operations.Count} items");
    }

    public void LoadAll() {
        foreach (var (key, _, load) in _operations)
            load();

        Debug.Log($"Loaded {_operations.Count} items");
    }
}
