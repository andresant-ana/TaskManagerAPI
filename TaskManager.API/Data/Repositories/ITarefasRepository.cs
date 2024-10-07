using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repositories
{
    public interface ITarefasRepository
    {
        Task AdicionarAsync(Tarefa tarefa);
        Task AtualizarAsync(string id, Tarefa tarefaAtualizada);
        Task<IEnumerable<Tarefa>> BuscarAsync();
        Task<Tarefa> BuscarAsync(string id);
        Task RemoverAsync(string id);
    }
}
