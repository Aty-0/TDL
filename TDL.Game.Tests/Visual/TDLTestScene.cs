using osu.Framework.Allocation;
using osu.Framework.IO.Stores;
using osu.Framework.Testing;

namespace TDL.Game.Tests.Visual
{
    public abstract partial class TDLTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new TestSceneTestRunner();
        public static TestTDLScreenStack screenStack;
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(screenStack = new TestTDLScreenStack());
        }

        private partial class DeveloperTestSceneTestRunner : TestSceneTestRunner
        {
            [BackgroundDependencyLoader]
            private void load()
            {
                Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(TestScene).Assembly), "Resources"));
            }
        }
    }
}