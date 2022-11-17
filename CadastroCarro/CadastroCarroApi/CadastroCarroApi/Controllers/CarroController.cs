using CadastroCarroApi.Data;
using CadastroCarroApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroCarroApi.Controllers
{
    // anotação para habilitar uso da api em diferentes domínios, inclusive localhost
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        // contexto do entityframework para conexão e operações com bd
        private readonly CadastroCarroApiContext _context;

        // inicializa no construtor
        public CarroController(CadastroCarroApiContext context)
        {
            _context = context;
        }

        // GET: api/Carro | retorna lista de carros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carro>>> GetCarro()
        {
            return await _context.Carro.ToListAsync();
        }

        // GET: api/Carro/5 | retorna um carro ao procurar pelo id no bd
        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> GetCarro(int id)
        {
            var carro = await _context.Carro.FindAsync(id);

            // se retornar vazio do bd, é retornado ao frontend erro 404, não encontrado
            if (carro == null)
            {
                return NotFound();
            }
            // se econtrado algo é retornado ao frontend em json modelo carro
            return carro;
        }

        // PUT: api/Carro/5 | atualiza carro passando a model carro em json e o id do carro
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarro(int id, Carro carro)
        {
            //compara o id e o id do modelo carro
            if (id != carro.Id)
            {
                return BadRequest();
            }

            // salva as modificações encontradas no modelo carro
            _context.Entry(carro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarroExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/Carro | insere um carro ao bd
        [HttpPost]
        public async Task<ActionResult<Carro>> PostCarro(Carro carro)
        {
            // adiciona um carro ao bd e salva
            _context.Carro.Add(carro);
            await _context.SaveChangesAsync();

            //retorna o carro criado em json para o frontend
            return CreatedAtAction("GetCarro", new { id = carro.Id }, carro);
        }

        // DELETE: api/Carro/5 | deletar um carro quando encontrado o id do carro
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarro(int id)
        {
            // procura um carro de id e verifica se retorna um carro ou vazio = null
            var carro = await _context.Carro.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            //remove com método Remove e salva SaveChangesAsync
            _context.Carro.Remove(carro);
            await _context.SaveChangesAsync();

            // retorna para o frontend nenhum conteúdo
            return NoContent();
        }

        //método para verificar se carro com id existe no bd 
        private bool CarroExists(int id)
        {
            return _context.Carro.Any(e => e.Id == id);
        }
    }
}
