//Joel Campos
//3/29/2024
//Clien FORM

using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCPChatClientTest
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            txtOutputStatic = txtOutput;
        }
        NetworkStream ns;
        static TextBox txtOutputStatic;

        static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                txtOutputStatic.AppendText(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
            }

        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Visible = false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            TcpClient client = new TcpClient();
            client.Connect(ip, port);

            txtOutput.AppendText("Connection request sent." + Environment.NewLine);
            ns = client.GetStream();
            Thread thread = new Thread(o => ReceiveData((TcpClient)o));
            thread.Start(client);
            btnDisconnect.Visible = true;
            txtOutput.Enabled = true;
            txtMessage.Enabled = true;
            btnSend.Enabled = true;
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //TODO
        }
        private void btnSend_Click_1(object sender, EventArgs e)
        {
            string s = txtMessage.Text;
            byte[] buffer = Encoding.ASCII.GetBytes(s);
            ns.Write(buffer, 0, buffer.Length);
            txtOutput.AppendText(string.Format("You: {0}{1}", s, Environment.NewLine));
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}