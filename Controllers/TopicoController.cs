using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApiForum.Models;
using WebApiForum.Repositorio;

namespace WebApiForum.Controllers
{
    [Route("api/v1/[controller]")]
    public class TopicoController : Controller
    {

        TopicoForumRep objTopicoRep = new TopicoForumRep();
        
        [HttpGet]
        public IEnumerable<TopicoForumModel> Listar()
        {
            return objTopicoRep.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] TopicoForumModel topico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objTopicoRep.Cadastrar(topico);
                    return Ok(topico);
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
        public IActionResult Editar([FromBody] TopicoForumModel topico, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    topico.Id = id;
                    objTopicoRep.Editar(topico);
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
                objTopicoRep.Excluir(id);
                return Ok(id);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}