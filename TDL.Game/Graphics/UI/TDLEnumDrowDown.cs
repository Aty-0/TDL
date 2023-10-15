using System;

namespace TDL.Game.Graphics.UI
{
    public partial class TDLEnumDrowDown<T> : TDLDropDown<T>
        where T : struct, Enum
    {
        public TDLEnumDrowDown()
        {
            Items = Enum.GetValues<T>();
        }
    }
}
