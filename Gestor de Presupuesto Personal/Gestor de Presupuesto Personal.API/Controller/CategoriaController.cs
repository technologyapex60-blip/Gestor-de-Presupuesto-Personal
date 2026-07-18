namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using PP.Domain.Entities;
using PP.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categorias = await _categoriaRepository.GetAllAsync();
        var response = categorias.Select(c => new CategoriaDTO
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Tipo = c.Tipo
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
            return NotFound("Categoria con Id " + id + " no encontrada.");

        var response = new CategoriaDTO
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Tipo = categoria.Tipo
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CategoriaDTO dto)
    {
        var categoria = new Categoria
        {
            Nombre = dto.Nombre,
            Tipo = dto.Tipo
        };

        await _categoriaRepository.AddAsync(categoria);

        var response = new CategoriaDTO
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Tipo = categoria.Tipo
        };
        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CategoriaDTO dto)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
            return NotFound("Categoria con Id " + id + " no encontrada.");

        categoria.Nombre = dto.Nombre;
        categoria.Tipo = dto.Tipo;

        await _categoriaRepository.UpdateAsync(categoria);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
            return NotFound("Categoria con Id " + id + " no encontrada.");

        await _categoriaRepository.DeleteAsync(id);
        return NoContent();
    }
}