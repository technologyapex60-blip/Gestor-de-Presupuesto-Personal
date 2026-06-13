namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Data;
using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using Gestor_de_Presupuesto_Personal.API.Model.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class IngresoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = DataStore.Ingresos.Select(i => new IngresoDTO
        {
            Id = i.Id,
            Monto = i.Monto,
            Fecha = i.Fecha,
            UsuarioId = i.UsuarioId,
            CategoriaId = i.CategoriaId
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var ingreso = DataStore.Ingresos.FirstOrDefault(i => i.Id == id);
        if (ingreso == null)
            return NotFound($"Ingreso con Id {id} no encontrado.");

        var response = new IngresoDTO
        {
            Id = ingreso.Id,
            Monto = ingreso.Monto,
            Fecha = ingreso.Fecha,
            UsuarioId = ingreso.UsuarioId,
            CategoriaId = ingreso.CategoriaId
        };
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post(IngresoDTO dto)
    {
        var ingreso = new Ingreso
        {
            Id = DataStore.Ingresos.Count > 0 ? DataStore.Ingresos.Max(i => i.Id) + 1 : 1,
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            UsuarioId = dto.UsuarioId,
            CategoriaId = dto.CategoriaId
        };
        DataStore.Ingresos.Add(ingreso);

        var response = new IngresoDTO
        {
            Id = ingreso.Id,
            Monto = ingreso.Monto,
            Fecha = ingreso.Fecha,
            UsuarioId = ingreso.UsuarioId,
            CategoriaId = ingreso.CategoriaId
        };
        return CreatedAtAction(nameof(GetById), new { id = ingreso.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, IngresoDTO dto)
    {
        var ingreso = DataStore.Ingresos.FirstOrDefault(i => i.Id == id);
        if (ingreso == null)
            return NotFound($"Ingreso con Id {id} no encontrado.");

        ingreso.Monto = dto.Monto;
        ingreso.Fecha = dto.Fecha;
        ingreso.UsuarioId = dto.UsuarioId;
        ingreso.CategoriaId = dto.CategoriaId;
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