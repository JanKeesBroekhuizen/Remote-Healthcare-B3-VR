using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Remote_Healthcare_VR;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public enum VRObjects { TERRAIN, PANEL, ROUTE, ROAD, NODE }




        static void Main(string[] args)
        {
         
            Client = new TcpClient("145.48.6.10", 6666);
            Init();
            //SaveBaseObjects();

            //Get groundplane id
            //Example methods dictionary

            VRObject object1 = new VRObject(VRObjects.NODE, "Object 1", 12345);
            VRObject object2 = new VRObject(VRObjects.PANEL, "Object 2", 45678);
            VRObject object3 = new VRObject(VRObjects.ROAD, "Object 3", 87654);
            VRObject object4 = new VRObject(VRObjects.ROUTE, "Object 4", 98666);

           

            int tempcounter = VRObject.TotalDictionaryEntries();
            Console.WriteLine(VRObject.LookupEntries(VRObjects.TERRAIN));

            // End Example Dictionary

            JObject find = Scene.Node.Find("GroundPlane");
            WriteTextMessage(GenerateMessage(find));
            JObject response = ReadTextMessage();
            var uuid = response["data"]["data"]["data"][0]["uuid"];

            //exercise 3a and exercise 3e: add terrain (flat and with heights)
            JObject flatTerain = Scene.Terrain.Add(Remote_Healthcare_VR.Properties.Resources.Height_Map1);
            WriteTextMessage(GenerateMessage(flatTerain));
            ReadTextMessage();

            JObject grondRender = Scene.Node.Add("Grond", new int[] { -40, 0, -40 }, 1, new int[] { 0, 0, 0 }, true);
            WriteTextMessage(GenerateMessage(grondRender));
            ReadTextMessage();

            //exercise 3b: delete groundplane
            JObject delNode = Scene.Node.Delete((string)uuid);
            WriteTextMessage(GenerateMessage(delNode));
            ReadTextMessage();

            //exercise 3c: change time skybox
            JObject setTime = Scene.Skybox.SetTime(7.0f);
            WriteTextMessage(GenerateMessage(setTime));
            ReadTextMessage();

            //exercise 3d: add 3D models
            JObject addTree1 = Scene.Node.Add("tree1", new int[] { 25, 0, 10 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(GenerateMessage(addTree1));
            ReadTextMessage();

            JObject addTree2 = Scene.Node.Add("tree2", new int[] { 7, 0, 12 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(GenerateMessage(addTree2));
            ReadTextMessage();

            JObject addTree3 = Scene.Node.Add("tree3", new int[] { 3, 0, 9 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(GenerateMessage(addTree3));
            ReadTextMessage();

            JObject addTree4 = Scene.Node.Add("tree4", new int[] { 14, 0, 1 }, 1, new int[] { 0, 0, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Tree 02/Tree.obj", true, false, "no");
            WriteTextMessage(GenerateMessage(addTree4));
            ReadTextMessage();

            Console.WriteLine("Add car");
            JObject addCar = Scene.Node.Add("car", new int[] { 15, 0, 15 }, 0.01f, new int[] { 0, 90, 0 }, "C:/Users/jkbro/Documents/Avans/TI2/Periode 1/Proftaak RH/Party_Bike_v1_L1.123c4456ce2b-9560-4051-8040-9c1998def616/20391_Party_Bike_v1_NEW.obj", true, false, "no");
            WriteTextMessage(GenerateMessage(addCar));
            response = ReadTextMessage();
            var carUuid = response["data"]["data"]["data"]["uuid"];

            //exercise 3f: add route
            Route.RouteNode[] routeNodes = new Route.RouteNode[4];
            routeNodes[0] = new Route.RouteNode(0, 0, 0, 5, 0, -5);
            routeNodes[1] = new Route.RouteNode(50, 0, 0, 5, 0, 5);
            routeNodes[2] = new Route.RouteNode(50, 0, 50, -5, 0, 5);
            routeNodes[3] = new Route.RouteNode(0, 0, 50, -5, 0, -5);

            JObject addRoute = Route.Add(routeNodes);
            WriteTextMessage(GenerateMessage(addRoute));
            response = ReadTextMessage();
            var routeUuid = response["data"]["data"]["data"]["uuid"];
            Console.WriteLine(uuid);

            //exercise 3g: add road to route 
            JObject addRoad = Scene.Road.Add((string)routeUuid);
            WriteTextMessage(GenerateMessage(addRoad));
            ReadTextMessage();

            //exercise 3h: object follows the route
            JObject followRoute = Route.Follow((string)routeUuid, (string)carUuid, 1.0, 0.0, Route.Rotation.XZ, 1.0, false, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 });
            WriteTextMessage(GenerateMessage(followRoute));
            ReadTextMessage();


        }

        static void Init()
        {
            WriteTextMessage("{\"id\":\"session/list\"}");

            JObject response = ReadTextMessage(); // stap 2 (get response)


            var properSession = response["data"].Where(e => e["clientinfo"]["user"].ToObject<string>() == "someb").Last();


            var sessionId = properSession["id"];
            //Console.WriteLine(sessionId); // stap 3 (getting id)

            WriteTextMessage("{\"id\":\"tunnel/create\",\"data\":{\"session\":\"" + sessionId + "\"}}");

            Console.WriteLine("Na tunnel create");

            response = ReadTextMessage(); // stap 4 (get response)
            var tunnelId = response["data"]["id"];
            TunnelId = (string)tunnelId;
            Console.WriteLine("Init(): " + TunnelId);

            WriteTextMessage(GenerateMessage(Scene.Reset()));
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

        public static string GenerateMessage(JObject message)
        {
            JObject totalMessage =
                new JObject(
                    new JProperty("id", "tunnel/send"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("dest", TunnelId),
                        new JProperty("data", new JObject(message)))));
            Console.WriteLine("GenerateMessage(): " + TunnelId);

            //JObject totalMessage = new JObject(new JProperty("id", "tunnel/send"), new JProperty("data", new JObject(new JProperty("dest", TunnelId), new JProperty("data", new JObject(message)))));
            return totalMessage.ToString();
        }

       
           





        

        public static void SaveObjects(JObject json/*, VRObjects objectType*/)
        {
            string jsonString = json.ToString();
            bool nextIsName = false;
            bool nextIsUuid = false;

            string name = "";
            string uuid = "";
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonString));
            while(reader.Read()) 
            {
                if (reader.Value != null)
                {
                    if (nextIsName)
                    {
                        name = (string)reader.Value;
                        nextIsName = false;

                    }else if (nextIsUuid)
                    {
                        uuid = (string)reader.Value;
                        nextIsUuid = false;

                    }else if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "name")
                    {
                        nextIsName = true;

                    }else if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "uuid")
                    {
                        nextIsUuid = true;

                    }
                }
                if (name != "" && uuid != "")
                {
                    //TODO: save it here
                    Console.WriteLine("Name: {0} \nuuid: {1}", name, uuid);

                    name = "";
                    uuid = "";
                }
            }
        }

        public static void SaveBaseObjects()
        {
            JObject get = Scene.Get();
            WriteTextMessage(GenerateMessage(get));
            JObject response = ReadTextMessage();
            SaveObjects(response/*, VRObjects.BASE*/);
        }
    }
}