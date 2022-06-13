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
///     Represents a mask chunk in an Aseprite file.
/// </summary>
[Obsolete("Per Aseprite file spec, this chunk is deprecated. Maybe be removed from this library in the future if Aseprite removes it from file spec", false)]
public sealed class MaskChunk : AseChunk
{
    /// <summary>
    ///     Gets the x-coordinate position of this mask.
    /// </summary>
    public short X { get; }

    /// <summary>
    ///     Gets the y-coordinate position of this mask.
    /// </summary>
    public short Y { get; }

    /// <summary>
    ///     Gets the width of this mask.
    /// </summary>
    public ushort Width { get; }

    /// <summary>
    ///     Gets the height of this mask.
    /// </summary>
    public ushort Height { get; }

    /// <summary>
    ///     Gets the name of this mask.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the bitmap data of this mask.
    /// </summary>
    /// <remarks>
    ///     Each byte contains 8 pixels (the leftmost pixels are packed into the
    ///     higher order bits).
    /// </remarks>
    public byte[] Data { get; }

    /// <summary>
    ///     Gets the top-left xy-coordiante position of this mask.
    /// </summary>
    /// <returns>
    ///     A <see cref="Point"/> that represents the top-left xy-coordinate
    ///     position of this mask.
    /// </returns>
    public Point Position => new Point(X, Y);

    /// <summary>
    ///     Gets the size of the mask.
    /// </summary>
    /// <returns>
    ///     A <see cref="Size"/> that represents the width and height of this
    ///     mask.
    /// </returns>
    public Size Size => new Size(Width, Height);

    /// <summary>
    ///
    /// </summary>
    /// <param name="length"></param>
    /// <param name="reader"></param>
    internal MaskChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Mask)
    {
        X = reader.ReadShort();
        Y = reader.ReadShort();
        Width = reader.ReadWord();
        Height = reader.ReadWord();
        reader.Skip(8);
        Name = reader.ReadString();

        //  Data len specified in Aseprite file spec
        int dataLen = Height * ((Width + 7) / 8);
        Data = reader.ReadBytes(dataLen);
    }
}
