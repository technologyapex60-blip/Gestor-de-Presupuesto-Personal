namespace Gestor_de_Presupuesto_Personal.API.Controller
{
    using Gestor_de_Presupuesto_Personal.API.Data;
    using Gestor_de_Presupuesto_Personal.API.Model;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class GastoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DataStore.Gastos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var gasto = DataStore.Gastos.FirstOrDefault(g => g.Id == id);
            if (gasto == null)
                return NotFound($"Gasto con Id {id} no encontrado.");

            return Ok(gasto);
        }

        [HttpPost]
        public IActionResult Post(Gasto gasto)
        {
            gasto.Id = DataStore.Gastos.Count > 0
                ? DataStore.Gastos.Max(g => g.Id) + 1
                : 1;

            DataStore.Gastos.Add(gasto);
            return CreatedAtAction(nameof(GetById), new { id = gasto.Id }, gasto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Gasto gastoActualizado)
        {
            if (id != gastoActualizado.Id)
                return BadRequest("El Id de la URL no coincide con el Id del cuerpo.");

            var gasto = DataStore.Gastos.FirstOrDefault(g => g.Id == id);
            if (gasto == null)
                return NotFound($"Gasto con Id {id} no encontrado.");

            gasto.Monto = gastoActualizado.Monto;
            gasto.Fecha = gastoActualizado.Fecha;
            gasto.UsuarioId = gastoActualizado.UsuarioId;
            gasto.CategoriaId = gastoActualizado.CategoriaId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gasto = DataStore.Gastos.FirstOrDefault(g => g.Id == id);
            if (gasto == null)
                return NotFound($"Gasto con Id {id} no encontrado.");

            DataStore.Gastos.Remove(gasto);
            return NoContent();
        }
    }

}