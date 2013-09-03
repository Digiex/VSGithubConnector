using IronGitHub;
using IronGitHub.Exceptions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Digiexnet.VSGithubConnectorPkg
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public string User
        {
            get
            {
                return usernameBox.Text;
            }
        }
        public event EventHandler LoggedIn;
        public LoginControl()
        {
            InitializeComponent();
        }

        public async void DoLogin()
        {
            loginButton.IsEnabled = false;
            try
            {
                var auth = await VSGithubConnectorPkgPackage.Api.Authorize(new System.Net.NetworkCredential(usernameBox.Text, passwordBox.Password), new Scopes[] { Scopes.Repo });
                if (LoggedIn != null && auth.Token != null)
                {
                    LoggedIn(this, new EventArgs());
                }
            }
            catch (UnauthorizedException)
            {
                System.Windows.Forms.MessageBox.Show("Wrong username or password.");
            }
            finally
            {
                loginButton.IsEnabled = true;
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (usernameBox.Text != "" && passwordBox.Password != "")
                DoLogin();
        }

        private void checkEnter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                loginButton_Click(this, null);
            }
        }
    }
}