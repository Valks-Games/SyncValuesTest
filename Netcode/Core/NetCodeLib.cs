using Netcode.Client;
using Netcode.Server;

namespace Netcode;

public class NetCodeLib
{
    public static ENetClient Client { get; set; }
    public static ENetServer Server { get; set; }
	public static ILogger Logger { get; set; } = new DefaultLogger();
    
    private static bool EnetInitialized { get; set; }

    public static void Init()
    {
        if (Client == null)
        {
            Logger.LogWarning("Client has not been setup yet");
            return;
        }

        if (Server == null)
        {
            Logger.LogWarning("Server has not been setup yet");
            return;
        }

        try
        {
            EnetInitialized = ENet.Library.Initialize();
        }
        catch (DllNotFoundException)
        {
            var message = "ENet failed to initialize because enet.dll was not found. Please restart the game and make sure enet.dll is right next to the games executable. Because ENet failed to initialize multiplayer has been disabled.";
            Logger.LogWarning(message);
            return;
        }

        if (!EnetInitialized) // probably won't get logged but lets keep it here because why not
            Logger.LogWarning("Failed to initialize ENet! Remember ENet-CSharp.dll and enet.dll are required in order for ENet to run properly!");
    }

	public static async void StartServer(ushort port, int maxPlayers, CancellationTokenSource cts)
    {
        if (!EnetInitialized)
        {
            Logger.LogWarning("Tried to start server but ENet was not initialized properly");
            return;
        }

        await Server.StartAsync(port, maxPlayers, cts);
    }
}

public class DefaultLogger : ILogger
{
	public void Log(object obj, ConsoleColor color = ConsoleColor.Gray) => 
        Console.WriteLine(obj);
	public void LogWarning(object obj, ConsoleColor color = ConsoleColor.Yellow) => 
        Console.WriteLine(obj);
	public void LogErr(Exception e, string hint = "", ConsoleColor color = ConsoleColor.Red, [CallerFilePath] string filePath = default, [CallerLineNumber] int lineNumber = 0) => 
        Console.WriteLine($"{hint} {e}");
}

public interface ILogger
{
	void Log(object obj, ConsoleColor color = ConsoleColor.Gray);
	void LogWarning(object obj, ConsoleColor color = ConsoleColor.Yellow);
	void LogErr(Exception e, string hint = "", ConsoleColor color = ConsoleColor.Red, [CallerFilePath] string filePath = default, [CallerLineNumber] int lineNumber = 0);
}
