using System.Drawing;

namespace AsepriteDecoder;

public sealed class AseCelChunk : AseChunk
{
    public ushort LayerIndex { get; }
    public Point Position { get; }
    public byte Opacity { get; }

}
