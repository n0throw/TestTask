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
    private readonly TcpClient tcpClient;

    internal string Message { get; set; }

    internal string FileName { get; set; }

    internal RequestSender(string ip, int port)
    {
        tcpClient = new TcpClient(ip, port);
        stream = tcpClient.GetStream();
    }

    /// <summary>
    /// Sends a message and receives a response
    /// </summary>
    internal void Processing()
    {
        SendMessage();
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tServer response to the file '{FileName}':\t{GetMessage()}");
        stream.Close();
        tcpClient.Close();
    }

    private void SendMessage()
    {
        byte[] data = Encoding.Unicode.GetBytes(Message);

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