using System.Drawing;

namespace AsepriteDecoder
{
    public sealed record Frame
    {
        public Size Size { get; internal init; }
        public int Duration { get; internal init; }
        public Color[] Pixels { get; internal init; }

        internal Frame(Size size, int duration, Color[] pixels)
            => (Size, Duration, Pixels) = (size, duration, pixels);
    }
}
