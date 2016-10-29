using Bot_Application1.Serialization;
using Bot_Application1.Services;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
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
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                Activity reply = activity.CreateReply("Rá! Ié ié! Em que posso ajudar?");
                reply.Attachments = new List<Attachment>();  //****** INIT
                reply.Attachments.Add(GetImage());
                reply.Attachments.Add(GetPdf());
                //reply.Attachments.Add(GetHeroCardCarousel());
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Type == ActivityTypes.Message)
            {
                // return our reply to the user
                var r = await Response(activity.Text);
                string retorno = "Não entendi";
                if (r.intents[0].intent == "ConsultarNeurologista")
                {
                    retorno = $"Temos os neurologistas João, Paulo etc '{r.intents[0].intent}'";
                }
                Activity reply = activity.CreateReply(retorno);            
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Attachment GetImage()
        {
            return new Attachment()
            {
                ContentUrl = $"http://4.bp.blogspot.com/-XyxcPFHEqrk/TzqhqdwvrPI/AAAAAAAADkc/q625G63G5eY/s1600/sergio+malandro.JPG",
                ContentType = "image/png",
                Name = "Sergio_Malandro.png"
            };
        }
        private Attachment GetPdf()
        {
            return new Attachment()
            {
                ContentUrl = $"http://ppgee.poli.usp.br/wp-content/uploads/sites/92/2016/10/Edital-1o-periodo-2017-18-10-2016.pdf",
                ContentType = "application/pdf",
                Name = "arquivo.pdf"
            };
        }
        private Attachment GetHeroCardCarousel()
        {
            List<CardImage> cardImages = new List<CardImage>();
            cardImages.Add(new CardImage(url: "http://4.bp.blogspot.com/-XyxcPFHEqrk/TzqhqdwvrPI/AAAAAAAADkc/q625G63G5eY/s1600/sergio+malandro.JPG"));
            cardImages.Add(new CardImage(url: "http://4.bp.blogspot.com/-XyxcPFHEqrk/TzqhqdwvrPI/AAAAAAAADkc/q625G63G5eY/s1600/sergio+malandro.JPG"));
            cardImages.Add(new CardImage(url: "http://4.bp.blogspot.com/-XyxcPFHEqrk/TzqhqdwvrPI/AAAAAAAADkc/q625G63G5eY/s1600/sergio+malandro.JPG"));
            cardImages.Add(new CardImage(url: "http://4.bp.blogspot.com/-XyxcPFHEqrk/TzqhqdwvrPI/AAAAAAAADkc/q625G63G5eY/s1600/sergio+malandro.JPG"));
            cardImages.Add(new CardImage(url: "http://4.bp.blogspot.com/-XyxcPFHEqrk/TzqhqdwvrPI/AAAAAAAADkc/q625G63G5eY/s1600/sergio+malandro.JPG"));
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction plButton = new CardAction()
            {
                Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                Type = "openUrl",
                Title = "WikiPedia Page"
            };
            cardButtons.Add(plButton);
            HeroCard plCard = new HeroCard()
            {
                Title = "I'm a hero card",
                Subtitle = "Pig Latin Wikipedia Page",
                Images = cardImages,
                Buttons = cardButtons
            };
            Attachment plAttachment = plCard.ToAttachment();
            return plAttachment;
        }

        private static async Task<Utterance> Response(string text)
        {
            return await LuisSaude.GetResponse(text);            
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