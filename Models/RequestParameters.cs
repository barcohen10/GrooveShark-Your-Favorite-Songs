using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FavoriteSongs.Models
{
    public class RequestParameters 
    {
        public RequestParameters()
        {
            parameters = new Dictionary<string, object>();
            header = new Dictionary<string, string>();
        }

        public string method { get; set; }
        public Dictionary<string, string> header { get; set; }
        public Dictionary<string, Object> parameters { get; set; }
    }
}