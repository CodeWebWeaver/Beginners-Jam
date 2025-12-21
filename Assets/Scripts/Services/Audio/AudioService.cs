using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService {
    void SetVolume(AudioChannelType channel, float normalizedVolume);
    float GetVolume(AudioChannelType channel);
    List<AudioChannelType> GetSupportedChannelsTypes();
}