using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Models;
using TempporalWS.Models.Dto;
using TempporalWS.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?Linkfolio=397860

namespace TempporalWS.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepositorio _ticketRepo;

        private readonly Response _respuesta;
        public TicketsController(ITicketRepositorio ticketRepo)
        {
            _ticketRepo = ticketRepo;
            _respuesta = new Response();
        }
        // GET: api/<ProductosTicketController>
        [EnableCors("corsConfiguration")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetProductosTicket()
        {
            try
            {
                var resultado = await _ticketRepo.GetAll();
                _respuesta.Result = resultado;
                _respuesta.DisplayMessage = "Lista de Tickets";
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_respuesta);
        }

        // GET api/<ProductosTicketController>/5
        [EnableCors("corsConfiguration")]
        [HttpGet("{folio}")]
        public async Task<ActionResult<Ticket>> GetProductoTicket(int folio)
        {
            var model = await _ticketRepo.GetById(folio);
            if (model == null)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "El Ticket No Existe";
                return BadRequest(_respuesta);
            }
            _respuesta.Result = model;
            _respuesta.DisplayMessage = "Informacion del Ticket";

            return Ok(_respuesta);
        }

        [EnableCors("corsConfiguration")]
        [HttpPut("{folio}")]
        // PUT api/<ProductosTicketController>/5
        public async Task<IActionResult> PutProductoTicket(int folio, TicketDto ticketDto)
        {
            ticketDto.Folio = folio;
            try
            {
                var model = await _ticketRepo.CreateUpdate(ticketDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Actualizado el Ticket";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrfolioo un Error al Actualizar el Ticket";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

        [EnableCors("corsConfiguration")]
        [HttpPost]
        // POST api/<ProductosTicketController>
        public async Task<ActionResult<Ticket>> PostProductoTicket(TicketDto ticketDto)
        {
            try
            {
                TicketDto model = await _ticketRepo.CreateUpdate(ticketDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Registrado el Ticket";
                return CreatedAtAction("GetProductoTicket", new { Folio = model.Folio }, _respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrfolioo un Error al Registrar el Ticket ";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

        // DELETE api/<ProductosTicketController>/5
        [EnableCors("corsConfiguration")]
        [HttpDelete("{folio}")]
        public async Task<IActionResult> DeleteProductoTicket(int folio)
        {
            try
            {
                var resultado = await _ticketRepo.Delete(folio);
                if (resultado)
                {
                    _respuesta.Result = resultado;
                    _respuesta.DisplayMessage = "Se ha Cancelado el Ticket Con Éxito";
                    return Ok(_respuesta);
                }
                else
                {
                    _respuesta.IsSuccess = false;
                    _respuesta.DisplayMessage = "Error al Cancelar el Ticket";
                    return BadRequest(_respuesta);
                }
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }
    }
}
