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
        private const string host = "192.168.10.35";//193.138.146.14 //192.168.10.64
        private const int port = 4444;
        static TcpClient client;
        static NetworkStream stream;
        static Random random = new Random();
        static System.Threading.Timer randomtimer;

        private int queuenumber = 1;
        private bool crittoken = true;
        private bool needcritprocess = false;
        private int Lamporttime = 1;
        private List<string> Usernames;
        public Superchat()
        {
            InitializeComponent();
            Usernames = new List<string>();
            chat.Text = "";
            randomtimer = new System.Threading.Timer(TimerCallback, null, random.Next(10000, 20000), -1);
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

                string message = userName + ";endmessage;status:login;sender:" + userName + ";target:all;timestamp:" + Lamporttime.ToString();
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
            if (target == "all")
                status = "broadcast";
            return ";endmessage;status:" + status + ";sender:" + userName + ";target:" + target + ";timestamp:" + Lamporttime.ToString();
        }
        private void SendMessage(string text, string status, string target = "notarget")
        {
            string message = text + FormatLine(status, target);
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        
        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024]; // буфер для получаемых данных
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
                        Regex gettarget = new Regex(@"target:(\w*)");
                     
                        MatchCollection matches = gettype.Matches(message);
                        string status = matches[0].Value.Split(':')[1];
                        string text = gettext.Match(message).Value.Split(';')[0];
                        string sender = getsender.Match(message).Value.Split(':')[1];
                        string target = gettarget.Match(message).Value.Split(':')[1];
                        int time = Convert.ToInt32(gettime.Match(message).Value.Split(':')[1]);
                        if (status == "login")
                        {
                            if (target == "all")
                                UpdateAndResponse(text);
                            else
                                UpdateUserList(text);
                            WriteMessage(text + " has joined chat!" + '\n');
                        }
                        else if (status == "recieve")
                        {
                            WriteMessage('\n' + sender + " : " + text + '\n');
                        }else if(status == "critzone")
                        {
                            var elements = text.Split(' ');
                            var action = elements[0];
                            int foregnnumber = Convert.ToInt32(elements[1]);
                            if(action == "sendtoken")
                            {
                                crittoken = true;
                                WriteMessage("Can enter critical zone now!");
                                if (needcritprocess)
                                {
                                    CritProcess();
                                }
                            }
                            else if(action == "asktoken")
                            {
                                if(crittoken == true)
                                {
                                    crittoken = false;
                                    WriteMessage("Can't enter critical zone no more!");
                                    SendMessage("sendtoken " + queuenumber.ToString(), "critzone", sender);
                                }
                            }
                            else if(action == "askqueue")
                            {
                                SendMessage("sendqueue " + queuenumber.ToString(), "critzone", sender);
                            }else if(action == "sendqueue")
                            {
                                if(foregnnumber >= queuenumber)
                                {
                                    queuenumber = foregnnumber + 1;
                                }
                            }
                        }
                        if (time > Lamporttime)
                        {
                            UpdateLamportTime(time);
                            
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
        public void CritProcess()
        {
            if (crittoken == false)
                return;
            int duration = random.Next(5, 15);
            WriteMessage("Entering critical zone!");
            for(int i = 0; i < duration; i++)
            {
                Thread.Sleep(500);
                WriteMessage("Calculations in process...");
                Thread.Sleep(500);
            }
            WriteMessage("Calculations finished! Leaving critical zone");
            needcritprocess = false;
            UpdateQueue();
        }
        public void UpdateQueue()
        {
            foreach(var user in selectuser.Items)
            {
                if(user.ToString() != "all")
                    SendMessage("askqueue " + queuenumber.ToString(), "critzone", user.ToString());
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
            
            
            selectuser.Items.Add(text);
        }
        public void UpdateAndResponse(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateAndResponse), new object[] { text });
                return;
            }
            
                string message = userName + FormatLine("login", text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            

            selectuser.Items.Add(text);
        }

        public void UpdateLamportTime(int newtime)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(UpdateLamportTime), new object[] { newtime });
                return;
            }

            Lamporttime = newtime;
            timelabel.Text = "Lamport time: " + Lamporttime.ToString();

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
            UpdateLamportTime(Lamporttime + 1);
            string message = Usermessage.Text + FormatLine("send");
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        private void InternalEvent_Click(object sender, EventArgs e)
        {
            UpdateLamportTime(Lamporttime + 1);
            WriteMessage("An internal event has taken place!");
        }

        private void TimerCallback(object obj)
        {
            UpdateLamportTime(Lamporttime + 1);
            WriteMessage("An internal random event has taken place!");
            randomtimer.Change(random.Next(10000, 30000), -1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            needcritprocess = true;
            if (crittoken)
                CritProcess();
            else
            {
                foreach (var user in selectuser.Items)
                {
                    if (user.ToString() != "all")
                        SendMessage("asktoken " + queuenumber.ToString(), "critzone", user.ToString());
                }
                
            }
        }
    }
}
