using nsoftware.IPWorks;
// See https://aka.ms/new-console-template for more information
namespace Server;

class Program
{
    static void Main(string[] args)
    {
        TCPServer server = new TCPServer();

        server.RuntimeLicense =
            "";
        
        server.LocalPort = 5000;


        server.OnConnected += OnConnectedHandler;
        server.OnConnectionRequest += OnConnectionRequestHandler;
        server.OnDataIn += OnDataInHandler;
        server.OnDisconnected += OnDisconnectedHandler;
        server.OnError += OnErrorHandler;
        
        try
        {
            // Start listening for incoming connections
            server.StartListening();
            Console.WriteLine("Server is listening on port 5000...");

            // Keep the server running
            Console.WriteLine("Press Enter to stop the server.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Stop the server
            if (server.Listening)
            {
                server.StopListening();
                Console.WriteLine("Server stopped.");
            }
        }
    }
    
    private static void OnConnectedHandler(object sender, TCPServerConnectedEventArgs e)
    {
        Console.WriteLine($"Client connected. Connection ID: {e.ConnectionId}");
    }

    private static void OnConnectionRequestHandler(object sender, TCPServerConnectionRequestEventArgs e)
    {
        Console.WriteLine($"Connection request from {e.Address}:{e.Port}");
        e.Accept = true;
    }
    
    private static void OnDataInHandler(object sender, TCPServerDataInEventArgs e)
    {
        Console.WriteLine($"Received from client: {e.Text}");
    }

    private static void OnDisconnectedHandler(object sender, TCPServerDisconnectedEventArgs e)
    {
        Console.WriteLine($"Client disconnected. Connection ID: {e.ConnectionId}");
    }

    private static void OnErrorHandler(object sender, TCPServerErrorEventArgs e)
    {
        Console.WriteLine($"Error: {e.Description}");
    }
}