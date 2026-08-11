using PP.Application.Contract;
using PP.Application.Core;
using PP.Domain.Entities;
using PP.Domain.Repository;
using System.Text.RegularExpressions;

namespace PP.Application.Service
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ServiceResult<Usuario>> GetByCorreoAsync(string correo)
        {
            var usuario = await _usuarioRepository.GetByCorreoAsync(correo);
            if (usuario == null)
                return ServiceResult<Usuario>.Fail("Usuario con correo " + correo + " no encontrado.");

            return ServiceResult<Usuario>.Ok(usuario);
        }

        public override async Task<ServiceResult<Usuario>> AddAsync(Usuario entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.AddAsync(entity);
        }

        public override async Task<ServiceResult<Usuario>> UpdateAsync(Usuario entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.UpdateAsync(entity);
        }

        private ServiceResult<Usuario> Validar(Usuario entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Nombre))
                return ServiceResult<Usuario>.Fail("El nombre es obligatorio.");

            if (entity.Nombre.Length > 100)
                return ServiceResult<Usuario>.Fail("El nombre no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(entity.Correo))
                return ServiceResult<Usuario>.Fail("El correo es obligatorio.");

            var patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(entity.Correo, patronCorreo))
                return ServiceResult<Usuario>.Fail("El correo no tiene un formato válido.");

            return ServiceResult<Usuario>.Ok(entity);
        }

        private ServiceResult<Usuario> Validar(string nombre, string correo)
        {
            var usuarioTemporal = new Usuario { Nombre = nombre, Correo = correo };
            return Validar(usuarioTemporal);
        }
    }
}