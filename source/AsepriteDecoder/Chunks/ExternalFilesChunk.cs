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

using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace AsepriteDecoder.Chunks;

/// <summary>
///     Represents an External Files chunk in an Aseprite file.
/// </summary>
public sealed class ExternalFilesChunk : AseChunk, IEnumerable<ExternalFileEntry>, IEnumerable
{
    //  The collection of all external file entries added.
    private List<ExternalFileEntry> _entries;

    /// <summary>
    ///     Gets a read only collection of all <see cref="ExternalFileEntry"/>
    ///     intances add to this chunk.
    /// </summary>
    public ReadOnlyCollection<ExternalFileEntry> Entries { get; }

    /// <summary>
    ///     Gets the <see cref="ExternalFileEntry"/> at the index provided.
    /// </summary>
    public ExternalFileEntry this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index must be greater than or equal to zero");
            }

            if (index >= _entries.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index value cannot excceed the total number of entries");
            }

            return _entries[index];
        }
    }

    /// <summary>
    ///     Creates a new <see cref="ExternalFilesChunk"/> class instance
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
    internal ExternalFilesChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.ExternalFiles)
    {
        uint nEntries = reader.ReadDword();
        reader.Skip(8);

        _entries = new List<ExternalFileEntry>((int)nEntries);
        Entries = _entries.AsReadOnly();

        for (int i = 0; i < nEntries; i++)
        {
            _entries.Add(new ExternalFileEntry(reader));
        }
    }

    /// <summary>
    ///     Gets the <see cref="ExternalFileEntry"/> instance from this chunk
    ///     with an <see cref="ExternalFileEntry.ID"/> value that matches
    ///     the one provided.
    /// </summary>
    /// <param name="id">
    ///     The ID of the <see cref="ExternalFileEntry"/> instance to get.
    /// </param>
    /// <returns>
    ///     The <see cref="ExternalFileEntry"/> class instance with the matching
    ///     ID if one was found; otherwise, <see langword="null"/> is returned.
    /// </returns>
    public ExternalFileEntry? GetEntryByID(int id)
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            ExternalFileEntry entry = _entries[i];
            if (entry.ID == id)
            {
                return entry;
            }
        }

        return null;
    }

    /// <summary>
    ///     Attempts to get the <see cref="ExternalFileEntry"/> instance with
    ///     the provided ID.
    /// </summary>
    /// <param name="id">
    ///     The ID value of the <see cref="ExternalFileEntry"/> instance to get.
    /// </param>
    /// <param name="entry">
    ///     When this method returns, contians the
    ///     <see cref="ExternalFileEntry"/> instance with the ID provided, if
    ///     one was found; otherwise this will be <see langword="null"/>. This
    ///     parameter is passed uninitialized.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if an entry was found with a matching ID;
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryGetEntryByID(int id, [NotNullWhen(true)] out ExternalFileEntry? entry)
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            if (_entries[i].ID == id)
            {
                entry = _entries[i];
                return true;
            }
        }

        entry = null;
        return false;
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the collection of
    ///     <see cref="ExternalFileEntry"/> instances that make up this external
    ///     files chunk.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerator{T}"/> for the collection of
    ///     <see cref="ExternalFileEntry"/> instances that make up this
    ///     external files chunk.
    /// </returns>
    public IEnumerator<ExternalFileEntry> GetEnumerator() => Entries.GetEnumerator();

    /// <summary>
    ///     Returns an enumerator that iterates through the collection of
    ///     <see cref="ExternalFileEntry"/> instances that make up this external
    ///     files chunk.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerator{T}"/> for the collection of
    ///     <see cref="ExternalFileEntry"/> instances that make up this
    ///     external files chunk.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
