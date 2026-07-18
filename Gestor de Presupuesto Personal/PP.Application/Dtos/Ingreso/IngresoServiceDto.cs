using PP.Application.Dtos;

namespace PP.Application.Dtos.Ingreso
{
    public class IngresoServiceDto : DtoBase
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
    }
}