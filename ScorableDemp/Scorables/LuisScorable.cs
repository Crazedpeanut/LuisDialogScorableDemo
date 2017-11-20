using Microsoft.Bot.Builder.Scorables.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs.Internals;
using ScorableDemp.Luis;
using Microsoft.Bot.Builder.Dialogs;

namespace ScorableDemp.Scorable
{
    public class LuisScorable : ScorableBase<IActivity, string, double>
    {
        Dictionary<string, LuisResult> luisResults;
        ILuisService luisService;
        IDialogTask task;
        ILogger logger;
        LuisIntentHandlerDialogFactory luisIntentFactory;

        public LuisScorable(ILuisService luisService, IDialogTask task, ILogger logger, LuisIntentHandlerDialogFactory luisIntentHandlerFactory)
        {
            this.luisService = luisService;
            this.luisResults = new Dictionary<string, LuisResult>();
            this.task = task;
            this.logger = logger;
            this.luisIntentFactory = luisIntentHandlerFactory;
        }

        protected override Task DoneAsync(IActivity item, string state, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        protected override double GetScore(IActivity item, string state)
        {
            return 1;
        }

        protected override bool HasScore(IActivity item, string state)
        {
            return string.IsNullOrEmpty(state) ? false : true;
        }

        protected override async Task PostAsync(IActivity item, string state, CancellationToken token)
        {
            var luisResult = luisResults[state];

            if(luisResult != null)
            {
                string intent = luisResult.TopScoringIntent.Intent;
                IList<EntityRecommendation> entities = luisResult.Entities;
                string originalQuery = luisResult.Query;
                IDialog luisIntentDialog;
                if(luisIntentFactory.TryFindBestMatchingDialog(luisResult, out luisIntentDialog))
                {
                    var interruption = luisIntentDialog.Void<object, IMessageActivity>();
                    task.Call(interruption, null);
                    await task.PollAsync(token);
                }

                return;
            }
        }

        protected override async Task<string> PrepareAsync(IActivity item, CancellationToken token)
        {
            if(item.Type == ActivityTypes.Message)
            {
                string message = ((IMessageActivity)item).Text;

                try
                {
                    LuisResult result = await luisService.QueryAsync(message, token);
                    if (result.TopScoringIntent.Score > 0.8)
                    {
                        luisResults.Add(message, result);
                        return message;
                    }
                }
                catch (Exception e)
                {
                    logger.Error("Error occured querying LUIS", e);
                    return null;
                }
            }

            return null;
        }
    }
}