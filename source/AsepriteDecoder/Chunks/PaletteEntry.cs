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
///     Represents an entry for a palette chunk in an Aseprite file.
/// </summary>
public sealed class PaletteEntry
{
    /// <summary>
    ///     Gets the bitmask flags set for this entry.
    /// </summary>
    public PaletteFlags Flags { get; }

    /// <summary>
    ///     Gets the <see cref="Color"/> value represented by this entry.
    /// </summary>
    public Color Color { get; }

    /// <summary>
    ///     Gets the name of this entry when <see cref="HasName"/> is
    ///     <see langword="true"/>; otherwise, this is <see langword="null"/>.
    /// </summary>
    /// <value></value>
    public string? Name { get; }

    /// <summary>
    ///     Gets whether this entry has a name.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if this entry has a name; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool HasName => (Flags & PaletteFlags.HasName) != 0;

    /// <summary>
    ///     Creates a new <see cref="PaletteEntry"/> class instance
    ///     initialized by the <see cref="AseBinaryReader"/> class instance
    ///     provided.
    /// </summary>
    /// <param name="reader">
    ///     The <see cref="AseBinaryReader"/> class instance used to initialize
    ///     this instance.
    /// </param>
    internal PaletteEntry(AseBinaryReader reader)
    {
        Flags = (PaletteFlags)reader.ReadWord();
        byte r = reader.ReadByte();
        byte g = reader.ReadByte();
        byte b = reader.ReadByte();
        byte a = reader.ReadByte();
        Name = HasName ? reader.ReadString() : null;

        Color = Color.FromArgb(a, r, g, b);
    }
}
