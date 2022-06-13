namespace AsepriteDecoder;

public class AseFile
{
    public AseHeader Header { get; private set; }
    public List<AseFrame> Frames { get; private set; }

    private AseFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException(nameof(filePath), "The file path cannot be null or an empty string");
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Cannot locate the file '{filePath}'");
        }

        using (AseBinaryReader reader = new AseBinaryReader(filePath))
        {
            Header = new AseHeader(reader);
            Frames = new List<AseFrame>(Header.FrameCount);

            for (int i = 0; i < Header.FrameCount; i++)
            {
                Frames.Add(new AseFrame(this, reader));
            }
        }
    }
}
