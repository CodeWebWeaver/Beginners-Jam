public class AudioServiceSaveAdapter : ISaveable<AudioSettings> {
    private readonly IAudioService _audioService;
    public string SaveKey => "audio_settings";

    public AudioServiceSaveAdapter(IAudioService audioService) {
        _audioService = audioService;
    }

    public AudioSettings CaptureState() {
        var settings = new AudioSettings();
        foreach (var channelType in _audioService.GetSupportedChannelsTypes()) {
            settings.channels.Add(new AudioChannel {
                ChannelType = channelType,
                Volume = _audioService.GetVolume(channelType)
            });
        }
        return settings;
    }

    public void ApplyState(AudioSettings state) {
        foreach (var channel in state.channels) {
            _audioService.SetVolume(channel.ChannelType, channel.Volume);
        }
    }
}