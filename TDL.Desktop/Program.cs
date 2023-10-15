using System;
using osu.Framework;
using osu.Framework.Platform;
using osu.Framework.Logging;

using TDL.Game;

namespace TDL.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            TDLBase.baseArgs = args;

            // Run game
            using (GameHost host = Host.GetSuitableDesktopHost(TDLBase.TDL_HOST_NAME))
            {
                TDLBase.tdlBase = new TDLBase();
                host.Run(TDLBase.tdlBase);
            }
        }
    }
}