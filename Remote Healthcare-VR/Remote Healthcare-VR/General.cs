using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Healthcare_VR
{
    class General
    {
        public static JObject Get(string head)
        {
            JObject Get = new JObject(
                new JProperty("id", "get"),
                new JProperty("data",
                new JObject(new JProperty("type", head)
              )));
            return Get;
        }

        public static JObject SetCallBack(string button, string trigger, string left)
        {
            JObject SetCallBack = new JObject(
                new JProperty("id", "setcallback"),
                new JProperty("data",
                new JObject(new JProperty("type", button),
                new JObject(new JProperty("button", trigger),
                new JObject(new JProperty("hand", left)
              )))));
            return SetCallBack;
        }

        public static JObject Play()
        {
            JObject Play = new JObject(
                new JProperty("id", "play"),
                new JProperty("data"));

            return Play;
        }

        public static JObject Pause()
        {
            JObject Pause = new JObject(
                new JProperty("id", "pause"),
                new JProperty("data"));

            return Pause;
        }
    }
}
