using IronGitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for IssuesControl.xaml
    /// </summary>
    public partial class IssuesControl : UserControl
    {
        GitHubApi Api;
        public IssuesControl()
        {
            InitializeComponent();
            Api = VSGithubConnectorPkgPackage.Api;
            if (Api.Context.Authorization.Token == null)
            {
                loginPanel.Visibility = System.Windows.Visibility.Visible;
                issuesPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                loginPanel.Visibility = System.Windows.Visibility.Collapsed;
                issuesPanel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void loginControl_LoggedIn(object sender, EventArgs e)
        {
            loginPanel.Visibility = System.Windows.Visibility.Collapsed;
            issuesPanel.Visibility = System.Windows.Visibility.Visible;
            LoadIssues();
        }

        private async void LoadIssues()
        {
            var repo = await VSGithubConnectorPkgPackage.Api.Repositories.Get("windowsphonehacker", "SparklrWP");
            if (repo.HasIssues)
            {
                var issues = await VSGithubConnectorPkgPackage.Api.Issues.Get("windowsphonehacker", "SparklrWP");
                foreach (var issue in issues)
                {
                    issueListView.Items.Add(issue.Title);
                }
            }
        }
    }
}
