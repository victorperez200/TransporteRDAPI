using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; } = "Usuario";
    }
}
