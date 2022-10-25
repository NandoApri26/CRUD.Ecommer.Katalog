using System;
using System.Collections.Generic;

namespace Floram.Models.Entities
{
    public partial class Pembeli
    {
        public Pembeli()
        {
            Barangs = new HashSet<Barang>();
        }

        public int IdPembeli { get; set; }
        public string? Nama { get; set; }
        public string? Alamat { get; set; }
        public string? NoHp { get; set; }
        public string? Negara { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<Barang> Barangs { get; set; }
    }
}
