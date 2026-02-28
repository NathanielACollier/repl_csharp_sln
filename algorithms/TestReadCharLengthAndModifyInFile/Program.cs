
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
            bool writingNewText = false;
            int newTextIndex = 0;
            
            // Read character by character
            while (true)
            {
                int charCode = reader.Read();
                if (charCode == -1) break;
                
                char currentChar = (char)charCode;
                
                // Check if we should start writing new text
                if (currentCharPosition == startPosition - 1)
                {
                    writingNewText = true;
                    newTextIndex = 0;
                }
                
                // Write appropriate character
                if (writingNewText)
                {
                    writer.Write(newTextToWrite[newTextIndex]);
                    newTextIndex++;
                    
                    // We've written all new text, so stop replacement
                    if (newTextIndex >= newTextToWrite.Length)
                    {
                        writingNewText = false;
                    }
                }
                else
                {
                    writer.Write(currentChar);
                }
                
                currentCharPosition++;
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