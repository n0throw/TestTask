using System.Net.Sockets;
using System.Text;

namespace TestTask.Server.Core;

/// <summary>
/// This class handles the request
/// </summary>
internal class RequestHandler
{
    private readonly NetworkStream stream;
    /// <summary>
    /// Initializing an instance of a class with an indication of the request stream
    /// </summary>
    /// <param name="stream">Request stream</param>
    internal RequestHandler(NetworkStream stream) => this.stream = stream;

    /// <summary>
    /// This method processes the request
    /// </summary>
    internal void Processing()
    {
        // To process the request for at least one second
        Thread.Sleep(3000);

        StringBuilder builder = new();
        byte[] buffer = new byte[256];

        // Get message
        while (stream.DataAvailable)
            builder.Append(Encoding.Unicode.GetString(buffer, 0, stream.Read(buffer, 0, buffer.Length)));

        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\tRequest received...");

        // Sending a response to a request
        buffer = Encoding.Unicode.GetBytes(IsPalindrome(builder.ToString()));
        stream.Write(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// This method checks whether the string is a palindrome
    /// </summary>
    /// <param name="text">The string being checked</param>
    /// <returns>Result string</returns>
    private static string IsPalindrome(string text) => text.Reverse().SequenceEqual(text) ? "this is a palindrome" : "it's not a palindrome";
}