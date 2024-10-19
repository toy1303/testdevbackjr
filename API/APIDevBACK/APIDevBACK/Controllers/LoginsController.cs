using APIDevBACK.DtoEntrada;
using APIDevBACK.DtoSalida;
using APIDevBACK.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIDevBACK.Controllers
{
    [Produces("application/json")]
    [Route("logins")]
    public class LoginsController : Controller
    {
        private readonly TestDevContext testContext;
        public LoginsController(TestDevContext testDevContext )
        {
            testContext = testDevContext;
        }

        [HttpGet]
        public IActionResult ObtieneLogins()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                respuesta.Datos = this.testContext.Ccloglogins.ToList();
                respuesta.Mensaje = "Exito";
            }
            catch (Exception ex)
            {
                respuesta.Mensaje=ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult Registranuevo([FromBody] DtoLogin dtoLogin) 
        {
            Respuesta respue = new Respuesta();
            if (!ModelState.IsValid)
            {
                // Retornar una respuesta con los errores de validación
                return BadRequest(ModelState);
            }

            try
            {
                CcUser ccUser = this.testContext.CcUsers.Where(c => c.UserId == dtoLogin.UserId).FirstOrDefault();
                if (ccUser == null)
                {
                    respue.Mensaje = "Usuario no encontrado";
                    return Ok(respue);
                }
                Ccloglogin ccloglogin = new Ccloglogin();
                ccloglogin.UserId=ccUser.UserId;
                ccloglogin.Extension= dtoLogin.Extension;
                ccloglogin.TipoMov = dtoLogin.TipoMov;
                ccloglogin.Fecha = dtoLogin.Fecha;
                this.testContext.Add(ccloglogin);
                this.testContext.SaveChanges();
                respue.Mensaje = "Opearción Exitosa";
                return Ok(respue);


            }
            catch (Exception ex)
            {
                respue.Mensaje = ex.Message;
            }


            return Ok("respue");
        }

        [HttpPut("{id}")]
        public IActionResult Actualiza(int id)
        {
            Respuesta respue = new Respuesta();
            try
            {
                CcUser Usuario = this.testContext.CcUsers.Where(m => m.UserId == id).FirstOrDefault();
                if (Usuario == null)
                {
                    respue.Mensaje = "No se encontro Usuario";
                    return Ok(respue);
                }
                Usuario.LastLoginAttempt = DateTime.Now;
                var entry = testContext.Entry(Usuario);
                Console.WriteLine(entry.State);
                Ccloglogin dCatlogion = this.testContext.Ccloglogins.Where(m=>m.UserId==Usuario.UserId).OrderBy(m=>m.Fecha).LastOrDefault();
                  // Guarda los cambios en la base de datos
                  Ccloglogin ccloglogin = new Ccloglogin();   
                  ccloglogin.UserId = Usuario.UserId;
                  ccloglogin.Extension = null; 
                  ccloglogin.User= Usuario;
                  ccloglogin.TipoMov = dCatlogion.TipoMov == 1 ? 0 : 1;
                  this.testContext.Ccloglogins.Add(ccloglogin);
                this.testContext.SaveChanges();
                respue.Mensaje = string.Format("Ultimo registro modificado correctamente IdUser: {0}", Usuario.UserId);
            }
            catch (Exception ex)
            {
                respue .Mensaje = ex.Message;
            }
            return Ok(respue);
        }

        [HttpDelete("{id}")]
        public IActionResult Borra(int id)
        {
            Respuesta respue = new Respuesta();
            Ccloglogin dCatlogion = this.testContext.Ccloglogins.Where(m=>m.LogId==id).FirstOrDefault();
            if (dCatlogion == null)
            {
                respue.Mensaje = "No existe registro con Id: " + id;
                return Ok(respue);
            }
            this.testContext.Remove(dCatlogion);
            this.testContext.SaveChanges();
            respue.Mensaje = "Operación Exitosa";
            return Ok(respue);
        }
    }
}
