
using TestReadCharLengthAndModifyInFile.models;
using repos = TestReadCharLengthAndModifyInFile.repos;
using models = TestReadCharLengthAndModifyInFile.models;

Console.WriteLine("Application Started");

string targetFilePath = commonUtilitiesLib.File.getOutputPath("FilePositionReadWrite_Test1.txt");

var product = new models.SetPositionWithTextInfoModel
{
    StartPosition = 124, Length = 12
};
var city = new models.SetPositionWithTextInfoModel
{
    StartPosition = 65, Length = 10
};
var phone = new models.SetPositionWithTextInfoModel
{
    StartPosition = 110, Length = 12
};

var fieldList = new models.SetPositionWithTextInfoModel[]
{
    product,
    city,
    phone
};

repos.FileCharPositionRepo.Read(path: targetFilePath, textPositionsToRead: fieldList);
Console.WriteLine($"Product is {product.Text}");
Console.WriteLine($"City is {city.Text}");
Console.WriteLine($"Phone is {phone.Text}");

/*
repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 65, length: 10, "Chicago Cubs");
repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 122, length: 2, "RE");
repos.FileCharPositionRepo.Write(filePath: targetFilePath, startPosition: 124, length: 12, "Apple Sauce");
*/

product.Text = "Reeses Cups";
city.Text = "Bryant, AR";
phone.Text = "902-555-1234";

repos.FileCharPositionRepo.Write(filePath: targetFilePath, textPositionsToWriteList: fieldList);

Console.WriteLine("Application Done");








