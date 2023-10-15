//https://github.com/ppy/osu/blob/master/osu.Game/Graphics/Cursor/MenuCursorContainer.cs

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Input;
using osu.Framework.Input.StateChanges;

using TDL.Game.Graphics.UI;

namespace TDL.Game.Graphics
{
    public interface IProvideCursor : IDrawable
    {
        /// <summary>
        /// The cursor provided by this <see cref="IDrawable"/>.
        /// May be null if no cursor should be visible.
        /// </summary>
        CursorContainer Cursor { get; }

        /// <summary>
        /// Whether <see cref="Cursor"/> should be displayed as the singular user cursor. This will temporarily hide any other user cursor.
        /// This value is checked every frame and may be used to control whether multiple cursors are displayed (e.g. watching replays).
        /// </summary>
        bool ProvidingUserCursor { get; }
    }

    public partial class DeveloperCursorContainer : Container, IProvideCursor
    {
        protected override Container<Drawable> Content => content;
        private readonly Container content;

        /// <summary>
        /// Whether any cursors can be displayed.
        /// </summary>
        internal bool CanShowCursor = true;

        public CursorContainer Cursor { get; }
        public bool ProvidingUserCursor => true;

        public DeveloperCursorContainer()
        {
            AddRangeInternal(new Drawable[]
            {
                Cursor = new TDLCursorContainer { State = { Value = Visibility.Visible } },
                content = new Container { RelativeSizeAxes = Axes.Both }
            });
        }

        private InputManager inputManager;

        protected override void LoadComplete()
        {
            base.LoadComplete();
            inputManager = GetContainingInputManager();
        }

        private IProvideCursor currentTarget;

        protected override void Update()
        {
            base.Update();

            var lastMouseSource = inputManager.CurrentState.Mouse.LastSource;
            bool hasValidInput = lastMouseSource != null && !(lastMouseSource is ISourcedFromTouch);

            if (!hasValidInput || !CanShowCursor)
            {
                currentTarget?.Cursor?.Hide();
                currentTarget = null;
                return;
            }

            IProvideCursor newTarget = this;

            foreach (var d in inputManager.HoveredDrawables)
            {
                if (d is IProvideCursor p && p.ProvidingUserCursor)
                {
                    newTarget = p;
                    break;
                }
            }

            if (currentTarget == newTarget)
                return;

            currentTarget?.Cursor?.Hide();
            newTarget.Cursor?.Show();

            currentTarget = newTarget;
        }
    }
}
