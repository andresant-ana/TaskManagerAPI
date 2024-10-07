using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Controllers;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Tests
{
    public class TarefasControllerTests
    {
        private readonly TarefasController _controller;
        private readonly Mock<ITarefasRepository> _mockRepo;

        public TarefasControllerTests()
        {
            _mockRepo = new Mock<ITarefasRepository>();
            _controller = new TarefasController(_mockRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfTarefas()
        {
            // Arrange
            var tarefas = new List<Tarefa> { new Tarefa("Teste", "Detalhes do teste") };
            _mockRepo.Setup(repo => repo.BuscarAsync()).ReturnsAsync(tarefas);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Tarefa>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenTarefaExists()
        {
            // Arrange
            var id = "1";
            var tarefa = new Tarefa("Tarefa", "Detalhes");
            _mockRepo.Setup(repo => repo.BuscarAsync(id)).ReturnsAsync(tarefa);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Tarefa>(okResult.Value);
            Assert.Equal(tarefa.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult_WhenTarefaDoesNotExist()
        {
            // Arrange
            var id = "1";
            _mockRepo.Setup(repo => repo.BuscarAsync(id)).ReturnsAsync((Tarefa)null);

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var novaTarefa = new TarefaInputModel { Nome = "Nova Tarefa", Detalhes = "Detalhes da nova tarefa" };
            var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Detalhes);
            _mockRepo.Setup(repo => repo.AdicionarAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(novaTarefa);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Tarefa>(createdAtActionResult.Value);
            Assert.Equal(novaTarefa.Nome, returnValue.Nome);
        }



        [Fact]
        public async Task Put_ReturnsOkResult_WhenTarefaExists()
        {
            // Arrange
            var id = "1";
            var tarefaAtualizada = new TarefaInputModel { Nome = "Tarefa Atualizada", Detalhes = "Detalhes Atualizados", Concluido = true };
            var tarefa = new Tarefa("Tarefa Original", "Detalhes Originais");
            _mockRepo.Setup(repo => repo.BuscarAsync(id)).ReturnsAsync(tarefa);
            _mockRepo.Setup(repo => repo.AtualizarAsync(id, tarefa)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(id, tarefaAtualizada);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Tarefa>(okResult.Value);
            Assert.Equal(tarefaAtualizada.Nome, returnValue.Nome);
            Assert.Equal(tarefaAtualizada.Detalhes, returnValue.Detalhes);
            Assert.True(returnValue.Concluido);
        }

        [Fact]
        public async Task Put_ReturnsNotFoundResult_WhenTarefaDoesNotExist()
        {
            // Arrange
            var id = "1";
            var tarefaAtualizada = new TarefaInputModel { Nome = "Tarefa Atualizada", Detalhes = "Detalhes Atualizados", Concluido = true };
            _mockRepo.Setup(repo => repo.BuscarAsync(id)).ReturnsAsync((Tarefa)null);

            // Act
            var result = await _controller.Put(id, tarefaAtualizada);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult_WhenTarefaExists()
        {
            // Arrange
            var id = "1";
            var tarefa = new Tarefa("Tarefa", "Detalhes");
            _mockRepo.Setup(repo => repo.BuscarAsync(id)).ReturnsAsync(tarefa);
            _mockRepo.Setup(repo => repo.RemoverAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenTarefaDoesNotExist()
        {
            // Arrange
            var id = "1";
            _mockRepo.Setup(repo => repo.BuscarAsync(id)).ReturnsAsync((Tarefa)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}
