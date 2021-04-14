using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using UserClient;

namespace ChatClient
{
    class Program
    {


        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Superchat Chat = new Superchat();
            Application.Run(Chat);
        }
       
    }
}