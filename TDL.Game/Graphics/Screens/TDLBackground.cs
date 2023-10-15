using System;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Allocation;


namespace TDL.Game.Graphics.Screens
{
    public partial class TDLBackground : CompositeDrawable, IEquatable<TDLBackground>
    {
        private const float        BLUR_SCALE = 0.5f;

        public readonly Sprite     Sprite;

        private BufferedContainer _bufferedContainer;
        private string            _textureName;

        public TDLBackground(string textureName, Vector2 blur)
        {
            RelativeSizeAxes = Axes.Both;

            AddInternal(Sprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                FillMode = FillMode.Fill,
                Colour = Color4.White
            });

            BlurTo(blur, 100, Easing.OutQuint);
            LoadTexture(textureName, TDLBase.tdlBase.Textures);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {

        }

        public Vector2 BlurSigma => _bufferedContainer?.BlurSigma / BLUR_SCALE ?? Vector2.Zero;

        /// <summary>
        /// Smoothly adjusts <see cref="IBufferedContainer.BlurSigma"/> over time.
        /// </summary>
        /// <returns>A <see cref="TransformSequence{T}"/> to which further transforms can be added.</returns>
        public void BlurTo(Vector2 newBlurSigma, double duration = 0, Easing easing = Easing.None)
        {
            if (_bufferedContainer == null && newBlurSigma != Vector2.Zero)
            {
                RemoveInternal(Sprite, false);

                AddInternal(_bufferedContainer = new BufferedContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    RedrawOnScale = false,
                    Child = Sprite
                });
            }

            if (_bufferedContainer != null)
                _bufferedContainer.FrameBufferScale = newBlurSigma == Vector2.Zero ? Vector2.One : new Vector2(BLUR_SCALE);

            _bufferedContainer?.BlurTo(newBlurSigma * BLUR_SCALE, duration, easing);
        }

        public virtual bool Equals(TDLBackground other)
        {
            if (ReferenceEquals(null, other)) 
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return other.GetType() == GetType() && other._textureName == _textureName;
        }

        public bool LoadTexture(string texture, TextureStore textures)
        {
            if (!string.IsNullOrEmpty(texture))
                Sprite.Texture = textures.Get(texture);

            if (Sprite.Texture == null)
            {
                Logger.Error(new NullReferenceException(), "Background texture not loaded!");
                return false;
            }

            return true;
        }
    }
}
