using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NTRPRS.NLog.Slack.Models
{
    /// <summary>
    /// Payload parameter transformed to a JSON string in the POST request to Slack
    /// </summary>
    [DataContract]
    public class Payload
    {
        /// <summary>
        /// Simple message that will be posted to the channel.
        /// To create a link in your text, enclose the URL in <> angle brackets.For example: <https://slack.com>
        /// will post a clickable link to https://slack.com.
        /// To display hyperlinked text instead of the actual URL, use the pipe character, as shown in this example: '<https://alert-system.com/alerts/1234|Click here> for details!'
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Display richly-formatted message blocks.
        /// </summary>
        [DataMember(Name = "attachments")]
        public readonly ICollection<Attachment> Attachments = new List<Attachment>();

        /// <summary>
        /// Create JSON that will be send to Slack
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var serializer = new DataContractJsonSerializer(typeof(Payload));
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, this);
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    var json = reader.ReadToEnd();
                    return json;
                }
            }
        }

        /// <summary>
        /// Send this payload via a POST request to the given slack Webhook
        /// </summary>
        /// <param name="webHookUrl">The WebhookUrl where Payload will be POST</param>
        public void SendTo(string webHookUrl)
        {
            var json = ToJson();
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, webHookUrl)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                client.SendAsync(request).Wait();
            }
        }
    }
}

