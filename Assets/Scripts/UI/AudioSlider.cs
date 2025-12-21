using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class AudioSlider : MonoBehaviour {
    [Header("References")]
    [SerializeField] private SliderValue sliderValue;

    [Header("Audio Settings")]
    [SerializeField] private AudioChannelType channel = AudioChannelType.Master;
    [Inject] IAudioService audioSystem;
    public AudioChannelType Channel => channel;

    public UnityEvent<AudioChannelType, float> OnVolumeChanged;

    private void Start() {
        sliderValue.OnValueChanged.AddListener(HandleSliderValueChanged);
        sliderValue.SetValue(audioSystem.GetVolume(channel), false);
    }

    private void HandleSliderValueChanged(float normalizedValue) {
        OnVolumeChanged?.Invoke(channel, normalizedValue);
        audioSystem.SetVolume(channel, normalizedValue);
    }

    public void SetVolume(float normalizedValue, bool triggerEvent = false) {
        sliderValue.SetValue(normalizedValue, triggerEvent);
    }

    public float GetVolume() {
        return sliderValue.GetValue();
    }

    private void OnDestroy() {
        sliderValue.OnValueChanged.RemoveListener(HandleSliderValueChanged);
    }
}
