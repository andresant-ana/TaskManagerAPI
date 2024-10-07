using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private ITarefasRepository _tarefasRepository;

        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }

        // GET: api/tarefas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tarefas = await _tarefasRepository.BuscarAsync();
            return Ok(tarefas);
        }

        // GET api/tarefas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var tarefa = await _tarefasRepository.BuscarAsync(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        // POST api/tarefas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarefaInputModel inputModel)
        {
            var tarefa = new Tarefa(inputModel.Nome, inputModel.Detalhes);
            await _tarefasRepository.AdicionarAsync(tarefa);

            return CreatedAtAction(nameof(Get), new { id = tarefa.Id }, tarefa);
        }


        // PUT api/tarefas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TarefaInputModel tarefaAtualizada)
        {
            var tarefa = await _tarefasRepository.BuscarAsync(id);

            if (tarefa == null)
                return NotFound();

            tarefa.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluido);

            await _tarefasRepository.AtualizarAsync(id, tarefa);

            return Ok(tarefa);
        }

        // DELETE api/tarefas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var tarefa = await _tarefasRepository.BuscarAsync(id);

            if (tarefa == null)
                return NotFound();

            await _tarefasRepository.RemoverAsync(id);

            return NoContent();
        }
    }
}
