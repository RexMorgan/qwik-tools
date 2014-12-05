using System;
using qwik.chatscan;
using qwik.helpers.Settings;
using System.Linq;
using System.Windows.Forms;

namespace qwik.coms
{
    public partial class Prompt : Form
    {
        private readonly Listener.ICommandExecutor _commandExecutor;
        private readonly IAppSettings _settings;

        public Prompt(Listener.ICommandExecutor commandExecutor, IAppSettings settings)
        {
            _commandExecutor = commandExecutor;
            _settings = settings;

            InitializeComponent();
        }

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            var command = txtCommand.Text;
            txtCommand.Clear();

            var chatMessage = new ChatMessage(_settings.ScreenNames.FirstOrDefault(), command);
            _commandExecutor.Execute(chatMessage);

            lstHistory.Items.Add(command);
        }

        private void lstHistory_DoubleClick(object sender, EventArgs e)
        {
            var command = lstHistory.SelectedItem.ToString();
            var chatMessage = new ChatMessage(_settings.ScreenNames.FirstOrDefault(), command);
            _commandExecutor.Execute(chatMessage);
        }
    }
}
