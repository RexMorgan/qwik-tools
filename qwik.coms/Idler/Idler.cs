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
            var handler = IdlingStopped;
            if (handler != null) handler(idleDuration);
        }

        protected virtual void OnIdlingStarted(string reason)
        {
            var handler = IdlingStarted;
            if (handler != null) handler(reason);
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
            _output.Formatted("[{0}] is going idle", _settings.Handle);
            OnIdlingStarted(_reason);
        }

        public void Stop()
        {
            IsIdling = false;
            _reason = null;
            _output.Formatted("[{0}] is no longer idle", _settings.Handle);
            OnIdlingStopped(_idleDuration);
            _timer = null;
            TimerFactory.Destroy(TimerType.Idler);
        }

        private TimeSpan _idleDuration;

        private void NotifyChat(TimeSpan totalTimeIdled, TimeSpan notificationInterval)
        {
            _idleDuration = totalTimeIdled;
            _output.Formatted("[{0}] has been idle for {1}", _settings.Handle, _idlerFormatter.Format(totalTimeIdled));
            if (!string.IsNullOrWhiteSpace(_reason))
            {
                _output.Formatted("reason: {0}", _reason);
            }
        }
    }
}