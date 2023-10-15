using System;
using System.Linq;
using osu.Framework;
using osu.Framework.Platform;

namespace TDL.Game.Tests
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            HostOptions hostOptions = new HostOptions();
            hostOptions.PortableInstallation = true;
            using (GameHost host = Host.GetSuitableDesktopHost(@"visual-tests", hostOptions))
                host.Run(new VisualTestGame());
        }
    }
}
