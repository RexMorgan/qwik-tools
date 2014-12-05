namespace qwik.coms.Output
{
    public interface IChatRateLimiter
    {
        bool IsRateLimited();
        void MessageSent();
    }
}