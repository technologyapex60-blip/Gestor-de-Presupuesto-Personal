namespace Gestor_de_Presupuesto_Personal.API.Controllers
{

    using Gestor_de_Presupuesto_Personal.API.Data;
    using Gestor_de_Presupuesto_Personal.API.Model;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DataStore.Usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = DataStore.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound($"Usuario con Id {id} no encontrado.");

            return Ok(usuario);
        }
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            usuario.Id = DataStore.Usuarios.Count > 0
                ? DataStore.Usuarios.Max(u => u.Id) + 1
                : 1;

            DataStore.Usuarios.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuarioActualizado)
        {
            if (id != usuarioActualizado.Id)
                return BadRequest("El Id de la URL no coincide con el Id del cuerpo.");

            var usuario = DataStore.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound($"Usuario con Id {id} no encontrado.");


            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Correo = usuarioActualizado.Correo;


            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = DataStore.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound($"Usuario con Id {id} no encontrado.");

            DataStore.Usuarios.Remove(usuario);
            return NoContent();
        }
    }

}