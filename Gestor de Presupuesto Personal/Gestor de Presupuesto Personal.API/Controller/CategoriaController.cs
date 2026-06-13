namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Data;
using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using Gestor_de_Presupuesto_Personal.API.Model.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = DataStore.Categorias.Select(c => new CategoriaDTO
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Tipo = c.Tipo
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var categoria = DataStore.Categorias.FirstOrDefault(c => c.Id == id);
        if (categoria == null)
            return NotFound($"Categoría con Id {id} no encontrada.");

        var response = new CategoriaDTO
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Tipo = categoria.Tipo
        };
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post(CategoriaDTO dto)
    {
        var categoria = new Categoria
        {
            Id = DataStore.Categorias.Count > 0 ? DataStore.Categorias.Max(c => c.Id) + 1 : 1,
            Nombre = dto.Nombre,
            Tipo = dto.Tipo
        };
        DataStore.Categorias.Add(categoria);

        var response = new CategoriaDTO
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Tipo = categoria.Tipo
        };
        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, CategoriaDTO dto)
    {
        var categoria = DataStore.Categorias.FirstOrDefault(c => c.Id == id);
        if (categoria == null)
            return NotFound($"Categoría con Id {id} no encontrada.");

        categoria.Nombre = dto.Nombre;
        categoria.Tipo = dto.Tipo;
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