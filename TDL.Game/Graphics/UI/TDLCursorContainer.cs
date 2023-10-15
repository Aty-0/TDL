using System;
using System.Collections.Generic;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Utils;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLCursorContainer : CursorContainer
    {
        public partial class Cursor : Container
        {
            private Container cursorContainer;
            public Sprite AdditiveLayer;

            public Cursor()
            {
                AutoSizeAxes = Axes.Both;
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                Children = new Drawable[]
                {
                    cursorContainer = new Container
                    {
                        AutoSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            new Sprite
                            {
                                Texture = textures.Get(@"cursor"),
                            },
                        }
                    }
                };

                cursorContainer.Scale = new Vector2(0.15f);
            }

        }

        protected override Drawable CreateCursor() => activeCursor = new Cursor();

        private Cursor activeCursor;

        protected override void PopIn()
        {
            activeCursor.FadeTo(1, 250, Easing.OutQuint);
            activeCursor.ScaleTo(1, 400, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            activeCursor.FadeTo(0, 250, Easing.OutQuint);
            activeCursor.ScaleTo(0.6f, 250, Easing.In);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            activeCursor.ScaleTo(0.7f, 800, Easing.OutQuint);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (!e.HasAnyButtonPressed)
                activeCursor.ScaleTo(1.0f, 800, Easing.OutQuint);

            

            base.OnMouseUp(e);
        }
    }
}
