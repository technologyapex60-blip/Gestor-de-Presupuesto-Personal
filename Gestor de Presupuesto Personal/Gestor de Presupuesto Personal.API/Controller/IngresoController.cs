namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Data;
using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using Gestor_de_Presupuesto_Personal.API.Model.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class IngresoController : ControllerBase
{
    private readonly GPPContext _context;

    public IngresoController(GPPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var response = _context.Ingresos.Select(i => new IngresoDTO
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
        var ingreso = _context.Ingresos.FirstOrDefault(i => i.Id == id);
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
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            UsuarioId = dto.UsuarioId,
            CategoriaId = dto.CategoriaId
        };

        _context.Ingresos.Add(ingreso);
        _context.SaveChanges();

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
        var ingreso = _context.Ingresos.FirstOrDefault(i => i.Id == id);
        if (ingreso == null)
            return NotFound($"Ingreso con Id {id} no encontrado.");

        ingreso.Monto = dto.Monto;
        ingreso.Fecha = dto.Fecha;
        ingreso.UsuarioId = dto.UsuarioId;
        ingreso.CategoriaId = dto.CategoriaId;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var ingreso = _context.Ingresos.FirstOrDefault(i => i.Id == id);
        if (ingreso == null)
            return NotFound($"Ingreso con Id {id} no encontrado.");

        _context.Ingresos.Remove(ingreso);
        _context.SaveChanges();
        return NoContent();
    }
}