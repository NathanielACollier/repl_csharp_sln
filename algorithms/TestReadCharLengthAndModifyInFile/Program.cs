
using repos = TestReadCharLengthAndModifyInFile.repos;

Console.WriteLine("Application Started");

string targetFilePath = commonUtilitiesLib.File.getOutputPath("FilePositionReadWrite_Test1.txt");

string product = repos.FileCharPositionRepo.Read(path: targetFilePath, startPos: 65, length: 10);

repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 65, length: 10, "Chicago Cubs");
repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 122, length: 2, "RE");

Console.WriteLine("Application Done");








