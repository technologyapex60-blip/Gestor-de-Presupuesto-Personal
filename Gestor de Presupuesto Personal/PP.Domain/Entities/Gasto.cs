using PP.Domain.Core;

namespace PP.Domain.Entities
{
    public class Gasto : BaseEntity
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public override string ObtenerDescripcion()
            => $"Gasto #{Id}: {Monto:C} el {Fecha:dd/MM/yyyy}";
    }
}