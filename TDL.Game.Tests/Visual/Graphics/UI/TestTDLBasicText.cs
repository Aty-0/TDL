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
    public partial class TestTDLBasicText : TDLTestScene
    {
        private TDLBasicText text;

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("create basic text smol xd", () =>
            {
                Children = new Drawable[]
                {
                    text = new TDLBasicText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = "Hello world",
                    },
                };
            });

            AddStep("create basic text big xd", () =>
            {
                Children = new Drawable[]
                {
                    text = new TDLBasicText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = "Hello world",
                        Scale = new Vector2(3.0f),
                    },
                };
            });

            AddStep("reset", () => text.ResetText());
        }
    }
}
