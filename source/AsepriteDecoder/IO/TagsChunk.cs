/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an Aseprite tag chunk.
    /// </summary>
    public sealed record TagsChunk
    {
        /// <summary>
        ///     Gets a value that indicates the total number of tags.
        /// </summary>
        public ushort Count { get; internal init; }

        /// <summary>
        ///     Gets a <see cref="TagEntry"/> array containing all tags read for this
        ///     chunk.
        /// </summary>
        public TagEntry[] Tags { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="TagsChunk"/>  record initialized
        ///     with the values provided.
        /// </summary>
        /// <param name="count">
        ///     The total number of tags.
        /// </param>
        /// <param name="tags">
        ///     The tags.
        /// </param>
        public TagsChunk(ushort count, TagEntry[] tags) => (Count, Tags) = (count, tags);
    }
}
