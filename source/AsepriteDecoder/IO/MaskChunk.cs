/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an Aseprite mask chunk.
    /// </summary>
    public sealed record MaskChunk
    {
        /// <summary>
        ///     Gets a value that indicates the top-left x-coordinate position
        ///     of this mask.
        /// </summary>
        public short X { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the top-left y-coordinate position
        ///     of this mask.
        /// </summary>
        public short Y { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the width of this mask.
        /// </summary>
        public ushort Width { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the height of this mask.
        /// </summary>
        public ushort Height { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the name of this mask.
        /// </summary>
        public string Name { get; internal init; }

        /// <summary>
        ///     Gets a byte array that contains the bit map data of the mask.
        /// </summary>
        /// <remarks>
        ///     Each byte contains 8 pixels (the left most pixels are packed
        ///     into the high order bits).
        /// </remarks>
        public byte[] Data { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="MaskChunk"/> record initialized with
        ///     the values provided.
        /// </summary>
        /// <param name="x">
        ///     The top-left x-coordinate position of the mask.
        /// </param>
        /// <param name="y">
        ///     The top-left y-coordinate position of the mask.
        /// </param>
        /// <param name="width">
        ///     The width of the mask.
        /// </param>
        /// <param name="height">
        ///     The height of the mask.
        /// </param>
        /// <param name="name">
        ///     The name of the mask.
        /// </param>
        /// <param name="data">
        ///     The bit map data of the mask.
        /// </param>
        internal MaskChunk(short x, short y, ushort width, ushort height, string name, byte[] data)
         => (X, Y, Width, Height, Name, Data) = (x, y, width, height, name, data);
    }
}