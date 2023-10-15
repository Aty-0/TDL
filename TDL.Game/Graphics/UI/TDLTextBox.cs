using System.Diagnostics;

using osu.Framework;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Layout;
using osu.Framework.Allocation;
using osu.Framework.Input.Events;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Bindables;
using osu.Framework.Utils;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLTextBox : BasicTextBox
    {
        private FontUsage _TextBoxFont;

        // Needed because NotifyInputError() is a protected function
        // and in some cases i need basic input error animation
        public void CallNotifyInputError()
        {
            NotifyInputError();
        }


        protected override Drawable GetDrawableCharacter(char c) => new FallingDownContainer
        {
            AutoSizeAxes = Axes.Both,
            Child = new TDLBasicText { Text = c.ToString(), Font = _TextBoxFont },
        };

        protected override SpriteText CreatePlaceholder() => new TDLBasicText
        {
             Font = _TextBoxFont,            
             Margin = new MarginPadding { Left = 2 },
         };

        [BackgroundDependencyLoader(true)]
        private void load()
        {
            BackgroundUnfocused = Color4.Black.Opacity(0.5f);
            BackgroundFocused   = Color4.Black.Opacity(0.8f);
            BackgroundCommit    = Color4.Aqua;
      
            TextContainer.Height = 0.5f;
            CornerRadius = 5;
            LengthLimit = 1000;

            _TextBoxFont = new FontUsage("Torus", weight: "Light", size: CalculatedTextSize);
        }
    }
}
