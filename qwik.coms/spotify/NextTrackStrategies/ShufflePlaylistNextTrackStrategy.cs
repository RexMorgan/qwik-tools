namespace qwik.coms.spotify.NextTrackStrategies
{
    public class ShufflePlaylistNextTrackStrategy : INextTrackStrategy
    {
        public bool Enabled()
        {
            return true;
        }

        public TrackInfo NextTrack(IPlayer player)
        {
            return player.CurrentPlaylist.Tracks.Random();
        }
    }
}