namespace TestTask.Client.Core;

internal interface IDataReader
{
    string PathDir { get; set; }
    List<string> GetData(string ex);
    string Reader(string filePath);
}