using qwik.coms.Commands;
using qwik.coms.Listener;
using qwik.coms.Output;
using qwik.coms.Secretary;
using qwik.helpers;
using qwik.helpers.Chatters;
using qwik.helpers.Settings;
using qwik.spotify;
using qwik.spotify.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace qwik.coms
{
    public partial class Main : Form
    {
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IOutput _output;
        private readonly IScreenNameRetriever _screenNameRetriever;
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;
        private readonly ISecretary _secretary;
        private readonly IPlayer _player;
        private readonly ISession _session;

        public Main(IAppSettings settings, IOutput output, ICommandListener commandListener,
            IEnumerable<ICommandHandler> commandHandlers, IAppSettingsWriter settingsWriter,
            IScreenNameRetriever screenNameRetriever, ISecretary secretary, ISession session, IPlayer player)
        {
            _settings = settings;
            _output = output;
            _commandHandlers = commandHandlers;
            _settingsWriter = settingsWriter;
            _screenNameRetriever = screenNameRetriever;
            _secretary = secretary;
            _session = session;
            _player = player;
            InitializeComponent();

            commandListener.Start();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lstCommands.Items.AddRange(_commandHandlers.Select(x => string.Join("/", x.Commands)).Cast<object>().ToArray());

            _output.Formatted("qwik.coms¹ loaded by [{0}]", _settings.Handle ?? "new user");
            _output.Formatted("{0} commands", _commandHandlers.Count());

            if (string.IsNullOrWhiteSpace(_settings.Handle))
            {
                _output.Formatted("type user [handle]");
            }

            try
            {
                _session.Relogin();
            }
            catch (SpotifyException)
            {
                var spotifyLogin = new SpotifyLogin();
                if (spotifyLogin.ShowDialog() == DialogResult.OK)
                {
                    _session.Login(spotifyLogin.Username, spotifyLogin.Password, spotifyLogin.RememberMe);
                }
            }

            if (!_settings.ScreenNames.Any())
            {
                var screenname = _screenNameRetriever.GetScreenName();
                if (!string.IsNullOrWhiteSpace(screenname))
                {
                    _settings.ScreenNames.Fill(new ScreenName(screenname));
                    _settingsWriter.SaveSettings(_settings);
                }
            }
            
            if (_settings.Secretary) _secretary.Start();

            _session.OnLoggedOut += ptr => Application.Exit();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            _output.Formatted("qwik.coms¹ unloaded by [{0}]", _settings.Handle);
        }
    }
}