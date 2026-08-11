using PP.Domain.Core;

namespace PP.Domain.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;

        public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

        public override string ObtenerDescripcion()
            => $"Categoría #{Id}: {Nombre} [{Tipo}]";
    }
}