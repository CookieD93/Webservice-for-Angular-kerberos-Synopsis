using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Webservice
{
    public class JWTHandler
    {
        
        public string signature { get; set; }


        private const string secretKey = "0DADfIprEiatG16NRY4Is0jAWEwm8ZYNH1CN5q5yL4YBQ4gUJLnnG9ZsgIhX8zddVUMuXMC3VCwrkSaGXWUiG8ErZkeoosaUxIuWwFDbwjQYLjbHgAWsYgROgoKbf7de5NjT7RRD0pjWfpNlkraNshWBLvsFFF6ccEtX5EY03dNKbxKer1SSPwQIYTh2iFpJx5SoKuTy";


        public JWTHandler()
        {

        }

        public string CreateToken(string Username)
        {
            JWT jwt = new JWT(Username);
            string EncodedHeader = Base64UrlEncoder.Encode(jwt.header);

            
            jwt.generatePayload();
            string EncodedPayload = Base64UrlEncoder.Encode(jwt.Payload);

            var sha256 = new SHA256CryptoServiceProvider();
            byte[] buffer = Encoding.ASCII.GetBytes($"{EncodedHeader}" + "." + $"{EncodedPayload}" + "," + secretKey);
            byte[] hashed = sha256.ComputeHash(buffer);
            signature = Base64UrlEncoder.Encode(hashed);

            return EncodedHeader + "." + EncodedPayload +"."+ signature;
        }
    }
}