using Microsoft.PowerPlatform.PowerAutomate.Desktop.Actions.SDK;
using Microsoft.PowerPlatform.PowerAutomate.Desktop.Actions.SDK.Attributes;
using System;

namespace Modules.GCScriptForPAD
{
    [Action(Id = "ProcessText", Order = 1, FriendlyName = "Process Text", Description = "Processes a text with the desired settings.")]
    [Throws("ProcessTextError")] // TODO: change error name (or delete if not needed)
    public class ProcessText : ActionBase
    {
        #region Properties

        [InputArgument(FriendlyName = "Input Text", Description = "The text to be processed")]
        public string InputText { get; set; }

        [OutputArgument(FriendlyName = "Output Text", Description = "Processed text.")]
        public string OutputText { get; set; }

        #endregion

        #region Methods Overrides

        public override void Execute(ActionContext context)
        {
            string teste = InputText;
            try
            {
                teste = Tools.ProcessText(teste,
                                          true,
                                          ETextTrim.Trim,
                                          ETextCase.ToLower,
                                          ETextType.OnlyLettersNumbersSpaces,
                                          ETextRemoveSpaces.Duplicate);
            }
            catch (Exception e)
            {
                if (e is ActionException) throw;

                throw new ActionException("ProcessTextError", e.Message, e.InnerException);
            }

            // TODO: set values to Output Arguments here
            OutputText = teste;


        }

        #endregion
    }
}
