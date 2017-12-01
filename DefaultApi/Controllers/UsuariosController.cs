using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DefaultApi.Controllers
{
    [Route("api/usuarios")]
    [Authorize]
    public class UsuariosController : Controller
    {
        [Route("yo")]
        public IActionResult GetCurrent()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}