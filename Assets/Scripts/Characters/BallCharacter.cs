using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class BallCharacter : MonoBehaviour {
    public EventReference blazingSoundEvent;

    private EventInstance blazingInstance;

    private void Start() {
        blazingInstance = RuntimeManager.CreateInstance(blazingSoundEvent);

        // Прив’язуємо звук до GameObject
        RuntimeManager.AttachInstanceToGameObject(
            blazingInstance,
            transform
        );

        blazingInstance.start();
    }

    private void OnDestroy() {
        blazingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        blazingInstance.release();
    }
}
