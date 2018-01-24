using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApiForum.Models;
using WebApiForum.Repositorio;

namespace WebApiForum.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsuarioController : Controller
    {

        UsuarioRep objUsuarioRep = new UsuarioRep();
        
        [HttpGet]
        public IEnumerable<UsuarioModel> Listar()
        {
            return objUsuarioRep.Listar();
        }

        [HttpGet("{login}/{senha}")]
        public IActionResult BuscarUsuarioPorLogineSenha(string login, string senha)
        {
            try
            {
                UsuarioModel usuario = objUsuarioRep.Listar().Where(c => c.Login == login && c.Senha == senha).FirstOrDefault();

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado") ;
                }
                else
                {
                    return Ok(usuario);
                }
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objUsuarioRep.Cadastrar(usuario);
                    return Ok(usuario);
                }

                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                return BadRequest(allErrors);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar([FromBody] UsuarioModel usuario, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario.Id = id;
                    objUsuarioRep.Editar(usuario);
                    return Ok(id);
                }

                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                
                return BadRequest(allErrors);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                objUsuarioRep.Excluir(id);
                return Ok(id);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}