using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace ScorableDemp.Luis
{
    public class ListSongs : ILuisIntentHandlerDialog
    {
        public bool CanHandleIntent(string intent)
        {
            return intent == "ListSongs";
        }

        public void Setup(LuisResult luisResult)
        {
            
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Here is your music..");
            PromptDialog.Confirm(context, AfterConfirm, "Prompt");
           
        }

        public async Task AfterConfirm(IDialogContext context, IAwaitable<bool> awaitable)
        {
            await context.PostAsync($"{await awaitable}");
            context.Done<object>(null);
        }
    }
}