using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;
using osuTK;
using osuTK.Input;

using osuTK.Graphics;

using TDL.Game.Tests;
using TDL.Game.Graphics.UI;

namespace TDL.Game.Tests.Visual.Graphics.UI
{
    [TestFixture]
    public partial class TestTDLButton : TDLTestScene
    {
        private TDLButton button;

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("create big button", () =>
            {
                Children = new Drawable[]
                {
                    button = new TDLButton
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(200, 200),
                        Text = "Test",
                    },
                };
            });
            AddStep("create wierd by x button", () =>
            {
                Children = new Drawable[]
                {
                    button = new TDLButton
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(20, 200),
                        Text = "Test",
                    },
                };
            });

            AddStep("create wierd by y button", () =>
            {
                Children = new Drawable[]
                {
                    button = new TDLButton
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(200, 20),
                        Text = "Test",
                    },
                };
            });
        }
    }
}

