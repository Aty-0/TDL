using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using System;
using TDL.Game.Base;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLTaskButton : TDLButton
    {
        public TDLTask Task;
        public Action<TDLTask> onTaskButtonClick;

        public TDLTaskButton()
        {
            DisableColorAnimationOnHover = false;
            DisableScaleAnimationOnHover = true;

            CornerRadius = 0.0f;

            Background.Colour = new Color4(150, 140, 140, 255);
        }
 
        protected override SpriteText CreateText()
        {
            this.SpriteText = new SpriteText();
            this.SpriteText.Depth = -1;
            this.SpriteText.Origin = Anchor.CentreLeft;
            this.SpriteText.Anchor = Anchor.CentreLeft;
            this.SpriteText.Font = TDLBase.commonFont.With(size: 32);
            this.SpriteText.Colour = this.Colour;
            //this.SpriteText.Scale = new Vector2(1.4f, 1.4f);
            this.SpriteText.Margin = new MarginPadding { Left = 5 };
            return SpriteText;
        }

        protected override bool OnClick(ClickEvent e)
        {
            Background.FlashColour(FlashColour, 200);

            if (onTaskButtonClick != null)
                onTaskButtonClick(Task);

            if (onButtonClick != null)
                onButtonClick();

            return base.OnClick(e);
        }
    }
}
