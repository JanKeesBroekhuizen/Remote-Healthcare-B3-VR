using SimpleTCPClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Healthcare_VR
{
    class VRObject {


        enum VRObjects { TERRAIN, PANEL, ROUTE, ROAD, NODE }
        public static String ObjectName { get; set; }

        public static String ObjectType { get; set; }
        public static int ObjectUUID { get; set; }

        public static Dictionary<String, int> TerrainDictionary = new Dictionary<String, int>();
        public static Dictionary<String, int> PanelDictionary = new Dictionary<String, int>();
        public static Dictionary<String, int> RouteDictionary = new Dictionary<String, int>();
        public static Dictionary<String, int> RoadDictionary = new Dictionary<String, int>();
        public static Dictionary<String, int> NodeDictionary = new Dictionary<String, int>();

        public VRObject(Enum objecttype, string objectName, int objectUUID)
        {
            ObjectName = objectName;
            ObjectUUID = objectUUID;
            ObjectType = objecttype.ToString();

            switch (ObjectType)
            {
                case "TERRAIN":
                    TerrainDictionary.Add(ObjectName, ObjectUUID);
                    break;
                case "PANEL":
                    PanelDictionary.Add(ObjectName, ObjectUUID);
                    break;
                case "ROUTE":
                    RouteDictionary.Add(ObjectName, ObjectUUID);
                    break;
                case "ROAD":
                    RoadDictionary.Add(ObjectName, ObjectUUID);
                    break;
                case "NODE":
                    NodeDictionary.Add(ObjectName, ObjectUUID);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }


        }



    }

    
}
