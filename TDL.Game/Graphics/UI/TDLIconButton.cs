using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics.Shapes;
using System.Linq;
using osu.Framework.Allocation;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLIconButton : TDLButton
    {
        public Box icon;
        public TDLIconButton()
        {
            DisableColorAnimationOnHover = true;
            DisableScaleAnimationOnHover = true;
            Add(icon = new Box
                {
                    Colour = new Color4(255, 255, 255, 255),
                    //RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    FillMode = FillMode.Fill,
                    Width = 10,
                    Height = 10,
                }
            );
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            icon.Size = new Vector2(10 / (Size.X / 2), 10 / (Size.Y / 2));
            SpriteText.Position = new Vector2(SpriteText.Position.X + (10 * (icon.Size.X / icon.Size.Y)), SpriteText.Position.Y);
        }

        protected override SpriteText CreateText()
        {
            this.SpriteText = new SpriteText();
            this.SpriteText.Depth = -1;
            this.SpriteText.Origin = Anchor.CentreLeft;
            this.SpriteText.Anchor = Anchor.CentreLeft;
            this.SpriteText.Font = TDLBase.commonFont;
            this.SpriteText.Colour = this.Colour;

            return SpriteText;
        }
    }
}
