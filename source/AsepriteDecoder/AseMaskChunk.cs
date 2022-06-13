using System.Drawing;

namespace AsepriteDecoder;

public sealed class AseMaskChunk : AseChunk
{
    public Point Position { get; }
    public Size Size { get; }
    public string Name { get; }
    public byte[] Data { get; }

    internal AseMaskChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Mask)
    {
        short x = reader.ReadShort();
        short y = reader.ReadShort();
        ushort w = reader.ReadWord();
        ushort h = reader.ReadWord();
        reader.Skip(8);
        Name = reader.ReadString();

        int dataLen = h * ((w + 7) / 8);
        Data = reader.ReadBytes(dataLen);

        Position = new Point(x, y);
        Size = new Size(w, h);
    }
}
