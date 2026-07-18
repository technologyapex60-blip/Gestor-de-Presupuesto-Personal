namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using PP.Domain.Entities;
using PP.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GastoController : ControllerBase
{
    private readonly IGastoRepository _gastoRepository;

    public GastoController(IGastoRepository gastoRepository)
    {
        _gastoRepository = gastoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var gastos = await _gastoRepository.GetAllAsync();
        var response = gastos.Select(g => new GastoDTO
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
    public async Task<IActionResult> GetById(int id)
    {
        var gasto = await _gastoRepository.GetByIdAsync(id);
        if (gasto == null)
            return NotFound("Gasto con Id " + id + " no encontrado.");

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
    public async Task<IActionResult> Post(GastoDTO dto)
    {
        var gasto = new Gasto
        {
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            UsuarioId = dto.UsuarioId,
            CategoriaId = dto.CategoriaId
        };

        await _gastoRepository.AddAsync(gasto);

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
    public async Task<IActionResult> Put(int id, GastoDTO dto)
    {
        var gasto = await _gastoRepository.GetByIdAsync(id);
        if (gasto == null)
            return NotFound("Gasto con Id " + id + " no encontrado.");

        gasto.Monto = dto.Monto;
        gasto.Fecha = dto.Fecha;
        gasto.UsuarioId = dto.UsuarioId;
        gasto.CategoriaId = dto.CategoriaId;

        await _gastoRepository.UpdateAsync(gasto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var gasto = await _gastoRepository.GetByIdAsync(id);
        if (gasto == null)
            return NotFound("Gasto con Id " + id + " no encontrado.");

        await _gastoRepository.DeleteAsync(id);
        return NoContent();
    }
}