using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Models.Dto;

namespace TempporalWS.Repositories
{
    public interface IProductoTicketRepositorio
    {
        Task<List<ProductoTicketDto>> GetAll();
        Task<ProductoTicketDto> GetById(string Codigo);

        Task<ProductoTicketDto> Create(ProductoTicketDto productoTicketDto);

        Task<ProductoTicketDto> Update(ProductoTicketDto productoTicketDto);

        Task<bool> Delete(string Codigo);
    }
}
