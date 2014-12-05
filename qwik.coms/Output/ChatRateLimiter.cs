using qwik.helpers.Timer;
using System;
using System.Threading;

namespace qwik.coms.Output
{
    /// <summary>
    /// Provides a way to see if we should slow down sending to the chat.
    /// 
    /// I'm not super happy with this, but it does keep you from being logged out for scrolling. The messages comes in waves of 3 with a second pause between.
    ///     I feel like it would be better if it was a smooth rate limit instead of spiky.
    /// </summary>
    public class ChatRateLimiter : IChatRateLimiter
    {
        private const int Rate = 3; // messages
        private const double Per = 1.25; // seconds

        private static int _bucket;
        private static long _lastMessageSent;
        
        static ChatRateLimiter()
        {
            _bucket = Rate;
            TimerFactory.CreateUnmanaged(TimeSpan.FromSeconds(Per), FillBucket);
        }

        private static void FillBucket(TimeSpan arg1, TimeSpan arg2)
        {
            if (_bucket < Rate && TimeSpan.FromTicks(DateTime.Now.Ticks - _lastMessageSent) > TimeSpan.FromSeconds(Per))
            {
                Interlocked.Exchange(ref _bucket, Rate);
            }
        }

        public bool IsRateLimited()
        {
            return _bucket <= 0;
        }

        public void MessageSent()
        {
            Interlocked.Decrement(ref _bucket);
            Interlocked.Exchange(ref _lastMessageSent, DateTime.Now.Ticks);
        }
    }
}