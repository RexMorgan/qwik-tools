using qwik.coms.Output;
using qwik.helpers.Settings;
using qwik.helpers.Timer;
using System;

namespace qwik.coms.idler
{
    public class Idler : IIdler
    {
        private readonly IAppSettings _settings;
        private readonly IIdlerFormatter _idlerFormatter;
        private readonly IOutput _output;

        private string _reason;
        private Timer _timer;

        public event Action<string> IdlingStarted;
        public event Action<TimeSpan> IdlingStopped;
        public event Action<TimeSpan, TimeSpan> IdlingNotification
        {
            add { if (_timer != null) _timer.DelayElapsed += value; }
            remove { if (_timer != null) _timer.DelayElapsed -= value; }
        }

        protected virtual void OnIdlingStopped(TimeSpan idleDuration)
        {
            IdlingStopped?.Invoke(idleDuration);
        }

        protected virtual void OnIdlingStarted(string reason)
        {
            IdlingStarted?.Invoke(reason);
        }

        public Idler(IOutput output, IAppSettings settings, IIdlerFormatter idlerFormatter)
        {
            _output = output;
            _settings = settings;
            _idlerFormatter = idlerFormatter;
        }

        public bool IsIdling { get; set; }

        public void Start()
        {
            Start(null);
        }

        public void Start(string reason)
        {
            if (IsIdling) return;
            IsIdling = true;
            _reason = reason;
            _timer = TimerFactory.Create(TimerType.Idler, TimeSpan.FromMinutes(1), NotifyChat);
            _output.Output($"[{_settings.Handle}] is going idle");
            OnIdlingStarted(_reason);
        }

        public void Stop()
        {
            IsIdling = false;
            _reason = null;
            _output.Output($"[{_settings.Handle}] is no longer idle");
            OnIdlingStopped(_idleDuration);
            _timer = null;
            TimerFactory.Destroy(TimerType.Idler);
        }

        private TimeSpan _idleDuration;

        private void NotifyChat(TimeSpan totalTimeIdled, TimeSpan notificationInterval)
        {
            _idleDuration = totalTimeIdled;
            _output.Output($"[{_settings.Handle}] has been idle for {_idlerFormatter.Format(totalTimeIdled)}");
            if (!string.IsNullOrWhiteSpace(_reason))
            {
                _output.Output($"reason: {_reason}");
            }
        }
    }
}