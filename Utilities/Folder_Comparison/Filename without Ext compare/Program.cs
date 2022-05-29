
string sourceFolderPath = @"C:\Users\drxs3\Downloads\roms\snes";

string destinationFolderPath = @"C:\Users\drxs3\OneDrive\ROM\Super Nintendo";


var sourceFiles = System.IO.Directory.EnumerateFiles(path: sourceFolderPath).Select(fi=> new {
    Name = System.IO.Path.GetFileNameWithoutExtension(fi),
    Path = fi
});

var destinationFiles = System.IO.Directory.EnumerateFiles(path: destinationFolderPath).Select(fi=> new {
    Name = System.IO.Path.GetFileNameWithoutExtension(fi),
    Path= fi
});


// find files in sourcefile where the name isn't in the destination folder right???

var sourceNotInDestination = from sourceFI in sourceFiles
                            let destMatch = destinationFiles.FirstOrDefault(destFI=> string.Equals(sourceFI.Name, destFI.Name))
                            where destMatch == null
                            select sourceFI;

foreach( var fi in sourceNotInDestination){
    Console.WriteLine($"Moving in {fi.Path}");
}