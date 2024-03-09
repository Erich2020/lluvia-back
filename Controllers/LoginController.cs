using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempporalWS.Models;
using TempporalWS.Models.Dto;
using TempporalWS.Repositories;


namespace TempporalWS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly Response _respuesta;
        public LoginController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _respuesta = new Response();
        }
        [EnableCors("corsConfiguration")]
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostLogin(UsuarioDto userDto)
        {
            try
            {
                UsuarioDto model = await _usuarioRepo.Login(userDto.Username, userDto.Password);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = (model.Id > 0) ? "Se ha Iniciado Sesión" : "Credenciales Incorrectas";
                _respuesta.IsSuccess =(model.Id > 0);
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Iniciar Sesión";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }
    }
    [Route("[controller]")]
    [ApiController]
    public class ChangePwdController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepo;

        private readonly Response _respuesta;
        public ChangePwdController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _respuesta = new Response();
        }
        [EnableCors("corsConfiguration")]
        [HttpPut]
        public async Task<ActionResult<Usuario>> PutChangePwd(int id, string pwd)
        {
            try
            {
                UsuarioDto model = await _usuarioRepo.ChangePwd(id, pwd);
                _respuesta.Result = model;
                _respuesta.DisplayMessage = "Se ha Realizado el Cambio Con Éxito";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Ha Ocurrido un Error al Cambiar la Contraseña";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_respuesta);
            }
        }
    }
}
