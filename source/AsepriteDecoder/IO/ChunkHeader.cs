/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents the values read from an Aseprite chunk header.
    /// </summary>
    public sealed record ChunkHeader
    {
        /// <summary>
        ///     Gets a value that indicates the total number of bytes
        ///     in the chunk, including the size of this header which is
        ///     six bytes.
        /// </summary>
        /// <value></value>
        public uint Length { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the type of chunk.
        /// </summary>
        public ushort Type { get; internal init; }
    }
}