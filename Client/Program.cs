using nsoftware.IPWorks;
// See https://aka.ms/new-console-template for more information
namespace Client;
class Program
{
    static void Main(string[] args)
    {
        // Initialize the TCP client
        TCPClient tcpClient = new TCPClient
        {
            RemoteHost = "127.0.0.1", // Replace with your server's IP address or hostname
            RemotePort = 5000        // Replace with your server's port number
        };

        tcpClient.RuntimeLicense =
            "";
        
        try
        {
            // Connect to the server
            tcpClient.Connect();
            Console.WriteLine("Enter data to send to the server (type 'exit' to quit):");

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;
                
                tcpClient.SendText(input);
                Console.WriteLine($"Sent");
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Disconnect from the server
            if (tcpClient.Connected)
            {
                tcpClient.Disconnect();
            }
        }
    }
    
    private static void OnConnectedHandler(object sender, TCPClientConnectedEventArgs e)
    {
        Console.WriteLine("Successfully connected to the server.");
    }
    private static void OnDataInHandler(object sender, TCPClientDataInEventArgs e)
    {
        string receivedData = e.Text;
        Console.WriteLine($"Received from server: {receivedData}");
    }

    private static void OnDisconnectedHandler(object sender, TCPClientDisconnectedEventArgs e)
    {
        Console.WriteLine("Disconnected from the server.");
    }

    private static void OnErrorHandler(object sender, TCPClientErrorEventArgs e)
    {
        Console.WriteLine($"Error: {e.Description}");
    }
}
