using System.Drawing;

namespace AsepriteDecoder
{
    public sealed record Tag
    {
        public int From { get; internal init; }
        public int To { get; internal init; }
        public LoopDirection Direction { get; internal init; }
        public Color Color { get; internal init; }
        public string Name { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="Tag"/> record initilized with the
        ///     provided values.
        /// </summary>
        /// <param name="from">
        ///     The starting frame.
        /// </param>
        /// <param name="to">
        ///     The ending frame.
        /// </param>
        /// <param name="direction">
        ///     The animation loop direction.
        /// </param>
        /// <param name="color">
        ///     The color of the tag.
        /// </param>
        /// <param name="name">
        ///     The name of the tag.
        /// </param>
        internal Tag(int from, int to, LoopDirection direction, Color color, string name)
            => (From, To, Direction, Color, Name) = (from, to, direction, color, name);
    }
}
