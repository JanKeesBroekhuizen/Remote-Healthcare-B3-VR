using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Healthcare_VR
{
    class Scene
    {
        public static Node Node = new Node();
        public static Terrain Terrain = new Terrain();
        public static Panel Panel = new Panel();
        public static Skybox Skybox = new Skybox();
        public static Road Road = new Road();

        public static string Get()
        {
            JObject get = 
                new JObject(
                    new JProperty("id", "scene/get"));
            return get.ToString();
        }

        public static string Reset()
        {
            JObject reset = 
                new JObject(
                    new JProperty("id", "scene/reset"));
            return reset.ToString();
        }

        public static string Save(string fileName, bool overwrite)
        {
            JObject save = 
                new JObject(
                    new JProperty("id", "scene/save"), 
                    new JProperty("data", 
                    new JObject(
                        new JProperty("filename", fileName), 
                        new JProperty("overwrite"))));
            return save.ToString();
        }


    }
    class Node
    {
        

    } 

    class Terrain
    {
        public static string Add(float[] size, float[] heights)
        {
            JObject add = new JObject(new JProperty("id", "scene/terrain/add"), new JProperty("data", new JObject(new JProperty("size", size), new JProperty("heights", heights))));
            return add.ToString();
        }

        public static string Update()
        {
            JObject update = new JObject(new JProperty("id", "scene/terrain/update"), new JProperty("data"));
            return update.ToString();
        }

        public static string Delete()
        {
            JObject delete = new JObject(new JProperty("id", "scene/terrain/delete"), new JProperty("data"));
            return delete.ToString();
        }

        public static string GetHeight(float[] position, float[] positions)
        {
            JObject getheight = new JObject(new JProperty("id", "scene/terrain/getheight"), new JProperty("data", new JObject(new JProperty("position", position), new JProperty("positions", positions))));
            return getheight.ToString();
        }

    }

    class Panel
    {

    }

    class Skybox
    {
        public static string SetTime(string time)
        {
            JObject settime = new JObject(new JProperty("id", "scene/skybox/settime"), new JProperty("data", new JObject(new JProperty("time", time))));
            return settime.ToString();
        }

    }

    class Road
    {

    }
}

