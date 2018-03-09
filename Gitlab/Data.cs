namespace gitlab_event_publisher.Gitlab
{
    public class Data
    {
        public string object_kind { get; set; }
        public string event_name { get; set; }
        public string before { get; set; }
        public string after { get; set; }
        public string _ref { get; set; }
        public string checkout_sha { get; set; }
        public object message { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_avatar { get; set; }
        public int project_id { get; set; }
        public Project project { get; set; }
        public Commit[] commits { get; set; }
        public int total_commits_count { get; set; }
        public Repository repository { get; set; }
    }




}