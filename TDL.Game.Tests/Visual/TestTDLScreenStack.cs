using osu.Framework.Screens;
using System;
using TDL.Game.Graphics.Screens;
using osu.Framework.Logging;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Testing;

namespace TDL.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestTDLScreenStack : TestScene
    {
        private TDLScreenStack screenStack;


        [BackgroundDependencyLoader]
        private void load()
        {
            Add(screenStack = new TDLScreenStack());
        }

        [Test]
        public void ShowScreen(TDLScreen screen, TDLScreen current_screen = null)
        {
            if (screenStack == null)
            {
                Logger.Error(new ArgumentNullException(), "Screen stack is null!");
                return;
            }

            if (current_screen != null && current_screen.IsAlive)
                current_screen.Exit();

            if (screen == null)
            {
                Logger.Error(new NullReferenceException(), "Can't show screen because it is null");
                return;
            }

            screenStack.Push(screen);
        }
    }
}

