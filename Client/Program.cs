namespace TestTask.Client;

public class Program
{
    private static Core.Client? client;
    public static void Main(string[] args)
    {
        // Cycle stop flag
        bool isStop = false;

        int? port = null;
        string? ip = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (isStop)
                break;

            switch (args[i])
            {
                case "-default":
                    isStop = true;
                    break;
                case "-port":
                    if (i + 1 >= args.Length)
                    {
                        Console.WriteLine("Invalid parameters");
                        return;
                    }
                    port = int.Parse(args[i + 1]);
                    break;
                case "-ip":
                    if (i + 1 >= args.Length)
                    {
                        Console.WriteLine("Invalid parameters");
                        return;
                    }
                    ip = args[i + 1];
                    break;
                default:
                    Console.WriteLine("Invalid parameters");
                    return;
            }
        }

        client = new Core.Client(ip ?? "127.0.0.1", port ?? 80);

        client.Start();
        Console.ReadKey();
    }
}