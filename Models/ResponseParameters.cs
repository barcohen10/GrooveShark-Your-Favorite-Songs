using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FavoriteSongs.Models
{
    public class ResponseParameters
    {
        public ResponseParameters()
        {
            result = new Dictionary<string, object>();
            header = new Dictionary<string, string>();
        }
        public Dictionary<string, string> header { get; set; }
        public Dictionary<string, Object> result { get; set; }
    }
}