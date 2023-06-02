using Microsoft.PowerPlatform.PowerAutomate.Desktop.Actions.SDK.ActionSelectors;

namespace Modules.GCScriptForPAD.Selectors
{
    public class Selector1 : ActionSelector<CentralCustomAction>
    {
        public Selector1()
        {
            UseName("DisplayOnlyFirstName");
            Prop(p => p.Selector).ShouldBe(ESelectorChoice.DisplayOnlyFirstName);
            ShowAll();
            Hide(p => p.LastName);
            Hide(p => p.Age);
            // or 
            // Show(p => p.FirstName); 
            // Show(p => p.DisplayedMessage);
        }
    }

    public class Selector2 : ActionSelector<CentralCustomAction>
    {
        public Selector2()
        {
            UseName("DisplayFullName");
            Prop(p => p.Selector).ShouldBe(ESelectorChoice.DisplayFullName);
            ShowAll();
            Hide(p => p.Age);
        }
    }

    public class Selector3 : ActionSelector<CentralCustomAction>
    {
        public Selector3()
        {
            UseName("DisplayFullDetails");
            Prop(p => p.Selector).ShouldBe(ESelectorChoice.DisplayFullDetails);
            ShowAll();
        }
    }
}
