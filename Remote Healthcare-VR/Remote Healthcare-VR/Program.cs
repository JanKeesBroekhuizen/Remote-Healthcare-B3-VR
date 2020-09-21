using Newtonsoft.Json.Linq;
using Remote_Healthcare_VR;
using System;
using System.Collections;
using System.Collections.Generic;
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
        enum VRObjects { TERRAIN, PANEL, ROUTE, ROAD, NODE }




        static void Main(string[] args)
        {
         
            Client = new TcpClient("145.48.6.10", 6666);
            Init();

            float[] heights = new float[65536];
            for (int i = 0; i < 65536; i++)
            {
                heights[i] = 0;
            }

            //Example methods dictionary

            VRObject object1 = new VRObject(VRObjects.NODE, "Object 1", 12345);
            VRObject object2 = new VRObject(VRObjects.PANEL, "Object 2", 45678);
            VRObject object3 = new VRObject(VRObjects.ROAD, "Object 3", 87654);
            VRObject object4 = new VRObject(VRObjects.ROUTE, "Object 4", 98666);

            int tempcounter = TotalDictionaryEntries();
            Console.WriteLine(LookupEntries(VRObjects.TERRAIN));

            // End Example Dictionary



            JObject find = Scene.Node.Find("GroundPlane");
            WriteTextMessage(generateMessage(find));
            JObject response = ReadTextMessage();
            var uuid = response["data"]["data"]["data"][0]["uuid"];

            JObject flatTerain = Scene.Terrain.Add(new float[] { 256, 256 }, heights);
            WriteTextMessage(generateMessage(flatTerain));
            ReadTextMessage();

            JObject grondRender = Scene.Node.Add("Grond", new int[] { -40, 0, -40 }, 1, new int[] { 0, 0, 0 }, false);
            WriteTextMessage(generateMessage(grondRender));
            ReadTextMessage();

            JObject delNode = Scene.Node.Delete((string)uuid);
            WriteTextMessage(generateMessage(delNode));
            ReadTextMessage();

            JObject setTime = Scene.Skybox.SetTime(7.0f);
            WriteTextMessage(generateMessage(setTime));
            ReadTextMessage();

            JObject addTree = Scene.Node.Add("tree", new int[] { 10, 0, 10 }, 1, new int[] { 0, 0, 0 }, "Resources/Tree.obj", true, false, "no");
            WriteTextMessage(generateMessage(addTree));
            ReadTextMessage();
        }

        static void Init()
        {
            WriteTextMessage("{\"id\":\"session/list\"}");

            JObject response = ReadTextMessage(); // stap 2 (get response)


           

            var sessionId = response["data"][0]["id"];
            //Console.WriteLine(sessionId); // stap 3 (getting id)

            WriteTextMessage("{\"id\":\"tunnel/create\",\"data\":{\"session\":\"" + sessionId + "\"}}");

            response = ReadTextMessage(); // stap 4 (get response)
            var tunnelId = response["data"]["id"];
            //Console.WriteLine(tunnelId);

            TunnelId = (string)tunnelId;

            
            WriteTextMessage(generateMessage(Scene.Reset()));

            response = ReadTextMessage();
        }

        public static void WriteTextMessage(String message)
        {
            var stream = Client.GetStream();
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

        public static JObject ReadTextMessage()
        {

            var stream = Client.GetStream();
            {
                Console.WriteLine("ReadTextMessage()");
                byte[] messageLength = new byte[4];
                int length = stream.Read(messageLength, 0, 4);
                foreach (byte b in messageLength)
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
            JObject totalMessage =
                new JObject(
                    new JProperty("id", "tunnel/send"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("dest", TunnelId),
                        new JProperty("data", new JObject(message)))));
            return totalMessage.ToString();
        }

        public static int TotalDictionaryEntries()
        {
            
            return VRObject.TerrainDictionary.Count + VRObject.PanelDictionary.Count + VRObject.RouteDictionary.Count + VRObject.RoadDictionary.Count + VRObject.NodeDictionary.Count;
        }

        public static ArrayList LookupEntries(Enum ObjectName)
        {
            ArrayList ReturnList = new ArrayList();

            if (ObjectName.ToString().Equals(VRObjects.NODE.ToString())) {
                foreach (KeyValuePair<String, int> vrobject in VRObject.NodeDictionary)
                {
                    Console.WriteLine("Key: {0}, Value: {1}",
                    vrobject.Key, vrobject.Value);
                }
            }

            if (ObjectName.ToString().Equals(VRObjects.ROAD.ToString()))
            {
                foreach (KeyValuePair<String, int> vrobject in VRObject.RoadDictionary)
                {
                    Console.WriteLine("Key: {0}, Value: {1}",
                    vrobject.Key, vrobject.Value);
                }
            }

            if (ObjectName.ToString().Equals(VRObjects.ROUTE.ToString()))
            {
                foreach (KeyValuePair<String, int> vrobject in VRObject.RouteDictionary)
                {
                    Console.WriteLine("Key: {0}, Value: {1}",
                    vrobject.Key, vrobject.Value);
                }
            }

            if (ObjectName.ToString().Equals(VRObjects.PANEL.ToString()))
            {
                foreach (KeyValuePair<String, int> vrobject in VRObject.PanelDictionary)
                {
                    Console.WriteLine("Key: {0}, Value: {1}",
                    vrobject.Key, vrobject.Value);
                }
            }

            if (ObjectName.ToString().Equals(VRObjects.TERRAIN.ToString()))
            {
                foreach (KeyValuePair<String, int> vrobject in VRObject.TerrainDictionary)
                {
                    Console.WriteLine("Key: {0}, Value: {1}",
                    vrobject.Key, vrobject.Value);
                }
            }


            return ReturnList;

        }
           





        }
}