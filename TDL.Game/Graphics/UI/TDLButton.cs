using System;
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

using TDL.Game;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLButton : BasicButton
    {
        public Action onButtonClick;

        public bool DisableScaleAnimationOnHover = false;
        public bool DisableColorAnimationOnHover = false;

        private Vector2 _previousScale;
        private float _sizeRatio = 0.0f;
        public TDLButton()
        {
            Background.Colour = onButtonClick != null ? Color4.Gray : Color4.Gray.Opacity(0.4f);
            Hover.Colour = Color4.Brown.Opacity(0.5f);
            SpriteText.Colour = Color4.White;
            CornerRadius = 10.0f;
            Masking = true;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ResizeText();      
        }

        public virtual void ResizeText()
        {
            if(this.Size.X == 0 || this.Size.Y == 0) 
                return;

            _sizeRatio = (this.Size.X <= this.Size.Y ? this.Size.X / this.Size.Y : this.Size.Y / this.Size.X);
            if (_sizeRatio <= 0.1f )
            {
                _sizeRatio += 0.5f;
            }
            else if (_sizeRatio >= 1)
            {
                _sizeRatio += 1.5f;
            }

            SpriteText.Scale = new Vector2(1.0f * _sizeRatio, 1.0f * _sizeRatio);

            _previousScale = Scale;
        }
        // TODO: Remove this and use the resizetext func
        // public virtual void ResizeByTextSize()
        // {
        //     this.Size = new Vector2(this.Size.X + (SpriteText.Text.ToString().Length * BUTTON_RESIZE_OFFSET), this.Size.Y);
        // }

        protected override void Update()
        {
            base.Update();
        }

        protected override bool OnClick(ClickEvent e)
        {
            Background.FlashColour(FlashColour, 200);
            
            if (onButtonClick != null)
                onButtonClick();

            return base.OnClick(e);
        }

        // TODO: Rework 
     
        protected override bool OnHover(HoverEvent e)
        {
            if (!DisableColorAnimationOnHover)
                Hover.FadeIn(150, Easing.OutQuint);

            //if(!DisableScaleAnimationOnHover)
            //    Content.ScaleTo(_previousScale.X + 0.15f, 300, Easing.OutQuint);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            //if (!DisableScaleAnimationOnHover)
            //    Content.ScaleTo(_previousScale.X, 300, Easing.OutQuint);

            if(!DisableColorAnimationOnHover)
                Hover.FadeOut(700);
        }
     

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            Content.ScaleTo(_previousScale.X - 0.3f, 4000, Easing.OutQuint);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            Content.ScaleTo(DisableScaleAnimationOnHover ? _previousScale.X : _previousScale.X + 0.15f, 1000, Easing.OutElastic);
        }

        private void enabledChanged(ValueChangedEvent<bool> e)
        {
            this.FadeColour(e.NewValue ? Color4.White : Color4.Gray, 200, Easing.OutQuint);
        }

        protected override SpriteText CreateText() 
        {
            this.SpriteText = new SpriteText();
            this.SpriteText.Depth = -1;
            this.SpriteText.Origin = Anchor.Centre;
            this.SpriteText.Anchor = Anchor.Centre;
            this.SpriteText.Font = TDLBase.commonFont;
            this.SpriteText.Colour = this.Colour;

            return SpriteText;
        }
    }
}
