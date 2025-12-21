public interface ISaveable<T> {
    string SaveKey { get; }
    T CaptureState();
    void ApplyState(T state);
}
