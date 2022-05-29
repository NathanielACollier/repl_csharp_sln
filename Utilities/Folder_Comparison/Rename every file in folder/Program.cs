

var folderPath = @"C:\Users\drxs3\Downloads\roms\Game Boy Advance";

var files = System.IO.Directory.EnumerateFiles(path: folderPath)
    .Select(filePath =>
    {
        var result = (Path: filePath, isRename: false, newName: "");
        var matches = System.Text.RegularExpressions.Regex.Match(filePath, @"(?<seq>\d+\s-\s)(?<name>.*)");

        if (matches.Success)
        {
            result.isRename = true;
            result.newName = matches.Groups["name"].Value;
        }

        return result;
    })
    .ToList();

foreach (var file in files.Where(_f => _f.isRename))
{
    Console.WriteLine($"Renaming ||\t\t{System.IO.Path.GetFileName(file.Path)}\t||\t\t to || {file.newName} ||");

    string destFilename = System.IO.Path.Combine(folderPath, file.newName);

    if (System.IO.File.Exists(destFilename))
    {
        Console.WriteLine($"Deleting\t\t{destFilename}");
        System.IO.File.Delete(destFilename);
    }
    
    System.IO.File.Move(sourceFileName: file.Path,
        destFileName: destFilename
        );
}
