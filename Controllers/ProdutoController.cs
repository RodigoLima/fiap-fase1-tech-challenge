using fiap_fase1_tech_challenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace fiap_fase1_tech_challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static List<Produto> produtos = new()
        {
            new Produto { Id = 1, Nome = "Arroz", Preco = 5.99M },
            new Produto { Id = 2, Nome = "Feijão", Preco = 7.49M },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() => Ok(produtos);

        [HttpPost]
        public ActionResult<Produto> Post([FromBody] Produto novo)
        {
            novo.Id = produtos.Count + 1;
            produtos.Add(novo);
            return Ok(novo);
        }
    }
}
