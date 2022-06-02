/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents a key in a <see cref="SliceChunk"/> record.
    /// </summary>
    public sealed record SliceKey
    {
        /// <summary>
        ///     Gets a value that represents the frame this slice is valid
        ///     starting on.
        /// </summary>
        public uint Frame { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the x-coordinate origin point of 
        ///     the slice relative to the sprite bounds.
        /// </summary>        
        public int OriginX { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the y-coordinate origin point of 
        ///     the slice relative to the sprite bounds.
        /// </summary>
        public int OriginY { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the width, in pixels, of this
        ///     slice.
        /// </summary>        
        public uint Width { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the height, in pixels, of this
        ///     slice.
        /// </summary>
        public uint Height { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the top-left x-coordinate position
        ///     of the center bounds in a nine patch slice.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="SliceChunk.Flags"/>
        ///     has bitmask 1 set; otherwise, this will be 
        ///     <see langword="null"/>
        /// </remarks>      
        public int? CenterX { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the top-left y-coordinate position
        ///     of the center bounds in a nine patch slice.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="SliceChunk.Flags"/>
        ///     has bitmask 1 set; otherwise, this will be 
        ///     <see langword="null"/>
        /// </remarks>
        public int? CenterY { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the width, in pixels, of the
        ///     center bounds in a nine patch slice.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="SliceChunk.Flags"/>
        ///     has bitmask 1 set; otherwise, this will be 
        ///     <see langword="null"/>
        /// </remarks>      
        public uint? CenterWidth { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the height, in pixels, of the
        ///     center bounds in a nine patch slice.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="SliceChunk.Flags"/>
        ///     has bitmask 1 set; otherwise, this will be 
        ///     <see langword="null"/>
        /// </remarks>
        public uint? CenterHeight { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the x-coordinate pivot point of the
        ///     slice relative to the <see cref="OriginX"/>
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="SliceChunk.Flags"/>
        ///     has bitmask 2 set; otherwise, this will be 
        ///     <see langword="null"/>
        /// </remarks> 
        public int? PivotX { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the y-coordinate pivot point of the
        ///     slice relative to the <see cref="OriginY"/>
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="SliceChunk.Flags"/>
        ///     has bitmask 2 set; otherwise, this will be 
        ///     <see langword="null"/>
        /// </remarks>
        public int? PivotY { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="SliceKey"/> record initialized with the
        ///     values provided.
        /// </summary>
        /// <param name="frame">
        ///     The frame number this slice is valid starting on.
        /// </param>
        /// <param name="originX">
        ///     The x-coordinate origin point of the slice.
        /// </param>
        /// <param name="originY">
        ///     The y-coordinate origin point of the slice.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the slice.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the slice.
        /// </param>
        internal SliceKey(uint frame, int originX, int originY, uint width, uint height)
            => (Frame, OriginX, OriginY, Width, Height) = (frame, originX, originY, width, height);
    }
}