using osu.Framework.Allocation;
using osu.Framework.Audio.Sample;
using osu.Framework.Audio;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using osuTK.Graphics;
using System.Linq;
using System;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLDropDown<T> : Dropdown<T>
    {
        public DropdownMenu TDLMenu;

        protected override DropdownMenu CreateMenu() => new TDLDropdownMenu();

        protected override DropdownHeader CreateHeader() => new TDLDropdownHeader();

        public partial class TDLDropdownHeader : DropdownHeader
        {
            private readonly SpriteText label;
            public Action onHover;

            protected override LocalisableString Label
            {
                get => label.Text;
                set => label.Text = value;
            }

            public TDLDropdownHeader()
            {
                var font = FrameworkFont.Condensed;
                
                Foreground.Padding = new MarginPadding(5);
                BackgroundColour = new Color4(100, 100, 100, 255);
                BackgroundColourHover = new Color4(140, 140, 140, 255);

                Children = new[]
                {
                    label = new SpriteText
                    {
                        AlwaysPresent = true,
                        Font = font,
                        Height = font.Size,
                    },
                };
            }

            protected override bool OnHover(HoverEvent e)
            {
                if(onHover != null)
                {
                    onHover();
                }

                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                base.OnHoverLost(e);
            }
        }

        public partial class TDLDropdownMenu : DropdownMenu
        {
            protected override Menu CreateSubMenu() => new BasicMenu(Direction.Vertical);

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawableBasicDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new BasicScrollContainer(direction);

            private partial class DrawableBasicDropdownMenuItem : DrawableDropdownMenuItem
            {
                public DrawableBasicDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    Foreground.Padding = new MarginPadding(2);
                    BackgroundColour        = new Color4(100, 100, 100, 255);
                    BackgroundColourHover   = new Color4(120, 120, 120, 255);
                    BackgroundColourSelected = new Color4(150, 150, 150, 255);
                }

                protected override Drawable CreateContent() => new SpriteText
                {
                    Font = new FontUsage("Torus", weight: "Light", size: 10)
                };
            }
        }

    }
}
