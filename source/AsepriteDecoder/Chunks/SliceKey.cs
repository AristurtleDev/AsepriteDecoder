using System.Drawing;

namespace AsepriteDecoder;

public sealed class SliceKey
{
    public uint FrameIndex { get; }
    public long OriginX { get; }
    public long OriginY { get; }
    public uint Width { get; }
    public uint Height { get; }

    public long? CenterX { get; }
    public long? CenterY { get; }
    public uint? CenterWidth { get; }
    public uint? CenterHeight { get; }

    public long? PivotX { get; }
    public long? PivotY { get; }

    internal SliceKey(bool isNinePatch, bool hasPivot, AseBinaryReader reader)
    {
        FrameIndex = reader.ReadDword();
        OriginX = reader.ReadLong();
        OriginY = reader.ReadLong();
        Width = reader.ReadDword();
        Height = reader.ReadDword();

        CenterX = isNinePatch ? reader.ReadLong() : null;
        CenterY = isNinePatch ? reader.ReadLong() : null;
        CenterWidth = isNinePatch ? reader.ReadDword() : null;
        CenterHeight = isNinePatch ? reader.ReadDword() : null;

        PivotX = hasPivot ? reader.ReadLong() : null;
        PivotY = hasPivot ? reader.ReadLong() : null;
    }
}
