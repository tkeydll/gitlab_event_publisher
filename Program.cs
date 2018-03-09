using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using gitlab_event_publisher.Gitlab;

namespace gitlab_event_publisher
{
    class Program
    {        private const string _gitlab_host = "https://<gitlab_host>";
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

        public static async Task<List<User>> GetAllUser()
        {
            int i = 0;
            var url = string.Format("{0}/api/{1}/users?private_token={2}&per_page=100&page={3}", _gitlab_host, _api_ver, _token, (++i).ToString());
            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<User>>(json);

                Console.WriteLine("Get " + result.Count + " users.");

                return result;
            }
        }

        public static async Task PublishUserEvent(int user_id, string user_name)
        {
            Console.WriteLine(string.Format("Publish {0} events", user_name));

            int i = 0;
            while(true)
            {
                var url = string.Format("{0}/api/{1}/users/{2}/events?private_token={3}&per_page=100&page={4}", _gitlab_host, _api_ver, user_id, _token, (++i).ToString());
                Console.Write("In page " + i.ToString() + ", ");
                using(var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Object>>(json);

                    Console.WriteLine("Get " + result.Count + " events.");
                    if (result.Count == 0)
                    {
                        return;
                    }

                    foreach(var o in result)
                    {
                        await PostToElastic(JsonConvert.SerializeObject(o));
                        //Console.WriteLine(o.ToString());
                    }
                }
            }
        }

        public static async Task PublishGitLabEvent()
        {
            int i = 0;
            while(true)
            {
                var url = string.Format("{0}/api/{1}/events?private_token={2}&per_page=100&page={3}", _gitlab_host, _api_ver, _token, (++i).ToString());
                Console.Write("In page " + i.ToString() + ", ");
                using(var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Object>>(json);

                    Console.WriteLine("Get " + result.Count + " events.");
                    if (result.Count == 0)
                    {
                        return;
                    }

                    foreach(var o in result)
                    {
                        await PostToElastic(JsonConvert.SerializeObject(o));
                        //Console.WriteLine(o.ToString());
                    }
                }
            }
        }

        public static async Task PostToElastic(string json)
        {
            var endpoint = string.Format("{0}/{1}/{2}", _elastic_host, _elastic_index, _elastic_type);
            using(var client = new HttpClient())
            {
                var content = new StringContent(json, new UTF8Encoding(), "application/json");
                var response = await client.PostAsync(endpoint, content);
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    Console.WriteLine(endpoint);
                    Console.WriteLine(content.ToString());
                    throw new ApplicationException(response.ToString());
                }
            }
        }
    }
}
