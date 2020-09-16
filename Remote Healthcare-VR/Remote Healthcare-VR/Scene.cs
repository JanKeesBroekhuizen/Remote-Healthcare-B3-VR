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

    }

    class Panel
    {

    }

    class Skybox
    {

    }

    class Road
    {

    }
}

