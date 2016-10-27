using Bot_Application1.Serialization;
using Bot_Application1.Services;
using Microsoft.Bot.Connector;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Services.Description;

namespace Bot_Application1
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
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return
                
                Activity reply;
                if (activity.Text == "a"
                 || activity.Text == "b"
                 || activity.Text == "c"
                 || activity.Text == "d"
                 || activity.Text == "e"
                 || activity.Text == "f")
                {
                    reply = activity.CreateReply($"{activity.Text} teste");
                }
                else
                {
                    // return our reply to the user
                    var r = await Response(activity.Text);
                    string retorno = "";
                    if (r.intents[0].intent == "ConsultarNeurologista")
                    {
                        retorno = $"Temos os neurologistas João, Paulo etc '{r.intents[0].intent}'";
                    }
                    reply = activity.CreateReply(retorno);
                }
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private static async Task<Utterance> Response(string text)
        {
            return await Luis.GetResponse(text);            
        }

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
    }
}