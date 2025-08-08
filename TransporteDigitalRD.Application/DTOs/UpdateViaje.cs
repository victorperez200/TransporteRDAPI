using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class UpdateViajeDto
    {
        [Required(ErrorMessage = "El UsuarioId es requerido")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El TipoId es requerido")]
        public int TipoId { get; set; }

        [Range(-90, 90, ErrorMessage = "La latitud del origen debe estar entre -90 y 90")]
        public double? OrigenLat { get; set; }

        [Range(-180, 180, ErrorMessage = "La longitud del origen debe estar entre -180 y 180")]
        public double? OrigenLong { get; set; }

        [Range(-90, 90, ErrorMessage = "La latitud del destino debe estar entre -90 y 90")]
        public double? DestLat { get; set; }

        [Range(-180, 180, ErrorMessage = "La longitud del destino debe estar entre -180 y 180")]
        public double? DestLong { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser mayor o igual a 0")]
        public decimal? Costo { get; set; }

        [StringLength(200, ErrorMessage = "La ubicación actual no puede exceder 200 caracteres")]
        public string UbicActual { get; set; }

        [StringLength(200, ErrorMessage = "El destino no puede exceder 200 caracteres")]
        public string Destino { get; set; }
    }
}
