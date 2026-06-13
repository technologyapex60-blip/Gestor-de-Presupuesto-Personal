namespace Gestor_de_Presupuesto_Personal.API.Controller
{
    using Gestor_de_Presupuesto_Personal.API.Data;
    using Gestor_de_Presupuesto_Personal.API.Model.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class IngresoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DataStore.Ingresos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ingreso = DataStore.Ingresos.FirstOrDefault(i => i.Id == id);
            if (ingreso == null)
                return NotFound($"Ingreso con Id {id} no encontrado.");

            return Ok(ingreso);
        }

        [HttpPost]
        public IActionResult Post(Ingreso ingreso)
        {
            ingreso.Id = DataStore.Ingresos.Count > 0
                ? DataStore.Ingresos.Max(i => i.Id) + 1
                : 1;

            DataStore.Ingresos.Add(ingreso);
            return CreatedAtAction(nameof(GetById), new { id = ingreso.Id }, ingreso);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Ingreso ingresoActualizado)
        {
            if (id != ingresoActualizado.Id)
                return BadRequest("El Id de la URL no coincide con el Id del cuerpo.");

            var ingreso = DataStore.Ingresos.FirstOrDefault(i => i.Id == id);
            if (ingreso == null)
                return NotFound($"Ingreso con Id {id} no encontrado.");

            ingreso.Monto = ingresoActualizado.Monto;
            ingreso.Fecha = ingresoActualizado.Fecha;
            ingreso.UsuarioId = ingresoActualizado.UsuarioId;
            ingreso.CategoriaId = ingresoActualizado.CategoriaId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ingreso = DataStore.Ingresos.FirstOrDefault(i => i.Id == id);
            if (ingreso == null)
                return NotFound($"Ingreso con Id {id} no encontrado.");

            DataStore.Ingresos.Remove(ingreso);
            return NoContent();
        }
    }
}