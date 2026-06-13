namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Data;
using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using Gestor_de_Presupuesto_Personal.API.Model.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly GPPContext _context;

    public UsuarioController(GPPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var response = _context.Usuarios.Select(u => new UsuarioDTO
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Correo = u.Correo
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
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
    public IActionResult Post(UsuarioDTO dto)
    {
        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Correo = dto.Correo
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        var response = new UsuarioDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo
        };
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UsuarioDTO dto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return NotFound($"Usuario con Id {id} no encontrado.");

        usuario.Nombre = dto.Nombre;
        usuario.Correo = dto.Correo;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return NotFound($"Usuario con Id {id} no encontrado.");

        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
        return NoContent();
    }
}