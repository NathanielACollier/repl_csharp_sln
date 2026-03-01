namespace TestReadCharLengthAndModifyInFile.repos;

public static class FileCharPositionRepo
{
    public static string Read(string path, int startPos, int length)
    {
        var position = new models.SetPositionWithTextInfoModel
        {
            StartPosition = startPos,
            Length = length,
            Text = ""
        };

        var positionList = new List<models.SetPositionWithTextInfoModel>();
        positionList.Add(position);

        Read(path: path, textPositionsToRead: positionList);
        return position.Text;
    }


    public static void Read(string path, IEnumerable<models.SetPositionWithTextInfoModel> textPositionsToRead)
    {
        var textPositionsQueue = new System.Collections.Generic.Queue<models.SetPositionWithTextInfoModel>(
            textPositionsToRead.OrderBy(r => r.StartPosition)
        );

        long currentCharPos = 0;
        
        using (var fs = new System.IO.FileStream(path: path, mode: FileMode.Open, access: FileAccess.ReadWrite))
        using (var reader = new System.IO.StreamReader(fs, System.Text.Encoding.UTF8))
        {
            while (true)
            {
                var currentTextPosition = DequeueWithFixes(textPositionsQueue);
                if (currentTextPosition == null)
                {
                    break;
                }
                
                SeekCharacterPosition(reader, lastCharPos: currentCharPos,
                    positionToReadTo: currentTextPosition.StartPosition);
                currentCharPos = currentTextPosition.StartPosition;

                // Read the specified number of characters
                char[] buffer = new char[currentTextPosition.Length];
                int charsRead = reader.Read(buffer, 0, currentTextPosition.Length);
                currentCharPos += currentTextPosition.Length;

                currentTextPosition.Text = new string(buffer, 0, charsRead);
            }
        }
    }


    private static void SeekCharacterPosition(StreamReader streamReader, long lastCharPos, long positionToReadTo)
    {
        // Skip to the starting position (startPos is 1-based, so we seek to startPos-1)
        for (long i = lastCharPos; i < positionToReadTo - 1; i++)
        {
            if (streamReader.Peek() == -1) break;
            streamReader.Read();
        }
    }


    public static void Write(string filePath, int startPosition, int length, string newTextToWrite)
    {
        Write(filePath: filePath,
            textPositionsToWriteList:[
                new models.SetPositionWithTextInfoModel
                {
                    Text = newTextToWrite,
                    Length = length,
                    StartPosition = startPosition
                }
            ]);
    }

    
    
    public static void Write(string filePath, IEnumerable<models.SetPositionWithTextInfoModel> textPositionsToWriteList)
    {
        string tempPath = filePath + ".tmp";

        try
        {
            var replacementsQueue = new System.Collections.Generic.Queue<models.SetPositionWithTextInfoModel>(
                textPositionsToWriteList.OrderBy(r => r.StartPosition)
            );

            using (var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var targetStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            using (var reader = new StreamReader(sourceStream, System.Text.Encoding.UTF8))
            using (var writer = new StreamWriter(targetStream, System.Text.Encoding.UTF8))
            {
                int currentCharPosition = 0;
                bool writingNewText = false;
                int newTextIndex = 0;
                models.SetPositionWithTextInfoModel currentNewText = DequeueWithFixes(replacementsQueue);

                // Read character by character
                while (true)
                {
                    int charCode = reader.Read();
                    if (charCode == -1) break;

                    char currentChar = (char)charCode;

                    // Check if we should start writing new text
                    if (currentNewText != null && currentCharPosition == currentNewText.StartPosition - 1)
                    {
                        writingNewText = true;
                        newTextIndex = 0;
                    }

                    // Write appropriate character
                    if (writingNewText)
                    {
                        writer.Write(currentNewText.Text[newTextIndex]);
                        newTextIndex++;

                        // We've written all new text, so stop replacement
                        if (newTextIndex >= currentNewText.Text.Length)
                        {
                            writingNewText = false;
                            // grab the next one from the queue
                            currentNewText = DequeueWithFixes(replacementsQueue);
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



    private static models.SetPositionWithTextInfoModel DequeueWithFixes(
        System.Collections.Generic.Queue<models.SetPositionWithTextInfoModel> queue)
    {
        if (queue.Count < 1)
        {
            return null;
        }
        
        var newText = queue.Dequeue();
        newText.Text = repos.TextUtilityRepo.EnsureTextIsLength(text: newText.Text, length: newText.Length);
        return newText;
    }
    
    
    
    
}