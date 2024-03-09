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
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepositorio _productoRepo;

        private readonly Response _respuesta;
        public ProductosController(IProductoRepositorio productoRepo)
        {
            _productoRepo = productoRepo;
            _respuesta = new Response();
        }
        // GET: api/<ProductosTicketController>
        [EnableCors("corsConfiguration")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                var resultado = await _productoRepo.GetAll();
                _respuesta.Result = resultado;
                _respuesta.DisplayMessage = "Lista de Productos";
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
        public async Task<ActionResult<Producto>> GetProducto(string Codigo)
        {
            var model = await _productoRepo.GetById(Codigo);
            if (model == null)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "El Producto No Existe";
                return BadRequest(_respuesta);
            }
            _respuesta.Result = model;
            _respuesta.DisplayMessage = "Informacion del Producto";

            return Ok(_respuesta);
        }
        [EnableCors("corsConfiguration")]
        [HttpPut("{Codigo}")]
        // PUT api/<ProductosTicketController>/5
        public async Task<IActionResult> PutProducto(string Codigo, ProductoDto productoDto)
        {
            productoDto.Codigo = Codigo;
            try
            {
                var model = await _productoRepo.Update(productoDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Actualizado el Producto";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Actualizar el Producto";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }
        [EnableCors("corsConfiguration")]
        [HttpPost]
        // POST api/<ProductosTicketController>
        public async Task<ActionResult<Producto>> PostProducto(ProductoDto productoDto)
        {
            try
            {
                var model = await _productoRepo.Create(productoDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Registrado el Producto";
                return CreatedAtAction("GetProductoTicket", new { Codigo = model.Codigo }, _respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Registrar el Producto";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

        // DELETE api/<ProductosTicketController>/5
        [EnableCors("corsConfiguration")]
        [HttpDelete("{Codigo}")]
        public async Task<IActionResult> DeleteProducto(string Codigo)
        {
            try
            {
                var resultado = await _productoRepo.Delete(Codigo);
                if (resultado)
                {
                    _respuesta.Result = resultado;
                    _respuesta.DisplayMessage = "Se ha Eliminado el Producto";
                    return Ok(_respuesta);
                }
                else
                {
                    _respuesta.IsSuccess = false;
                    _respuesta.DisplayMessage = "Error al Eliminar el Producto";
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

    [Route("api/productos/[controller]")]
    [ApiController]

    public class AddController : ControllerBase
    {

        private readonly IProductoRepositorio _productoRepo;
        private readonly Response _respuesta;
        public AddController(IProductoRepositorio productoRepo)
        {
            _productoRepo = productoRepo;
            _respuesta = new Response();
        }


        [EnableCors("corsConfiguration")]
        [HttpPut("{Cantidad}")]
        // PUT api/<ProductosController>/5
        public async Task<IActionResult> AgregarCantidad(string Cantidad, ProductoDto productoDto)
        {
            try
            {
                var model = await _productoRepo.UpdateQuantity
                    (decimal.Parse(Cantidad),
                    productoDto,
                    true);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Aumentado la Existencia del Producto";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Aumentar la Existencia del Producto";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

    }


    [Route("api/productos/[controller]")]
    [ApiController]
    public class MinusController : ControllerBase
    {
        private readonly IProductoRepositorio _productoRepo;
        private readonly Response _respuesta;
        public MinusController(IProductoRepositorio productoRepo)
        {
            _productoRepo = productoRepo;
            _respuesta = new Response();
        }

        [EnableCors("corsConfiguration")]
        [HttpPut("{Cantidad}")]
        // PUT api/<ProductosTicketController>/5
        public async Task<IActionResult> DescontarCantidad(string Cantidad, ProductoDto productoDto)
        {
            try
            {
                var model = await _productoRepo.UpdateQuantity
                    (decimal.Parse(Cantidad),
                    productoDto,
                    false);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Descontado la Existencia del Producto";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Descontar la Existencia del Producto";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }


    }
}
