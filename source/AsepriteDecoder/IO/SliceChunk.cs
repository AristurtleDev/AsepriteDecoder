/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an Aseprite slice chunk.
    /// </summary>
    public sealed record SliceChunk
    {
        /// <summary>
        ///     Gets a value that represents the total number of keys in this
        ///     slice.
        /// </summary>
        public uint Count { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the bitmask flags set for this
        ///     slice.
        /// </summary>
        public uint Flags { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the name of this slice.
        /// </summary>
        public string Name { get; internal init; }

        /// <summary>
        ///     Gets a <see cref="SliceKeyEntry"/> array that contains all slice keys
        ///     for this slice.
        /// </summary>
        public SliceKeyEntry[] Keys { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="SliceChunk"/> record initialized with
        ///     the values provided.
        /// </summary>
        /// <param name="count">
        ///     The total number of keys in this slice.
        /// </param>
        /// <param name="flags">
        ///     The bitmask flags set for this slice.
        /// </param>
        /// <param name="name">
        ///     The name of this slice.
        /// </param>
        /// <param name="keys">
        ///     The <see cref="SliceKeyEntry"/> records that make up this slice.
        /// </param>
        internal SliceChunk(uint count, uint flags, string name, SliceKeyEntry[] keys)
            => (Count, Flags, Name, Keys) = (count, flags, name, keys);
    }
}
