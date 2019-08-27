using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace D_Library.Models.Plugins
{
    public class Tags
    {
        public string Value { get; set; }

        public Tags()
        {

        }

        [JsonConstructor]
        public Tags(string value)
        {
            this.Value = value;
        }
    }
}