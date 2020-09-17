using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Healthcare_VR
{
    class General
    {
        public static string Get(string head)
        {
            JObject Get = new JObject(
                new JProperty("id", "get"),
                new JProperty("data",
                new JObject(new JProperty("type", head)
              )));
            return Get.ToString();
        }

        public static string SetCallBack(string button, string trigger, string left)
        {
            JObject SetCallBack = new JObject(
                new JProperty("id", "setcallback"),
                new JProperty("data",
                new JObject(new JProperty("type", button),
                new JObject(new JProperty("button", trigger),
                new JObject(new JProperty("hand", left)
              )))));
            return SetCallBack.ToString();
        }

        public static string Play()
        {
            JObject Play = new JObject(
                new JProperty("id", "play"),
                new JProperty("data"));

            return Play.ToString();
        }

        public static string Pause()
        {
            JObject Pause = new JObject(
                new JProperty("id", "pause"),
                new JProperty("data"));

            return Pause.ToString();
        }
    }
}
