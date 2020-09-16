using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Healthcare_VR
{
    class ID
    {
        public string Id { get; set; }

        public ID(string id)
        {
            Id = id;
        }

        public string ToJSON()
        {
            //return JsonConvert.SerializeObject(this);
            return "{\r\n\"id\" : \"session/list\"\r\n}";
        }
    }
}
