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
    private string message = string.Empty;
    private string fileName = string.Empty;

    internal string Message
    {
        get => message;
        set => message = value;
    }

    internal string FileName
    {
        get => fileName;
        set => fileName = value;
    }

    internal RequestSender(string ip, int port) => stream = new TcpClient(ip, port).GetStream();

    /// <summary>
    /// Sends a message and receives a response
    /// </summary>
    internal void Processing()
    {
        SendMessage();
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tServer response to the file '{fileName}':\t{GetMessage()}");
    }

    private void SendMessage()
    {
        byte[] data = Encoding.Unicode.GetBytes(message);

        stream.Write(data, 0, data.Length);
    }

    private string GetMessage()
    {
        string getMessage;
        StringBuilder builder = new();

        byte[] data = new byte[256];

        do
        {
            while (stream.DataAvailable)
                builder.Append(Encoding.Unicode.GetString(data, 0, stream.Read(data, 0, data.Length)));

            getMessage = builder.ToString();
        } while (getMessage == string.Empty);

        return getMessage;
    }
}