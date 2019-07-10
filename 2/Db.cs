using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace _2
{
    public class Db
    {
        protected SqlConnection conn;
        protected SqlCommand stmt;
        protected string db_host = ".//",
                         db_name = "HR";
        public string status;
        public Db()
        {
            string connstr = "Server=" + this.db_host + ";Database=" + this.db_name + ";Trusted_Connection= true";
            try
            {
                this.conn = new SqlConnection(connstr);
                this.status = "connection ok";
            }
            catch(SqlException e)
            {
                this.status = e.Message.ToString();
            }
        }


    }
}