using osu.Framework.Graphics.Sprites;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLBasicText : SpriteText
    {
        public TDLBasicText()
        {
            Shadow = true;
            Font = TDLBase.commonFont;
        }

        public virtual void ResetText()
        {
            Text = default;
        }
    }
}
