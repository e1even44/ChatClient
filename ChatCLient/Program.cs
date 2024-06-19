using System.Net.Sockets;
using System.Text;

class ChatClient
{
    private static TcpClient client;
    private static NetworkStream stream;

    static void Main(string[] args)
    {
        client = new TcpClient("172.30.62.42", 54321); // replace with server IP
        stream = client.GetStream();

        Thread readThread = new Thread(ReadMessages);
        readThread.Start();

        while (true)
        {
            string message = Console.ReadLine();
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }

    private static void ReadMessages()
    {
        byte[] buffer = new byte[1024];
        int byteCount;

        while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
            Console.WriteLine("Server: " + message);
        }
    }
}