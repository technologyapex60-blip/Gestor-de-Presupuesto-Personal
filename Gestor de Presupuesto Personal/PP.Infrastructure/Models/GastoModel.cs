namespace PP.Infrastructure.Models
{
    public class GastoModel
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
    }
}