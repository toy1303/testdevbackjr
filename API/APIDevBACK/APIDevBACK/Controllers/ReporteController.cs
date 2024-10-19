using APIDevBACK.DtoSalida;
using APIDevBACK.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Text;

namespace APIDevBACK.Controllers
{
    [Route("Reporte")]
    public class ReporteController : Controller
    {
        private readonly TestDevContext testContext;
        string pathreport = Path.Combine(Directory.GetCurrentDirectory(), "Reporte.csv");
        public ReporteController(TestDevContext testDevContext)
        {
            testContext = testDevContext;
        }

        [HttpGet]
        public ActionResult ObtieneReporte()
        {
            List<DtoRepporte> dtoRepportes = new List<DtoRepporte>();
            try
            {
                dtoRepportes = this.testContext.ReporteHorasTrabajdas();

                using (var stream = new MemoryStream())
                {
                    var csv = new StringBuilder();

                    // Agregar encabezados
                    csv.AppendLine("User Id,NombreCompleto,Total de Horas ,Area");

                    // Agregar los datos
                    foreach (var lista in dtoRepportes)
                    {
                        var line = $"{lista.User_id},{lista.NombreCompleto},{lista.TotalHoras},{lista.Areaname.ToString(CultureInfo.InvariantCulture)}";
                        csv.AppendLine(line);
                    }

                    var content = Encoding.UTF8.GetBytes(csv.ToString());
                    stream.Write(content, 0, content.Length);
                    stream.Position = 0; // Reiniciar el stream para la lectura

                    // Devolver el archivo CSV
                    return File(stream.ToArray(), "text/csv", "EficienciaBancoRep.csv");
                }

            }
            catch (Exception ex) {
                return Ok(ex.Message);
            }
            
        }
    }
}
