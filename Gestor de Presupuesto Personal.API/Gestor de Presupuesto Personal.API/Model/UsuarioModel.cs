namespace Gestor_de_Presupuesto_Personal.API.Model;

public class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public List<Ingreso> Ingresos { get; set; } = new();



}
