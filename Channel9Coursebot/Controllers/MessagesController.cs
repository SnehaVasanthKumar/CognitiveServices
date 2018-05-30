using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Channel9Coursebot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Cognitive.LUIS;
using Newtonsoft.Json.Linq;

namespace Channel9Coursebot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {

                ConnectorClient connector = new ConnectorClient(new System.Uri(activity.ServiceUrl));

                //Sentiment Analytics
                var sentiment = activity.CreateReply();
                sentiment.Text = await IsPositive(activity.Text) ? "Good, Happy for you" : "I am really sorry!";
                await connector.Conversations.ReplyToActivityAsync(sentiment);

            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        /// <summary>
        /// Calls setiment analysis API
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static async Task<bool> IsPositive(string text)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "9e5653eae16c44a5afc2978bddaa2ca9");
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] byteData = encoding.GetBytes(@"{
              
                'documents': [
                    {
                                        'id':'1',
                    'text': '" + text.Replace("'", "") + @"'
                    }
                    ]
                    }");
            using (var content = new ByteArrayContent(byteData))
            {
                const string actionBeginUri = "https://westeurope.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
                HttpResponseMessage response = await client.PostAsync(actionBeginUri, content);
                response.EnsureSuccessStatusCode();
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(responseBodyAsText);
                return (double)(JArray.Parse(o["documents"].ToString()))[0]["score"] > 0.5;
            }


        }

        /// <summary>
        /// Handles SystemMessages
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        /// <summary>
        /// Calls Language detection API
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static async Task<string> DetectLanguage(string text)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "9e5653eae16c44a5afc2978bddaa2ca9");
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] byteData = encoding.GetBytes(@"{
              
                'documents': [
                    {
                                        'id':'1',
                    'text': '" + text.Replace("'", "") + @"'
                    }
                    ]
                    }");
            using (var content = new ByteArrayContent(byteData))
            {
                const string actionBeginUri = "https://westeurope.api.cognitive.microsoft.com/text/analytics/v2.0/languages";
                HttpResponseMessage response = await client.PostAsync(actionBeginUri, content);
                response.EnsureSuccessStatusCode();

                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(responseBodyAsText);
                return o["documents"][0]["detectedLanguages"][0]["iso6391Name"].ToString();


            }
        }

        static async Task<string> GetPhrases(string language, string text)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "9e5653eae16c44a5afc2978bddaa2ca9");
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] byteData = encoding.GetBytes(@"{
              
                'documents': [
                    {
                                        'id':'1',
                    'language': '" + language.Replace("'", "") + @"',
                    'text': '" + text.Replace("'", "") + @"'
                    }
                    ]
                    }");


            using (var content = new ByteArrayContent(byteData))
            {
                const string actionBeginUri = "https://westeurope.api.cognitive.microsoft.com/text/analytics/v2.0/keyphrases";
                HttpResponseMessage response = await client.PostAsync(actionBeginUri, content);
                response.EnsureSuccessStatusCode();

                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(responseBodyAsText);
                var phrases = JArray.Parse(o["documents"][0]["keyPhrases"].ToString()).ToObject<List<string>>();
                // LuisClient

                LuisClient luiClient = new LuisClient("a173d6c6-d539-4c8f-a521-ecfadc468c24", "87756b23f81a411d895ad41f75d43961");
                var keyPhrases = string.Join(" ", phrases);
                var result = luiClient.Predict(keyPhrases).Result;
                string values = string.Empty;
                foreach (var e in result.Entities.Values)
                {
                    values = string.Concat(e.ToString());
                }
                return values;
            }
        }
    }
}