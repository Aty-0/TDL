using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;
using osuTK;
using osuTK.Input;

using osuTK.Graphics;

using TDL.Game.Tests;
using TDL.Game.Graphics.UI;
using TDL.Game.Graphics.Screens;

namespace TDL.Game.Tests.Visual.Graphics.UI
{
    [TestFixture]
    public class TestTDLMainScreen : TDLTestScene
    {
        private TDLMainScreen _mainScreen;

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("show screen", () => 
            {   if(_mainScreen == null)
                {
                    _mainScreen = new TDLMainScreen();
                    screenStack.ShowScreen(_mainScreen); 

                }
            });

        }
    }
}
