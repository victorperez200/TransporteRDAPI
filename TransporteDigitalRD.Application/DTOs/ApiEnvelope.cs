using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class ApiEnvelope<T>
    {
        public T Result { get; set; }
        public string Message { get; set; } // opcional
        public string Error { get; set; }   // opcional
    }

}
