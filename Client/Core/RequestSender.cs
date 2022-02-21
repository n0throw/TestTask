using System.Net.Sockets;
using System.Text;

namespace TestTask.Client.Core;

/// <summary>
/// A class for emulating sending individual packets.
/// Since I haven't found how to split data into packets, when sending data, a new client is created
/// </summary>
internal class RequestSender
{
    private readonly NetworkStream stream;
    private string message;

    internal string Message
    {
        get => message;
        set => message = value;
    }

    internal RequestSender(string ip, int port) => stream = new TcpClient(ip, port).GetStream();

    /// <summary>
    /// Sends a message and receives a response
    /// </summary>
    internal void Processing()
    {
        string sendMessage = string.Empty;
        byte[] data = Encoding.Unicode.GetBytes(message);
        StringBuilder builder = new();

        stream.Write(data, 0, data.Length);

        data = new byte[256];

        do
        {
            while (stream.DataAvailable)
                builder.Append(Encoding.Unicode.GetString(data, 0, stream.Read(data, 0, data.Length)));

            sendMessage = builder.ToString();
        } while (sendMessage == string.Empty);

        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tServer Answer:\t{sendMessage}");
    }
}