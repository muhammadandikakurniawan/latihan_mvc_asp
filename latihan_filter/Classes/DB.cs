using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace latihan_filter.Classes
{
    public class DB
    {
        private SqlConnection connection;
        private SqlCommand command;
        private DataTable dt;
        private string db_name = "db_adp";

        public DB()
        {
            string conn_str = @"Server=.\;Database="+this.db_name+";Trusted_Connection=True";
            this.connection = new SqlConnection(conn_str);
        }
        
        public SqlConnection Connection()
        {
            return this.connection;
        }

        public DB Query(string query)
        {
            this.command = this.connection.CreateCommand();
            this.command.CommandText = query;
            this.command.Prepare();
            return this;
        }

        public DB Bind(string param)
        {
            string[] part = param.Split(',');
            foreach (string p in part)
            {
                this.command.Parameters.AddWithValue(p.Split('=')[0].Trim(), (object)p.Split('=')[1].Trim());
            }
            return this;
        }
        public DataTable Fetch()
        {
            this.connection.Open();
            this.dt = new DataTable("table");
            dt.Load(this.command.ExecuteReader());
            this.connection.Close();
            return this.dt;
        }
        public int Execute()
        {
            this.connection.Open();
            int r = this.command.ExecuteNonQuery();
            this.connection.Close();
            return r;
        }
    }
}