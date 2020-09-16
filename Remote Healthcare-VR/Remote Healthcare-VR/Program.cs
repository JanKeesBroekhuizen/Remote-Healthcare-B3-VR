using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace SimpleTCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("145.48.6.10", 6666);
            /*
            bool done = false;
            Console.WriteLine("Type 'bye' to end connection");
            while (!done)
            {
                Console.Write("Enter a message to send to server: ");
                string message = Console.ReadLine();

                WriteTextMessage(client, message);

                string response = ReadTextMessage(client);
                Console.WriteLine("Response: " + response);
                done = response.Equals("BYE");
            }
            */
            WriteID(client);

        }

        public static void WriteTextMessage(TcpClient client, string message)
        {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            {
                int messageLength = message.Length;
                byte[] dataLength = BitConverter.GetBytes(messageLength);
                byte[] messageData = Encoding.ASCII.GetBytes(message);
                byte[] data = dataLength.Concat(messageData).ToArray();
                foreach(byte info in data)
                {
                    Console.WriteLine(info);
                }
                stream.Write(data);
                stream.Flush();
            }
        }

        public static string ReadTextMessage(TcpClient client)
        {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                return stream.ReadLine();
            }
        }

        public static void WriteID(TcpClient client)
        {
            string id = "session/list";
            string Jsonstring = JsonSerializer.Serialize(id);
            WriteTextMessage(client, id);
        }
    }
}