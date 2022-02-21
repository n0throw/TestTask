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
        dataReader = new DataReaderTXT(Assembly.GetExecutingAssembly().Location.Replace("Client.dll", "") + "Data");
    }

    internal void Start()
    {
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tThe client is running");
        dataReader.GetData(".txt").ForEach(data =>
        {
            RequestSender requestSender = new(ip, port)
            {
                Message = data.fileContent,
                FileName = data.fileName
            };

            Thread clientThread = new(new ThreadStart(requestSender.Processing));
            clientThread.Start();
        });
    }
}