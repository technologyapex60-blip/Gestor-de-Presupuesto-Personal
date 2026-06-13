namespace Gestor_de_Presupuesto_Personal.API.Model;

public class Gasto
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public int UsuarioId { get; set; }

    public int CategoriaId { get; set; }
}