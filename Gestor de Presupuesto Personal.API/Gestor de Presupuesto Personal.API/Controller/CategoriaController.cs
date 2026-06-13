namespace Gestor_de_Presupuesto_Personal.API.Controllers
{
    using Gestor_de_Presupuesto_Personal.API.Data;
    using Gestor_de_Presupuesto_Personal.API.Model;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DataStore.Categorias);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var categoria = DataStore.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return NotFound($"Categoría con Id {id} no encontrada.");

            return Ok(categoria);
        }


        [HttpPost]
        public IActionResult Post(Categoria categoria)
        {
            categoria.Id = DataStore.Categorias.Count > 0
                ? DataStore.Categorias.Max(c => c.Id) + 1
                : 1;

            DataStore.Categorias.Add(categoria);
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Categoria categoriaActualizada)
        {
            if (id != categoriaActualizada.Id)
                return BadRequest("El Id de la URL no coincide con el Id del cuerpo.");

            var categoria = DataStore.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return NotFound($"Categoría con Id {id} no encontrada.");

            categoria.Nombre = categoriaActualizada.Nombre;
            categoria.Tipo = categoriaActualizada.Tipo;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoria = DataStore.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return NotFound($"Categoría con Id {id} no encontrada.");

            DataStore.Categorias.Remove(categoria);
            return NoContent();
        }
    }

}