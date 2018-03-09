using System;

namespace gitlab_event_publisher.Gitlab
{
    public class Event
    {
        public object title { get; set; }
        public int project_id { get; set; }
        public string action_name { get; set; }
        public int? target_id { get; set; }
        public string target_type { get; set; }
        public int author_id { get; set; }
        public Data data { get; set; }
        public string target_title { get; set; }
        public DateTime created_at { get; set; }
        public User author { get; set; }
        public string author_username { get; set; }
        public Note note { get; set; }
    }




}