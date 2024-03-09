using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Models.Dto;

namespace TempporalWS.Repositories
{
    public interface IProductoRepositorio
    {
        Task<List<ProductoDto>> GetAll();
        Task<ProductoDto> GetById(string Codigo);

        Task<ProductoDto> Update(ProductoDto productoDto);
        Task<ProductoDto> Create(ProductoDto productoDto);
        Task<bool> Delete(string Codigo);
         Task<ProductoDto> UpdateQuantity(decimal cantidad, ProductoDto productoDto, bool isAdd);
    }
}
