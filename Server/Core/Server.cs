using System.Net;
using System.Net.Sockets;

namespace TestTask.Server.Core;

public class Server
{
    private readonly TcpListener TCPserver;
    private readonly RequestQueue requestQueue;

    /// <summary>
    /// The client to be processed
    /// </summary>
    /// <param name="maxThreads">Maximum number of packages</param>
    /// <param name="port">Server port</param>
    /// <param name="ip">Server ip</param>
    public Server(int maxThreads, int port = 80, string ip = "127.0.0.1")
    {
        TCPserver = new(IPAddress.Parse(ip), port);
        RequestQueue.MaxThreads = maxThreads;
        requestQueue = RequestQueue.GetInstance();
    }

    /// <summary>
    /// Start server method
    /// </summary>
    public void Start()
    {
        TCPserver.Start();
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tThe server is running");
        try
        {
            while (true)
                requestQueue.Add(TCPserver.AcceptTcpClient().GetStream());
        }
        catch (Exception excpet)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tSocketException: {excpet.Message}");
        }
        finally
        {
            TCPserver.Stop();
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tThe server is stopped");
        }
    }
}