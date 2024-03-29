//Joel Campos
//3/29/24
//Server FORM

using SimpleTCP;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPChatServerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtOutput = txtStatus;
        }

        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        static readonly Dictionary<int, Match> list_Matches = new Dictionary<int, Match>();
        static int clinetcount = 1;
        static int matchcount = 1;
        static TcpListener ServerSocket;
        static TextBox txtOutput;
        Thread waitingforclients;

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            ServerSocket = new TcpListener(IPAddress.Any, 5000);
            ServerSocket.Start();

            //Start the first match
            list_Matches.Add(matchcount, new Match(matchcount));
            txtStatus.AppendText(string.Format("Match ID: {0} open for players.{1}", matchcount, Environment.NewLine));
            waitingforclients = new Thread(WaitForClients);
            waitingforclients.Start();
            btnStop.Visible = true;
        }
        private void btnStop_Click_1(object sender, EventArgs e)
        {
            btnStop.Visible = false;
            ServerSocket.Stop();
            list_Matches.Clear();
            txtStatus.Clear();
            waitingforclients.Suspend();
            btnStart.Visible = true;
        }
        public static void WaitForClients()
        {
            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(clinetcount, client);
                txtOutput.AppendText(string.Format("Client ID: {0} connected!{1}", clinetcount, Environment.NewLine));

                //add the client to the current active match
                Match activeMatch = list_Matches[matchcount];
                activeMatch.addPlayer(clinetcount);
                txtOutput.AppendText(string.Format("Client added to Match ID: {0}{1}", matchcount, Environment.NewLine));

                if (activeMatch.IsMatchReady())
                {

                    txtOutput.AppendText("2 Players joined the match, match is ready!" + Environment.NewLine);

                    //Now create a new match to be open
                    matchcount++;
                    list_Matches.Add(matchcount, new Match(matchcount));
                    txtOutput.AppendText(string.Format("Match ID: {0} open for players.{1}", matchcount, Environment.NewLine));
                }

                Thread t = new Thread(handle_clients);
                t.Start(clinetcount);
                clinetcount++;
                forwardMessage("You connected successfully!!", client);
            }
        }
        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            while (true)
            {
                TcpClient opponentClient;
                Match ActiveMatch = null;

                //find the match this client is at
                foreach (KeyValuePair<int, Match> match in list_Matches)
                {
                    Match thisMatch = match.Value;

                    if (thisMatch.ContainsPlayer(id))
                    {
                        ActiveMatch = thisMatch;
                        break;
                    }
                }

                if (ActiveMatch.IsMatchReady())
                {
                    //Set the ref to the opponent client
                    int opponentClientID = ActiveMatch.GetOpponentPlayerID(id);
                    opponentClient = list_clients[opponentClientID];
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);

                    if (byte_count == 0)
                    {
                        break;
                    }

                    string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                    string dataToForward = string.Format("Player ID {0}: {1}", opponentClientID, data);
                    txtOutput.AppendText(string.Format("Message from client ID {0}: [ {1} ]{2}", id, data, Environment.NewLine));
                    forwardMessage(dataToForward, opponentClient);
                    txtOutput.AppendText(string.Format("Message has been forwarded to client ID {0}{1}", opponentClientID, Environment.NewLine));
                }
            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        public static void forwardMessage(string data, TcpClient recepient)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                NetworkStream stream = recepient.GetStream();
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }

    public class Match
    {
        public Match(int id)
        {
            MatchID = id;

        }
        public void addPlayer(int playerid)
        {
            if (IDPlayerA == -1)
            {
                IDPlayerA = playerid;
            }
            else
            {
                IDPlayerB = playerid;
                MatchReady = true;
            }
        }
        public bool IsMatchReady()
        {
            return MatchReady;
        }
        public bool ContainsPlayer(int id)
        {
            return (IDPlayerA == id || IDPlayerB == id);
        }
        public int GetOpponentPlayerID(int id)
        {
            if (IDPlayerA == id)
            {
                return IDPlayerB;
            }
            else
            {
                return IDPlayerA;
            }

        }

        int MatchID;
        int IDPlayerA = -1;
        int IDPlayerB = -1;
        bool MatchReady = false;
    }

}