using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;





namespace WorkerRole1
{
    public class dbSynchro
    {

        public static String GetData()
        {
            string results;
            string conn = "server=mssql.oldtowndining.com;database=myDb;uid=myUID;pwd=myPWD;pooling=true;connection lifetime=120;max pool size=25;";
            string query = "Select * from tblCustomers where CLastName = 'Baily'";
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ArrayList objs = new ArrayList();

                //get the data reader, etc.
                while (reader.Read())
                {
                    objs.Add(new
                    {
                        Id = reader["CustomerId"],
                        LastName = reader["CLastName"],
                        FirstName = reader["CFirstName"],
                        Email = reader["CEmail"]
                    });
                }

                //clean up datareader

                //Console.WriteLine(JsonConvert.SerializeObject(objs));
                //Console.ReadLine();
                results = JsonConvert.SerializeObject(objs);

            }
            //return new string[] { "Bid1", "Bid2" };
            Trace.Write(results);
            
            return results;
        }
    }
}
