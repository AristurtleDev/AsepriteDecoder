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
///     Represents the bitmask flags that can be set for a
///     <see cref="LayerChunk"/>.
/// </summary>
[Flags]
public enum LayerFlags : ushort
{
    /// <summary>
    ///     Indicates that the layer is visible.
    /// </summary>
    Visible = 1,

    /// <summary>
    ///     Indicates that the layer is editable.
    /// </summary>
    Editable = 2,

    /// <summary>
    ///     Indicates that the movement of the layer should be locked in the UI.
    /// </summary>
    LockMovement = 4,

    /// <summary>
    ///     Indicates that this layer is marked as a background layer.
    /// </summary>
    Background = 8,

    /// <summary>
    ///     Indicates that this layer prefers linked cels.
    /// </summary>
    PreferLinkedCels = 16,

    /// <summary>
    ///     Indicates that this layer should be displayed collapsed in the UI.
    /// </summary>
    DisplayedCollapsed = 32,

    /// <summary>
    ///     Indicates that this layer is a reference image layer.
    /// </summary>
    ReferenceLayer = 64
}
