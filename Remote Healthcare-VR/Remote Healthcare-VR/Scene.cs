using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

        public static JObject Get()
        {
            JObject get = 
                new JObject(
                    new JProperty("id", "scene/get"));
            return get;
        }

        public static JObject Reset()
        {
            JObject reset = 
            new JObject(
            new JProperty("id", "scene/reset"));
            return reset;
        }

        public static JObject Save(string fileName, bool overwrite)
        {
            JObject save = 
                new JObject(
                    new JProperty("id", "scene/save"), 
                    new JProperty("data", 
                    new JObject(
                        new JProperty("filename", fileName), 
                        new JProperty("overwrite", overwrite))));
            return save;
        }

        public static JObject Load(string fileName)
        {
            JObject load = 
                new JObject(
                    new JProperty("id", "scene/load"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("filename", fileName))));
            return load;
        }

        public static JObject Raycast(int[] start, int[] direction, bool physics)
        {
            JObject raycast =
                new JObject(
                    new JProperty("id", "scene/raycast"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("start", start), 
                        new JProperty("direction", direction),
                        new JProperty("physics", physics))));
            return raycast;
        }
    }
    class Node
    {
        public JObject Add(string name, string guid, 
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
            return add;
        }

        public JObject Add(string name,
            int[] position, float scale, int[] rotation,
             string filename, bool cullbackfaces, bool animated, string animationname)
        {
            JObject add =
    new JObject(
        new JProperty("id", "scene/node/add"),
        new JProperty("data",
        new JObject(
            new JProperty("name", name),
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
                    new JProperty("animation", animationname))))))));
            return add;

        }

        public JObject Add(string name, bool smoothnormals)
        {
            JObject add =
                new JObject(
                    new JProperty("id", "scene/node/add"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("name", name),
                        new JProperty("components",
                        new JObject(
                            new JProperty("terrain",
                            new JObject(
                                new JProperty("smoothnormals", smoothnormals))))))));
            return add;
        }

        public JObject Add(string name, int[] position, float scale, int[] rotation, bool smoothnormals)
        {
            JObject add =
                new JObject(
                    new JProperty("id", "scene/node/add"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("name", name),
                        new JProperty("components",
                        new JObject(
                            new JProperty("transform",
                            new JObject(
                                new JProperty("position", position),
                                new JProperty("scale", scale),
                                new JProperty("rotation", rotation))),
                            new JProperty("terrain",
                            new JObject(
                                new JProperty("smoothnormals", smoothnormals))))))));
            return add;
        }

        public JObject Add(string name, int[] waterSize, float waterResolution)
        {
            JObject add =
                new JObject(
                    new JProperty("id", "scene/node/add"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("name", name),
                        new JProperty("components",
                        new JObject(
                            new JProperty("water",
                            new JObject(
                                new JProperty("size", waterSize),
                                new JProperty("resolulion", waterResolution))))))));
            return add;
        }

        public JObject Add(string name, int[] panelSize, int[] panelResolution, int[] background, bool castshadow)
        {
            JObject add =
                new JObject(
                    new JProperty("id", "scene/node/add"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("name", name),
                        new JProperty("components",
                        new JObject(
                            new JProperty("panel",
                            new JObject(
                                new JProperty("size", panelSize),
                                new JProperty("resolution", panelResolution),
                                new JProperty("background", background),
                                new JProperty("castShadow", castshadow))))))));
            return add;
        }

        public JObject Update(string id, string parent, int[] position, float scale, int[] rotation, string name, float speed)
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
            return update;
        }

        public JObject MoveTo(string id, int[] position, string rotate, string interpolate, bool followheight, int time)
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
            return moveto;
        }

        public JObject MoveTo(string id, int[] position, string rotate, string interpolate, bool followheight, float speed)
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
            return moveto;
        }

        public JObject MoveTo(string id, string stop)
        {
            JObject moveto =
                new JObject(
                    new JProperty("id", "scene/node/moveto"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("stop", stop))));
            return moveto;
        }

        public JObject Delete(string id)
        {
            JObject delete =
                new JObject(
                    new JProperty("id", "scene/node/delete"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("id", id))));
            return delete;
        }

        public JObject Find(string name)
        {
            JObject find =
                new JObject(
                    new JProperty("id", "scene/node/find"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("name", name))));
            return find;
        }

        public JObject AddLayer(string id, string diffuse, string normal, int minHeight, int maxHeight, float fadeDist)
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
            return addLayer;
        }

        public JObject DelLayer()
        {
            JObject delLayer =
                new JObject(
                    new JProperty("id", "scene/node/dellayer"),
                    new JProperty("data",
                    new JObject()));
            return delLayer;
        }
    } 

    class Terrain
    {
        public JObject Add(float[] size, float[] heights)
        {
            JObject add = 
                new JObject(
                    new JProperty("id", "scene/terrain/add"), 
                    new JProperty("data", 
                    new JObject(
                        new JProperty("size", size), 
                        new JProperty("heights", heights))));
            return add;
        }

        public JObject Add(Image image)
        {
            Bitmap bitMap = new Bitmap(image);
            float[] size = new float[] { bitMap.Width, bitMap.Height };

            float[] heights = new float[bitMap.Width * bitMap.Height];
            for (int x = 0; x < bitMap.Width; x++)
            {
                for (int y = 0; y < bitMap.Height; y++)
                {
                    float redValue = (bitMap.GetPixel(x, y).R) / 16 - 8f;
                    heights[(x * bitMap.Height) + y] = redValue;
                }
            }

            JObject add =
                new JObject(
                    new JProperty("id", "scene/terrain/add"),
                    new JProperty("data",
                    new JObject(
                        new JProperty("size", size),
                        new JProperty("heights", heights))));
            return add;
        }

        public JObject Update()
        {
            JObject update = 
                new JObject(
                    new JProperty("id", "scene/terrain/update"), 
                    new JProperty("data"));
            return update;
        }

        public JObject Delete() 
        {
            JObject delete = 
                new JObject(
                    new JProperty("id", "scene/terrain/delete"), 
                    new JProperty("data"));
            return delete;
        }

        public JObject GetHeight(float[] position, float[] positions) //Volgens mij is dit niet correct!
        {
            JObject getheight = 
                new JObject(
                    new JProperty("id", "scene/terrain/getheight"), 
                    new JProperty("data", 
                    new JObject(
                        new JProperty("position", position), 
                        new JProperty("positions", positions))));
            return getheight;
        }

    }

    class Panel
    {
        public JObject Clear(string id)
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
            return clear;
        }

        public JObject DrawLines(string id, int width, List<int[]> lines)
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
            return drawLines;
        }

        public JObject DrawText(string id, string text, float[] position, float size, int[] color, string font)
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
            return drawText;
        }

        public JObject Image(string id, string imagePath, float[] position, float[] size)
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
            return image;
        }

        public JObject SetClearColor(string id, int[] color)
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
            return setClearColor;
        }

        public JObject Swap(string id, List<int[]> lines)
        {
            JObject swap = new JObject(
                new JProperty("id", "scene/panel/swap"),
                new JProperty("data",
                new JObject(
                    new JProperty("id", id))));
            return swap;
        }
    }

    class Skybox
    {
        public JObject SetTime(float time)
        {
            JObject settime = new JObject(
                new JProperty("id", "scene/skybox/settime"), 
                new JProperty("data", 
                new JObject(
                    new JProperty("time", time))));
            return settime;
        }

        public JObject Update()
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
            return update;
        }

    }

    class Road
    {

        //scene/road/add
        public JObject Add(string routeUuid)
        {
            JObject AddRoad = new JObject(
                new JProperty("id", "scene/road/add"), 
                new JProperty("data", 
                new JObject(new JProperty("route", routeUuid), 
                    new JProperty("diffuse", "data/NetworkEngine/textures/tarmac_diffuse.png"),
                    new JProperty("normal", "data/NetworkEngine/textures/tarmac_normale.png"),
                    new JProperty("specular", "data/NetworkEngine/textures/tarmac_specular.png"),
                    new JProperty("heightoffset", 0.01)
              )));
            return AddRoad;
        }

        //scene/road/update
        public JObject Update(string roadUuid, string routeUuid)
        {
            JObject updateRoad = new JObject(
                new JProperty("id", "scene/road/update"),
                new JProperty("data",
                new JObject(
                    new JProperty("id", roadUuid),
                    new JProperty("route", routeUuid),
                    new JProperty("diffuse", "data/NetworkEngine/textures/tarmac_diffuse.png"),
                    new JProperty("normal", "data/NetworkEngine/textures/tarmac_normale.png"),
                    new JProperty("specular", "data/NetworkEngine/textures/tarmac_specular.png"),
                    new JProperty("heightoffset", 0.01)
              )));
            return updateRoad;
        }
    }
}

