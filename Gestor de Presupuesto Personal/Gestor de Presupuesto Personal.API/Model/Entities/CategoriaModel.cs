namespace Gestor_de_Presupuesto_Personal.API.Model.Entities;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;

    public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
}