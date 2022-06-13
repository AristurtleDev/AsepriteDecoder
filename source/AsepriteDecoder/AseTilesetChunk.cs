using System.Drawing;
using AsepriteDecoder.Compression;

namespace AsepriteDecoder;

public sealed class AseTilesetChunk : AseChunk
{
    public uint ID { get; }
    public TilesetFlags Flags { get; }
    public uint NumberOfTiles { get; }
    public ushort TileWidth { get; }
    public ushort TileHeight { get; }
    public short BaseIndex { get; }
    public string Name { get; }
    public uint? ExternalFileID { get; }
    public uint? ExternalTilesetID { get; }
    public byte[]? TilesetImage { get; }

    public bool IncludesExternalFileLink => (Flags & TilesetFlags.ExternalFileLink) != 0;
    public bool IncludesTilesetImage => (Flags & TilesetFlags.TilesInsideFile) != 0;
    public bool TilemapsUseIDZero => (Flags & TilesetFlags.TilemapsUseIDZero) != 0;

    internal AseTilesetChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Tileset)
    {
        ID = reader.ReadDword();
        Flags = (TilesetFlags)reader.ReadDword();
        NumberOfTiles = reader.ReadDword();
        TileWidth = reader.ReadWord();
        TileHeight = reader.ReadWord();
        BaseIndex = reader.ReadShort();
        reader.Skip(14);
        Name = reader.ReadString();

        if(IncludesExternalFileLink)
        {
            ExternalFileID = reader.ReadDword();
            ExternalTilesetID = reader.ReadDword();
        }
        else
        {
            ExternalFileID = null;
            ExternalTilesetID = null;
        }

        if(IncludesTilesetImage)
        {
            uint dataLen = reader.ReadDword();
            byte[] compressedImage = reader.ReadBytes((int)dataLen);

            TilesetImage = ZlibDeflater.Deflate(compressedImage);
        }
        else
        {
            TilesetImage = null;
        }
    }
}
