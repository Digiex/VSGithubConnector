using IronGitHub;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public MainViewModel ViewModel = new MainViewModel();
        public IssuesControl()
        {
            this.DataContext = ViewModel;
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
                    ViewModel.Items.Add(new IssueListItem() {
                        Name = issue.Title,
                        Id = issue.Number,
                        Body = issue.Body
                    });
                }
            }
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
            private set
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
