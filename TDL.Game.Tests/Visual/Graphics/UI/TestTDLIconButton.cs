using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;
using TDL.Game.Graphics.UI;

namespace TDL.Game.Tests.Visual.Graphics.UI
{
    [TestFixture]
    public partial class TestTDLIconButton : TDLTestScene
    {
        private TDLIconButton button;

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("create big button", () =>
            {
                Children = new Drawable[]
                {
                    button = new TDLIconButton
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
                    button = new TDLIconButton
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
                    button = new TDLIconButton
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

