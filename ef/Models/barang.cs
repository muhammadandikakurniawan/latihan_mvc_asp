//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ef.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class barang
    {
        public string id_barang { get; set; }
        public string id_pesanan { get; set; }
        public string nama_barang { get; set; }
        public Nullable<decimal> harga_barang { get; set; }
        public Nullable<int> stock_barang { get; set; }
        public string deskripsi_barang { get; set; }
    }
}
