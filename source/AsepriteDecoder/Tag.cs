using System.Drawing;

namespace AsepriteDecoder;

public sealed class Tag
{
    public ushort From { get; }
    public ushort To { get; }
    public LoopDirection LoopDirection { get; }
    public Color Color { get; }
    public string Name { get; }

    public Tag(AseBinaryReader reader)
    {
        From = reader.ReadWord();
        To = reader.ReadWord();
        LoopDirection = (LoopDirection)reader.ReadByte();
        reader.Skip(8);
        byte r = reader.ReadByte();
        byte g = reader.ReadByte();
        byte b = reader.ReadByte();
        reader.Skip(1);
        Name = reader.ReadString();

        Color = Color.FromArgb(255, r, g, b);

    }
}
