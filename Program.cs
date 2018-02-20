using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace gitlab_event_publisher
{
    class Program
    {
        private const string _gitlab_host = "https://<gitlab_host>";
        private const string _token = "<gitlab_private_token>";
        private const string _api_ver = "v4";
        private const string _project_id = "<project_id>";

        private const string _elastic_host = "http://<elastic_host>:9200";
        private const string _elastic_index = "index";
        private const string _elastic_type = "type";

        static void Main(string[] args)
        {
            Console.WriteLine("Publish start ...");

            PublishGitLabEvent().Wait();

            Console.WriteLine("Done.");
        }

        public static async Task PublishGitLabEvent()
        {
            var url = string.Format("{0}/api/{1}/projects/{2}/events?private_token={3}&per_page=100&page=1", _gitlab_host, _api_ver, _project_id, _token);
            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Object>>(json);

                Console.WriteLine("Get " + result.Count + " events.");
                foreach(var o in result)
                {
                    await PostTo(JsonConvert.SerializeObject(o));
                    //Console.WriteLine(o.ToString());
                }
            }            
        }

        public static async Task PostTo(string json)
        {
            var endpoint = string.Format("{0}/{1}/{2}", _elastic_host, _elastic_index, _elastic_type);
            using(var client = new HttpClient())
            {
                var content = new StringContent(json, new UTF8Encoding(), "application/json");
                var response = await client.PostAsync(endpoint, content);
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new ApplicationException(response.ToString());
                }
            }
        }
    }
}
