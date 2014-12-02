using NAudio.Wave;
using qwik.spotify.Sessions;
using System;
using System.Collections.Generic;

namespace qwik.spotify
{
    public interface IPlayer
    {
        void Play(IntPtr track);
        void Pause();

        TrackInfo Track(IntPtr track);
        IEnumerable<PlaylistInfo> Playlists();
    }

    public class Player : IPlayer
    {
        private readonly ISession _session;

        private readonly IWavePlayer _wavePlayer;
        private readonly BufferedWaveProvider _waveProvider;

        private int _stutterCount;

        public Player(ISession session)
        {
            _session = session;

            _lazyPlaylists = new Lazy<IEnumerable<PlaylistInfo>>(() => _session.Playlists());
            
            _wavePlayer = new WaveOut();
            _waveProvider = new BufferedWaveProvider(new WaveFormat())
            {
                BufferDuration = TimeSpan.FromSeconds(10)
            };

            _wavePlayer.Init(_waveProvider);

            _session.OnAudioDataReceived += SessionOnAudioDataReceived;
            _session.OnGetAudioBufferStats += SessionOnGetAudioBufferStats;
            _session.OnEndOfTrack += TrackEnded;
        }

        public void Play(IntPtr track)
        {
            _wavePlayer.Play();
            _session.Play(track);
        }

        public void Pause()
        {
            _session.Pause();
        }

        public TrackInfo Track(IntPtr track)
        {
            return new TrackInfo(track);
        }

        private readonly Lazy<IEnumerable<PlaylistInfo>> _lazyPlaylists;

        public IEnumerable<PlaylistInfo> Playlists()
        {
            return _lazyPlaylists.Value;
        }

        private void CheckStutter()
        {
            if (_waveProvider.BufferedBytes == 0) ++_stutterCount;
        }

        private void SessionOnAudioDataReceived(AudioDataReceivedEventArgs audioDataReceivedEventArgs)
        {
            CheckStutter();
            var bytesRemainingInBuffer = _waveProvider.BufferLength - _waveProvider.BufferedBytes;
            var amountToAddToBuffer = Math.Min(audioDataReceivedEventArgs.Bytes.Length, bytesRemainingInBuffer);
            _waveProvider.AddSamples(audioDataReceivedEventArgs.Bytes, 0, amountToAddToBuffer);
            audioDataReceivedEventArgs.NumberOfFramesConsumed = amountToAddToBuffer/audioDataReceivedEventArgs.BytesPerFrame;
        }

        private void SessionOnGetAudioBufferStats(GetAudioBufferStatsEventArgs audioBufferStatsEventArgs)
        {
            CheckStutter();
            audioBufferStatsEventArgs.Samples = _waveProvider.BufferedBytes/2;
            audioBufferStatsEventArgs.Stutter = _stutterCount;
            _stutterCount = 0;
        }

        private void TrackEnded(TrackEndedEventArgs trackEndedEventArgs)
        {
            
        }
    }
}