using Microsoft.PowerPlatform.PowerAutomate.Desktop.Actions.SDK;
using Microsoft.PowerPlatform.PowerAutomate.Desktop.Actions.SDK.ActionSelectors;
using Microsoft.PowerPlatform.PowerAutomate.Desktop.Actions.SDK.Attributes;
using System;
using System.ComponentModel;

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
                                          ETextCase.ToUpper,
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

    public enum ESelectorChoice
    {
        DisplayOnlyFirstName,
        DisplayFullName,
        DisplayFullDetails
    }

    [Action(Id = "CentralCustomAction")]
    public class CentralCustomAction : ActionBase
    {
        #region Properties

        [InputArgument, DefaultValue(ESelectorChoice.DisplayOnlyFirstName)]
        public ESelectorChoice Selector { get; set; }

        [InputArgument(Order = 1), Description("FirstName")]
        public string FirstName { get; set; }

        [InputArgument(Order = 2), Description("LastName")]
        public string LastName { get; set; }

        [InputArgument(Order = 3), Description("Age")]
        public int Age { get; set; }

        [OutputArgument]
        public string DisplayedMessage { get; set; }

        #endregion

        #region Methods Overrides

        public override void Execute(ActionContext context)
        {
            if (Selector == ESelectorChoice.DisplayOnlyFirstName)
            {
                DisplayedMessage = $"Hello, {FirstName}!";
            }
            else if (Selector == ESelectorChoice.DisplayFullName)
            {
                DisplayedMessage = $"Hello, {FirstName} {LastName}!";
            }
            else // The 3rd Selector was chosen 
            {
                DisplayedMessage = $"Hello, {FirstName} {LastName}!\nYour age is: {Age}";
            }
        }

        #endregion
    } // you can see below how to implement an action selector

    [Action]
    public class CentralCustomActionWithBoolean : ActionBase
    {
        #region Properties

        [InputArgument, DefaultValue(true)]
        public bool TimeExpired { get; set; }

        [InputArgument]
        public string ElapsedTime { get; set; }

        [InputArgument]
        public string RemainingTime { get; set; }

        [OutputArgument]
        public string DisplayedMessage { get; set; }

        #endregion

        #region Methods Overrides

        public override void Execute(ActionContext context)
        {
            DisplayedMessage = TimeExpired ? $"The timer has expired. Elapsed time: {ElapsedTime}" : $"Remaining time: {RemainingTime}";
        }

        #endregion
    }

    public class NoTime : ActionSelector<CentralCustomActionWithBoolean>
    {
        public NoTime()
        {
            UseName("TimeHasExpired");
            Prop(p => p.TimeExpired).ShouldBe(true);
            ShowAll();
            Hide(p => p.RemainingTime);
        }
    }

    public class ThereIsTime : ActionSelector<CentralCustomActionWithBoolean>
    {
        public ThereIsTime()
        {
            UseName("TimeHasNotExpired");
            Prop(p => p.TimeExpired).ShouldBe(false);
            ShowAll();
            Hide(p => p.RemainingTime);
        }
    }
}
