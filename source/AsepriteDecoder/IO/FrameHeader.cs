/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents the values read from an Aseprite frame header.
    /// </summary>
    public sealed record FrameHeader
    {
        /// <summary>
        ///     Gets a value that indicates the total number of bytes in the
        ///     frame.
        /// </summary>
        /// <value></value>
        public uint Length { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the frame magic number used to
        ///     validate the file.
        /// </summary>
        public ushort MagicNumber { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the number of chunks in this frame.
        /// </summary>
        /// <remarks>
        ///     This is the old field.  If this value is 0xFFFF, there might be
        ///     more chunks to read.
        /// </remarks>
        public ushort OldChunkCount { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the duration, in milliseconds, of
        ///     this frame.
        /// </summary>
        public ushort Duration { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the number of chunks in the frame.
        /// </summary>
        /// <remarks>
        ///     This is the new field. If this is 0, use the old field.
        /// </remarks>
        public uint NewChunkCount { get; internal set; }
    }
}