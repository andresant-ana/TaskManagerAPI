using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repositories
{
    public class TarefasRepository : ITarefasRepository
    {
        private readonly IMongoCollection<Tarefa> _tarefas;

        public TarefasRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _tarefas = database.GetCollection<Tarefa>("tarefas");
        }

        public async Task AdicionarAsync(Tarefa tarefa)
        {
            await _tarefas.InsertOneAsync(tarefa);
        }

        public async Task AtualizarAsync(string id, Tarefa tarefaAtualizada)
        {
            await _tarefas.ReplaceOneAsync(tarefa => tarefa.Id == id, tarefaAtualizada);
        }

        public async Task<IEnumerable<Tarefa>> BuscarAsync()
        {
            return await _tarefas.Find(tarefa => true).ToListAsync();
        }

        public async Task<Tarefa> BuscarAsync(string id)
        {
            return await _tarefas.Find(tarefa => tarefa.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoverAsync(string id)
        {
            await _tarefas.DeleteOneAsync(tarefa => tarefa.Id == id);
        }
    }
}
