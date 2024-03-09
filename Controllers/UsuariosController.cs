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
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioRepositorio _usuarioRepo;

        private readonly Response _respuesta;
        public UsuariosController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _respuesta = new Response();
        }
        // GET: api/<ProductosTicketController>
        [EnableCors("corsConfiguration")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetProductosTicket()
        {
            try
            {
                var resultado = await _usuarioRepo.GetAll();
                _respuesta.Result = resultado;
                _respuesta.DisplayMessage = "Lista de Usuarios";
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var model = await _usuarioRepo.GetById(id);
            if (model == null)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "El Usuario No Existe";
                return BadRequest(_respuesta);
            }
            _respuesta.Result = model;
            _respuesta.DisplayMessage = "Informacion del Usuario";

            return Ok(_respuesta);
        }

        [EnableCors("corsConfiguration")]
        [HttpPut("{id}")]
        // PUT api/<ProductosTicketController>/5
        public async Task<IActionResult> PutUsuario(int Id, UsuarioDto usuarioDto)
        {
            usuarioDto.Id = Id;
            try
            {
                var model = await _usuarioRepo.CreateUpdate(usuarioDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Actualizado el Usuario";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Actualizar el Usuario";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

        [EnableCors("corsConfiguration")]
        [HttpPost]
        // POST api/<ProductosTicketController>
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                UsuarioDto model = await _usuarioRepo.CreateUpdate(usuarioDto);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Registrado el Usuario";
                return CreatedAtAction("GetUsuario", new { Id = model.Id }, _respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Registrar el Usuario";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }

        // DELETE api/<ProductosTicketController>/5
        [EnableCors("corsConfiguration")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var resultado = await _usuarioRepo.Delete(id);
                if (resultado)
                {
                    _respuesta.Result = resultado;
                    _respuesta.DisplayMessage = "Se ha Eliminado El Usuario";
                    return Ok(_respuesta);
                }
                else
                {
                    _respuesta.IsSuccess = false;
                    _respuesta.DisplayMessage = "Error al Eliminar el Usuario";
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
