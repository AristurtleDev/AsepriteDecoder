using System.Drawing;

namespace AsepriteDecoder;

public sealed class AseCelChunk : AseChunk
{
    public ushort LayerIndex { get; }
    public Point Position { get; }
    public byte Opacity { get; }
    public CelType Type { get; }

    internal AseCelChunk(uint length, Point position, byte opacity, CelType type)
        : base(length, ChunkType.Cel) => ()

}
