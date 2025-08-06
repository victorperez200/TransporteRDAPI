using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.UseCases
{
    public class ReportarProblemaService
    {
        private readonly TransRDDataContext _db;

        public ReportarProblemaService(TransRDDataContext db)
        {
            _db = db;
        }

        public async Task<List<ReportarProblemaResponse>> GetReportarProblema()
        {
            var reportProblem = _db.ReportarProblema.ToList();
            var reportProblemResponse = new List<ReportarProblemaResponse>();

            foreach (var item in reportProblem)
            {
                var result = new ReportarProblemaResponse
                {
                    Desc_Problema = item.Desc_Problema,
                    Origen_Lat = item.Origen_Lat,
                    Origen_Lng = item.Origen_Lng,
                    TipoProblemaId = item.TipoProblemaId,
                    TipoTransporteId = item.TipoTransporteId,
                };
                reportProblemResponse.Add(result);
            }

            return reportProblemResponse;
        }

        public async Task<bool> CreateReportarProblema(CreateReportarProblemaDto dto)
        {
            if (dto == null) return false;

            var reportarProblema = new ReportarProblema()
            {
                TipoTransporteId = dto.TipoTransporteId,
                TipoProblemaId = dto.TipoProblemaId,
                Desc_Problema = dto.Desc_Problema,
                Origen_Lat = dto.Origen_Lat,
                Origen_Lng = dto.Origen_Lng
            };

            _db.ReportarProblema.InsertOnSubmit(reportarProblema);

            try
            {
                _db.SubmitChanges(); // Guardar cambios en la base de datos
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteReportarProblema(int id)
        {
            if (id <= 0 || !_db.ReportarProblema.Any(x => x.Id == id)) return false;

            var problema = _db.ReportarProblema.FirstOrDefault(x => x.Id == id);

            _db.ReportarProblema.DeleteOnSubmit(problema);

            try
            {
                _db.SubmitChanges(); // Guardar cambios en la base de datos
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
