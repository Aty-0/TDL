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

        }
        protected override void LoadComplete()
        {
            SetPriorityColor();
            base.LoadComplete();
        }

        public void SetPriorityColor()
        {
            switch (Task.priority)
            {
                case TDLPriority.VeryHigh:
                    Background.Colour = new Color4(220, 20, 60, 255);
                    break;
                case TDLPriority.High:
                    Background.Colour = new Color4(250, 128, 114, 255);
                    break;
                case TDLPriority.Medium:
                    Background.Colour = new Color4(147, 112, 219, 255);
                    break;
                case TDLPriority.Low:
                case TDLPriority.VeryLow:
                    Background.Colour = new Color4(192, 192, 192, 255);
                    break;

            }
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
