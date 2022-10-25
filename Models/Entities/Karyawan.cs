using System;
using System.Collections.Generic;

namespace Floram.Models.Entities
{
    public partial class Karyawan
    {
        public int IdKaryawan { get; set; }
        public string? NamaKaryawan { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Posisi { get; set; }
    }
}
