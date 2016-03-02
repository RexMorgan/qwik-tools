using System;
using System.Threading;
using System.Threading.Tasks;

namespace qwik.helpers.Timer
{
    public class Timer : IDisposable
    {
        private const string ThreadName = "qwik.timer";
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private bool _isRunning;
        private TimeSpan _notificationDelay;

        public void Dispose()
        {
            DelayElapsed = null;
        }

        public void Start(TimeSpan notificationDelay)
        {
            if (_isRunning) Stop();
            _notificationDelay = notificationDelay;
            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();
            Action action = () =>
            {
                Thread.CurrentThread.Name = ThreadName;
                Idle();
            };
            var taskFactory = new TaskFactory();
            taskFactory.StartNew(action, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _cancellationTokenSource.Cancel();
        }

        private void Idle()
        {
            var totalTimeElapsed = TimeSpan.Zero;
            do
            {
                Thread.Sleep(_notificationDelay);
                if (_cancellationTokenSource.IsCancellationRequested) break;
                totalTimeElapsed = totalTimeElapsed.Add(_notificationDelay);
                OnDelayElapsed(totalTimeElapsed, _notificationDelay);
            } while (!_cancellationTokenSource.IsCancellationRequested);
        }

        public event Action<TimeSpan, TimeSpan> DelayElapsed;

        protected virtual void OnDelayElapsed(TimeSpan totalIdledTime, TimeSpan delay)
        {
            DelayElapsed?.Invoke(totalIdledTime, delay);
        }
    }
}