
using TestReadCharLengthAndModifyInFile.models;
using repos = TestReadCharLengthAndModifyInFile.repos;

Console.WriteLine("Application Started");

string targetFilePath = commonUtilitiesLib.File.getOutputPath("FilePositionReadWrite_Test1.txt");

string product = repos.FileCharPositionRepo.Read(path: targetFilePath, startPos: 65, length: 10);
Console.WriteLine($"Product is {product}");

/*
repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 65, length: 10, "Chicago Cubs");
repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 122, length: 2, "RE");
repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 124, length: 12, "Apple Sauce");
*/

repos.FileCharPositionRepo.Write(filePath: targetFilePath, replacementsInputArg: [
    new SetPositionWithTextInfoModel
    {
        StartPosition = 65,Length = 10,
        Text = "Little Rock"
    },
    new SetPositionWithTextInfoModel
    {
        StartPosition = 122, Length = 2,
        Text = "RZ"
    },
    new SetPositionWithTextInfoModel
    {
        StartPosition = 124, Length = 12,
        Text = "Peanut Butter Jam"
    }
]);

Console.WriteLine("Application Done");








