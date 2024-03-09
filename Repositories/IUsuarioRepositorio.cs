using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Models.Dto;

namespace TempporalWS.Repositories
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioDto>> GetAll();
        Task<UsuarioDto> GetById(int id);

        Task<UsuarioDto> CreateUpdate(UsuarioDto productoTicketDto);

        Task<bool> Delete(int id);
        Task<UsuarioDto> Login(string userName, string password);
        Task<UsuarioDto> ChangePwd(int id, string password);
    }
}
