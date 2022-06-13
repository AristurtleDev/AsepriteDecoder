namespace AsepriteDecoder;

public sealed class ExternalFileEntry
{
    public uint ID { get; }
    public string Name { get; }

    internal ExternalFileEntry(AseBinaryReader reader)
    {
        ID = reader.ReadDword();
        reader.Skip(8);
        Name = reader.ReadString();
    }
}
