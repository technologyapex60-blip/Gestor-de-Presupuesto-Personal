namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using PP.Domain.Entities;
using PP.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        var response = usuarios.Select(u => new UsuarioDTO
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
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound($"Usuario con Id {id} no encontrado.");

        var response = new UsuarioDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo
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

        await _usuarioRepository.AddAsync(usuario);

        var response = new UsuarioDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo
        };
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UsuarioDTO dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound($"Usuario con Id {id} no encontrado.");

        usuario.Nombre = dto.Nombre;
        usuario.Correo = dto.Correo;

        await _usuarioRepository.UpdateAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound($"Usuario con Id {id} no encontrado.");

        await _usuarioRepository.DeleteAsync(id);
        return NoContent();
    }
}