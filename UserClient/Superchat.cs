using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace UserClient
{
    public partial class Superchat : Form
    {

        static string userName;
        private const string host = "193.138.146.14";//193.138.146.14 //192.168.10.64
        private const int port = 4444;
        static TcpClient client;
        static NetworkStream stream;

        private int Lamporttime = 1;
        private List<string> Usernames;
        public Superchat()
        {
            InitializeComponent();
            Usernames = new List<string>();
            chat.Text = "";
        }

        private void Superchat_Load(object sender, EventArgs e)
        {

        }

       

        private void Connect_button_Click(object sender, EventArgs e)
        {
            if (Usernamebox.Text == "")
                return;
            userName = Usernamebox.Text;

            client = new TcpClient();

            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = userName + ";endmessage;status:login;sender:" + userName + ";timestamp:" + Lamporttime.ToString();
                byte[] data = Encoding.Unicode.GetBytes(message);
                //Progress<string> progress = new Progress<string>(text => chat.Text += text);
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                
                chat.Enabled = true;
                Usermessage.Enabled = true;
                selectuser.Enabled = true;
                Send_button.Enabled = true;
                Connect_button.Enabled = false;
                //Console.WriteLine("Добро пожаловать, {0}", userName);
                //SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private string FormatLine(string status, string target = "notarget")
        {
            if (target == "notarget")
                target = selectuser.Text;
            if (selectuser.Text == "all")
                status = "broadcast";
            return ";endmessage;status:" + status + ";sender:" + userName + ";target:" + target + ";timestamp:" + Lamporttime.ToString();
        }
        
        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[256]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    if (message == "" || message == null)
                        continue;
                    try
                    {
                        Regex gettype = new Regex(@"status:(\w*)");
                        Regex gettext = new Regex(@"(.*);endmessage");
                        Regex gettime = new Regex(@"timestamp:(\d*)");
                        Regex getsender = new Regex(@"sender:(\w*)");
                        MatchCollection matches = gettype.Matches(message);
                        string status = matches[0].Value.Split(':')[1];
                        string text = gettext.Match(message).Value.Split(';')[0];
                        string sender = getsender.Match(message).Value.Split(':')[1];
                        int time = Convert.ToInt32(gettime.Match(message).Value.Split(':')[1]);
                        if (status == "login")
                        {

                            UpdateUserList(text);
                            WriteMessage(text + " has joined chat!" + '\n');
                        }
                        else if (status == "recieve")
                        {
                            WriteMessage('\n' + sender + " : " + text + '\n');
                        }
                        if (time > Lamporttime)
                        {
                            Lamporttime = time;
                            timelabel.Text = "Lamport time: " + Lamporttime.ToString();
                        }
                    }
                    catch
                    {

                    }
                }
                catch
                {
                    Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    //Console.ReadLine();
                    Disconnect();
                }
            }
        }
        public void WriteMessage(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(WriteMessage), new object[] { text });
                return;
            }
            chat.Text += text + Environment.NewLine;
        }

        public void UpdateUserList(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateUserList), new object[] { text });
                return;
            }
            if(text != userName)
            {
                string message = userName + " is in the chat!" + FormatLine("send", text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            
            selectuser.Items.Add(text);
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            Lamporttime++;
            timelabel.Text = "Lamport time: " + Lamporttime.ToString();
            string message = Usermessage.Text + FormatLine("send");
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }
}
