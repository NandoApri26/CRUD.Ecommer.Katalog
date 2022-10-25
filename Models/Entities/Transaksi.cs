using System;
using System.Collections.Generic;

namespace Floram.Models.Entities
{
    public partial class Transaksi
    {
        public Transaksi()
        {
            ItemTransaksis = new HashSet<ItemTransaksi>();
        }

        public int Id { get; set; }
        public decimal TotalHarga { get; set; }
        public int MetodePembayaran { get; set; }
        public string? StatusTransaksi { get; set; }
        public string? StatusBayar { get; set; }
        public string? AlamatPengirim { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<ItemTransaksi> ItemTransaksis { get; set; }
    }
}
