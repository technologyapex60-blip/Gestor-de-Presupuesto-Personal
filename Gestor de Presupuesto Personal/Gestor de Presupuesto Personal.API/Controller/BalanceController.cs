namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using Gestor_de_Presupuesto_Personal.API.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BalanceController : ControllerBase
{
    private readonly GPPContext _context;

    public BalanceController(GPPContext context)
    {
        _context = context;
    }

    [HttpGet("{usuarioId}")]
    public IActionResult GetBalance(int usuarioId)
    {
        var usuarioExiste = _context.Usuarios.Any(u => u.Id == usuarioId);
        if (!usuarioExiste)
            return NotFound($"Usuario con Id {usuarioId} no encontrado.");

        var totalIngresos = _context.Ingresos
            .Where(i => i.UsuarioId == usuarioId)
            .Sum(i => i.Monto);

        var totalGastos = _context.Gastos
            .Where(g => g.UsuarioId == usuarioId)
            .Sum(g => g.Monto);

        var balance = totalIngresos - totalGastos;

        return Ok(new
        {
            UsuarioId = usuarioId,
            TotalIngresos = totalIngresos,
            TotalGastos = totalGastos,
            Balance = balance,
            Estado = balance >= 0 ? "Positivo" : "Negativo"
        });
    }
}