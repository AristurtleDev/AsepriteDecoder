/* -----------------------------------------------------------------------------
    Copyright 2022 Christopher Whitley

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to
    deal in the Software without restriction, including without limitation the
    rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
    sell copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
    IN THE SOFTWARE.
----------------------------------------------------------------------------- */

using System.Drawing;

namespace AsepriteDecoder.Chunks;

/// <summary>
///     Represents a Cel Extra chunk from an Aseprite file.
/// </summary>
public sealed class CelExtraChunk : AseChunk
{
    /// <summary>
    ///     Gets the bitmask flags that have been set for this instance.
    /// </summary>
    public CelExtraFlags Flags { get; }

    /// <summary>
    ///     Gets the precise x-coordinate position of the cel.
    /// </summary>
    public float PreciseX { get; }

    /// <summary>
    ///     Gets the precise y-coordiante position of the cel.
    /// </summary>
    public float PreciseY { get; }

    /// <summary>
    ///     Gets the width of the cel in the sprite.
    /// </summary>
    public float Width { get; }

    /// <summary>
    ///     Gets the height of the cel in the sprite.
    /// </summary>
    public float Height { get; }

    /// <summary>
    ///     Gets whether the precise bounds have been set for the cel.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if the precise bounds have been set;
    ///     otherwise, <see langword="false"/>
    /// </returns>
    public bool PreciseBoundsSet => (Flags & CelExtraFlags.PreciseBoundsSet) != 0;

    /// <summary>
    ///     Gets the precise top-left xy-coordinate position of this cel.
    /// </summary>
    /// <returns>
    ///     A <see cref="PointF"/> that represents the precise top-left
    ///     xy-coordinate position of this cel.
    /// </returns>
    public PointF PrecisePosition => new PointF(PreciseX, PreciseY);

    /// <summary>
    ///     Gets the size of the cel.
    /// </summary>
    /// <returns>
    ///     A <see cref="Size"/> that represents the width and height of the
    ///     cel.
    /// </returns>
    public SizeF Size => new SizeF(Width, Height);

    /// <summary>
    ///     Creates a new <see cref="CelExtraChunk"/> class instance
    ///     initialized by the <see cref="AseBinaryReader"/> class instance
    ///     provided.
    /// </summary>
    /// <param name="length">
    ///     The total length, in bytes, of the chunk.
    /// </param>
    /// <param name="reader">
    ///     The <see cref="AseBinaryReader"/> class instance used to initialize
    ///     this instance.
    /// </param>
    internal CelExtraChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.CelExtra)
    {

        Flags = (CelExtraFlags)reader.ReadDword();
        PreciseX = reader.ReadFixed();
        PreciseY = reader.ReadFixed();
        Width = reader.ReadFixed();
        Height = reader.ReadFixed();
        reader.Skip(16);
    }
}
