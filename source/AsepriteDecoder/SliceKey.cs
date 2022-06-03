using System.Drawing;

namespace AsepriteDecoder
{
    public sealed record SliceKey
    {
        public int Frame { get; internal init; }
        public Point Origin { get; internal init; }
        public Size Size { get; internal init; }
        public Rectangle? Center { get; internal init; }
        public Point? Pivot { get; internal init; }

        internal SliceKey(int frame, Point origin, Size size)
            => (Frame, Origin, Size) = (frame, origin, size);
    }
}
