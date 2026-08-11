using PP.Domain.Core;

namespace PP.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

        public override string ObtenerDescripcion()
            => $"Usuario #{Id}: {Nombre} ({Correo})";
    }
}