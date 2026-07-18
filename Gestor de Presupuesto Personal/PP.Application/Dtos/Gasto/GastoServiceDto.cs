using PP.Application.Dtos;

namespace PP.Application.Dtos.Gasto
{
    public class GastoServiceDto : DtoBase
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
    }
}