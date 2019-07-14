using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using latihan_filter.Classes;
using System.Data.SqlClient;
using System.Data;

namespace latihan_filter.Models
{
    public class Barang
    {
        DB db = new DB();

        //---------------------------------------------------------------------
        public object id { set; get; }
        public object name { set; get; }
        public object price { set; get; }
        public object stock { set; get; }
        //---------------------------------------------------------------------

        public List<Barang> GetData()
        {
            List<Barang> datas = new List<Barang>();
            foreach(DataRow d in db.Query("select * from barang ").Fetch().Rows)
            {
                datas.Add(new Barang{id = d[0], name = d[2], price = d[3], stock = d[4]});
            }
           return datas ;
        }

        public int PostData(Barang b)
        {
            return db.Query("insert into barang(id_barang,nama_barang,harga_barang,stock_barang) values(@id,@nama,@harga,@stock)")
                    .Bind("@id=" + b.id + ",@nama=" + b.name + ",@harga=" + b.price + ",@stock=" + b.stock).Execute();
        }

        public int PutData(Barang b)
        {
            return db.Query("update barang set nama_barang=@name,harga_barang=@price,stock_barang=@stock where id_barang=@id")
                .Bind("@id=" + b.id + ",@name=" + b.name + ",@price=" + b.price + ",@stock=" + b.stock).Execute();
        }

        public int DeleteData(Barang b)
        {
            return db.Query("delete from barang where id_barang = @id").Bind("@id=" + b.id).Execute();
        }

    }
}