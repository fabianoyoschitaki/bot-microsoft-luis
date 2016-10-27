using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis.Models;

namespace Bot_Application1.Dialogs
{
    [LuisModel("391ad9fa-d332-4279-bc79-5ed39cdefed1", "15db44f891c044cd81d37d4018a528eb")]
    [Serializable]
    public class MedicoDialog : LuisDialog<object>
    {
        [LuisIntent("ConsultarDermatologista")]
        public async Task ConsultaDermatologista(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Você quer um dermatologista!");
            context.Wait(MessageReceived);
        }
    }
}