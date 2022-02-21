namespace TestTask.Client.Core;

internal class DataReaderTXT : IDataReader
{
    public string PathDir { get; set; }

    public DataReaderTXT(string pathDir) => PathDir = pathDir;

    public List<string> GetData(string ex = ".txt")
    {
        List<string> files = Directory.GetFiles(PathDir, "*", SearchOption.AllDirectories).ToList();
        List<string> data = new(files.Count);

        files.ForEach(file => data.Add(Reader(Path.GetFullPath(file))));

        return data;
    }

    public string Reader(string filePath) => new StreamReader(filePath).ReadToEnd();
}

