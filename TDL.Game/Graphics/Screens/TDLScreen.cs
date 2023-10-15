using System;
using System.Collections.Generic; 
using osuTK;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Input.Events;

namespace TDL.Game.Graphics.Screens
{
    public partial class TDLScreenStack : ScreenStack
    {
        public TDLScreenStack() : base(false)
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }
        public bool Push(TDLScreen screen)
        {
            if (screen == null || EqualityComparer<TDLScreen>.Default.Equals((TDLScreen)CurrentScreen, screen))
                return false;

            base.Push(screen);
            return true;
        }
    }

    public abstract partial class TDLScreen : Screen, IEquatable<TDLScreen>
    {
        private const float x_movement_amount = 50;
        protected TDLScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }
        public virtual bool Equals(TDLScreen other)
        {
            return other?.GetType() == GetType();
        }
        protected override bool OnKeyDown(KeyDownEvent e)
        {
            // we don't want to handle escape key.
            return false;
        }
        protected override void Update()
        {
            base.Update();
            Scale = new Vector2(1 + x_movement_amount / DrawSize.X * 2);
        }

        public override bool OnExiting(ScreenExitEvent e)
        {
            if (IsLoaded)
                this.FadeOut(500, Easing.OutQuint);

            return base.OnExiting(e);
        }
        public override void OnEntering(ScreenTransitionEvent e)
        {
            this.FadeInFromZero<Screen>(1000, Easing.OutQuint);
            base.OnEntering(e);
        }

        public override void OnSuspending(ScreenTransitionEvent next)
        {
            this.Delay(100).FadeIn(1000, Easing.OutQuint);
            base.OnSuspending(next);
        }
        public override void OnResuming(ScreenTransitionEvent e)
        {
            base.OnResuming(e);
        }
    }
}
