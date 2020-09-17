using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            JObject add = 
                new JObject(
                    new JProperty("id", "scene/terrain/add"), 
                    new JProperty("data", 
                    new JObject(
                        new JProperty("size", size), 
                        new JProperty("heights", heights))));
            return add.ToString();
        }

        public static string Update()
        {
            JObject update = 
                new JObject(
                    new JProperty("id", "scene/terrain/update"), 
                    new JProperty("data"));
            return update.ToString();
        }

        public static string Delete() 
        {
            JObject delete = 
                new JObject(
                    new JProperty("id", "scene/terrain/delete"), 
                    new JProperty("data"));
            return delete.ToString();
        }

        public static string GetHeight(float[] position, float[] positions) //Volgens mij is dit niet correct!
        {
            JObject getheight = 
                new JObject(
                    new JProperty("id", "scene/terrain/getheight"), 
                    new JProperty("data", 
                    new JObject(
                        new JProperty("position", position), 
                        new JProperty("positions", positions))));
            return getheight.ToString();
        }

    }

    class Panel
    {

        public static string Clear(string id)
        {
            JObject clear = 
                new JObject(
                new JProperty("id", "scene/panel/clear"),
                new JProperty("data", 
                    new JObject(
                        new JProperty("id", id)
                    )
                )
            );
            return clear.ToString();
        }

        public static string DrawLines(string id, int width, List<int[]> lines) //Deze werkt nog niet!!!!
        {
            JObject drawLines = 
                new JObject(
                new JProperty("id", "scene/panel/drawlines"),
                new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("width", width),
                        new JProperty("lines", from l in lines select new JArray(l))
                    )
                )
            );
            return drawLines.ToString();
        }

        public static string DrawText(string id, string text, float[] position, float size, int[] color, string font)
        {
            JObject drawText = new JObject(
                new JProperty("id", "scene/panel/drawtext"),
                new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("text", text),
                        new JProperty("position", position),
                        new JProperty("size", size),
                        new JProperty("color", color),
                        new JProperty("font", font)
                    )
                )
            );
            return drawText.ToString();
        }

        public static string Image(string id, string imagePath, float[] position, float[] size)
        {
            JObject image = new JObject(
                new JProperty("id", "scene/panel/image"),
                new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("image", imagePath),
                        new JProperty("position", position),
                        new JProperty("size", size)
                    )
                )
            );
            return image.ToString();
        }

        public static string SetClearColor(string id, int[] color)
        {
            JObject setClearColor = new JObject(
                new JProperty("id", "scene/panel/setclearcolor"),
                new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("color", color)
                    )
                )
            );
            return setClearColor.ToString();
        }

        public static string Swap(string id)
        {
            JObject swap = new JObject(
                new JProperty("id", "scene/panel/swap"),
                new JProperty("data",
                    new JObject(
                        new JProperty("id", id)
                    )
                )
            );
            return swap.ToString();
        }
    }

    class Skybox
    {
        public static string SetTime(string time)
        {
            JObject settime = new JObject(
                new JProperty("id", "scene/skybox/settime"), 
                new JProperty("data", 
                new JObject(
                    new JProperty("time", time))));
            return settime.ToString();
        }

        public static string Update()
        {
            JObject update = new JObject(
                new JProperty("id", "scene/terrain/update"),
                new JProperty("data", 
                new JObject(
                    new JProperty("type", "static"),
                    new JProperty("files", 
                    new JObject(
                        new JProperty("xpos", "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_rt.png"),
                        new JProperty("xneg", "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_lf.png"),
                        new JProperty("ypos", "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_up.png"),
                        new JProperty("yneg", "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_dn.png"),
                        new JProperty("zpos", "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_bk.png"),
                        new JProperty("zneg", "data/NetworkEngine/textures/SkyBoxes/interstellar/interstellar_ft.png"))))));
            return update.ToString();
        }

    }

    class Road
    {

        //scene/road/add
        public static string AddRoad(string routeuuid)
        {
            JObject AddRoad = new JObject(
                new JProperty("id", "scene/road/add"), 
                new JProperty("data", 
                new JObject(new JProperty("route", routeuuid), 
                new JObject(new JProperty("diffuse", "data/NetworkEngine/textures/tarmac_diffuse.png"),
                new JObject(new JProperty("normal", "data/NetworkEngine/textures/tarmac_normale.png"),
                new JObject(new JProperty("specular", "data/NetworkEngine/textures/tarmac_specular.png"),
                new JObject(new JProperty("heightoffset", 0.01)
              )))))));
            return AddRoad.ToString();
        }

        //scene/road/update
        public static string UpdateRoad(string roaduuid, string routeuuid)
        {
            JObject AddRoad = new JObject(
                new JProperty("id", "scene/road/update"),
                new JProperty("data",
                new JObject(new JProperty("id", roaduuid),
                new JObject(new JProperty("route", routeuuid),
                new JObject(new JProperty("diffuse", "data/NetworkEngine/textures/tarmac_diffuse.png"),
                new JObject(new JProperty("normal", "data/NetworkEngine/textures/tarmac_normale.png"),
                new JObject(new JProperty("specular", "data/NetworkEngine/textures/tarmac_specular.png"),
                new JObject(new JProperty("heightoffset", 0.01)
              ))))))));
            return AddRoad.ToString();
        }
    }
}

