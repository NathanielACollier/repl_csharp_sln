
Console.WriteLine("Application Started");

string targetFilePath = commonUtilitiesLib.File.getOutputPath("FilePositionReadWrite_Test1.txt");

string product = Read(path: targetFilePath, startPos: 65, length: 10);

Write(filePath: targetFilePath, startPosition: 65, length: 10, "Chicago Cubs");
Write(filePath: targetFilePath, startPosition: 122, length: 2, "RE");

Console.WriteLine("Application Done");


void Write(string filePath, int startPosition, int length, string newTextToWrite)
{
    string tempPath = filePath + ".tmp";

    try
    {
        newTextToWrite = EnsureTextIsLength(text: newTextToWrite, length: length);


        using (var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (var targetStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
        using (var reader = new StreamReader(sourceStream, System.Text.Encoding.UTF8))
        using (var writer = new StreamWriter(targetStream, System.Text.Encoding.UTF8))
        {
            int currentCharPosition = 0;
            int charsToWrite = newTextToWrite.Length;
            bool writingNewText = false;
            int newTextIndex = 0;

            // Read and process characters one by one
            while (true)
            {
                int charCode = reader.Read();
                if (charCode == -1) break; // End of file

                if (currentCharPosition == startPosition - 1) // 1-based to 0-based
                {
                    // Start writing new text
                    writingNewText = true;
                    newTextIndex = 0;
                }

                if (writingNewText)
                {
                    if (newTextIndex < newTextToWrite.Length)
                    {
                        // Write character from new text
                        writer.Write(newTextToWrite[newTextIndex]);
                        newTextIndex++;
                    }
                    else
                    {
                        // Skip original character (we've written enough)
                        // Continue to next character
                    }
                }
                else
                {
                    // Copy original character
                    writer.Write((char)charCode);
                }

                currentCharPosition++;

                // If we've written all new text and skipped the original content
                if (writingNewText && newTextIndex >= newTextToWrite.Length)
                {
                    // Skip the original characters that were replaced
                    int charsToSkip = length - newTextToWrite.Length;
                    for (int i = 0; i < charsToSkip && !reader.EndOfStream; i++)
                    {
                        reader.Read(); // Skip
                        currentCharPosition++;
                    }

                    writingNewText = false;
                }
            }

            // Handle case where newText is longer than original content
            if (writingNewText && newTextIndex < newTextToWrite.Length)
            {
                // Write remaining new text
                for (int i = newTextIndex; i < newTextToWrite.Length; i++)
                {
                    writer.Write(newTextToWrite[i]);
                }
            }
        }

        // Atomic replacement
        File.Delete(filePath);
        File.Move(tempPath, filePath);
    }
    catch
    {
        if (File.Exists(tempPath))
            File.Delete(tempPath);
        throw;
    }
}


string EnsureTextIsLength(string text, int length)
{
    if (string.IsNullOrWhiteSpace(text))
    {
        return "".PadRight(totalWidth: length, paddingChar: ' ');
    }

    if (text.Length > length)
    {
        return text.Substring(0, length);
    }

    return text.PadRight(totalWidth: length, paddingChar: ' ');
}


string Read(string path, int startPos, int length)
{
    using (var fs = new System.IO.FileStream(path: path, mode: FileMode.Open, access: FileAccess.ReadWrite))
    using (var reader = new System.IO.StreamReader(fs, System.Text.Encoding.UTF8))
    {
        SeekCharacterPosition(reader, startPos);

        // Read the specified number of characters
        char[] buffer = new char[length];
        int charsRead = reader.Read(buffer, 0, length);

        return new string(buffer, 0, charsRead);
    }
}


void SeekCharacterPosition(StreamReader streamReader, int characterPosition)
{
    // Skip to the starting position (startPos is 1-based, so we seek to startPos-1)
    for (int i = 0; i < characterPosition - 1; i++)
    {
        if (streamReader.Peek() == -1) break;
        streamReader.Read();
    }
}