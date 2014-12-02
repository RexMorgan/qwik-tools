using System;
using System.Collections.Concurrent;

namespace qwik.helpers.Timer
{
    public class TimerFactory
    {
        private static readonly ConcurrentDictionary<TimerType, Timer> Timers = new ConcurrentDictionary<TimerType, Timer>();

        public static Timer Create(TimerType type, TimeSpan duration, Action<TimeSpan, TimeSpan> action)
        {
            if (Timers.ContainsKey(type)) throw new InvalidOperationException("A timer with that type already exists.");
            var timer = new Timer();
            if (!Timers.TryAdd(type, timer))
                throw new InvalidOperationException("Unable to add timer to the dictionary.");
            timer.DelayElapsed += action;
            timer.Start(duration);
            return timer;
        }

        public static Timer CreateUnmanaged(TimeSpan duration, Action<TimeSpan, TimeSpan> action)
        {
            var timer = new Timer();
            timer.DelayElapsed += action;
            timer.Start(duration);
            return timer;
        }

        public static Timer Get(TimerType type)
        {
            if (!Timers.ContainsKey(type)) throw new ArgumentException("A timer with that type does not exist.");
            return Timers[type];
        }

        public static void Destroy(TimerType type)
        {
            if (!Timers.ContainsKey(type)) return;
            Timer timer;
            Timers.TryRemove(type, out timer);
            if (timer == null) return;

            timer.Stop();
            timer.Dispose();
        }
    }
}