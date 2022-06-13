using System.Drawing;

namespace AsepriteDecoder;

public sealed class PaletteEntry
{
    public PaletteFlags Flags { get; }
    public Color Color { get; }
    public string? Name { get; }

    public bool HasName => (Flags & PaletteFlags.HasName) != 0;

    internal PaletteEntry(AseBinaryReader reader)
    {
        Flags = (PaletteFlags)reader.ReadWord();
        byte r = reader.ReadByte();
        byte g = reader.ReadByte();
        byte b = reader.ReadByte();
        byte a = reader.ReadByte();
        Name = HasName ? reader.ReadString() : null;

        Color = Color.FromArgb(a, r, g, b);
    }
}
