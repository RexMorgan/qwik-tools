using System.Windows.Forms;

namespace qwik.coms
{
    public partial class SpotifyLogin : Form
    {
        public SpotifyLogin()
        {
            InitializeComponent();
        }

        public string Username
        {
            get { return txtUsername.Text.Trim(); }
        }

        public string Password
        {
            get { return txtPassword.Text; }
        }

        public bool RememberMe
        {
            get { return chkRememberMe.Checked; }
        }
    }
}
