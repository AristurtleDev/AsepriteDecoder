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

namespace AsepriteDecoder.Chunks;

/// <summary>
///     Represents an entry in an external file chunk.
/// </summary>
public sealed class ExternalFileEntry
{
    /// <summary>
    ///     Gets the ID of this entry.
    /// </summary>
    public uint ID { get; }

    /// <summary>
    ///     Gets the name of this entry.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Creates a new <see cref="ExternalFileEntry"/> class instance
    ///     initialized by the <see cref="AseBinaryReader"/> class instance
    ///     provided.
    /// </summary>
    /// <param name="reader">
    ///     The <see cref="AseBinaryReader"/> class instance used to initialize
    ///     this instance.
    /// </param>
    internal ExternalFileEntry(AseBinaryReader reader)
    {
        ID = reader.ReadDword();
        reader.Skip(8);
        Name = reader.ReadString();
    }
}
