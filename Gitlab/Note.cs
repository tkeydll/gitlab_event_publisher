using System;

namespace gitlab_event_publisher.Gitlab
{
    public class Note
    {
        public int id { get; set; }
        public string body { get; set; }
        public object attachment { get; set; }
        public User author { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool system { get; set; }
        public int? noteable_id { get; set; }
        public string noteable_type { get; set; }
        public bool upvote { get; set; }
        public bool downvote { get; set; }
    }




}