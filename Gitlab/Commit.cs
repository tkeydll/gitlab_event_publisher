using System;

namespace gitlab_event_publisher.Gitlab
{
    public class Commit
    {
        public string id { get; set; }
        public string message { get; set; }
        public DateTime timestamp { get; set; }
        public string url { get; set; }
        public User author { get; set; }
        public string[] added { get; set; }
        public string[] modified { get; set; }
        public string[] removed { get; set; }
    }




}