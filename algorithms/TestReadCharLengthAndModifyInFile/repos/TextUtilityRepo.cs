namespace TestReadCharLengthAndModifyInFile.repos;

public static class TextUtilityRepo
{
    public static string EnsureTextIsLength(string text, int length)
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
    
    
}