using System.Text;

namespace AsepriteDecoder.IO;

public sealed class AseBinaryReader : IDisposable
{
    private Stream _stream;
    private BinaryReader _reader;
    private bool _isDiposed;

    public AseBinaryReader(string filePath)
    {
        if(string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException("The file path cannot be null or an empty string");
        }

        if(!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Cannot locate the file '{filePath}'");
        }

        _stream = File.OpenRead(filePath);
        _reader = new BinaryReader(_stream, Encoding.UTF8);
    }

    ~AseBinaryReader() => Dispose(false);

    public byte ReadByte() => _reader.ReadByte();
    public byte[] ReadBytes(int count) => _reader.ReadBytes(count);
    public ushort ReadWord() => _reader.ReadUInt16();
    public short ReadShort() => _reader.ReadInt16();
    public uint ReadDword() => _reader.ReadUInt32();
    public long ReadLong() => _reader.ReadInt32();
    public float ReadFixed() => _reader.ReadSingle();
    public string ReadString() => Encoding.UTF8.GetString(ReadBytes(ReadWord()));
    public void Skip(int count) => _stream.Position += count;
    public void Seek(long pos) => _stream.Position = pos;


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposeManaged)
    {
        if(_isDiposed)
        {
            return;
        }

        if(disposeManaged)
        {
            _reader.Dispose();
        }

        _isDiposed = true;
    }
}
