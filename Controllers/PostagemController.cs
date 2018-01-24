using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApiForum.Models;
using WebApiForum.Repositorio;

namespace WebApiForum.Controllers
{
    [Route("api/v1/[controller]")]
    public class PostagemController : Controller
    {
        
        PostagemRep objPostagemRep = new PostagemRep();
        
        [HttpGet]
        public IEnumerable<PostagemModel> Listar()
        {
            return objPostagemRep.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] PostagemModel postagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objPostagemRep.Cadastrar(postagem);
                    return Ok(postagem);
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
        public IActionResult Editar([FromBody] PostagemModel postagem, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    postagem.Id = id;
                    objPostagemRep.Editar(postagem);
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
                objPostagemRep.Excluir(id);
                return Ok(id);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}