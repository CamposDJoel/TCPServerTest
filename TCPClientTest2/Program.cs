//Joel Campos
//10/24/2024
//TCP Client test 2

using System.Net.Sockets;
using System.Net;
using System.Text;

public class TCPClient
{
    public static void Main(string[] args)
    {
        string ServerIP = "24.2.153.141";
        int port = 1560;
        Console.WriteLine(string.Format("Connecting to server on IP: {0} | Port: {1}", ServerIP, port));
        IPAddress ip = IPAddress.Parse("24.2.153.141");
        //IPAddress ip = IPAddress.Parse("192.168.1.48");
        //IPAddress ip = IPAddress.Parse("2601:183:4c01:8a00::52");
        TcpClient client = new TcpClient();
        client.Connect(ip, port);
        Console.WriteLine("client connected!!");
        NetworkStream ns = client.GetStream();
        Thread thread = new Thread(o => ReceiveData((TcpClient)o));

        thread.Start(client);

        string s;
        while (!string.IsNullOrEmpty((s = Console.ReadLine())))
        {
            byte[] buffer = Encoding.ASCII.GetBytes(s);
            ns.Write(buffer, 0, buffer.Length);
        }

        client.Client.Shutdown(SocketShutdown.Send);
        thread.Join();
        ns.Close();
        client.Close();
        Console.WriteLine("disconnect from server!!");
        Console.ReadKey();
    }

    static void ReceiveData(TcpClient client)
    {
        NetworkStream ns = client.GetStream();
        byte[] receivedBytes = new byte[1024];
        int byte_count;

        while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
        {
            Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
        }
    }

    private Socket ConnectSocket(string server, int port)
    {
        Socket s = null;

        try
        {
            // Get host related information.
            IPAddress[] ips;
            ips = Dns.GetHostAddresses(server);

            Socket tempSocket = null;
            IPEndPoint ipe = null;

            ipe = new IPEndPoint((IPAddress)ips.GetValue(0), port);
            tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Console.Write("Attempting socket connection to " + ips.GetValue(0).ToString() + " on port " + port.ToString());
            tempSocket.Connect(ipe);

            if (tempSocket.Connected)
            {
                s = tempSocket;
                //s.SendTimeout = Coordinate.HL7SendTimeout;
                //s.ReceiveTimeout = Coordinate.HL7ReceiveTimeout;
            }
            else
            {
                return null;
            }

            return s;
        }
        catch (Exception e)
        {
            Console.Write("Error creating socket connection to " + server + " on port " + port.ToString());
            Console.Write("The error is: " + e.ToString());
            //if (g_NoOutputForThreading == false)
                //rtbResponse.AppendText("Error creating socket connection to " + server + " on port " + port.ToString());
            return null;
        }
    }
}