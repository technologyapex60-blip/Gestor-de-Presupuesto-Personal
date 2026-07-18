using PP.Application.Dtos;

namespace PP.Application.Dtos.Usuario
{
    public class UsuarioServiceDto : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}