using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Webservice
{
    [DataContract]
    public class token
    {
        [DataMember]
        public string Token { get; set; }

        public token(string token)
        {
            Token = token;
        }
    }
}