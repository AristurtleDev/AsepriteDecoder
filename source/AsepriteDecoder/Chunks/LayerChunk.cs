namespace AsepriteDecoder.Chunks;

public sealed class LayerChunk : AseChunk
{
    //  The parent layer of this layer if there is one.
    private LayerChunk? _parent;

    /// <summary>
    ///     Gets the bitmask flags that have been set for this chunk.
    /// </summary>
    public LayerFlags Flags { get; }

    /// <summary>
    ///     Gets the type of layer this is.
    /// </summary>
    public LayerType Type { get; }

    /// <summary>
    ///     Gets the level of this layer in relation to the last layer that
    ///     was read.
    /// </summary>
    public int ChildLevel { get; }

    /// <summary>
    ///     Gets the default width, in pixels, of this layer.
    /// </summary>
    /// <remarks>
    ///     Per Aseprite file spec, this value is ignored internally by
    ///     Aseprite.
    /// </remarks>
    public int DefaultWidth { get; }

    /// <summary>
    ///     Gets the default height, in pixels, of this layer.
    /// </summary>
    /// <remarks>
    ///     Per Aseprite file spec, this value is ignored internally by
    ///     Aseprite.
    /// </remarks>
    public int DefaultHeight { get; }

    /// <summary>
    ///     Gets the <see cref="BlendMode"/> used by this layer.
    /// </summary>
    public BlendMode BlendMode { get; }

    /// <summary>
    ///     Gets the opaicty level of this layer.
    /// </summary>
    /// <remarks>
    ///     This value is only valid when the
    ///     <see cref="AseHeader.LayerOpacityIsValid"/> is
    ///     <see langword="true"/>; otherwise, you should use a value of
    ///     255;
    /// </remarks>
    public int Opacity { get; }

    /// <summary>
    ///     Gets the name of this layer.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the index of the tileset used by this layer when the
    ///     <see cref="Type"/> is <see cref="LayerType.Tilemap"/>.
    /// </summary>
    /// <remarks>
    ///     This value is only valid when the <see cref="Type"/> is
    ///     <see cref="LayerType.Tilemap"/>; otherwise, this will be
    ///     <see langword="null"/>.
    /// </remarks>
    public int? TilesetIndex { get; }

    /// <summary>
    ///     Gets whether this layer is visible.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if this layer is visible; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool IsVisible => (Flags & LayerFlags.Visible) != 0;

    /// <summary>
    ///     Gets whether this layer is editable.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if this layer is editable; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool IsEditable => (Flags & LayerFlags.Editable) != 0;

    /// <summary>
    ///     Gets whether the movement of this layer should be locked in the UI.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if movement of this layer is locked in the
    ///     UI; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsMovementLocked => (Flags & LayerFlags.LockMovement) != 0;

    /// <summary>
    ///     Gets whether this layer is a background layer.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if this layer is a background layer;
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsBackgroundLayer => (Flags & LayerFlags.Background) != 0;

    /// <summary>
    ///     Gets whether this layer prefers linked cels.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if this layer prefers linked cels; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool PrefersLinkedCels => (Flags & LayerFlags.PreferLinkedCels) != 0;

    /// <summary>
    ///     Gets whether this layer should be displayed collapsed in the UI.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if this layer should be displayed collapsed
    ///     in the UI; otherwise, <see langword="false"/>.
    /// </returns>
    public bool DisplayCollapsed => (Flags & LayerFlags.DisplayedCollapsed) != 0;

    /// <summary>
    ///     Gets a whether this layer is a reference image layer.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if this layer is a reference layer;
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsReferenceLayer => (Flags & LayerFlags.ReferenceLayer) != 0;

    /// <summary>
    ///     Gets a reference to the parent <see cref="LayerChunk"/> instance
    ///     of this layer; if there is one; otherwise, <see langword="null"/>.
    /// </summary>
    public LayerChunk? Parent => _parent;

    /// <summary>
    ///     Creates a new <see cref="LayerChunk"/> class instance
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
    internal LayerChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Layer)
    {
        Flags = (LayerFlags)reader.ReadWord();
        Type = (LayerType)reader.ReadWord();
        ChildLevel = reader.ReadWord();
        DefaultWidth = reader.ReadWord();
        DefaultHeight = reader.ReadWord();
        BlendMode = (BlendMode)reader.ReadWord();
        Opacity = reader.ReadByte();
        reader.Skip(3);
        Name = reader.ReadString();
        TilesetIndex = Type == LayerType.Tilemap ? (int)reader.ReadDword() : null;
    }

    /// <summary>
    ///     Adds a referenceo the <see cref="LayerChunk"/> that is a parent of
    ///     this layer.
    /// </summary>
    /// <param name="layer">
    ///     The <see cref="LayerChunk"/> instance that is a parent of this one.
    /// </param>
    internal void SetParentLayer(LayerChunk layer) => _parent = layer;
}
