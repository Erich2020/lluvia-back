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
    public class ProductoTicketRepositorio : IProductoTicketRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductoTicketRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;

        }
        public async Task<ProductoTicketDto> Update(ProductoTicketDto ptDto)
        {
            ProductoTicket pt = _mapper.Map<ProductoTicket>(ptDto);
            _db.ProductosTicket.Update(pt);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductoTicket, ProductoTicketDto>(pt);
        }

        public async Task<ProductoTicketDto> Create(ProductoTicketDto ptDto)
        {
            ProductoTicket pt = _mapper.Map<ProductoTicket>(ptDto);
            await _db.ProductosTicket.AddAsync(pt);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductoTicket, ProductoTicketDto>(pt);
        }

        public async Task<bool> Delete(string codigo)
        {
            try
            {
                ProductoTicket pt = await _db.ProductosTicket.FindAsync(codigo);
                if (pt == null) return false;
                _db.ProductosTicket.Remove(pt);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductoTicketDto>> GetAll()
        {
            List<ProductoTicket> resultado = await _db.ProductosTicket.ToListAsync();
            return _mapper.Map<List<ProductoTicketDto>>(resultado);
        }

        public async Task<ProductoTicketDto> GetById(string codigo)
        {
            ProductoTicket pt = await _db.ProductosTicket.FindAsync(codigo);
            return _mapper.Map<ProductoTicketDto>(pt);
        }

    }
}
