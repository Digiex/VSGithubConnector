using IronGitHub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Digiexnet.VSGithubConnectorPkg
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public event EventHandler LoggedIn;
        public LoginControl()
        {
            InitializeComponent();
        }

        public async void DoLogin()
        {

            var auth = await VSGithubConnectorPkgPackage.Api.Authorize(new System.Net.NetworkCredential(usernameBox.Text, passwordBox.Password), new Scopes[] { Scopes.Repo });
            if (LoggedIn != null && auth.Token != null)
            {
                LoggedIn(this, new EventArgs());
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }
    }
}