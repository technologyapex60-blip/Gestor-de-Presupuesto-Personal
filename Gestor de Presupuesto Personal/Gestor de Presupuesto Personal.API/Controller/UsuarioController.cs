namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Data;
using Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;
using Gestor_de_Presupuesto_Personal.API.Model.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = DataStore.Usuarios.Select(u => new UsuarioDTO
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
        var usuario = DataStore.Usuarios.FirstOrDefault(u => u.Id == id);
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
            Id = DataStore.Usuarios.Count > 0 ? DataStore.Usuarios.Max(u => u.Id) + 1 : 1,
            Nombre = dto.Nombre,
            Correo = dto.Correo
        };

        DataStore.Usuarios.Add(usuario);

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
        var usuario = DataStore.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return NotFound($"Usuario con Id {id} no encontrado.");

        usuario.Nombre = dto.Nombre;
        usuario.Correo = dto.Correo;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var usuario = DataStore.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return NotFound($"Usuario con Id {id} no encontrado.");

        DataStore.Usuarios.Remove(usuario);
        return NoContent();
    }
}