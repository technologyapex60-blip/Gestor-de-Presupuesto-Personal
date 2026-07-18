namespace Gestor_de_Presupuesto_Personal.API.Controllers;

using PP.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BalanceController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IIngresoRepository _ingresoRepository;
    private readonly IGastoRepository _gastoRepository;

    public BalanceController(
        IUsuarioRepository usuarioRepository,
        IIngresoRepository ingresoRepository,
        IGastoRepository gastoRepository)
    {
        _usuarioRepository = usuarioRepository;
        _ingresoRepository = ingresoRepository;
        _gastoRepository = gastoRepository;
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> GetBalance(int usuarioId)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
        if (usuario == null)
            return NotFound("Usuario con Id " + usuarioId + " no encontrado.");

        var ingresos = await _ingresoRepository.GetByUsuarioIdAsync(usuarioId);
        var gastos = await _gastoRepository.GetByUsuarioIdAsync(usuarioId);

        var totalIngresos = ingresos.Sum(i => i.Monto);
        var totalGastos = gastos.Sum(g => g.Monto);
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