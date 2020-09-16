﻿using Newtonsoft.Json.Linq;
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

        public static string Load(string fileName)
        {
            JObject load = 
                new JObject(
                    new JProperty("id", "scene/load"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("filename", fileName))));
            return load.ToString();
        }

        public static string Raycast(int[] start, int[] direction, bool physics)
        {
            JObject raycast =
                new JObject(
                    new JProperty("id", "scene/raycast"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("start", start), 
                        new JProperty("direction", direction),
                        new JProperty("physics", physics))));
            return raycast.ToString();
        }
    }
    class Node
    {
        public static string Add(string name, string guid, 
            int[] position, float scale, int[] rotation,
            string filename, bool cullbackfaces, bool animated, string animationname,
            bool smoothnormals, int[] panelSize, int[] panelResolution, int[] background, bool castshadow,
            int[] waterSize, float waterResolution)
        {
            JObject add =
                new JObject(
                    new JProperty("id", "scene/node/add"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("name", name),
                        new JProperty("parent", guid),
                        new JProperty("components",
                        new JObject(
                            new JProperty("transform", 
                            new JObject(
                                new JProperty("position", position),
                                new JProperty("scale", scale),
                                new JProperty("rotation", rotation))),
                            new JProperty("model",
                            new JObject(
                                new JProperty("file", filename),
                                new JProperty("cullbackfaces", cullbackfaces),
                                new JProperty("animated", animated),
                                new JProperty("animation", animationname))),
                            new JProperty("terrain",
                            new JObject(
                                new JProperty("smoothnormals", smoothnormals))),
                            new JProperty("panel",
                            new JObject(
                                new JProperty("size", panelSize),
                                new JProperty("resolution", panelResolution),
                                new JProperty("background", background),
                                new JProperty("castShadow", castshadow))),
                            new JProperty("water",
                            new JObject(
                                new JProperty("size", waterSize),
                                new JProperty("resolulion", waterResolution))))))));
            return add.ToString();
        }

        public static string Update(string id, string parent, int[] position, float scale, int[] rotation, string name, float speed)
        {
            JObject update =
                new JObject(
                    new JProperty("id", "scene/node/update"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("parent", parent),
                        new JProperty("transform",
                        new JObject(
                            new JProperty("posotion", position),
                            new JProperty("scale", scale),
                            new JProperty("rotation", rotation)),
                        new JProperty("animation", 
                        new JObject(
                            new JProperty("name", name),
                            new JProperty("speed", speed)))))));
            return update.ToString();
        }

        public static string MoveTo(string id, int[] position, string rotate, string interpolate, bool followheight, int time)
        {
            JObject moveto =
                new JObject(
                    new JProperty("id", "scene/node/moveto"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("position", position),
                        new JProperty("rotate", rotate),
                        new JProperty("interpolate", interpolate),
                        new JProperty("followheight", followheight),
                        new JProperty("time", time))));
            return moveto.ToString();
        }

        public static string MoveTo(string id, int[] position, string rotate, string interpolate, bool followheight, float speed)
        {
            JObject moveto =
                new JObject(
                    new JProperty("id", "scene/node/moveto"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("position", position),
                        new JProperty("rotate", rotate),
                        new JProperty("interpolate", interpolate),
                        new JProperty("followheight", followheight),
                        new JProperty("speed", speed))));
            return moveto.ToString();
        }

        public static string MoveTo(string id, string stop)
        {
            JObject moveto =
                new JObject(
                    new JProperty("id", "scene/node/moveto"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("stop", stop))));
            return moveto.ToString();
        }

        public static string Delete(string id)
        {
            JObject delete =
                new JObject(
                    new JProperty("id", "scene/node/delete"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id))));
            return delete.ToString();
        }

        public static string Find(string name)
        {
            JObject find =
                new JObject(
                    new JProperty("id", "scene/node/find"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("name", name))));
            return find.ToString();
        }

        public static string AddLayer(string id, string diffuse, string normal, int minHeight, int maxHeight, float fadeDist)
        {
            JObject addLayer =
                new JObject(
                    new JProperty("id", "scene/node/addlayer"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("diffuse", diffuse),
                        new JProperty("normal", normal),
                        new JProperty("minHeight", minHeight),
                        new JProperty("maxHeight", maxHeight),
                        new JProperty("fadeDist", fadeDist))));
            return addLayer.ToString();
        }

        public static string DelLayer(string name)
        {
            JObject delLayer =
                new JObject(
                    new JProperty("id", "scene/node/find"),
                    new JProperty("data",
                    new JObject()));
            return delLayer.ToString();
        }
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

