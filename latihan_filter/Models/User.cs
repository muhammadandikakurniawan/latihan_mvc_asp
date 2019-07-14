using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using latihan_filter.Classes;

namespace latihan_filter.Models
{
    public class User
    {
        DB db = new DB();

        //---------------------------------------------------------
        public object id { set; get; }
        public object username { set; get; }
        public object password { set; get; }
        //---------------------------------------------------------

        public List<User> GetData()
        {
            List<User> data = new List<User>();

            foreach(DataRow d in db.Query("select * from tb_User").Fetch().Rows)
            {
                data.Add(new User{id = (object)d["user_id"], username = (object)d["username"],password = (object)d["password"]});
            }

            return data;
        }
    }
}