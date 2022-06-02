/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an Aseprite tileset chunk.
    /// </summary>
    public sealed record TilesetChunk
    {
        /// <summary>
        ///     Gets a value that represents the ID of this tileset.
        /// </summary>
        public uint TilesetID { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the bitmask flags set for this
        ///     chunk.
        /// </summary>
        public uint Flags { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the total number of tiles in this
        ///     tileset.
        /// </summary>
        public uint TileCount { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the width, in pixels, of a tile.
        /// </summary>        
        public ushort TileWidth { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the height, in pixels, of a tile.
        /// </summary>
        public ushort TileHeight { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the number to show on screen from
        ///     the tile with index 1.
        /// </summary>
        /// <remarks>
        ///     Per Aseprite file spec, this value isn't used to represent the
        ///     data in the file, it's just used for UI purposes.
        /// </remarks>
        public short BaseIndex { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the name of this tileset.
        /// </summary>
        public string Name { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the ID of the external file that
        ///     contains the tileset this is linked to.
        /// </summary>
        public uint? ExternalFileID { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the ID of the tileset in the
        ///     external file this is linked to.
        /// </summary>
        public uint? ExternalTilesetID { get; internal init; }

        /// <summary>
        ///     Gets a byte array containing the pixel data for the embedded
        ///     tileset image.
        /// </summary>
        /// <value></value>
        public byte[]? TilesetImage { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="TilesetChunk"/> record initialized with
        ///     the values provided.
        /// </summary>
        /// <param name="id">
        ///     The ID of this tileset.
        /// </param>
        /// <param name="flags">
        ///     The bitmask flags set for this chunk.
        /// </param>
        /// <param name="count">
        ///     The total number of tiles.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of a tile.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of a tile.
        /// </param>
        /// <param name="baseIndex">
        ///     The base index for the tile with index 1.
        /// </param>
        /// <param name="name">
        ///     The name of the tileset.
        /// </param>
        internal TilesetChunk(uint id, uint flags, uint count, ushort width, ushort height, short baseIndex, string name)
            => (TilesetID, Flags, TileCount, TileWidth, TileHeight, BaseIndex, Name) = (id, flags, count, width, height, baseIndex, name);

    }
}