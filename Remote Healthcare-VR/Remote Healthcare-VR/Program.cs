using Remote_Healthcare_VR;
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
            WriteID(client);
            
            bool done = false;
            
            
            while (!done)
            {
                string response = ReadTextMessage(client);
                Console.WriteLine("Response: " + response);
               
            }
            */
            WriteID(client);
            ReadTextMessage(client);
        }

        public static void WriteTextMessage(TcpClient client, string message)
        {
            var stream = client.GetStream();
            {
                Console.WriteLine("WriteTextMessage()");
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
            
            var stream = client.GetStream();
            {
                Console.WriteLine("ReadTextMessage()");
                byte[] messageLength = new byte[4];
                int length = stream.Read(messageLength, 0, 4);
                foreach (byte b in messageLength)
                {
                    Console.WriteLine(b + " ");
                }
                messageLength.Reverse();
                int messageLenghtInt = BitConverter.ToInt32(messageLength);
                Console.WriteLine("Length: " + messageLenghtInt);

                byte[] message = new byte[messageLenghtInt];
                int totalRead = 0;

                do
                {
                    int test = stream.Read(message, totalRead, messageLenghtInt - totalRead);
                    totalRead += test;
                } while (totalRead < messageLenghtInt);
                
                //Console.WriteLine("Length array: " + test);

                Console.WriteLine(Encoding.ASCII.GetString(message));

                return " ";
            }
        }

        public static void WriteID(TcpClient client)
        {
            ID id = new ID("session/list");
            string idToJson = id.ToJSON();
            Console.WriteLine(idToJson);
            
            WriteTextMessage(client, idToJson);
        }
    }
}