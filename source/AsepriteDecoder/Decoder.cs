using System.Drawing;
using AsepriteDecoder.IO;

namespace AsepriteDecoder
{
    public static class Decoder
    {
        public static AsepriteFile FromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "A valid file path must be provided");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Unable to find the file at '{filePath}'");
            }

            using (AsepriteReader reader = new AsepriteReader(filePath))
            {
                FileHeader header = reader.ReadFileHeader();
                ThrowIfInvalidFileHeader(header);

                List<LayerChunk> layers = new List<LayerChunk>();
                Color[] palette = new Color[header.NumberOfColors];
                Frame[] frames = new Frame[header.FrameCount];
                Tag[]? tags = null;
                List<Slice>? slices = null;
                Dictionary<int, string>? externalFiles = null;
                Dictionary<int, Tileset>? tilesets = null;

                for (int f = 0; f < header.FrameCount; f++)
                {
                    List<CelChunk> cels = new List<CelChunk>();

                    FrameHeader frameHeader = reader.ReadFrameHeader();
                    ThrowIfInvalidFrameHeader(frameHeader);

                    int chunkCount = frameHeader.OldChunkCount;

                    if (chunkCount == 0xFFFF && chunkCount < frameHeader.NewChunkCount)
                    {
                        chunkCount = (int)frameHeader.NewChunkCount;
                    }

                    for (int c = 0; c < chunkCount; c++)
                    {
                        long chunkStart = reader.BaseStream.Position;

                        ChunkHeader chunkHeader = reader.ReadChunkHeader();
                        ChunkType chunkType = (ChunkType)chunkHeader.Type;

                        switch (chunkType)
                        {
                            case ChunkType.Layer:
                                layers.Add(reader.ReadLayerChunk());
                                break;

                            case ChunkType.Cel:
                                cels.Add(reader.ReadCelChunk(chunkStart + chunkHeader.Length));
                                break;

                            case ChunkType.ExternalFiles:
                                ExternalFilesChunk externalFilesChunk = reader.ReadExternalFilesChunk();

                                externalFiles = new Dictionary<int, string>();
                                for (int e = 0; e < externalFilesChunk.Count; e++)
                                {
                                    ExternalFilesEntry entry = externalFilesChunk.ExternalFiles[e];
                                    externalFiles.Add((int)entry.ID, entry.Name);
                                }
                                break;

                            case ChunkType.Tags:
                                TagsChunk tagsChunk = reader.ReadTagsChunk();

                                tags = new Tag[tagsChunk.Count];
                                for (int t = 0; t < tagsChunk.Count; t++)
                                {
                                    TagEntry tag = tagsChunk.Tags[t];
                                    Color color = Color.FromArgb(255, tag.Color[0], tag.Color[1], tag.Color[2]);
                                    tags[t] = new Tag(tag.From, tag.To, (LoopDirection)tag.Direction, color, tag.Name);
                                }
                                break;
                            case ChunkType.Palette:
                                PaletteChunk paletteChunk = reader.ReadPaletteChunk();

                                if (paletteChunk.NewSize > 0)
                                {
                                    Array.Resize(ref palette, (int)paletteChunk.NewSize);
                                }

                                int from = (int)paletteChunk.First;
                                int to = (int)paletteChunk.Last;

                                for (int p = 0, e = from; p < paletteChunk.NewSize || e <= to; p++, e++)
                                {
                                    PaletteEntry entry = paletteChunk.Entries[p];
                                    palette[e] = Color.FromArgb(entry.Alpha, entry.Red, entry.Green, entry.Blue);
                                }
                                break;

                            case ChunkType.UserData:
                                break;

                            case ChunkType.Slice:
                                if(slices is null)
                                {
                                    slices = new List<Slice>();
                                }
                                SliceChunk chunk = reader.ReadSliceChunk();
                                slices.Add(DecodeSliceChunk(chunk));
                                break;

                            case ChunkType.Tileset:
                                break;
                        }

                    }
                }
            }
        }

        private static Slice DecodeSliceChunk(SliceChunk chunk)
        {
            bool isNinePatch = (chunk.Flags & 1) != 0;
            bool hasPivot = (chunk.Flags & 2) != 0;

            SliceKey[] keys = new SliceKey[chunk.Count];

            for (int i = 0; i < chunk.Count; i++)
            {
                SliceKeyEntry entry = chunk.Keys[i];
                Rectangle? center = null;
                Point? pivot = null;

                int frame = (int)entry.Frame;
                Point origin = new Point(entry.OriginX, entry.OriginY);
                Size size = new Size((int)entry.Width, (int)entry.Height);

                if (isNinePatch)
                {
                    int cx = (int)entry.CenterX!;
                    int cy = (int)entry.CenterY!;
                    int cw = (int)entry.CenterWidth!;
                    int ch = (int)entry.CenterHeight!;
                    center = new Rectangle(cx, cy, cw, ch);
                }

                if (hasPivot)
                {
                    int px = (int)entry.PivotX!;
                    int py = (int)entry.PivotY!;
                    pivot = new Point(px, py);
                }

                keys[i] = new SliceKey(frame, origin, size)
                {
                    Center = center,
                    Pivot = pivot
                };

            }

            return new Slice(isNinePatch, hasPivot, chunk.Name, keys);
        }

        /// <summary>
        ///     Throws an exception if certain values within the file header are
        ///     invalid.
        /// </summary>
        /// <param name="header">
        ///     The <see cref="FileHeader"/> record to validate the values of.
        /// </param>
        /// <exception cref="InvalidContentException{ushort}">
        ///     Thrown if invalid values in the header are found.
        /// </exception>
        private static void ThrowIfInvalidFileHeader(FileHeader header)
        {
            if (header.MagicNumber != 0xA5E0)
            {
                string message = "The file header magic number is inavlid";
                throw new InvalidContentException<ushort>(message, nameof(header.MagicNumber), header.MagicNumber);
            }

            if (header.ColorDepth != 32 ||
               header.ColorDepth != 16 ||
               header.ColorDepth != 8)
            {
                string message = "Invalid color depth value in file header.";
                throw new InvalidContentException<ushort>(message, nameof(header.ColorDepth), header.ColorDepth);
            }

            if (header.FrameWidth < 1)
            {
                string message = "Invalid frame width";
                throw new InvalidContentException<ushort>(message, nameof(header.FrameWidth), header.FrameWidth);
            }

            if (header.FrameHeight < 1)
            {
                string message = "Invalid frame height";
                throw new InvalidContentException<ushort>(message, nameof(header.FrameHeight), header.FrameHeight);
            }
        }

        /// <summary>
        ///     Throws an exception if certain values within the frame header
        ///     are invalid.
        /// </summary>
        /// <param name="header">
        ///     The <see cref="FrameHeader"/> record to validate the values of.
        /// </param>
        /// <exception cref="InvalidContentException{ushort}">
        ///     Throw if invalid values in the header are found.
        /// </exception>
        private static void ThrowIfInvalidFrameHeader(FrameHeader header)
        {
            if (header.MagicNumber != 0xF1FA)
            {
                string message = "The frame header magic number is inavlid";
                throw new InvalidContentException<ushort>(message, nameof(header.MagicNumber), header.MagicNumber);
            }
        }
    }
}
