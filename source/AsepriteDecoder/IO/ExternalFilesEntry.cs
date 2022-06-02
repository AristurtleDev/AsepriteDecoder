/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an external file in an <see cref="ExternalFilesChunk"/>
    /// </summary>
    public sealed record ExternalFilesEntry
    {
        /// <summary>
        ///     Gets a value that indicates the ID of the external file.
        /// </summary>
        /// <value></value>
        public uint ID { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the name of the external file.
        /// </summary>
        public string Name { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="ExternalFilesEntry"/> record
        ///     initialized with the values provided.
        /// </summary>
        /// <param name="id">
        ///     The ID of the external file.
        /// </param>
        /// <param name="name">
        ///     The name of the external file.
        /// </param>
        internal ExternalFilesEntry(uint id, string name)
         => (ID, Name) = (id, name);
    }
}