using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remote_Healthcare_VR
{
    class Route
    {
        public struct RouteNode
        {
            public short[] pos { get; }
            public short[] dir { get; }

            public int index { get; set; }

            public RouteNode(int pos1, int pos2, int pos3, int dir1, int dir2, int dir3, int index)
            {
                this.pos = new short[3] { (short)pos1, (short)pos2, (short)pos3 };
                this.dir = new short[3] { (short)dir1, (short)dir2, (short)dir3 };
                this.index = index;
            }

            public RouteNode(int pos1, int pos2, int pos3, int dir1, int dir2, int dir3)
            {
                this.pos = new short[3] { (short)pos1, (short)pos2, (short)pos3 };
                this.dir = new short[3] { (short)dir1, (short)dir2, (short)dir3 };
                this.index = -1;
            }
       
            public string PrintPos()
            {
                string print = "[ ";
                for(int i = 0; i < pos.Length; i++)
                {
                    print += pos[i];
                    if(i < pos.Length - 1)
                    {
                        print += ", ";
                    }
                }
                print += "]";
                return print;
            }

            public string printDir()
            {
                string print = "[ ";
                for (int i = 0; i < dir.Length; i++)
                {
                    print += dir[i];
                    if (i < dir.Length - 1)
                    {
                        print += ", ";
                    }
                }
                print += "]";
                return print;
            } 
        }

        public static JObject Add(RouteNode[] RouteNodes)
        {

            JObject Route =
                new JObject(
                    new JProperty("id", "route/add"),
                    new JProperty("data", new JObject(
                            new JProperty("nodes", new JArray( from n in RouteNodes select new JObject(
                                 new JProperty("pos", n.pos),
                                 new JProperty("dir", n.dir)
                                )
                            ))
                        )
                        )
                    );
            return Route;
        }

        public static JObject Update(string RouteID, RouteNode[] NewNodes)
        {
            //TODO check if the indexes are correct
            JObject Update =
                new JObject(
                    new JProperty("id","route/update"),
                    new JProperty("data", new JObject(
                        new JProperty("id", RouteID),
                        new JProperty("nodes", new JArray(from n in NewNodes select new JObject(
                            new JProperty("index", n.index),
                            new JProperty("pos", n.pos),
                            new JProperty("dir", n.dir)
                            )))
                        ))
                    );
            return Update;
        }

        public static JObject Delete(string RouteID)
        {
            JObject Delete =
                new JObject(
                    new JProperty("id","route/delete"),
                    new JProperty("data", new JObject(
                        new JProperty("id", RouteID)
                        ))
                    );
            return Delete;
        }

        public static JObject Follow(string RouteId, string NodeId, double speed, double offset, Rotation rotation, double smoothing, bool followHeight, int[] rotateOffset, int[] positionOffset)
        {
            string rotationValue;
            switch (rotation)
            {
                case Rotation.NONE:
                    rotationValue = "NONE";
                    break;
                case Rotation.XYZ:
                    rotationValue = "XYZ";
                    break;
                case Rotation.XZ:
                    rotationValue = "XZ";
                    break;
                default:
                    rotationValue = "NONE";
                    break;
            }

            JObject Follow =
                new JObject(
                    new JProperty("id", "route/follow"),
                    new JProperty("data", new JObject(
                        new JProperty("route", RouteId),
                        new JProperty("node", NodeId),
                        new JProperty("speed", speed),
                        new JProperty("offset", offset),
                        new JProperty("rotate", rotationValue),
                        new JProperty("smoothing", smoothing),
                        new JProperty("followHeight", followHeight),
                        new JProperty("rotateOffset", rotateOffset),
                        new JProperty("positionOffset", positionOffset)
                        ))
                    );
            return Follow;
        }

        public static JObject SetFollowSpeed(string NodeId, double speed)
        {
            JObject FollowSpeed =
                new JObject(
                    new JProperty("id", "route/follow/speed"),
                    new JProperty("data", new JObject(
                        new JProperty("node", NodeId),
                        new JProperty("speed", speed)
                        ))
                    );
            return FollowSpeed;
        }

        public static JObject Show(bool isShown)
        {
            JObject Show =
                new JObject(
                    new JProperty("id", "route/show"),
                    new JProperty("data", new JObject(
                        new JProperty("show", isShown)
                        ))
                    );
            return Show;
        }

        public enum Rotation
        {
            XZ,
            XYZ,
            NONE
        }
    }
}
