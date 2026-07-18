namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using PP.Domain.Entities;
using PP.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class IngresoController : ControllerBase
{
    private readonly IIngresoRepository _ingresoRepository;

    public IngresoController(IIngresoRepository ingresoRepository)
    {
        _ingresoRepository = ingresoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var ingresos = await _ingresoRepository.GetAllAsync();
        var response = ingresos.Select(i => new IngresoDTO
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
    public async Task<IActionResult> GetById(int id)
    {
        var ingreso = await _ingresoRepository.GetByIdAsync(id);
        if (ingreso == null)
            return NotFound("Ingreso con Id " + id + " no encontrado.");

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
    public async Task<IActionResult> Post(IngresoDTO dto)
    {
        var ingreso = new Ingreso
        {
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            UsuarioId = dto.UsuarioId,
            CategoriaId = dto.CategoriaId
        };

        await _ingresoRepository.AddAsync(ingreso);

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
    public async Task<IActionResult> Put(int id, IngresoDTO dto)
    {
        var ingreso = await _ingresoRepository.GetByIdAsync(id);
        if (ingreso == null)
            return NotFound("Ingreso con Id " + id + " no encontrado.");

        ingreso.Monto = dto.Monto;
        ingreso.Fecha = dto.Fecha;
        ingreso.UsuarioId = dto.UsuarioId;
        ingreso.CategoriaId = dto.CategoriaId;

        await _ingresoRepository.UpdateAsync(ingreso);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ingreso = await _ingresoRepository.GetByIdAsync(id);
        if (ingreso == null)
            return NotFound("Ingreso con Id " + id + " no encontrado.");

        await _ingresoRepository.DeleteAsync(id);
        return NoContent();
    }
}