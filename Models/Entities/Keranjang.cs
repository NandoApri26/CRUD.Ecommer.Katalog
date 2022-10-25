using System;
using System.Collections.Generic;

namespace Floram.Models.Entities
{
    public partial class Keranjang
    {
        internal bool dataUser;

        public int Id { get; set; }
        public decimal HargaSatuan { get; set; }
        public int Jumlah { get; set; }
        public int IdBarang { get; set; }
        public int IdUser { get; set; }

        public virtual Barang IdBarangNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
