using System;
using System.Collections.Generic;

namespace Floram.Models.Entities
{
    public partial class Barang
    {
        public Barang()
        {
            ItemTransaksis = new HashSet<ItemTransaksi>();
            Keranjangs = new HashSet<Keranjang>();
        }

        public int Id { get; set; }
        public string? Kode { get; set; }
        public string? Nama { get; set; }
        public string? Description { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public int IdPenjual { get; set; }
        public int? PembeliIdPembeli { get; set; }
        public string? Image { get; set; }
        public string? Url { get; set; }

        public virtual Penjual IdPenjualNavigation { get; set; } = null!;
        public virtual Pembeli? PembeliIdPembeliNavigation { get; set; }
        public virtual ICollection<ItemTransaksi> ItemTransaksis { get; set; }
        public virtual ICollection<Keranjang> Keranjangs { get; set; }
    }
}
