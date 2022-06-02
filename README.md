<h1 align="center">
<img src="https://raw.githubusercontent.com/AristurtleDev/Branding/main/imgs/aristurtle-logo/aristurtle-logo-256-dark.png#gh-light-mode-only" alt="Aristurtle" width="256">
<img src="https://raw.githubusercontent.com/AristurtleDev/Branding/main/imgs/aristurtle-logo/aristurtle-logo-256-light.png#gh-dark-mode-only" alt="Aristurtle" width="256">
<br/>
Aseprite Decoder
</h1>

### A C# Aseprite file reader and decoder
Reads the contents of an Aseprite file and provides the values as they are presented in the file. This allows users to implement the usage of the data in their projects as needed.

## Usage
In the current state, usage of this library requires knowledge of how an Aseprite file is structured and the internal values used there (e.g. a layer chunk is chunk type 0x2004).

```cs
//  Add using statement
using AsepriteDecoder.IO
```

```cs
using(AsepriteReader reader = new AsepritReader("path/to/aseprite/file"))
{
    //  Read file header
    FileHeader header = reader.ReadFileHeader();

    //  Read frame-by-frame
    for(int i = 0; i < header.Frames; i++)
    {
        //  Read frame header
        FrameHeader fHeader = reader.ReadFrameHeader();

        //  Read the chunks in the frame
        for(int j = 0; j < fHeader.Chunks; j++)
        {
            //  Read the chunk header
            ChunkHeader cHeader = reader.ReadChunkHeader();

            //  Read the chunk based on the type
            if(cHeader.Type = 0x2004)
            {
                LayerChunk layer = reader.ReadLayerChunk();
            }

            // ...
        }
    }
}
```

## License
Aseprite Decoder is licensed under the MIT license.  For the full license text, please refer to the [LICENSE](./LICENSE) file.

<br />
<br />
<br />

---
The Aristurle logo design, OCTOTURTLE, and the OCTOTURTLE deisgn are copyright © 2022 by Christopher Whitley. All rights reserved.

Usage of the OCTOTURTLE design is not allowed without the express written permission by Christopher Whitley
