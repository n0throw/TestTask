namespace TestTask.Client.Core;

internal class DataReaderTXT : IDataReader
{
    public string PathDir { get; set; }

    public DataReaderTXT(string pathDir) => PathDir = pathDir;

    public List<(string fileName, string fileContent)>? GetData(string ex)
    {
        List<string> files;
        List<(string fileName, string fileContent)>? data = null;
        try
        {
            files = Directory.GetFiles(PathDir, "*", SearchOption.AllDirectories).ToList();
            data = new(files.Count);

            files.ForEach(file =>
            {
                if (Path.GetExtension(file) == ex)
                    data.Add((Path.GetFileNameWithoutExtension(file), Reader(Path.GetFullPath(file))));
            });

            if (data.Count == 0)
                throw new Exception("There is no valid data to send to the server");

            return data;
        }
        catch (Exception except)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\t{except.Message}");
        }

        return data;
    }

    public string Reader(string filePath) => new StreamReader(filePath).ReadToEnd();
}

