namespace qwik.coms.spotify.NextTrackStrategies
{
    public interface INextTrackStrategy
    {
        bool Enabled();
        TrackInfo NextTrack(IPlayer player);
    }
}