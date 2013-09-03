
namespace Digiexnet.VSGithubConnectorPkg
{
    public class IssueListItem
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string Body { get; set; }
        public string Assigned { get; set; }
        public string Link { get; set; }
        public bool CurrentUser { get; set; }
    }
}
