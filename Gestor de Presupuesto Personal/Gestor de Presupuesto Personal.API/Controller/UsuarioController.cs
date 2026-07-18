namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using PP.Application.Contract;
using PP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _usuarioService.GetAllAsync();
        var response = result.Data!.Select(u => new UsuarioDTO
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Correo = u.Correo
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _usuarioService.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result.Message);

        var response = new UsuarioDTO
        {
            Id = result.Data!.Id,
            Nombre = result.Data.Nombre,
            Correo = result.Data.Correo
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UsuarioDTO dto)
    {
        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Correo = dto.Correo
        };

        var result = await _usuarioService.AddAsync(usuario);
        if (!result.Success)
            return BadRequest(result.Message);

        var response = new UsuarioDTO
        {
            Id = result.Data!.Id,
            Nombre = result.Data.Nombre,
            Correo = result.Data.Correo
        };
        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UsuarioDTO dto)
    {
        var existingResult = await _usuarioService.GetByIdAsync(id);
        if (!existingResult.Success)
            return NotFound(existingResult.Message);

        var usuario = existingResult.Data!;
        usuario.Nombre = dto.Nombre;
        usuario.Correo = dto.Correo;

        var updateResult = await _usuarioService.UpdateAsync(usuario);
        if (!updateResult.Success)
            return BadRequest(updateResult.Message);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _usuarioService.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result.Message);

        return NoContent();
    }
}