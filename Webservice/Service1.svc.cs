using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

//kodeordet i databasen er "kodeord" hashed med sha256 og den secretkey som også er brugt i JWT handleren.
namespace Webservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private JWTHandler jwtHandler = new JWTHandler();
        private const string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=synopsisUserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public token GetWebToken(string username, string password)
        {
            var sha256 = new SHA256CryptoServiceProvider();

            const string getUser = "select * from Users where Username = @username";
            using (SqlConnection dataConnection = new SqlConnection(connectionstring))
            {
                dataConnection.Open();
                using (SqlCommand GetCommand = new SqlCommand(getUser,dataConnection))
                {
                    GetCommand.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = GetCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read();

                        //prøver at undlade at gemme ting som variabler
                        if (reader.GetString(2)== "41bc394f343b2fe951bb305eabd522b2a7ee78e0f57e3bc01427dd58a055788b")
                        {
                            
                            return new token(jwtHandler.CreateToken(username));
                        }
                        return null;
                    }
                }
            }

        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
