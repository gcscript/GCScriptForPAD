using Microsoft.PowerPlatform.PowerAutomate.Desktop.Actions.SDK.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Modules.GCScriptForPAD.Tests
{
    [TestClass]
    public class ProcessTextTests
    {
        [TestMethod]
        public void Action_IsValid()
        {
            bool isValid = ActionValidator.IsValid(typeof(ProcessText), out var errors);

            Assert.IsTrue(isValid, $"Action is invalid. Validation Errors: {Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
        }

        [TestMethod]
        public void Action_Execute_Success()
        {
            try
            {
                ProcessText action = new ProcessText() { InputText = "Test Input" };
                action.Execute(null);
                var actionOutput = action.OutputText;
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got: " + e.Message);
            }
        }
    }
}
