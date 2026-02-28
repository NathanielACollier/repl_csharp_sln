

string targetFilePath = commonUtilitiesLib.File.getOutputPath("FilePositionReadWrite_Test1.txt");

string product = Read(path: targetFilePath, startPos: 65, length: 10);

Write(path: targetFilePath, startPos: 65, length: 10, "Hewlit Packard");



void Write(string path, int startPos, int length, string newTextToWrite)
{
    using (var fs = new System.IO.FileStream(path: path, mode: FileMode.Open, access: FileAccess.ReadWrite))
    using (var reader = new System.IO.StreamReader(fs, System.Text.Encoding.UTF8))
    {
        SeekCharacterPosition(reader, startPos);
        long bytePosition = reader.BaseStream.Position;
        
        // move the filestream
        fs.Seek(bytePosition, SeekOrigin.Begin);
        
        // form a string that is the exact length with the new content
        string textToWrite = EnsureTextIsLength(text: newTextToWrite, length: length);
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(textToWrite);
        fs.Write(buffer, 0, length);
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





