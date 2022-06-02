/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents the common values read from an Aseprite cel chunk.
    /// </summary>
    public sealed record CelChunk
    {
        /// <summary>
        ///     Gets a value that indicates the index of the layer this cel is
        ///     on.
        /// </summary>
        public ushort LayerIndex { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the top-left x-coordinate position
        ///     of this cel relative to the frame bounds.
        /// </summary>
        public short X { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the top-left y-coordinate position
        ///     of this cel relative to the frame bounds.
        /// </summary>
        public short Y { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the level of opacity.
        /// </summary>
        public byte Opacity { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the type of cel this is.
        /// </summary>
        public ushort Type { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the width of this cel. When the
        ///     <see cref="Type"/> is 0 or 2, this is in pixels. When the 
        ///     <see cref="Type"/> is 3, this is in tiles.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 0, 2,
        ///     or 3; otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public ushort? Width { get; internal set; } = null;

        /// <summary>
        ///     Gets a value that indicates the height of this cel. When the
        ///     <see cref="Type"/> is 0 or 2, this is in pixels. When the 
        ///     <see cref="Type"/> is 3, this is in tiles.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 0, 2,
        ///     or 3; otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public ushort? Height { get; internal set; } = null;

        /// <summary>
        ///     Gets a byte array that contains the pixel data for this cel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 0 or 2;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public byte[]? Pixels { get; internal set; } = null;

        /// <summary>
        ///     Gets a value that indicates the frame position of the cel this
        ///     cel is linked with.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 1;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public ushort? LinkedFrame { get; internal set; } = null;

        /// <summary>
        ///     Gets a value that indicates the total number of bits per tile.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 3;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public ushort? BitsPerTile { get; internal set; } = null;

        /// <summary>
        ///     Gets a value that indicates the tile ID bitmask set for this
        ///     cel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 3;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public uint? TileIDBitmask { get; internal set; } = null;

        /// <summary>
        ///     Gets a value that indicates the x-flip bitmask set for this cel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 3;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public uint? XFlipBitmask { get; internal set; } = null;

        /// <summary>
        ///     Gets a value that indicates the y-flip bitmask set for this
        ///     cel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 3;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public uint? YFlipBitmask { get; internal set; } = null;

        /// <summary>
        ///     Gets a value that indicates the 90CW rotation bitmask set for
        ///     this cel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 3;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public uint? RotationBitmask { get; internal set; } = null;

        /// <summary>
        ///     Gets a byte array containing the tile data for this cel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 3;
        ///     otherwise, it will always be <see langword="null"/>
        /// </remarks>
        public byte[]? Tiles { get; internal set; } = null;

        /// <summary>
        ///     Creates a new <see cref="CommonCelChunk"/> record initialized 
        ///     with the values provided.
        /// </summary>
        /// <param name="layer">
        ///     The index of the layer this cel is on.
        /// </param>
        /// <param name="x">
        ///     The top-left x-coordinate position of this cel relative to the
        ///     frame bounds.
        /// </param>
        /// <param name="y">
        ///     The top-left y-coordinate position of this cel relative to the
        ///     frame bounds.
        /// </param>
        /// <param name="opacity">
        ///     The opacity level of this cel.
        /// </param>
        /// <param name="type">
        ///     The type of cel this is.
        /// </param>
        internal CelChunk(ushort layer, short x, short y, byte opacity, ushort type)
         => (LayerIndex, X, Y, Opacity, Type) = (layer, x, y, opacity, type);
    }
}