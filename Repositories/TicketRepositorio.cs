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
    public class TicketRepositorio :ITicketRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TicketRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;

        }
        public async Task<List<TicketDto>> GetAll() {
            var resultado = await _db.Tickets.ToListAsync();
            return _mapper.Map<List<TicketDto>>(resultado);
        }
        public async Task<TicketDto> GetById(int id)
        {
            Ticket ticket = await _db.Tickets.FindAsync(id);
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<TicketDto> CreateUpdate(TicketDto ticketDto) {
            Ticket ticket = _mapper.Map<Ticket>(ticketDto);
            if (ticket.Folio > 0)
                _db.Tickets.Update(ticket);
            else
                await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();
            return _mapper.Map<Ticket, TicketDto>(ticket);
        }

        public async Task<bool> Delete(int id)
        {

            try
            {
                Ticket ticket = await _db.Tickets.FindAsync(id);
                if (ticket == null) return false;
                _db.Tickets.Remove(ticket);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
