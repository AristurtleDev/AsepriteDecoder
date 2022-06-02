/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents a tag entry for a <see cref="TagsChunk"/>.
    /// </summary>
    public sealed record Tag
    {
        /// <summary>
        ///     Gets a value that indicates the starting frame.
        /// </summary>
        public ushort From { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the ending frame.
        /// </summary>
        public ushort To { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the animation loop direction
        /// </summary>
        public byte Direction { get; internal init; }

        /// <summary>
        ///     Gets a byte array that contains the RGB values of the tag's
        ///     color.
        /// </summary>
        /// <remarks>
        ///     Per Aseprite file spec, this value is deprecated and only used
        ///     for backwards compatibility with Aseprite v1.2.x files.  The
        ///     color of the tag is the one in the user data field.
        /// </remarks>
        public byte[] Color { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the name of this tag.
        /// </summary>

        public string Name { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="Tag"/> record initialized with the
        ///     values provided.
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
        internal Tag(ushort from, ushort to, byte direction, byte[] color, string name)
            => (From, To, Direction, Color, Name) = (from, to, direction, color, name);
    }
}