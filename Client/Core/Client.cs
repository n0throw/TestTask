using System.Reflection;

namespace TestTask.Client.Core;

/// <summary>
/// Client class, reads and sends data
/// </summary>
internal class Client
{
    private readonly string ip;
    private readonly int port;
    private readonly DataReaderTXT dataReader;

    internal Client(string ip, int port)
    {
        this.ip = ip;
        this.port = port;
        dataReader = new(Assembly.GetExecutingAssembly().Location.Replace("Client.dll", "") + "Data");
    }

    internal void Start()
    {
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tThe client is running");
        dataReader.GetData().ForEach(text =>
        {
            RequestSender requestSender = new(ip, port)
            {
                Message = text
            };
            Thread clientThread = new(new ThreadStart(requestSender.Processing));
            clientThread.Start();
        });
    }
}