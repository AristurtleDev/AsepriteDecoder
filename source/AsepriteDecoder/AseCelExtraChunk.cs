using System.Drawing;

namespace AsepriteDecoder;

public sealed class AseCelExtraChunk : AseChunk
{
    public CelExtraFlags Flags { get; }
    public PointF PrecisePosition { get; }
    public SizeF Size { get; }

    public AseCelExtraChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.CelExtra)
    {
        Flags = (CelExtraFlags)reader.ReadDword();
        float x = reader.ReadFixed();
        float y = reader.ReadFixed();
        float w = reader.ReadFixed();
        float h = reader.ReadFixed();
        reader.Skip(16);

        PrecisePosition = new PointF(x, y);
        Size = new SizeF(w, h);
    }
}
