using Newtonsoft.Json.Linq;
using Remote_Healthcare_VR;
using System;
using System.Collections;
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
            //TcpClient client = new TcpClient("145.48.6.10", 6666);
            //String tunnelId = Init(client);

            Console.WriteLine(Scene.Load("test.txt"));

            
            float[] heights = new float[255360];
            for (int i = 0; i < 65536; i++)
            {
                heights[i] = 0;
            }
            string flatTerain = Scene.Terrain.Add(new float[]{ 256, 256}, heights);
            string dellayer = Scene.Node.DelLayer();
            string setTime = Scene.Skybox.SetTime(7.0f);

        }

        static string Init(TcpClient client)
        {
            WriteTextMessage(client, "{\"id\":\"session/list\"}");

            JObject response = ReadTextMessage(client); // stap 2 (get response)

            var sessionId = response["data"][0]["id"];
            //Console.WriteLine(sessionId); // stap 3 (getting id)

            WriteTextMessage(client, "{\"id\":\"tunnel/create\",\"data\":{\"session\":\"" + sessionId + "\"}}");

            response = ReadTextMessage(client); // stap 4 (get response)
            var tunnelId = response["data"]["id"];
            //Console.WriteLine(tunnelId);
            return (string)tunnelId;
        }

        public static void WriteTextMessage(TcpClient client, String message)
        {
            var stream = client.GetStream();
            {
                Console.WriteLine("WriteTextMessage()");
                int messageLength = message.Length;
                byte[] dataLength = BitConverter.GetBytes(messageLength);
                byte[] messageData = Encoding.ASCII.GetBytes(message);
                byte[] data = dataLength.Concat(messageData).ToArray();

                //foreach(byte info in data)
                //{
                //    Console.WriteLine(info);
                //}

                stream.Write(data);
                stream.Flush();
            }
        }

        public static JObject ReadTextMessage(TcpClient client)
        {

            var stream = client.GetStream();
            {
                Console.WriteLine("ReadTextMessage()");
                byte[] messageLength = new byte[4];
                int length = stream.Read(messageLength, 0, 4);
                //foreach (byte b in messageLength)
                //{
                //    Console.WriteLine(b + " ");
                //}
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

                //Console.WriteLine(Encoding.ASCII.GetString(message));

                JObject messageJson = JObject.Parse(Encoding.UTF8.GetString(message));
                Console.WriteLine(messageJson);
                return messageJson;
            }
        }
    }
}