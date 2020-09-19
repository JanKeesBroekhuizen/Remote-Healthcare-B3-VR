using Newtonsoft.Json.Linq;
using Remote_Healthcare_VR;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace SimpleTCPClient
{
    class Program
    {
        private static TcpClient Client;
        private static string TunnelId;

        static void Main(string[] args)
        {
            Client = new TcpClient("145.48.6.10", 6666);
            Init();

            float[] heights = new float[65536];
            for (int i = 0; i < 65536; i++)
            {
                heights[i] = 3;
            }

            JObject find = Scene.Node.Find("GroundPlane");
            WriteTextMessage(generateMessage(find));
            JObject response = ReadTextMessage();
            var uuid = response["data"]["data"]["data"][0]["uuid"];

            JObject flatTerain = Scene.Terrain.Add(Remote_Healthcare_VR.Properties.Resources.Height_Map1);
            WriteTextMessage(generateMessage(flatTerain));
            ReadTextMessage();

            JObject grondRender = Scene.Node.Add("Grond", new int[] { -40, 0, -40 }, 1, new int[] { 0, 0, 0 }, true);
            WriteTextMessage(generateMessage(grondRender));
            ReadTextMessage();

            JObject delNode = Scene.Node.Delete((string)uuid);
            WriteTextMessage(generateMessage(delNode));
            ReadTextMessage();

            JObject setTime = Scene.Skybox.SetTime(7.0f);
            WriteTextMessage(generateMessage(setTime));
            ReadTextMessage();

            //node objects
            JObject addTree1 = Scene.Node.Add("tree1", new int[] { 10, 0, 10 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(generateMessage(addTree1));
            ReadTextMessage();

            JObject addTree2 = Scene.Node.Add("tree2", new int[] { 9, 0, 9 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(generateMessage(addTree2));
            ReadTextMessage();

            JObject addTree3 = Scene.Node.Add("tree3", new int[] { 8, 0, 8 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(generateMessage(addTree3));
            ReadTextMessage();

            JObject addTree4 = Scene.Node.Add("tree4", new int[] { 7, 0, 7 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(generateMessage(addTree4));
            ReadTextMessage();

            //add new route
            Route.RouteNode[] routeNodes = new Route.RouteNode[4];
            routeNodes[0] = new Route.RouteNode(0, 0, 0, 5, 0, -5);
            routeNodes[1] = new Route.RouteNode(50, 0, 0, 5, 0, 5);
            routeNodes[2] = new Route.RouteNode(50, 0, 50, -5, 0, 5);
            routeNodes[3] = new Route.RouteNode(0, 0, 50, -5, 0, -5);

            JObject addRoute = Route.Add(routeNodes);
            WriteTextMessage(generateMessage(addRoute));
            response = ReadTextMessage();
            var routeUuid = response["data"]["data"]["data"]["uuid"];
            Console.WriteLine(uuid);

            //add a road over the route 
            JObject addRoad = Scene.Road.Add((string)routeUuid);
            WriteTextMessage(generateMessage(addRoad));
            response = ReadTextMessage();
            
        }

        static void Init()
        {
            WriteTextMessage("{\"id\":\"session/list\"}");

            JObject response = ReadTextMessage(); // stap 2 (get response)

            
            var sessionId = response["data"][0]["id"];
            //Console.WriteLine(sessionId); // stap 3 (getting id)

            WriteTextMessage("{\"id\":\"tunnel/create\",\"data\":{\"session\":\"" + sessionId + "\"}}");

            Console.WriteLine("Na tunnel create");

            response = ReadTextMessage(); // stap 4 (get response)
            var tunnelId = response["data"]["id"];
            TunnelId = (string)tunnelId;

            WriteTextMessage(generateMessage(Scene.Reset()));
            response = ReadTextMessage();
        }

        public static void WriteTextMessage(String message)
        {
            var stream = Client.GetStream();
            {
                Console.WriteLine("WriteTextMessage():  " + message);
                int messageLength = message.Length;
                byte[] dataLength = BitConverter.GetBytes(messageLength);
                byte[] messageData = Encoding.ASCII.GetBytes(message);
                byte[] data = dataLength.Concat(messageData).ToArray();

                stream.Write(data);
                stream.Flush();
            }
        }

        public static JObject ReadTextMessage()
        {

            var stream = Client.GetStream();
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
                Console.WriteLine(messageJson + "\n\n\n\n");
                
                return messageJson;
            }
        }

        public static string generateMessage(JObject message)
        {
            /*JObject totalMessage =
                new JObject(
                    new JProperty("id", "tunnel/send"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("dest", TunnelId),
                        new JProperty("data", new JObject(message)))));*/

            JObject totalMessage = new JObject(new JProperty("id", "tunnel/send"), new JProperty("data", new JObject(new JProperty("dest", TunnelId), new JProperty("data", new JObject(message)))));
            return totalMessage.ToString();
        }
    }
}