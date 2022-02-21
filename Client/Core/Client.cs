using System.Reflection;

namespace TestTask.Client.Core;

/// <summary>
/// Client class, reads and sends data
/// </summary>
internal class Client
{
    private readonly string ip;
    private readonly int port;
    private readonly IDataReader dataReader;

    internal Client(string ip, int port)
    {
        this.ip = ip;
        this.port = port;
        dataReader = new DataReaderTXT(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Data");
    }

    internal void Start()
    {
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tThe client is running");
        List<(string fileName, string fileContent)>? data = dataReader.GetData(".txt");

        try
        {
            if (data != null)
            {
                data.ForEach(item =>
                {
                    RequestSender requestSender = new(ip, port)
                    {
                        Message = item.fileContent,
                        FileName = item.fileName
                    };

                    Thread clientThread = new(new ThreadStart(requestSender.Processing));
                    clientThread.Start();
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\t{ex.Message}");
        }
    }
}