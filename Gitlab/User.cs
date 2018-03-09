using System;

namespace gitlab_event_publisher.Gitlab
{
    public class User
    {
        public string name { get; set; }
        public string username { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public string avatar_url { get; set; }
        public string web_url { get; set; }
        public DateTime created_at { get; set; }
        public bool is_admin { get; set; }
        public string bio { get; set; }
        public string location { get; set; }
        public string skype { get; set; }
        public string linkedin { get; set; }
        public string twitter { get; set; }
        public string website_url { get; set; }
        public string organization { get; set; }
        public DateTime? last_sign_in_at { get; set; }
        public DateTime confirmed_at { get; set; }
        public string email { get; set; }
        public int theme_id { get; set; }
        public int color_scheme_id { get; set; }
        public int projects_limit { get; set; }
        public DateTime? current_sign_in_at { get; set; }
        public object[] identities { get; set; }
        public bool can_create_group { get; set; }
        public bool can_create_project { get; set; }
        public bool two_factor_enabled { get; set; }
        public bool external { get; set; }
    }

}