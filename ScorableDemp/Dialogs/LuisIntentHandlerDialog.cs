using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScorableDemp.Luis
{
    public interface ILuisIntentHandlerDialog : IDialog
    {
        bool CanHandleIntent(string intent);
        void Setup(LuisResult luisResult);
    }

    public class LuisIntentHandlerDialogFactory
    {
        IEnumerable<ILuisIntentHandlerDialog> luisIntentHandlerDialogs;

        public LuisIntentHandlerDialogFactory(IEnumerable<ILuisIntentHandlerDialog> luisIntentHandlerDialogs)
        {
            this.luisIntentHandlerDialogs = luisIntentHandlerDialogs;
        }

        public bool TryFindBestMatchingDialog(LuisResult luisResult, out IDialog dialog)
        {
            var luisIntentDialog = luisIntentHandlerDialogs.Where(d => d.CanHandleIntent(luisResult.TopScoringIntent.Intent)).FirstOrDefault();

            if(luisIntentDialog != null)
            {
                luisIntentDialog.Setup(luisResult);
                dialog = luisIntentDialog;
                return true;
            }

            dialog = null;
            return false;
        }
    }
}
