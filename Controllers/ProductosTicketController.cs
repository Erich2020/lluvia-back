using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Models;
using TempporalWS.Models.Dto;
using TempporalWS.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TempporalWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosTicketController : ControllerBase
    {
        private readonly IProductoTicketRepositorio _productoTicketRepo;

        private readonly Response _respuesta;
        public ProductosTicketController(IProductoTicketRepositorio productoTicketRepo)
        {
            _productoTicketRepo = productoTicketRepo;
            _respuesta = new Response();
        }
        // GET: api/<ProductosTicketController>
        [EnableCors("corsConfiguration")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoTicket>>> GetProductosTicket()
        {
            try
            {
                var resultado = await _productoTicketRepo.GetAll();
                _respuesta.Result = resultado;
                _respuesta.DisplayMessage = "Lista de Productos Vendidos";
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
        [HttpGet("{Codigo}")]
        public async Task<ActionResult<ProductoTicket>> GetProductoTicket(string Codigo)
        {
            var model = await _productoTicketRepo.GetById(Codigo);
            if (model == null)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "El Producto Vendido No Existe";
                return BadRequest(_respuesta);
            }
            _respuesta.Result = model;
            _respuesta.DisplayMessage = "Informacion del Producto Vendido";

            return Ok(_respuesta);
        }

        [EnableCors("corsConfiguration")]
        [HttpPut("{Codigo}")]
        // PUT api/<ProductosTicketController>/5
        public async Task<IActionResult> PutProductoTicket(string  Codigo, ProductoTicketDto productoTicketDto)
        {
            productoTicketDto.Codigo = Codigo;
            try
            {
                var model = await _productoTicketRepo.Update(productoTicketDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Actualizado el Producto Vendido";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Actualizar el Producto Vendido";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

        [EnableCors("corsConfiguration")]
        [HttpPost]
        // POST api/<ProductosTicketController>
        public async Task<ActionResult<ProductoTicket>> PostProductoTicket(ProductoTicketDto productoTicketDto)
        {
            try
            {
                ProductoTicketDto model = await _productoTicketRepo.Create(productoTicketDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Registrado el Producto Vendido";
                return CreatedAtAction("GetProductoTicket", new { Codigo = model.Codigo}, _respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Registrar el Producto Vendido";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

        // DELETE api/<ProductosTicketController>/5
        [EnableCors("corsConfiguration")]
        [HttpDelete("{Codigo}")]
        public async Task<IActionResult> DeleteProductoTicket(string  Codigo)
        {
            try
            {
                var resultado = await _productoTicketRepo.Delete(Codigo);
                if (resultado)
                {
                    _respuesta.Result = resultado;
                    _respuesta.DisplayMessage = "Se ha Cancelado el Producto Vendido Con Éxito";
                    return Ok(_respuesta);
                }
                else
                {
                    _respuesta.IsSuccess = false;
                    _respuesta.DisplayMessage = "Error al Cancelar el Producto Vendido";
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
