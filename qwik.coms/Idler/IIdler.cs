using System;

namespace qwik.coms.idler
{
    public interface IIdler
    {
        event Action<string> IdlingStarted;
        event Action<TimeSpan> IdlingStopped;
        event Action<TimeSpan, TimeSpan> IdlingNotification;

        bool IsIdling { get; set; }

        void Start();
        void Start(string reason);
        void Stop();
    }
}