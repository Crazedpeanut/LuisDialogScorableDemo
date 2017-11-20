using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ScorableDemp.Dialogs
{
    public class QuitDialog : IDialog<bool>
    {
        public Task StartAsync(IDialogContext context)
        {
            PromptDialog.Confirm(context, AfterPrompt, "Are you sure you want to quit?");
            return Task.CompletedTask;
        }

        async Task AfterPrompt(IDialogContext context, IAwaitable<bool> awaitable)
        {
            context.Done(await awaitable);
        }
    }
}