using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexaPOS
{
    
    internal class DBConnect
    {
        private string con;
        public string myConnection()
        {
            string constring = "Server=LAPTOP-RJH1H2L0\\SQLEXPRESS;Database=dbNexaPOS;Trusted_Connection=True;TrustServerCertificate=True;";
            return constring;
        }
    }
}
