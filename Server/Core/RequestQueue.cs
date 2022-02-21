using System.Net.Sockets;
using System.Text;

namespace TestTask.Server.Core;

/// <summary>
/// This class emulates asynchronous processing of N requests stream
/// </summary>
internal class RequestQueue
{
    private List<Thread> data;
    private static RequestQueue instance;

    /// <summary>
    /// Maximum value of processed threads
    /// </summary>
    internal static int MaxThreads { get; set; }

    private RequestQueue() => data = new();

    /// <summary>
    /// This method returns an instance of the class
    /// </summary>
    internal static RequestQueue GetInstance()
    {
        if (instance == null)
            instance = new RequestQueue();

        return instance;
    }

    /// <summary>
    /// This method adds a new request stream
    /// </summary>
    /// <param name="stream">Request stream</param>
    internal void Add(NetworkStream stream)
    {
        Update();

        if (data.Count != MaxThreads)
        {
            RequestHandler rh = new(stream);
            Thread rhThread = new(new ThreadStart(rh.Processing));
            rhThread.Start();

            data.Add(rhThread);
        }
        else
            SendError(stream, "The server is overloaded");
    }

    /// <summary>
    /// This method updates the request stream collection
    /// </summary>
    private void Update()
    {
        List<Thread> threads = new(data.Count);

        foreach (Thread thread in data)
            if (thread.IsAlive)
                threads.Add(thread);

        data = new(threads);
    }

    /// <summary>
    /// This method sends an error to the request stream
    /// </summary>
    /// <param name="stream">Request stream to send an error</param>
    /// <param name="message">Error message</param>
    private static void SendError(NetworkStream stream, string message)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(message);
        stream.Write(buffer, 0, buffer.Length);
        stream.Close();
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]\t{message}");
    }
}