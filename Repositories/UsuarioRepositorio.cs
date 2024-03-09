using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Data;
using TempporalWS.Models;
using TempporalWS.Models.Dto;

namespace TempporalWS.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UsuarioRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;

        }
        public async Task<List<UsuarioDto>> GetAll()
        {
            List<Usuario> resultado = await _db.Usuarios.ToListAsync();
            return _mapper.Map<List<UsuarioDto>>(resultado);
        }

        public async Task<UsuarioDto> GetById(int id)
        {
            Usuario usuario = await _db.Usuarios.FindAsync(id);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> CreateUpdate(UsuarioDto usuarioDto)
        {
            Usuario usuarioTemp = _mapper.Map<Usuario>(usuarioDto);
            Usuario usuario = usuarioTemp;
            if (usuario.Id > 0)
                _db.Usuarios.Update(usuario);
            else{
            	usuario = paswordHash(usuarioTemp);
                usuario.Username = await ValidarUsername(usuarioDto.Username);
                await _db.Usuarios.AddAsync(usuario);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Usuario, UsuarioDto>(usuario);
        }
        private Usuario paswordHash(Usuario usuario)
        {
            usuario.Password = Encrypt.GetSHA256(usuario.Password);
            return usuario;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Usuario usuario = await _db.Usuarios.FindAsync(id);
                if (usuario == null) return false;
                _db.Usuarios.Remove(usuario);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
 
        private async Task<bool> ExisteUsuario(string userName)
        {
            var usuarios = await this.GetAll();
            var usuarioExiste = usuarios.Where(user => user.Username.ToUpper().Equals(userName.ToUpper())).ToList();

            return (usuarioExiste.Count > 0);
        }
 
        public async Task<UsuarioDto> Login(string userName, string password)
        {
            UsuarioDto resultado = new UsuarioDto();
            if (await ExisteUsuario(userName))
            {
                var usuario = await ObtenerUsuario(userName);
                 if(Encrypt.VerificarPassword(usuario, password))
                    resultado = _mapper.Map<Usuario, UsuarioDto>( usuario);
            }
            resultado.Password = "";
            return resultado;
        }
        private async Task<Usuario> ObtenerUsuario(string userName)
        {
            var usuarios = await GetAll();
            var usuarioExistente = usuarios.Where(user => user.Username.Equals(userName)).First();
            return await _db.Usuarios.FindAsync(usuarioExistente.Id);

        }
        private async Task<string> ValidarUsername(string username)
        {
            string resultado = username.Trim();
            var usuarios = await GetAll();
            if (usuarios.Where(x => x.Username.Equals(username)).Count() > 0)
            {
                var numero = new Random();
                username += numero.Next(0, 99);
                resultado = await ValidarUsername(username);
            }
            return resultado;
        }

        public async Task<UsuarioDto> ChangePwd(int id, string password)
        {
            var usuarioTemp = await GetById(id);
            Usuario usuario = _mapper.Map<Usuario>(usuarioTemp);
            usuario.Password = Encrypt.GetSHA256(password);
            if (usuario.Id > 0)
                _db.Usuarios.Update(usuario);
            else
                await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            return _mapper.Map<Usuario, UsuarioDto>(usuario);
        }
    }
}
