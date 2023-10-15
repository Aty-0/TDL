using System;
using System.Diagnostics;
using System.Reflection;

using osuTK;
using osuTK.Graphics;
using osu.Framework;
using osu.Framework.Platform;
using osu.Framework.IO;
using osu.Framework.IO.Stores;
using osu.Framework.Development;
using osu.Framework.Logging;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Framework.Extensions.Color4Extensions;


using TDL.Game.Graphics;
using TDL.Game.Graphics.Screens;
using TDL.Game.Graphics.UI;
using TDL.Game.Resources;
using NuGet.Protocol;

namespace TDL.Game
{
    public partial class TDLBase : osu.Framework.Game
    {
        public static string[] baseArgs; 
        public static FontUsage commonFont => new FontUsage("Torus", weight: "Light");
        
        // Host name 
        public static readonly string TDL_HOST_NAME = @"tdl";


        public FontStore fontStore;
        public ResourceStore<byte[]> resourceStore;
        //private static FontUsage DebugFont => new FontUsage("Torus", weight: "Bold");




        public static TDLBase tdlBase;
        public TDLScreenStack screenStack;

        private TDLCursorContainer _cursorContainer;

        public TDLBase()
        {
            tdlBase = this;
        }

        private void LoadFonts()
        {
            // Load fonts taken from osu game
            AddFont(Resources, @"Fonts/Torus/Torus-Regular");
            AddFont(Resources, @"Fonts/Torus/Torus-Light");
            AddFont(Resources, @"Fonts/Torus/Torus-SemiBold");
            AddFont(Resources, @"Fonts/Torus/Torus-Bold");

            AddFont(Resources, @"Fonts/Inter/Inter-Regular");
            AddFont(Resources, @"Fonts/Inter/Inter-RegularItalic");
            AddFont(Resources, @"Fonts/Inter/Inter-Light");
            AddFont(Resources, @"Fonts/Inter/Inter-LightItalic");
            AddFont(Resources, @"Fonts/Inter/Inter-SemiBold");
            AddFont(Resources, @"Fonts/Inter/Inter-SemiBoldItalic");
            AddFont(Resources, @"Fonts/Inter/Inter-Bold");
            AddFont(Resources, @"Fonts/Inter/Inter-BoldItalic");

            AddFont(Resources, @"Fonts/Noto/Noto-Basic");
            AddFont(Resources, @"Fonts/Noto/Noto-Hangul");
            AddFont(Resources, @"Fonts/Noto/Noto-CJK-Basic");
            AddFont(Resources, @"Fonts/Noto/Noto-CJK-Compatibility");
            AddFont(Resources, @"Fonts/Noto/Noto-Thai");

            AddFont(Resources, @"Fonts/Venera/Venera-Light");
            AddFont(Resources, @"Fonts/Venera/Venera-Bold");
        }

        /*
        private void CreateDebugWarnText()
        {
            if (DebugUtils.IsDebugBuild)
            {
                Add(new Box
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 20,
                    Colour = Color4.Red.Opacity(0.6f),
                });

                Add(new TDLBasicText
                {
                    Colour = Color4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.Centre,
                    Scale = new Vector2(1.0f, 1.0f),
                    Position = new Vector2(0.0f, -10.0f),
                    Text = "! Is Debug Build !",
                    Font = DebugFont,
                });
            }
        }
        */

        private void CheckARGS()
        {
            Logger.Log("Check app args...");

            foreach(var i in baseArgs)
            {
                switch(i)
                {
                    case @"-dev":
                        Logger.Enabled = true;
                        Logger.Level = LogLevel.Verbose;
                        Logger.Log("Dev mode on", level: LogLevel.Debug);
                        break;
                }
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Logger.Log("Initialize game...");

            Resources.AddStore(new DllResourceStore(TDLResourceAssembly.ResourceAssembly));
            LoadFonts();
            CheckARGS();

            Add(screenStack = new TDLScreenStack());
            Add(_cursorContainer = new TDLCursorContainer { RelativeSizeAxes = Axes.Both });
            ShowScreen(new TDLMainScreen());

            //CreateDebugWarnText();

        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }

        protected override void Update()
        {
            base.Update();
        }

        public void ShowScreen(TDLScreen screen, TDLScreen current_screen = null)
        {
            if (screenStack == null)
            {
                Logger.Error(new ArgumentNullException(), "Screen stack is null!");
                return;
            }

            if (current_screen != null && current_screen.IsAlive)
                current_screen.Exit();

            if(screen == null)
            {
                Logger.Error(new NullReferenceException(), "Can't show screen because it is null");
                return;
            }

            screenStack.Push(screen);
        }
    }
}
