using Gestor_de_Presupuesto_Personal.API.Model;
namespace Gestor_de_Presupuesto_Personal.API.Data;



public static class DataStore
{
    public static List<Usuario> Usuarios { get; } = new();

    public static List<Categoria> Categorias { get; } = new();

}