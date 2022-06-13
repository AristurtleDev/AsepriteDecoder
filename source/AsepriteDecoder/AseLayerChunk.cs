namespace AsepriteDecoder;

public sealed class AseLayerChunk : AseChunk
{
    public LayerFlags Flags { get; }
    public LayerType Type { get; }
    public ushort ChildLevel { get; }
    public ushort DefaultWidth { get; }
    public ushort DefaultHeight { get; }
    public BlendMode BlendMode { get; }
    public byte Opacity { get; }
    public string Name { get; }
    public uint? TilesetIndex { get; }

    public bool IsVisible => (Flags & LayerFlags.Visible) != 0;

    internal AseLayerChunk(uint length, AseBinaryReader reader)
        :base(length, ChunkType.Layer)
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
        TilesetIndex = Type == LayerType.Tilemap ? reader.ReadDword() : null;
    }
}
