namespace TestTask.Client.Core;

internal interface IDataReader
{
    string PathDir { get; set; }
    List<(string fileName, string fileContent)>? GetData(string ex);
    string Reader(string filePath);
}