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
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductoRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;

        }
        public async Task<ProductoDto> Update(ProductoDto productoDto)
        {
            Producto producto = _mapper.Map<Producto>(productoDto);
            _db.Productos.Update(producto);
            await _db.SaveChangesAsync();
            return _mapper.Map<Producto, ProductoDto>(producto);
        }

        public async Task<ProductoDto> Create(ProductoDto productoDto)
        {
            Producto producto = _mapper.Map<Producto>(productoDto);
            await _db.Productos.AddAsync(producto);
            await _db.SaveChangesAsync();
            return _mapper.Map<Producto, ProductoDto>(producto);
        }


        public async Task<bool> Delete(string codigo)
        {
            try
            {
                Producto producto = await _db.Productos.FindAsync(codigo);
                if (producto == null) return false;
                _db.Productos.Remove(producto);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductoDto>> GetAll()
        {
            List<Producto> resultado = await _db.Productos.ToListAsync();
            return _mapper.Map<List<ProductoDto>>(resultado);
        }

        public async Task<ProductoDto> GetById(string codigo)
        {
            Producto producto = await _db.Productos.FindAsync(codigo);
            return _mapper.Map<ProductoDto>(producto);
        }
        public async Task<ProductoDto> UpdateQuantity(decimal cantidad, ProductoDto productoDto, bool isAdd)
        {
            if(isAdd)
                productoDto.Existencia += cantidad;
            else productoDto.Existencia -= cantidad;
            Producto producto = _mapper.Map<Producto>(productoDto);
            _db.Productos.Update(producto);
            await _db.SaveChangesAsync();
            return _mapper.Map<Producto, ProductoDto>(producto);
        }

    }
}
