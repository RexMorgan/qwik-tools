using qwik.helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace qwik.chatscan
{
    public class ChatScanner : IDisposable
    {
        private const string ThreadName = "qwik.chatscan";
        private static readonly Lazy<ChatScanner> LazyInstance = new Lazy<ChatScanner>(() => new ChatScanner());

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _isRunning;

        public static ChatScanner Instance
        {
            get { return LazyInstance.Value; }
        }

        public void Dispose()
        {
            NewMessage = null;
        }

        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();
            Action action = () =>
            {
                Thread.CurrentThread.Name = ThreadName;
                Listen();
            };

            var taskFactory = new TaskFactory();
            taskFactory.StartNew(action, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _cancellationTokenSource.Cancel();
        }

        private void Listen()
        {
            var lineCount = Chat.GetLineCount();
            do
            {
                Thread.Sleep(10);
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                var newLineCount = Chat.GetLineCount();
                if (newLineCount == -1 || newLineCount == lineCount) continue;

                lineCount = newLineCount;

                var lastLine = Chat.GetLastLine();
                if (string.IsNullOrWhiteSpace(lastLine)) continue;
                var parts = lastLine.Split(new[] {':'}, 2);
                if (parts.Length != 2) continue;
                OnNewMessage(new ChatMessage(parts[0], parts[1].Substring(2)));
            } while (true);
            // ReSharper disable once FunctionNeverReturns
        }

        public event Action<ChatMessage> NewMessage;

        protected virtual void OnNewMessage(ChatMessage message)
        {
            var handler = NewMessage;
            if (handler != null) handler(message);
        }
    }
}