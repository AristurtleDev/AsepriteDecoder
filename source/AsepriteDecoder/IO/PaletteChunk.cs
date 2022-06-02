/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an Aseprite palette chunk.
    /// </summary>
    public sealed record PaletteChunk
    {
        /// <summary>
        ///     Gets a value that represents the new palette size.
        /// </summary>
        public uint NewSize { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the first color index to change.
        /// </summary>
        public uint First { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the last color index to change.
        /// </summary>
        public uint Last { get; internal init; }

        /// <summary>
        ///     Gets a <see cref="PaletteEntry"/> array containing all entries
        ///     for this palette.
        /// </summary>
        public PaletteEntry[] Entries { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="PaletteChunk"/> record initialized with
        ///     the values provided.
        /// </summary>
        /// <param name="newSize">
        ///     The new palette size (total number of entries).
        /// </param>
        /// <param name="first">
        ///     The first color index to change.
        /// </param>
        /// <param name="last">
        ///     The last color index to change.
        /// </param>
        /// <param name="entries">
        ///     The palette entries.
        /// </param>
        internal PaletteChunk(uint newSize, uint first, uint last, PaletteEntry[] entries)
            => (NewSize, First, Last, Entries) = (newSize, first, last, entries);
    }
}