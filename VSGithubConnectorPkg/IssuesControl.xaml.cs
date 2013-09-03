using IronGitHub;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace Digiexnet.VSGithubConnectorPkg
{
    /// <summary>
    /// Interaction logic for IssuesControl.xaml
    /// </summary>
    public partial class IssuesControl : UserControl
    {
        private string user = "";

        GitHubApi Api;
        public MainViewModel ViewModel = new MainViewModel();
        public IssuesControl()
        {
            this.DataContext = ViewModel;
            InitializeComponent();
            Api = VSGithubConnectorPkgPackage.Api;

            if (Api.Context.Authorization.Token == null)
            {
                loginControl.Visibility = System.Windows.Visibility.Visible;
                issueListView.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                loginControl.Visibility = System.Windows.Visibility.Collapsed;
                issueListView.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void loginControl_LoggedIn(object sender, EventArgs e)
        {
            user = loginControl.User;
            loginControl.Visibility = System.Windows.Visibility.Collapsed;
            issueListView.Visibility = System.Windows.Visibility.Visible;
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
                    ViewModel.Items.Add(new IssueListItem()
                    {
                        Name = issue.Title,
                        Id = issue.Number,
                        Body = issue.Body,
                        Assigned = issue.Assignee != null ? issue.Assignee.Login : null,
                        Link = issue.Url.Replace("https://api.github.com/repos/", "https://github.com/"),
                        CurrentUser = issue.Assignee != null ? (String.Compare(user, issue.Assignee.Login, true) == 0) : false
                    });
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            if (e.Uri != null)
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<IssueListItem> _items = new ObservableCollection<IssueListItem>();

        public ObservableCollection<IssueListItem> Items
        {
            get
            {
                return _items;
            }
            internal set
            {
                if (_items != value)
                {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
