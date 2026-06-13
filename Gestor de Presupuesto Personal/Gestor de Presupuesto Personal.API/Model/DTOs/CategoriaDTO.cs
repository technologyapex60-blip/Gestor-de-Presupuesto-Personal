namespace Gestor_de_Presupuesto_Personal.API.Model.Entities.DTOs;

public class CategoriaDTO
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;
}