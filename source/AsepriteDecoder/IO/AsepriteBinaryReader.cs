/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

using System.Text;
using AsepriteDecoder.Compression;

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Used to read the contents of an Aseprite file.
    /// </summary>
    public class AsepriteReader : IDisposable
    {
        private Stream _stream;
        private BinaryReader _reader;

        /// <summary>
        ///     Gets a reference to the underlying stream used to read the
        ///     contents of the Aseprite file.
        /// </summary>
        public Stream BaseStream => _stream;

        /// <summary>
        ///     Gets a value that indicates whether hte resources held by this
        ///     <see cref="AsepriteBinaryReader"/> class instance have been
        ///     disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Creates a new <see cref="AsepriteBinaryReader"/> class instance
        ///     initialized to read the contents of the file at the file path
        ///     provided.
        /// </summary>
        /// <param name="filePath">
        ///     The fully-qualified absolute path to the file to read.
        /// </param>
        public AsepriteReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be empty or null");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist");
            }

            _stream = File.OpenRead(filePath);
            _reader = new BinaryReader(_stream, Encoding.UTF8);
        }

        /// <summary>
        ///     Finalizer implementation to internally call Dispose
        /// </summary>
        ~AsepriteReader() => Dispose(false);

        /// <summary>
        ///     Reads the file header from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="FileHeader"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public FileHeader ReadFileHeader()
        {
            uint size = ReadDword();
            ushort magic = ReadWord();
            ushort frames = ReadWord();
            ushort width = ReadWord();
            ushort height = ReadWord();
            ushort depth = ReadWord();
            uint flags = ReadDword();
            ushort speed = ReadWord();
            Skip(8);
            byte transparent = ReadByte();
            Skip(3);
            ushort numColors = ReadWord();
            byte pixelWidth = ReadByte();
            byte pixelHeight = ReadByte();
            short gridX = ReadShort();
            short gridY = ReadShort();
            ushort gridWidth = ReadWord();
            ushort gridHeigh = ReadWord();

            return new FileHeader()
            {
                FileSize = size,
                MagicNumber = magic,
                FrameCount = frames,
                FrameWidth = width,
                FrameHeight = height,
                ColorDepth = depth,
                Flags = flags,
                Speed = speed,
                TransparentIndex = transparent,
                NumberOfColors = numColors,
                PixelWidth = pixelWidth,
                PixelHeight = pixelHeight,
                GridX = gridX,
                GridY = gridY,
                GridWidth = gridWidth,
                GridHeight = gridHeigh
            };
        }

        /// <summary>
        ///     Reads a frame header from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="FrameHeader"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public FrameHeader ReadFrameHeader()
        {
            uint len = ReadDword();
            ushort magic = ReadWord();
            ushort oldChunk = ReadWord();
            ushort duration = ReadWord();
            Skip(2);
            uint newChunk = ReadDword();

            return new FrameHeader()
            {
                Length = len,
                MagicNumber = magic,
                OldChunkCount = oldChunk,
                Duration = duration,
                NewChunkCount = newChunk
            };
        }

        /// <summary>
        ///     Reads a chunk header from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="ChunkHeader"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public ChunkHeader ReadChunkHeader()
        {
            uint len = ReadDword();
            ushort type = ReadWord();

            return new ChunkHeader()
            {
                Length = len,
                Type = type
            };
        }

        /// <summary>
        ///     Reads an old palette chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="OldPaletteChunk"/> record initialized with the
        ///     values read from the underlying stream.
        /// </returns>
        public OldPaletteChunk ReadOldPaletteChunk()
        {
            ushort numPackets = ReadWord();

            OldPalettePacket[] packets = new OldPalettePacket[numPackets];

            for (int i = 0; i < numPackets; i++)
            {
                byte toSkip = ReadByte();
                byte numColors = ReadByte();

                OldPaletteColor[] colors = new OldPaletteColor[numColors];

                for (int j = 0; j < numColors; j++)
                {
                    byte red = ReadByte();
                    byte green = ReadByte();
                    byte blue = ReadByte();

                    colors[j] = new OldPaletteColor(red, green, blue);
                }

                packets[i] = new OldPalettePacket(toSkip, numColors, colors);
            }

            return new OldPaletteChunk(numPackets, packets);
        }

        /// <summary>
        ///     Reads a layer chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="LayerChunk"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public LayerChunk ReadLayerChunk()
        {
            ushort flags = ReadWord();
            ushort type = ReadWord();
            ushort level = ReadWord();
            ushort defaultWidth = ReadWord();
            ushort defaultHeight = ReadWord();
            ushort blend = ReadWord();
            byte opacity = ReadByte();
            Skip(3);
            string name = ReadString();
            uint? tileset = type == 2 ? ReadDword() : null;

            return new LayerChunk(name)
            {
                Flags = flags,
                Type = type,
                ChildLevel = level,
                DefaultWidth = defaultWidth,
                DefaultHeight = defaultHeight,
                BlendMode = blend,
                Opacity = opacity,
                TilesetIndex = tileset
            };
        }

        /// <summary>
        ///     Reads a cel chunk from the underlying stream.
        /// </summary>
        /// <param name="chunkEnd">
        ///     The position within the underlying stream that marks the end of
        ///     this chunk.
        /// </param>
        /// <returns>
        ///     A <see cref="CelChunk"/> record initialized with teh values
        ///     read from the underlying stream.
        /// </returns>
        public CelChunk ReadCelChunk(long chunkEnd)
        {
            ushort layer = ReadWord();
            short x = ReadShort();
            short y = ReadShort();
            byte opacity = ReadByte();
            ushort type = ReadWord();
            Skip(7);

            if (type == 0 || type == 2)
            {
                ushort width = ReadWord();
                ushort height = ReadWord();
                byte[] data = ReadBytes((int)(chunkEnd - _stream.Position));

                return new CelChunk(layer, x, y, opacity, type)
                {
                    Width = width,
                    Height = height,
                    Pixels = type == 0 ? data : ZlibDeflater.Deflate(data)
                };
            }
            else if (type == 1)
            {
                ushort linkedFrame = ReadWord();

                return new CelChunk(layer, x, y, opacity, type)
                {
                    LinkedFrame = linkedFrame
                };
            }
            else if (type == 3)
            {
                ushort width = ReadWord();
                ushort height = ReadWord();
                ushort bitsPer = ReadWord();
                uint tileID = ReadDword();
                uint xFlip = ReadDword();
                uint yFlip = ReadDword();
                uint rotation = ReadDword();
                Skip(10);
                byte[] data = ReadBytes((int)(chunkEnd - _stream.Position));

                return new CelChunk(layer, x, y, opacity, type)
                {
                    Width = width,
                    Height = height,
                    BitsPerTile = bitsPer,
                    TileIDBitmask = tileID,
                    XFlipBitmask = xFlip,
                    YFlipBitmask = yFlip,
                    RotationBitmask = rotation,
                    Tiles = ZlibDeflater.Deflate(data)
                };
            }
            else
            {
                throw new UnknownCelTypeException("The type of cel to read is unknown", type);
            }
        }

        /// <summary>
        ///     Reads an external files chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     An <see cref="ExternalFilesChunk"/> record initialized with the
        ///     values read from the underlying stream.
        /// </returns>
        public ExternalFilesChunk ReadExternalFilesChunk()
        {
            uint count = ReadDword();
            Skip(8);

            ExternalFilesEntry[] entries = new ExternalFilesEntry[count];
            for (int i = 0; i < count; i++)
            {
                uint id = ReadDword();
                Skip(8);
                string name = ReadString();

                entries[i] = new ExternalFilesEntry(id, name);
            }

            return new ExternalFilesChunk(count, entries);
        }

        /// <summary>
        ///     Reads a mask chunk from the underlying stream.
        /// </summary>
        /// <param name="chunkEnd">
        ///     The position within the underlying stream that marks the end of
        ///     this chunk.
        /// </param>
        /// <returns>
        ///     A <see cref="MaskChunk"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public MaskChunk ReadMaskChunk(long chunkEnd)
        {
            short x = ReadShort();
            short y = ReadShort();
            ushort width = ReadWord();
            ushort height = ReadWord();
            Skip(8);
            string name = ReadString();
            byte[] data = ReadBytes((int)(chunkEnd - _stream.Position));

            return new MaskChunk(x, y, width, height, name, data);
        }

        /// <summary>
        ///     Reads a tags chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="TagsChunk"/> record initialized with the values 
        ///     read from the underlying stream.
        /// </returns>
        public TagsChunk ReadTagsChunk()
        {
            ushort count = ReadWord();
            Skip(8);

            Tag[] tags = new Tag[count];
            for (int i = 0; i < count; i++)
            {
                ushort from = ReadWord();
                ushort to = ReadWord();
                byte direction = ReadByte();
                Skip(8);
                byte[] color = ReadBytes(3);
                Skip(1);
                string name = ReadString();

                tags[i] = new Tag(from, to, direction, color, name);
            }

            return new TagsChunk(count, tags);
        }

        /// <summary>
        ///     Reads a palette chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="PaletteChunk"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public PaletteChunk ReadPaletteChunk()
        {
            uint newSize = ReadDword();
            uint first = ReadDword();
            uint last = ReadDword();
            Skip(8);

            PaletteEntry[] entries = new PaletteEntry[newSize];
            for (int i = 0; i <= newSize; i++)
            {
                ushort flags = ReadWord();
                byte red = ReadByte();
                byte green = ReadByte();
                byte blue = ReadByte();
                byte alpha = ReadByte();
                string? name = (flags & 1) != 0 ? ReadString() : null;

                entries[i] = new PaletteEntry(flags, red, green, blue, alpha)
                {
                    Name = name
                };
            }

            return new PaletteChunk(newSize, first, last, entries);
        }

        /// <summary>
        ///     Reads a user data chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="UserDataChunk"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public UserDataChunk ReadUserDataChunk()
        {
            uint flags = ReadDword();

            bool hasText = (flags & 1) != 0;
            bool hasColor = (flags & 2) != 0;

            string? text = hasText ? ReadString() : null;
            byte? red = hasColor ? ReadByte() : null;
            byte? green = hasColor ? ReadByte() : null;
            byte? blue = hasColor ? ReadByte() : null;
            byte? alpha = hasColor ? ReadByte() : null;

            return new UserDataChunk(flags)
            {
                Text = text,
                Red = red,
                Green = green,
                Blue = blue,
                Alpha = alpha
            };
        }

        /// <summary>
        ///     Reads a slice chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="SliceChunk"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public SliceChunk ReadSliceChunk()
        {
            uint count = ReadDword();
            uint flags = ReadDword();
            Skip(4);
            string name = ReadString();

            bool isNinePatch = (flags & 1) != 0;
            bool hasPivot = (flags & 2) != 0;

            SliceKey[] keys = new SliceKey[count];
            for (int i = 0; i < count; i++)
            {
                uint frame = ReadDword();
                int x = ReadLong();
                int y = ReadLong();
                uint width = ReadDword();
                uint height = ReadDword();

                int? centerX = isNinePatch ? ReadLong() : null;
                int? centerY = isNinePatch ? ReadLong() : null;
                uint? centerWidth = isNinePatch ? ReadDword() : null;
                uint? centerHeight = isNinePatch ? ReadDword() : null;

                int? pivotX = hasPivot ? ReadLong() : null;
                int? pivotY = hasPivot ? ReadLong() : null;

                keys[i] = new SliceKey(frame, x, y, width, height)
                {
                    CenterX = centerX,
                    CenterY = centerY,
                    CenterWidth = centerWidth,
                    CenterHeight = centerHeight,
                    PivotX = pivotX,
                    PivotY = pivotY
                };
            }

            return new SliceChunk(count, flags, name, keys);
        }

        /// <summary>
        ///     Reads a tileset chunk from the underlying stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="TilesetChunk"/> record initialized with the values
        ///     read from the underlying stream.
        /// </returns>
        public TilesetChunk ReadTilesetChunk()
        {
            uint id = ReadDword();
            uint flags = ReadDword();
            uint count = ReadDword();
            ushort width = ReadWord();
            ushort height = ReadWord();
            short baseIndex = ReadShort();
            Skip(14);
            string name = ReadString();

            bool includeLink = (flags & 1) != 0;
            bool hasEmbeddedImage = (flags & 2) != 0;

            uint? externalFileID = includeLink ? ReadDword() : null;
            uint? externalTilesetID = includeLink ? ReadDword() : null;

            byte[]? embeddedImage = null;

            if (hasEmbeddedImage)
            {
                uint len = ReadDword();
                byte[] buffer = ReadBytes((int)len);

                embeddedImage = ZlibDeflater.Deflate(buffer);
            }

            return new TilesetChunk(id, flags, count, width, height, baseIndex, name)
            {
                ExternalFileID = externalFileID,
                ExternalTilesetID = externalTilesetID,
                TilesetImage = embeddedImage
            };
        }

        /// <summary>
        ///     Reads 1-byte from the underlying stream and advances the stream
        ///     by one byte.
        /// </summary>
        /// <returns>
        ///     The byte that was read from the underlying stream.
        /// </returns>
        private byte ReadByte() => _reader.ReadByte();

        /// <summary>
        ///     Reads the specified number of bytes from the underlying stream
        ///     and advances the stream by that number of bytes.
        /// </summary>
        /// <param name="count">
        ///     The total number of bytes to read from the underyling stream.
        /// </param>
        /// <returns>
        ///     A byte array containing the bytes read from the underlying
        ///     stream.  This may contain less than the number requested if the
        ///     end of stream was reached while reading.
        /// </returns>
        private byte[] ReadBytes(int count) => _reader.ReadBytes(count);

        /// <summary>
        ///     Reads a 2-byte unsigned integer form the underlying stream and
        ///     advances the stream by four bytes.
        /// </summary>
        /// <returns>
        ///     The 2-byte unsigned integer read from the underlying stream.
        /// </returns>        
        private ushort ReadWord() => _reader.ReadUInt16();

        /// <summary>
        ///     Reads a 2-byte signed integer form the underlying stream and
        ///     advances the stream by four bytes.
        /// </summary>
        /// <returns>
        ///     The 2-byte signed integer read from the underlying stream.
        /// </returns>
        private short ReadShort() => _reader.ReadInt16();

        /// <summary>
        ///     Reads a 4-byte unsigned integer form the underlying stream and
        ///     advances the stream by four bytes.
        /// </summary>
        /// <returns>
        ///     The 4-byte unsigned integer read from the underlying stream.
        /// </returns>        
        private uint ReadDword() => _reader.ReadUInt32();

        /// <summary>
        ///     Reads a 4-byte signed integer form the underlying stream and
        ///     advances the stream by four bytes.
        /// </summary>
        /// <returns>
        ///     The 4-byte signed integer read from the underlying stream.
        /// </returns>
        private int ReadLong() => _reader.ReadInt32();

        /// <summary>
        ///     REads a string from the current stream.  Strings are prepended
        ///     byt a 2-byte unsigned integer that indicates the length of the
        ///     string.  When this method returns, the underlying stream will
        ///     be advanced by 2+len bytes.
        /// </summary>
        /// <returns>
        ///     The string read from the underlying stream.
        /// </returns>
        private string ReadString() => Encoding.UTF8.GetString(ReadBytes(ReadWord()));

        /// <summary>
        ///     Skips the number of bytes given by advancing the position of
        ///     the underlying stream by that many bytes.
        /// </summary>
        /// <param name="count">
        ///     The number of bytes to skip.
        /// </param>
        private void Skip(int count) => _stream.Position += count;

        /// <summary>
        ///     Releases resources held by this 
        ///     <see cref="AsepriteBinaryReader"/> class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases resources held by this 
        ///     <see cref="AsepriteBinaryReader"/> class instance.
        /// </summary>
        /// <param name="disposeManaged">
        ///     Whether managed resources should be disposed of.
        /// </param>
        /// <remarks>
        ///     The value of <paramref name="disposeManaged"/> should only be
        ///     <see langword="true"/> when calling this method from the
        ///     main thread.
        /// </remarks>
        public void Dispose(bool disposeManaged)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposeManaged)
            {
                _reader.Dispose();
            }

            IsDisposed = true;
        }
    }
}