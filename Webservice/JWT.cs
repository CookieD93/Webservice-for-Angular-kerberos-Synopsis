using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webservice
{
    public class JWT
    {
        public string header { get; set; } = "{ \"alg\": \"HS256\", \"typ\": \"JWT\" }";
        public string iss { get; set; } = "Local";
        public string aud { get; set; } = "Local";
        public string exp { get; set; } = DateTime.UtcNow.Add(new TimeSpan(8, 0, 0)).ToString("MMM. dd, yyyy HH:mm:ss");
        public string CID { get; set; }
        public string Payload { get; set; }


        public JWT(string CID = null)
        {
            this.CID = CID;
        }

        public void generatePayload()
        {
            Payload = "{\"iss\": \"" + iss + "\",\"aud\": \"" + aud + "\",\"exp\": \"" + exp + "\",\"CID\": \"" + CID +
                      "\"}";
        }
    }
}