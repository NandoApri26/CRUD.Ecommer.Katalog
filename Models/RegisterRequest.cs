using Microsoft.VisualBasic.CompilerServices;
namespace Floram.Models;

public class RegisterRequest {
    public string? Username {get; set;}
    public string? Password {get; set;}
    public string? Nama {get; set;}
    public string? Alamat {get; set;}
    public string? NoHp {get; set;}
    public string Tipe { get; set; } = null!;
}