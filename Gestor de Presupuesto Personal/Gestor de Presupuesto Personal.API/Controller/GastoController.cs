namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Data;
using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using Gestor_de_Presupuesto_Personal.API.Model.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GastoController : ControllerBase
{
    private readonly GPPContext _context;

    public GastoController(GPPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var response = _context.Gastos.Select(g => new GastoDTO
        {
            Id = g.Id,
            Monto = g.Monto,
            Fecha = g.Fecha,
            UsuarioId = g.UsuarioId,
            CategoriaId = g.CategoriaId
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var gasto = _context.Gastos.FirstOrDefault(g => g.Id == id);
        if (gasto == null)
            return NotFound($"Gasto con Id {id} no encontrado.");

        var response = new GastoDTO
        {
            Id = gasto.Id,
            Monto = gasto.Monto,
            Fecha = gasto.Fecha,
            UsuarioId = gasto.UsuarioId,
            CategoriaId = gasto.CategoriaId
        };
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post(GastoDTO dto)
    {
        var gasto = new Gasto
        {
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            UsuarioId = dto.UsuarioId,
            CategoriaId = dto.CategoriaId
        };

        _context.Gastos.Add(gasto);
        _context.SaveChanges();

        var response = new GastoDTO
        {
            Id = gasto.Id,
            Monto = gasto.Monto,
            Fecha = gasto.Fecha,
            UsuarioId = gasto.UsuarioId,
            CategoriaId = gasto.CategoriaId
        };
        return CreatedAtAction(nameof(GetById), new { id = gasto.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, GastoDTO dto)
    {
        var gasto = _context.Gastos.FirstOrDefault(g => g.Id == id);
        if (gasto == null)
            return NotFound($"Gasto con Id {id} no encontrado.");

        gasto.Monto = dto.Monto;
        gasto.Fecha = dto.Fecha;
        gasto.UsuarioId = dto.UsuarioId;
        gasto.CategoriaId = dto.CategoriaId;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var gasto = _context.Gastos.FirstOrDefault(g => g.Id == id);
        if (gasto == null)
            return NotFound($"Gasto con Id {id} no encontrado.");

        _context.Gastos.Remove(gasto);
        _context.SaveChanges();
        return NoContent();
    }
}