/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    public sealed record LayerChunk
    {
        /// <summary>
        ///     Gets a value that indicates the bitmask flags set for this
        ///     layer.
        /// </summary>
        public ushort Flags { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the type of layer.
        /// </summary>
        public ushort Type { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the level of this layer relative to
        ///     the last layer chunk read.
        /// </summary>
        public ushort ChildLevel { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the default width, in pixels.
        /// </summary>
        /// <remarks>
        ///     Per Aseprite file spec, this value is ignored.
        /// </remarks>
        public ushort DefaultWidth { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the default height, in pixels.
        /// </summary>
        /// <remarks>
        ///     Per Aseprite file spec, this value is ignored.
        /// </remarks>        
        public ushort DefaultHeight { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the blend mode used by this layer.
        /// </summary>
        public ushort BlendMode { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the opacity level of this layer.
        /// </summary>
        /// <remarks>
        ///     This value is only valid if the <see cref="FileHeader.Flags"/> 
        ///     has bitmask 1 set.
        /// </remarks>
        public byte Opacity { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the name of this layer.
        /// </summary>
        public string Name { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the index of the tileset used by
        ///     this layer.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="Type"/> is 2; 
        ///     otherwise, this value will be null.
        /// </remarks>
        public uint? TilesetIndex { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="LayerChunk"/> record initialized with
        ///     the values provided.
        /// </summary>
        /// <param name="name">
        ///     The name of the layer.
        /// </param>
        internal LayerChunk(string name) => Name = name;
    }
}