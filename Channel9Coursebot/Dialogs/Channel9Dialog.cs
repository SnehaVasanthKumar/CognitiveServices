using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Channel9Coursebot.Dialogs
{
    [LuisModel("a173d6c6-d539-4c8f-a521-ecfadc468c24", "87756b23f81a411d895ad41f75d43961")]

    [Serializable]
    public class Channel9Dialog :LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, IAwaitable<IMessageActivity> message, object actionResult)
        {
            var reply = context.MakeMessage();
            // TODO: Use actionResult to provide a meaningful response to the user within the reply message
            // For example if the result is just a string you could simply do reply.Text = actionResult.ToString()
            await context.PostAsync("I do not undersatnd your question");
        }

        [LuisIntent("casual-chat")]
        public async Task CasualChatIntent(IDialogContext context, IAwaitable<IMessageActivity> message, object actionResult)
        {
            var reply = context.MakeMessage();
            // TODO: Use actionResult to provide a meaningful response to the user within the reply message
            // For example if the result is just a string you could simply do reply.Text = actionResult.ToString()
            await context.PostAsync("You have entered casual chat intent");
        }

        [LuisIntent("it-topic")]
        public async Task ittopicIntent(IDialogContext context, IAwaitable<IMessageActivity> message, object actionResult)
        {
            var reply = context.MakeMessage();
            // TODO: Use actionResult to provide a meaningful response to the user within the reply message
            // For example if the result is just a string you could simply do reply.Text = actionResult.ToString()
            await context.PostAsync("You have entered it topicIntent");
        }

        [LuisIntent("expertize-lookup")]
        public async Task expertizelookupIntent(IDialogContext context, IAwaitable<IMessageActivity> message, object actionResult)
        {
            var reply = context.MakeMessage();
            // TODO: Use actionResult to provide a meaningful response to the user within the reply message
            // For example if the result is just a string you could simply do reply.Text = actionResult.ToString()
            await context.PostAsync("You have entered expertize-lookup");
        }

        [LuisIntent("ticket-status")]
        public async Task ticketstatusIntent(IDialogContext context, IAwaitable<IMessageActivity> message, object actionResult)
        {
            var reply = context.MakeMessage();
            // TODO: Use actionResult to provide a meaningful response to the user within the reply message
            // For example if the result is just a string you could simply do reply.Text = actionResult.ToString()
            await context.PostAsync("You have entered ticketstatusIntent");
        }

        [LuisIntent("find-documentation")]
        public async Task finddocumentation(IDialogContext context, IAwaitable<IMessageActivity> message, object actionResult)
        {
            var reply = context.MakeMessage();
            // TODO: Use actionResult to provide a meaningful response to the user within the reply message
            // For example if the result is just a string you could simply do reply.Text = actionResult.ToString()
            await context.PostAsync("You have entered find-documentation");
        }

        [LuisIntent("incident-report")]
        public async Task incidentreportIntent(IDialogContext context, IAwaitable<IMessageActivity> message, object actionResult)
        {
            var reply = context.MakeMessage();
            // TODO: Use actionResult to provide a meaningful response to the user within the reply message
            // For example if the result is just a string you could simply do reply.Text = actionResult.ToString()
            await context.PostAsync("You have entered incidentreportIntent");
        }
    }


}
