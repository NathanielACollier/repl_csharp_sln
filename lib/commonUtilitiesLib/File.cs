using System;

namespace commonUtilitiesLib;

public static class File
{

    public static void WriteAllBytes(string filename, byte[] fileData){

        string filePath = getOutputPath(filename);
        System.IO.File.WriteAllBytes(filePath, fileData);
    }

    public static void WriteAllText(string fileName, string fileTextContents){
        string filePath = getOutputPath(fileName);
        System.IO.File.WriteAllText(filePath, fileTextContents);
    }

    public static string getOutputPath(string fileName){
        string outDir = System.IO.Path.Combine(
            Directory.getRootDirectory(),
            "output"
        );
        System.IO.Directory.CreateDirectory(outDir);

        string filePath = System.IO.Path.Combine(outDir, fileName);
        return filePath;
    }



}
