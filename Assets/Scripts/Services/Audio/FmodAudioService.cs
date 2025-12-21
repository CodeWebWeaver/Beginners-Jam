using FMODUnity;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FmodAudioService : IAudioService {
    // Словник для маппінгу каналів на FMOD шини
    private readonly Dictionary<AudioChannelType, string> busNames = new() {
        { AudioChannelType.Master, "bus:/" },
        { AudioChannelType.Music, "bus:/Music" },
        { AudioChannelType.SFX, "bus:/SFX" },
        { AudioChannelType.Ambience, "bus:/Ambience" }
    };

    private Dictionary<AudioChannelType, FMOD.Studio.Bus> buses = new();

    public string SaveKey => "audio_settings";

    public FmodAudioService() {
        // Ініціалізація словника шин
        InitDictionary();
    }

    private void InitDictionary() {
        foreach (var kvp in busNames) {
            buses[kvp.Key] = FMODUnity.RuntimeManager.GetBus(kvp.Value);
        }
    }

    public float GetVolume(AudioChannelType channel) {
        if (!buses.TryGetValue(channel, out var bus)) {
            Debug.LogError($"Bus for channel {channel} not found!");
            return 0f;
        }


        bus.getVolume(out float volume);
        return volume;
    }

    public void SetVolume(AudioChannelType channel, float normalizedVolume) {
        if (!buses.TryGetValue(channel, out var bus)) {
            Debug.LogError($"Bus for channel {channel} not found!");
            return;
        }

        bus.setVolume(Mathf.Clamp01(normalizedVolume));
    }

    public void PlaySound(string eventPath) {
        FMODUnity.RuntimeManager.PlayOneShot(eventPath);
    }

    public List<AudioChannelType> GetSupportedChannelsTypes() {
        return new List<AudioChannelType>(buses.Keys);
    }
}

[Serializable]
public class AudioSettings {
    public List<AudioChannel> channels = new();
}

[Serializable]
public class AudioChannel {
    public string name;
    public AudioChannelType ChannelType;
    public float Volume;
}

public interface IPersistentObject<T> {
    T LoadData();
    void SaveData(T data);
}