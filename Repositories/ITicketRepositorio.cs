using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Models.Dto;

namespace TempporalWS.Repositories
{
    public interface ITicketRepositorio
    {
        Task<List<TicketDto>> GetAll();
        Task<TicketDto> GetById(int id);

        Task<TicketDto> CreateUpdate(TicketDto ticketDto);

        Task<bool> Delete(int id);
    }
}
